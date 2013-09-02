// -----------------------------------------------------------------------
// <copyright file="BlockVertices.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;

namespace BlockWorldPrototype.World.Block
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BlockVertices
    {
        public static Vector3 CubeSize = new Vector3(0.5f, 0.5f, 0.5f);
        public static Vector3 VertexOffset = new Vector3(CubeSize.X / 2f, CubeSize.Y / 2f, CubeSize.Z / 2f);

        public static Vector3 UpTopLeft = new Vector3(-VertexOffset.X, VertexOffset.Y, VertexOffset.Z);
        public static Vector3 UpTopRight = new Vector3(VertexOffset.X, VertexOffset.Y, VertexOffset.Z);
        public static Vector3 UpBottomLeft = new Vector3(-VertexOffset.X, VertexOffset.Y, -VertexOffset.Z);
        public static Vector3 UpBottomRight = new Vector3(VertexOffset.X, VertexOffset.Y, -VertexOffset.Z);

        public static Vector3 DownTopLeft = new Vector3(-VertexOffset.X, -VertexOffset.Y, VertexOffset.Z);
        public static Vector3 DownTopRight = new Vector3(VertexOffset.X, -VertexOffset.Y, VertexOffset.Z);
        public static Vector3 DownBottomLeft = new Vector3(-VertexOffset.X, -VertexOffset.Y, -VertexOffset.Z);
        public static Vector3 DownBottomRight = new Vector3(VertexOffset.X, -VertexOffset.Y, -VertexOffset.Z);
    }
}
