// -----------------------------------------------------------------------
// <copyright file="EntityType.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.CompilerServices;
using BlockWorldPrototype.Core.Graphics;
using BlockWorldPrototype.World.Entity.Property;
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
        public Dictionary<Type, BaseProperty> Behaviours;
        public string Name;
        public Vector3 RenderOffset;
        public Vector3 BaseScale;
        public string Description;
    }

    public static class EntitySchematics
    {
        public static EntitySchematic Tree;
        public static EntitySchematic Log;
        public static EntitySchematic Man;

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
            Tree.RenderOffset = new Vector3(0, 0.85f, 0);
            Tree.BaseScale = new Vector3(0.8f, 0.8f, 0.75f);
            Tree.Name = "Tree";
            Tree.Description = "It's a tree, not much else can be said.";

            // Harvest behaviour
            Harvestable onHarvest = new Harvestable();
            onHarvest.AddAction((e => e.RemoveSelf()));
            onHarvest.AddAction((e => e.GW.AddEntityByChance(new BaseEntity(e.GW, EntitySchematics.Log, e.Position), 1)));
            onHarvest.AddAction((e => e.GW.AddEntityByChance(new BaseEntity(e.GW, EntitySchematics.Log, e.Position + WorldDirection.North), 2)));



            Tree.Behaviours = new Dictionary<Type, BaseProperty>();


            Tree.Behaviours.Add(typeof(Harvestable), onHarvest);

            // -- LOG -- \\
            Log = new EntitySchematic();
            Log.RenderBasic = new RenderBasic(content.Load<Model>("Models/Entity/log3"), effect);
            Log.OccupiedSpace = new List<Vector3>();
            Log.BaseScale = new Vector3(0.1f,0.1f,0.25f);
            Log.RenderOffset = new Vector3(0f, -0.15f, 0f);
            Log.Name = "Log";
            Log.Description = "It's big, it's heavy, it's wood.";
            Log.Behaviours = new Dictionary<Type, BaseProperty>();
            Harvestable logOnHarvest = new Harvestable();
            Log.Behaviours.Add(typeof(Harvestable),  new Harvestable());

            // -- MAN -- \\
            Man = new EntitySchematic();
            Man.RenderBasic = new RenderBasic(content.Load<Model>("Models/Entity/manmodel"), effect);
            Man.OccupiedSpace = new List<Vector3>();
            Man.BaseScale = new Vector3(0.1f, 0.1f, 0.25f);
            Man.RenderOffset = new Vector3(0f, -0.15f, 0f);
            Man.Name = "Log";
            Man.Description = "It's big, it's heavy, it's wood.";
            Man.Behaviours = new Dictionary<Type, BaseProperty>();
            Harvestable ManOnHarvest = new Harvestable();
            Man.Behaviours.Add(typeof(Harvestable), new Harvestable());
        }
    }
}
