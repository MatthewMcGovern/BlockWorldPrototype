// -----------------------------------------------------------------------
// <copyright file="WorldDirection.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;

namespace BlockWorldPrototype.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class WorldDirection
    {
        public static Vector3 Centre = new Vector3(0, 0, 0);
        public static Vector3 Up = new Vector3(0, 1, 0);
        public static Vector3 Down = new Vector3(0, -1, 0);
        public static Vector3 North = new Vector3(0, 0, 1);
        public static Vector3 East = new Vector3(1, 0, 0);
        public static Vector3 South = new Vector3(0, 0, -1);
        public static Vector3 West = new Vector3(-1, 0, 0);
    }
}
