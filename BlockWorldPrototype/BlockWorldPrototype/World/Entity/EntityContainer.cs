// -----------------------------------------------------------------------
// <copyright file="EntityContainer.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.World.Segments;
using Microsoft.Xna.Framework;

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

        public EntityContainer(GameWorld parent)
        {
            GW = parent;
            All = new Dictionary<int, BaseEntity>();
            BySegment = new Dictionary<Segment, List<BaseEntity>>();

            foreach (Segment segment in GW._SM.Segments)
            {
                BySegment.Add(segment, new List<BaseEntity>());
            }
            

            BySchematic = new Dictionary<EntitySchematic, List<BaseEntity>>();
            BySchematic.Add(EntitySchematics.Tree, new List<BaseEntity>());
        }

        public void AddEntitiy(BaseEntity entity)
        {
            All.Add(entity.ID, entity);
            BySegment[GW._SM.GetSegmentAt(new SegmentLocation(entity.Position))].Add(entity);
            BySchematic[entity.Schematic].Add(entity);
        }

        public void RemoveEntitiy(BaseEntity entity)
        {
            All.Remove(entity.ID);
            BySegment[GW._SM.GetSegmentAt(new SegmentLocation(entity.Position))].Remove(entity);
            BySchematic[entity.Schematic].Remove(entity);
        }
    }
}
