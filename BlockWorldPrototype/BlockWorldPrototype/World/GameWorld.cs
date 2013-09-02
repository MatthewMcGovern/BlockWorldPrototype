// -----------------------------------------------------------------------
// <copyright file="World.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.Core;
using BlockWorldPrototype.World.Editor;
using BlockWorldPrototype.World.Entity;
using BlockWorldPrototype.World.Segments;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BlockWorldPrototype.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GameWorld
    {
        public SegmentManager _SM;
        private WorldEditor _editor;
        public EntityContainer Entities;

        private Effect _blockWorldEffect;
        private Effect _modelEffect;
        public GraphicsDevice Device;
        private Camera3D _camera;
        private Texture2D _blockAtlas;


        public GameWorld(GraphicsDevice device)
        {
            Device = device;
            _camera = new Camera3D(Device);
        }

        public void Load(ContentManager content)
        {
            _blockWorldEffect = content.Load<Effect>("blockWorld");
            _modelEffect = content.Load<Effect>("modelEffect");
            _blockAtlas = content.Load<Texture2D>("IsomitesAtlas");
            EntitySchematics.Init(content, _modelEffect);

            _SM = new SegmentManager(Device);
            Entities = new EntityContainer(this);

            _editor = new WorldEditor(_SM);

            _editor.LoadContent(content, _modelEffect);
        }


        public void Update(GameTime gameTime)
        {
            _camera.Update(gameTime);
            _SM.Update(gameTime);
            _editor.Update();
        }

        public void Draw()
        {
            Matrix worldMatrix = Matrix.Identity;
            _blockWorldEffect.CurrentTechnique = _blockWorldEffect.Techniques["Textured"];
            _blockWorldEffect.Parameters["xWorld"].SetValue(worldMatrix);
            _blockWorldEffect.Parameters["xView"].SetValue(_camera.ViewMatrix);
            _blockWorldEffect.Parameters["xProjection"].SetValue(_camera.ProjectionMatrix);
            _blockWorldEffect.Parameters["xTexture"].SetValue(_blockAtlas);

            foreach (EffectPass pass in _blockWorldEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                // draw block world stuff
                _SM.DrawBlocks();
                
            }

            foreach (EffectPass pass in _blockWorldEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _editor.DrawBlocks();
            }

            _editor.DrawModels(_camera);
        }
    }
}
