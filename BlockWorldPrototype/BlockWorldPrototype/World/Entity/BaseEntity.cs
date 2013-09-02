// -----------------------------------------------------------------------
// <copyright file="Entity.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.World.Entity.Behaviour;
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
    public class BaseEntity
    {
        public EntitySchematic Schematic;
        public GameWorld GW;
        public int ID;
        public Vector3 Position;

        public BaseEntity(GameWorld gw, EntitySchematic schematic, Vector3 position)
        {
            GW = gw;
            Schematic = schematic;
            ID = WorldGlobal.GetNextEntityID();
        }

        public void Remove()
        {
            // eh it needs to do something...
            // be it remove from the list... or tell the manager to remove it... I dunno.
        }

        public bool GetBehaviour<T>() where T : class
        {
            return Schematic.Behaviours.ContainsKey(typeof (T));
        }
    }
}
