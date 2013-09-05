// -----------------------------------------------------------------------
// <copyright file="PathNode.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;

namespace BlockWorldPrototype.World.Entity.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PathNode
    {
        public float Heuristic;
        public PathNode PrevNode;
        public Vector3 Position;

        public PathNode(Vector3 position, PathNode prevNode, float value)
        {
            Heuristic = value;
            PrevNode = prevNode;
            Position = position;
        }

        public string GetKey()
        {
            return Position.X + ":" + Position.Y + ":" + Position.Z;
        }
    }
}
