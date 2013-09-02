// -----------------------------------------------------------------------
// <copyright file="VertexHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockWorldPrototype.World.Block
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class VertexHelper
    {
        public static VertexPositionNormalTexture[] TranslateVerticesToWorldLocation(
            VertexPositionNormalTexture[] verticesIn, Vector3 worldGridLocation)
        {
            VertexPositionNormalTexture[] verticesOut = new VertexPositionNormalTexture[verticesIn.Length];
            for (int currentVertex = 0; currentVertex < verticesIn.Length; currentVertex++)
            {
                verticesOut[currentVertex] = new VertexPositionNormalTexture(new Vector3(verticesIn[currentVertex].Position.X + ((BlockVertices.CubeSize.X) * worldGridLocation.X), verticesIn[currentVertex].Position.Y + ((BlockVertices.CubeSize.Y) * worldGridLocation.Y), verticesIn[currentVertex].Position.Z + ((BlockVertices.CubeSize.Z) * worldGridLocation.Z)),
                         verticesIn[currentVertex].Normal, verticesIn[currentVertex].TextureCoordinate);
            }

            return verticesOut;
        }
    }
}
