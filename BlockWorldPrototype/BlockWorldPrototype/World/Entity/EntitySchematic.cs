// -----------------------------------------------------------------------
// <copyright file="EntityType.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.Core.Graphics;
using BlockWorldPrototype.World.Entity.Behaviour;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
    public class EntitySchematic
    {
        public RenderBasic RenderBasic;
        public List<Vector3> OccupiedSpace;
        public Dictionary<Type, BaseBehaviour> Behaviours;
        public string Name;
    }

    public static class EntitySchematics
    {
        public static EntitySchematic Tree;

        public static void Init(ContentManager content, Effect effect)
        {
            // -- TREE -- \\
            Tree = new EntitySchematic();
            Tree.RenderBasic = new RenderBasic(content.Load<Model>("Models/Entity/tree"), effect);
            Tree.OccupiedSpace = new List<Vector3>();
            Tree.OccupiedSpace.Add(Vector3.Zero);
            Tree.OccupiedSpace.Add(new Vector3(0, 1, 0));
            Tree.OccupiedSpace.Add(new Vector3(0, 2, 0));
            Tree.OccupiedSpace.Add(new Vector3(0, 3, 0));
            // no shit
            Tree.Name = "Tree";

            // Harvest behaviour
            Harvestable onHarvest = new Harvestable();
            onHarvest.AddAction(new Action<BaseEntity>(e => e.Remove()));

            Tree.Behaviours = new Dictionary<Type, BaseBehaviour>();


            Tree.Behaviours.Add(typeof(Harvestable), new Harvestable());
        }
    }
}
