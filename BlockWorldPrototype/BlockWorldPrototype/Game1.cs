using System;
using System.Collections.Generic;
using System.Linq;
using BlockWorldPrototype.Core;
using BlockWorldPrototype.Core.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BlockWorldPrototype.World;

namespace BlockWorldPrototype
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // GAME STUFF
        private GameWorld _world;
        private SpriteFont _debugFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 1000,
                PreferredBackBufferWidth = 1000,
                IsFullScreen = false
            };

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            WorldGlobal.Init();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _debugFont = Content.Load<SpriteFont>("Fonts/debugFont");
            DebugLog.Init(_debugFont);

            _world = new GameWorld(GraphicsDevice);
            _world.Load(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            InputHelper.Update(gameTime);

            // Allows the game to exit
            if (InputHelper.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            
            _world.Update(gameTime);

            if (InputHelper.IsNewKeyPress(Keys.F5))
            {
                IsFixedTimeStep = !IsFixedTimeStep;
                graphics.SynchronizeWithVerticalRetrace = !graphics.SynchronizeWithVerticalRetrace;
                graphics.ApplyChanges();
            }

            DebugLog.Update();

            // HACK: camera kept resetting movement as mouse is being forced to centre of screen by Camera3D
            // Update again because it needs to set CurrentState to LastState again
            InputHelper.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            RasterizerState rs = new RasterizerState();

            rs.CullMode = CullMode.None;
            rs.FillMode = FillMode.Solid;
            GraphicsDevice.RasterizerState = rs;

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            _world.Draw();

            DebugLog.Draw(spriteBatch);



            base.Draw(gameTime);
        }
    }
}
