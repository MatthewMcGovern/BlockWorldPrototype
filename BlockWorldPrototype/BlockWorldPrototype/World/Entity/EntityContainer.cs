// -----------------------------------------------------------------------
// <copyright file="EntityContainer.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.Core;
using BlockWorldPrototype.World.Segments;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockWorldPrototype.World.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EntityContainer
    {
        public GameWorld GW;
        public Dictionary<int, BaseEntity> All;
        public Dictionary<Segment, List<BaseEntity>> BySegment;
        public Dictionary<EntitySchematic, List<BaseEntity>> BySchematic;
        public List<BaseEntity> _toRemove;
        public List<BaseEntity> _toAdd; 

        public EntityContainer(GameWorld parent)
        {
            GW = parent;
            All = new Dictionary<int, BaseEntity>();
            BySegment = new Dictionary<Segment, List<BaseEntity>>();

            foreach (Segment segment in GW.SM.Segments)
            {
                BySegment.Add(segment, new List<BaseEntity>());
            }
            
            _toRemove = new List<BaseEntity>();
            _toAdd = new List<BaseEntity>();

            BySchematic = new Dictionary<EntitySchematic, List<BaseEntity>>();
            BySchematic.Add(EntitySchematics.Tree, new List<BaseEntity>());
            BySchematic.Add(EntitySchematics.Log, new List<BaseEntity>());
        }

        public void Update()
        {
            if (_toRemove.Count > 0)
            {
                foreach (BaseEntity entity in _toRemove)
                {
                    RemoveEntity(entity);
                }

                _toRemove.Clear();
            }

            if (_toAdd.Count > 0)
            {
                foreach (BaseEntity entity in _toAdd)
                {
                    AddEntity(entity);
                }

                _toAdd.Clear();
            }
        }

        public BaseEntity GetByID(int ID)
        {
            return All[ID];
        }

        private void AddEntity(BaseEntity entity)
        {
            All.Add(entity.ID, entity);
            BySegment[GW.SM.GetSegmentAt(new SegmentLocation(entity.Position))].Add(entity);
            BySchematic[entity.Schematic].Add(entity);
        }

        public void AddEntityExternal(BaseEntity entity)
        {
            _toAdd.Add(entity);
        }

        public void RemoveEntityExternal(BaseEntity entity)
        {
            _toRemove.Add(entity);
        }

        private void RemoveEntity(BaseEntity entity)
        {   

            All.Remove(entity.ID);
            BySegment[GW.SM.GetSegmentAt(new SegmentLocation(entity.Position))].Remove(entity);
            BySchematic[entity.Schematic].Remove(entity);
        }

        public BaseEntity GetClosestEntityByType(Vector3 position, EntitySchematic schematic)
        {
            BaseEntity closestEntity = null;
            float closestDistance = -1;
            foreach (BaseEntity entity in BySchematic[schematic])
            {
                if (closestDistance == -1 || Vector3.Distance(entity.Position, position) < closestDistance)
                {
                    closestDistance = Vector3.Distance(entity.Position, position);
                    closestEntity = entity;
                }
            }

            return closestEntity;
        }

        public void DrawAll(GraphicsDevice device, Camera3D camera)
        {
            foreach (KeyValuePair<int, BaseEntity> KeyPair in All)
            {
                BaseEntity entity = KeyPair.Value;
                entity.Schematic.RenderBasic.Draw(device, camera, entity.Position, entity.Schematic.RenderOffset,
                    entity.Rotation, entity.Scale);
            }
        }
    }
}
