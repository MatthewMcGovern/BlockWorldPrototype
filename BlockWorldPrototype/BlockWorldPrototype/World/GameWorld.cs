// -----------------------------------------------------------------------
// <copyright file="World.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using BlockWorldPrototype.Core;
using BlockWorldPrototype.Core.Debug;
using BlockWorldPrototype.World.Editor;
using BlockWorldPrototype.World.Entity;
using BlockWorldPrototype.World.Entity.AI;
using BlockWorldPrototype.World.Entity.Property;
using BlockWorldPrototype.World.Job;
using BlockWorldPrototype.World.Segments;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        private Random _rand;
        public SegmentManager SM;
        private WorldEditor _editor;
        public EntityContainer EC;
        public JobManager JM;

        private Effect _blockWorldEffect;
        private Effect _modelEffect;
        public GraphicsDevice Device;
        private Camera3D _camera;
        private Texture2D _blockAtlas;
        private BaseEntityAi _man;


        public GameWorld(GraphicsDevice device)
        {
            Device = device;
            _camera = new Camera3D(Device);
            _rand = new Random();
            JM = new JobManager();
            
        }

        public void AddEntity(BaseEntity entity)
        {
            EC.AddEntityExternal(entity);
        }

        public void AddEntityByChance(BaseEntity entity, int oneInRatio)
        {
            if (_rand.Next(0, oneInRatio) == 0)
            {
                AddEntity(entity);
            }
            else
            {
                DebugLog.Log("Drop chance not met :( for " + entity.ToString());
            }
        }

        public void Load(ContentManager content)
        {
            _blockWorldEffect = content.Load<Effect>("blockWorld");
            _modelEffect = content.Load<Effect>("modelEffect");
            _blockAtlas = content.Load<Texture2D>("IsomitesAtlas");
            EntitySchematics.Init(content, _modelEffect);

            SM = new SegmentManager(Device);

            _editor = new WorldEditor(SM);

            _editor.LoadContent(content, _modelEffect);

            EC = new EntityContainer(this);
            EC.AddEntityExternal(new BaseEntity(this, EntitySchematics.Tree, new Vector3(0,4,0)));
            EC.AddEntityExternal(new BaseEntity(this, EntitySchematics.Tree, new Vector3(2, 4, 0)));
            EC.AddEntityExternal(new BaseEntity(this, EntitySchematics.Tree, new Vector3(4, 4, 0)));
            EC.AddEntityExternal(new BaseEntity(this, EntitySchematics.Tree, new Vector3(6, 4, 0)));
            EC.AddEntityExternal(new BaseEntity(this, EntitySchematics.Tree, new Vector3(8, 4, 0)));
            EC.AddEntityExternal(new BaseEntity(this, EntitySchematics.Tree, new Vector3(10, 4, 0)));

            _man = new BaseEntityAi(this, EntitySchematics.Man, new Vector3(10,4,10));
            JM.AddJob(new EntityJob(EntitySchematics.Tree));
        }


        public void Update(GameTime gameTime)
        {
            _camera.Update(gameTime);
            SM.Update(gameTime);
            _editor.Update();
            EC.Update();
            _man.Update(gameTime);

            if (InputHelper.IsNewKeyPress(Keys.H))
            {
                // harvest
                foreach (KeyValuePair<int, BaseEntity> keypair in EC.All)
                {
                    Harvestable beh = keypair.Value.GetProperty<Harvestable>();

                    if (beh != null)
                    {
                        beh.Harvest(keypair.Value);
                    }

                    if (keypair.Value.Schematic == EntitySchematics.Tree)
                        break;
                }
            }
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
                SM.DrawBlocks();
                
            }

            foreach (EffectPass pass in _blockWorldEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _editor.DrawBlocks();
            }

            // Draw entitys
            EC.DrawAll(Device, _camera);
            _man.Schematic.RenderBasic.Draw(Device, _camera, _man.Position, _man.Schematic.RenderOffset, _man.Rotation, Vector3.One );

            _editor.DrawModels(_camera);
        }
    }
}
