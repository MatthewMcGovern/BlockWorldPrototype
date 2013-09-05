// -----------------------------------------------------------------------
// <copyright file="Entity.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.World.Entity.Property;
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
        public float Rotation;
        public Vector3 Scale;

        public BaseEntity(GameWorld gw, EntitySchematic schematic, Vector3 position)
        {
            GW = gw;
            Schematic = schematic;
            ID = WorldGlobal.GetNextEntityID();
            Position = position;
            Rotation = 0f;
            Scale = Schematic.BaseScale;
        }

        public void RemoveSelf()
        {
            // entity aren't allowed to directly remove themselves, otherwise the list gets out of sync.
            GW.EC.RemoveEntityExternal(this);
        }

        public bool HasBehaviour<T>() where T : class
        {
            return Schematic.Behaviours.ContainsKey(typeof (T));
        }

        public T GetProperty<T>() where T : class
        {
            BaseProperty returnVal = null;
            if (Schematic.Behaviours.TryGetValue(typeof(T), out returnVal))
            {
                return returnVal as T;
            }

            return null;
        }

        new public string ToString()
        {
            return Schematic.Name + " (ID: " + ID + ")";
        }
    }
}
