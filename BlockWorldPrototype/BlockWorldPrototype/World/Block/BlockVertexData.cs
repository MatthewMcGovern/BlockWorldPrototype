// -----------------------------------------------------------------------
// <copyright file="BlockVertexData.cs" company="Microsoft">
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
    public class BlockVertexData
    {
        private static List<BlockVertexData> _blockData;
        private static float textureOffset = 0;
        private static float bleedingFix = 0;

        public VertexPositionNormalTexture[] UpVertices;
        public ushort[] UpIndices;
        public VertexPositionNormalTexture[] DownVertices;
        public ushort[] DownIndices;
        public VertexPositionNormalTexture[] NorthVertices;
        public ushort[] NorthIndices;
        public VertexPositionNormalTexture[] EastVertices;
        public ushort[] EastIndices;
        public VertexPositionNormalTexture[] SouthVertices;
        public ushort[] SouthIndices;
        public VertexPositionNormalTexture[] WestVertices;
        public ushort[] WestIndices;

        public BlockVertexData()
        {
            UpVertices = new VertexPositionNormalTexture[0];
            UpIndices = new ushort[0];
            DownIndices = new ushort[0];
            DownVertices = new VertexPositionNormalTexture[0];
            NorthIndices = new ushort[0];
            NorthVertices = new VertexPositionNormalTexture[0];
            EastIndices = new ushort[0];
            EastVertices = new VertexPositionNormalTexture[0];
            SouthIndices = new ushort[0];
            SouthVertices = new VertexPositionNormalTexture[0];
            WestIndices = new ushort[0];
            WestVertices = new VertexPositionNormalTexture[0];
        }

        static BlockVertexData()
        {
            textureOffset = 32f / 2048f;
            bleedingFix = 1f / 2048f;
            _blockData = new List<BlockVertexData>();

            BlockVertexData debugBlock = new BlockVertexData();
            debugBlock.UpVertices = CalculateUpVertices(0);
            debugBlock.UpIndices = BlockIndices.UpIndices;
            debugBlock.DownVertices = CalculateDownVertices(4);
            debugBlock.DownIndices = BlockIndices.DownIndices;
            debugBlock.NorthVertices = CalculateNorthVertices(5);
            debugBlock.NorthIndices = BlockIndices.NorthIndices;
            debugBlock.EastVertices = CalculateEastVertices(1);
            debugBlock.EastIndices = BlockIndices.EastIndices;
            debugBlock.SouthVertices = CalculateSouthVertices(2);
            debugBlock.SouthIndices = BlockIndices.SouthIndices;
            debugBlock.WestVertices = CalculateWestVertices(3);
            debugBlock.WestIndices = BlockIndices.WestIndices;
            debugBlock.CalculateNormals();

            BlockVertexData soil = new BlockVertexData();
            soil.UpVertices = CalculateUpVertices(6);
            soil.UpIndices = BlockIndices.UpIndices;
            soil.DownVertices = CalculateDownVertices(8);
            soil.DownIndices = BlockIndices.DownIndices;
            soil.NorthVertices = CalculateNorthVertices(7);
            soil.NorthIndices = BlockIndices.NorthIndices;
            soil.EastVertices = CalculateEastVertices(7);
            soil.EastIndices = BlockIndices.EastIndices;
            soil.SouthVertices = CalculateSouthVertices(7);
            soil.SouthIndices = BlockIndices.SouthIndices;
            soil.WestVertices = CalculateWestVertices(7);
            soil.WestIndices = BlockIndices.WestIndices;
            soil.CalculateNormals();

            _blockData.Add(new BlockVertexData());
            _blockData.Add(debugBlock);
            _blockData.Add(soil);
        }

        public BlockVertexData Copy()
        {
            BlockVertexData toCopy = this;
            BlockVertexData toReturn = new BlockVertexData();
            toReturn.UpIndices = toCopy.UpIndices;
            toReturn.UpVertices = toCopy.UpVertices;
            toReturn.DownIndices = toCopy.DownIndices;
            toReturn.DownVertices = toCopy.DownVertices;
            toReturn.NorthIndices = toCopy.NorthIndices;
            toReturn.NorthVertices = toCopy.NorthVertices;
            toReturn.EastIndices = toCopy.EastIndices;
            toReturn.EastVertices = toCopy.EastVertices;
            toReturn.SouthIndices = toCopy.SouthIndices;
            toReturn.SouthVertices = toCopy.SouthVertices;
            toReturn.WestIndices = toCopy.WestIndices;
            toReturn.WestVertices = toCopy.WestVertices;

            return toReturn;
        }

        public void CalculateNormals()
        {
            for (int i = 0; i < UpIndices.Length / 3; i++)
            {
                Vector3 firstvec = UpVertices[UpIndices[i * 3 + 1]].Position - UpVertices[UpIndices[i * 3]].Position;
                Vector3 secondvec = UpVertices[UpIndices[i * 3]].Position - UpVertices[UpIndices[i * 3 + 2]].Position;
                Vector3 normal = Vector3.Cross(firstvec, secondvec);
                normal.Normalize();
                UpVertices[UpIndices[i * 3]].Normal += normal;
                UpVertices[UpIndices[i * 3 + 1]].Normal += normal;
                UpVertices[UpIndices[i * 3 + 2]].Normal += normal;
            }
            for (int i = 0; i < DownIndices.Length / 3; i++)
            {
                Vector3 firstvec = DownVertices[DownIndices[i * 3 + 1]].Position - DownVertices[DownIndices[i * 3]].Position;
                Vector3 secondvec = DownVertices[DownIndices[i * 3]].Position - DownVertices[DownIndices[i * 3 + 2]].Position;
                Vector3 normal = Vector3.Cross(firstvec, secondvec);
                normal.Normalize();
                DownVertices[DownIndices[i * 3]].Normal += normal;
                DownVertices[DownIndices[i * 3 + 1]].Normal += normal;
                DownVertices[DownIndices[i * 3 + 2]].Normal += normal;
            }
            for (int i = 0; i < NorthIndices.Length / 3; i++)
            {
                Vector3 firstvec = NorthVertices[NorthIndices[i * 3 + 1]].Position - NorthVertices[NorthIndices[i * 3]].Position;
                Vector3 secondvec = NorthVertices[NorthIndices[i * 3]].Position - NorthVertices[NorthIndices[i * 3 + 2]].Position;
                Vector3 normal = Vector3.Cross(firstvec, secondvec);
                normal.Normalize();
                NorthVertices[NorthIndices[i * 3]].Normal += normal;
                NorthVertices[NorthIndices[i * 3 + 1]].Normal += normal;
                NorthVertices[NorthIndices[i * 3 + 2]].Normal += normal;
            }
            for (int i = 0; i < EastIndices.Length / 3; i++)
            {
                Vector3 firstvec = EastVertices[EastIndices[i * 3 + 1]].Position - EastVertices[EastIndices[i * 3]].Position;
                Vector3 secondvec = EastVertices[EastIndices[i * 3]].Position - EastVertices[EastIndices[i * 3 + 2]].Position;
                Vector3 normal = Vector3.Cross(firstvec, secondvec);
                normal.Normalize();
                EastVertices[EastIndices[i * 3]].Normal += normal;
                EastVertices[EastIndices[i * 3 + 1]].Normal += normal;
                EastVertices[EastIndices[i * 3 + 2]].Normal += normal;
            }
            for (int i = 0; i < SouthIndices.Length / 3; i++)
            {
                Vector3 firstvec = SouthVertices[SouthIndices[i * 3 + 1]].Position - SouthVertices[SouthIndices[i * 3]].Position;
                Vector3 secondvec = SouthVertices[SouthIndices[i * 3]].Position - SouthVertices[SouthIndices[i * 3 + 2]].Position;
                Vector3 normal = Vector3.Cross(firstvec, secondvec);
                normal.Normalize();
                SouthVertices[SouthIndices[i * 3]].Normal += normal;
                SouthVertices[SouthIndices[i * 3 + 1]].Normal += normal;
                SouthVertices[SouthIndices[i * 3 + 2]].Normal += normal;
            }
            for (int i = 0; i < WestIndices.Length / 3; i++)
            {
                Vector3 firstvec = WestVertices[WestIndices[i * 3 + 1]].Position - WestVertices[WestIndices[i * 3]].Position;
                Vector3 secondvec = WestVertices[WestIndices[i * 3]].Position - WestVertices[WestIndices[i * 3 + 2]].Position;
                Vector3 normal = Vector3.Cross(firstvec, secondvec);
                normal.Normalize();
                WestVertices[WestIndices[i * 3]].Normal += normal;
                WestVertices[WestIndices[i * 3 + 1]].Normal += normal;
                WestVertices[WestIndices[i * 3 + 2]].Normal += normal;
            }

        }

        public static BlockVertexData GetBlockMaskVertexData(BlockMask blockMask)
        {
            if (BlockHelper.IsBlock(blockMask))
            {
                return _blockData[BlockHelper.GetBlockID(blockMask)];
            }

            return new BlockVertexData();
        }

        public static BlockVertexData GetCopyOfBlockMasBlockVertexData(BlockMask blockMask)
        {
            BlockVertexData toCopy = GetBlockMaskVertexData(blockMask);
            BlockVertexData toReturn = new BlockVertexData();
            toReturn.UpIndices = toCopy.UpIndices;
            toReturn.UpVertices = toCopy.UpVertices;
            toReturn.DownIndices = toCopy.DownIndices;
            toReturn.DownVertices = toCopy.DownVertices;
            toReturn.NorthIndices = toCopy.NorthIndices;
            toReturn.NorthVertices = toCopy.NorthVertices;
            toReturn.EastIndices = toCopy.EastIndices;
            toReturn.EastVertices = toCopy.EastVertices;
            toReturn.SouthIndices = toCopy.SouthIndices;
            toReturn.SouthVertices = toCopy.SouthVertices;
            toReturn.WestIndices = toCopy.WestIndices;
            toReturn.WestVertices = toCopy.WestVertices;

            return toReturn;
        }

        public void TranslateToWorldLocation(Vector3 worldLocation)
        {
            UpVertices = VertexHelper.TranslateVerticesToWorldLocation(UpVertices, worldLocation);
            DownVertices = VertexHelper.TranslateVerticesToWorldLocation(DownVertices, worldLocation);
            NorthVertices = VertexHelper.TranslateVerticesToWorldLocation(NorthVertices, worldLocation);
            EastVertices = VertexHelper.TranslateVerticesToWorldLocation(EastVertices, worldLocation);
            SouthVertices = VertexHelper.TranslateVerticesToWorldLocation(SouthVertices, worldLocation);
            WestVertices = VertexHelper.TranslateVerticesToWorldLocation(WestVertices, worldLocation);
        }

        static VertexPositionNormalTexture[] CalculateUpVertices(int topFaceIndex)
        {
            VertexPositionNormalTexture[] topVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int topRow = (int)Math.Floor((double)topFaceIndex / 64);
            int topColumn = topFaceIndex % 64;

            float topRowStartX = (topColumn * textureOffset) + bleedingFix;
            float topRowEndX = ((topColumn + 1) * textureOffset) - bleedingFix;
            float topRowStartY = (topRow * textureOffset) + bleedingFix;
            float topRowEndY = ((topRow + 1) * textureOffset) - bleedingFix;

            topVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopLeft, Vector3.Zero, new Vector2(topRowEndX, topRowStartY));
            topVertices[1] = new VertexPositionNormalTexture(BlockVertices.UpTopRight, Vector3.Zero, new Vector2(topRowStartX, topRowStartY));
            topVertices[2] = new VertexPositionNormalTexture(BlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));
            topVertices[3] = new VertexPositionNormalTexture(BlockVertices.UpBottomRight, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateDownVertices(int bottomFaceIndex)
        {
            VertexPositionNormalTexture[] bottomVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int bottomRow = (int)Math.Floor((double)bottomFaceIndex / 64);
            int bottomColumn = bottomFaceIndex % 64;

            float bottomRowStartX = (bottomColumn * textureOffset) + bleedingFix;
            float bottomRowEndX = ((bottomColumn + 1) * textureOffset) - bleedingFix;
            float bottomRowStartY = (bottomRow * textureOffset) + bleedingFix;
            float bottomRowEndY = ((bottomRow + 1) * textureOffset) - bleedingFix;

            bottomVertices[0] = new VertexPositionNormalTexture(BlockVertices.DownTopLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowStartY));
            bottomVertices[1] = new VertexPositionNormalTexture(BlockVertices.DownTopRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowStartY));
            bottomVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowEndY));
            bottomVertices[3] = new VertexPositionNormalTexture(BlockVertices.DownBottomRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowEndY));

            return bottomVertices;
        }

        private static VertexPositionNormalTexture[] CalculateNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int northRow = (int)Math.Floor((double)northFaceIndex / 64);
            int northColumn = northFaceIndex % 64;

            float northRowStartX = (northColumn * textureOffset) + bleedingFix;
            float northRowEndX = ((northColumn + 1) * textureOffset) - bleedingFix;
            float northRowStartY = (northRow * textureOffset) + bleedingFix;
            float northRowEndY = ((northRow + 1) * textureOffset) - bleedingFix;

            northVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowStartY));
            northVertices[1] = new VertexPositionNormalTexture(BlockVertices.UpTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowStartY));
            northVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowEndY));
            northVertices[3] = new VertexPositionNormalTexture(BlockVertices.DownTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowEndY));

            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int eastRow = (int)Math.Floor((double)eastFaceIndex / 64);
            int eastColumn = eastFaceIndex % 64;

            float eastRowStartX = (eastColumn * textureOffset) + bleedingFix;
            float eastRowEndX = ((eastColumn + 1) * textureOffset) - bleedingFix;
            float eastRowStartY = (eastRow * textureOffset) + bleedingFix;
            float eastRowEndY = ((eastRow + 1) * textureOffset) - bleedingFix;

            eastVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowStartY));
            eastVertices[1] = new VertexPositionNormalTexture(BlockVertices.UpBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowStartY));
            eastVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowEndY));
            eastVertices[3] = new VertexPositionNormalTexture(BlockVertices.DownTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowEndY));

            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int southRow = (int)Math.Floor((double)southFaceIndex / 64);
            int southColumn = southFaceIndex % 64;

            float southRowStartX = (southColumn * textureOffset) + bleedingFix;
            float southRowEndX = ((southColumn + 1) * textureOffset) - bleedingFix;
            float southRowStartY = (southRow * textureOffset) + bleedingFix;
            float southRowEndY = ((southRow + 1) * textureOffset) - bleedingFix;

            southVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowStartY));
            southVertices[1] = new VertexPositionNormalTexture(BlockVertices.UpBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowStartY));
            southVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowEndY));
            southVertices[3] = new VertexPositionNormalTexture(BlockVertices.DownBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowEndY));

            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int westRow = (int)Math.Floor((double)westFaceIndex / 64);
            int westColumn = westFaceIndex % 64;

            float westRowStartX = (westColumn * textureOffset) + bleedingFix;
            float westRowEndX = ((westColumn + 1) * textureOffset) - bleedingFix;
            float westRowStartY = (westRow * textureOffset) + bleedingFix;
            float westRowEndY = ((westRow + 1) * textureOffset) - bleedingFix;

            westVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowStartY));
            westVertices[1] = new VertexPositionNormalTexture(BlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowStartY));
            westVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowEndY));
            westVertices[3] = new VertexPositionNormalTexture(BlockVertices.DownTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowEndY));

            return westVertices;
        }

        public void Merge(BlockVertexData vertexData)
        {
            // prepare verts
            List<VertexPositionNormalTexture> upVertices = new List<VertexPositionNormalTexture>();
            List<VertexPositionNormalTexture> downVertices = new List<VertexPositionNormalTexture>();
            List<VertexPositionNormalTexture> northVertices = new List<VertexPositionNormalTexture>();
            List<VertexPositionNormalTexture> eastVertices = new List<VertexPositionNormalTexture>();
            List<VertexPositionNormalTexture> southVertices = new List<VertexPositionNormalTexture>();
            List<VertexPositionNormalTexture> westVertices = new List<VertexPositionNormalTexture>();

            // merge verts
            upVertices.AddRange(UpVertices);
            downVertices.AddRange(DownVertices);
            northVertices.AddRange(NorthVertices);
            eastVertices.AddRange(EastVertices);
            southVertices.AddRange(SouthVertices);
            westVertices.AddRange(WestVertices);
            upVertices.AddRange(vertexData.UpVertices);
            downVertices.AddRange(vertexData.DownVertices);
            northVertices.AddRange(vertexData.NorthVertices);
            eastVertices.AddRange(vertexData.EastVertices);
            southVertices.AddRange(vertexData.SouthVertices);
            westVertices.AddRange(vertexData.WestVertices);

            //prepare indices
            List<ushort> upIndices = new List<ushort>();
            List<ushort> downIndices = new List<ushort>();
            List<ushort> northIndices = new List<ushort>();
            List<ushort> eastIndices = new List<ushort>();
            List<ushort> southIndices = new List<ushort>();
            List<ushort> westIndices = new List<ushort>();

            // merge indices
            ushort upOffset = (ushort)UpVertices.Length;
            ushort downOffset = (ushort)DownVertices.Length;
            ushort northOffset = (ushort)NorthVertices.Length;
            ushort eastOffset = (ushort)EastVertices.Length;
            ushort southOffset = (ushort)SouthVertices.Length;
            ushort westOffset = (ushort)WestVertices.Length;

            foreach (ushort index in UpIndices)
            {
                upIndices.Add(index);
            }
            foreach (ushort index in vertexData.UpIndices)
            {
                upIndices.Add((ushort)(index + upOffset));
            }
            foreach (ushort index in DownIndices)
            {
                downIndices.Add(index);
            }
            foreach (ushort index in vertexData.DownIndices)
            {
                downIndices.Add((ushort)(index + downOffset));
            }
            foreach (ushort index in NorthIndices)
            {
                northIndices.Add(index);
            }
            foreach (ushort index in vertexData.NorthIndices)
            {
                northIndices.Add((ushort)(index + northOffset));
            }
            foreach (ushort index in EastIndices)
            {
                eastIndices.Add(index);
            }
            foreach (ushort index in vertexData.EastIndices)
            {
                eastIndices.Add((ushort)(index + eastOffset));
            }
            foreach (ushort index in SouthIndices)
            {
                southIndices.Add(index);
            }
            foreach (ushort index in vertexData.SouthIndices)
            {
                southIndices.Add((ushort)(index + southOffset));
            }
            foreach (ushort index in WestIndices)
            {
                westIndices.Add(index);
            }
            foreach (ushort index in vertexData.WestIndices)
            {
                westIndices.Add((ushort)(index + westOffset));
            }

            // set to object
            UpVertices = upVertices.ToArray();
            DownVertices = downVertices.ToArray();
            NorthVertices = northVertices.ToArray();
            EastVertices = eastVertices.ToArray();
            SouthVertices = southVertices.ToArray();
            WestVertices = westVertices.ToArray();
            UpIndices = upIndices.ToArray();
            DownIndices = downIndices.ToArray();
            NorthIndices = northIndices.ToArray();
            EastIndices = eastIndices.ToArray();
            SouthIndices = southIndices.ToArray();
            WestIndices = westIndices.ToArray();
        }

        public VertexPositionNormalTexture[] GetAllVertsTranslated(Vector3 worldGridLocation)
        {
            int totalCount = UpVertices.Length + DownVertices.Length + NorthVertices.Length + EastVertices.Length +
                             WestVertices.Length + SouthVertices.Length;

            VertexPositionNormalTexture[] toReturn = new VertexPositionNormalTexture[totalCount];
            int index = 0;

            foreach (var vert in VertexHelper.TranslateVerticesToWorldLocation(UpVertices, worldGridLocation))
            {
                toReturn[index++] = vert;
            }
            foreach (var vert in VertexHelper.TranslateVerticesToWorldLocation(DownVertices, worldGridLocation))
            {
                toReturn[index++] = vert;
            }
            foreach (var vert in VertexHelper.TranslateVerticesToWorldLocation(NorthVertices, worldGridLocation))
            {
                toReturn[index++] = vert;
            }
            foreach (var vert in VertexHelper.TranslateVerticesToWorldLocation(EastVertices, worldGridLocation))
            {
                toReturn[index++] = vert;
            }
            foreach (var vert in VertexHelper.TranslateVerticesToWorldLocation(SouthVertices, worldGridLocation))
            {
                toReturn[index++] = vert;
            }
            foreach (var vert in VertexHelper.TranslateVerticesToWorldLocation(WestVertices, worldGridLocation))
            {
                toReturn[index++] = vert;
            }

            return toReturn;
        }

        public short[] GetAllIndices()
        {
            int totalCount = UpIndices.Length + DownIndices.Length + NorthIndices.Length + EastIndices.Length +
                             WestIndices.Length + SouthIndices.Length;

            short[] toReturn = new short[totalCount];

            short index = 0;
            short offset = 0;
            foreach (var vert in UpIndices)
            {
                toReturn[index++] = (short)(vert + offset);
            }
            offset += (short)UpVertices.Length;
            foreach (var vert in DownIndices)
            {
                toReturn[index++] = (short)(vert + offset);
            }
            offset += (short)DownVertices.Length;
            foreach (var vert in NorthIndices)
            {
                toReturn[index++] = (short)(vert + offset);
            }
            offset += (short)NorthVertices.Length;
            foreach (var vert in EastIndices)
            {
                toReturn[index++] = (short)(vert + offset);
            }
            offset += (short)EastVertices.Length;
            foreach (var vert in SouthIndices)
            {
                toReturn[index++] = (short)(vert + offset);
            }
            offset += (short)SouthVertices.Length;
            foreach (var vert in WestIndices)
            {
                toReturn[index++] = (short)(vert + offset);
            }

            return toReturn;
        }

    }
}
