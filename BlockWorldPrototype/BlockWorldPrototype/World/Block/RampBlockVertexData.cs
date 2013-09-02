// -----------------------------------------------------------------------
// <copyright file="RampBlockVertexData.cs" company="Microsoft">
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
    public class RampBlockVertexData
    {
        public BlockVertexData TopNorthRampVertexData;
        public BlockVertexData TopEastRampVertexData;
        public BlockVertexData TopSouthRampVertexData;
        public BlockVertexData TopWestRampVertexData;
        public BlockVertexData BottomNorthRampVertexData;
        public BlockVertexData BottomEastRampVertexData;
        public BlockVertexData BottomSouthRampVertexData;
        public BlockVertexData BottomWestRampVertexData;

        private static float textureOffset = 0;
        private static float bleedingFix = 0;

        private static List<RampBlockVertexData> _blockRampData;


        public RampBlockVertexData()
        {
            TopNorthRampVertexData = new BlockVertexData();
            TopNorthRampVertexData = new BlockVertexData();
            TopEastRampVertexData = new BlockVertexData();
            TopSouthRampVertexData = new BlockVertexData();
            TopWestRampVertexData = new BlockVertexData();
            BottomNorthRampVertexData = new BlockVertexData();
            BottomEastRampVertexData = new BlockVertexData();
            BottomSouthRampVertexData = new BlockVertexData();
            BottomWestRampVertexData = new BlockVertexData();
        }

        static RampBlockVertexData()
        {
            textureOffset = 32f / 2048f;
            bleedingFix = 1f / 2048f;

            _blockRampData = new List<RampBlockVertexData>();
            _blockRampData.Add(new RampBlockVertexData());

            RampBlockVertexData debugRamp = new RampBlockVertexData();
            debugRamp.BottomNorthRampVertexData = GetBottomNorthRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.BottomEastRampVertexData = GetBottomEastRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.BottomSouthRampVertexData = GetBottomSouthRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.BottomWestRampVertexData = GetBottomWestRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.TopNorthRampVertexData = GetTopNorthRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.TopSouthRampVertexData = GetTopSouthRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.TopEastRampVertexData = GetTopEastRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.TopWestRampVertexData = GetTopWestRampBlockVertexData(0, 4, 5, 1, 2, 3);
            //debugRamp.down
            _blockRampData.Add(debugRamp);

            RampBlockVertexData soilRamp = new RampBlockVertexData();
            soilRamp.BottomNorthRampVertexData = GetBottomNorthRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.BottomEastRampVertexData = GetBottomEastRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.BottomSouthRampVertexData = GetBottomSouthRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.BottomWestRampVertexData = GetBottomWestRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.TopNorthRampVertexData = GetTopNorthRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.TopSouthRampVertexData = GetTopSouthRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.TopEastRampVertexData = GetTopEastRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.TopWestRampVertexData = GetTopWestRampBlockVertexData(6, 8, 7, 7, 7, 7);
            _blockRampData.Add(soilRamp);
        }

        static public BlockVertexData GetBlockMaskVertexData(BlockMask blockMask)
        {
            BlockMask direction = BlockHelper.GetRampBlockDirection(blockMask);
            if (BlockHelper.IsRampBlock(blockMask))
            {
                if (BlockHelper.HasBottomRamp(blockMask) && BlockHelper.HasTopRamp(blockMask))
                {
                    if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.North)
                    {
                        BlockVertexData toReturnDown = _blockRampData[BlockHelper.GetBottomRampID(blockMask)].BottomNorthRampVertexData.Copy();
                        BlockVertexData toReturnTop = _blockRampData[BlockHelper.GetTopRampID(blockMask)].TopNorthRampVertexData.Copy();
                        toReturnDown.UpVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.UpIndices = new ushort[0];
                        toReturnTop.DownIndices = new ushort[0];
                        toReturnTop.DownVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.Merge(toReturnTop.Copy());
                        return toReturnDown;
                    }
                    if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.East)
                    {
                        BlockVertexData toReturnDown = _blockRampData[BlockHelper.GetBottomRampID(blockMask)].BottomEastRampVertexData.Copy();
                        BlockVertexData toReturnTop = _blockRampData[BlockHelper.GetTopRampID(blockMask)].TopEastRampVertexData.Copy();
                        toReturnDown.UpVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.UpIndices = new ushort[0];
                        toReturnTop.DownIndices = new ushort[0];
                        toReturnTop.DownVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.Merge(toReturnTop.Copy());
                        return toReturnDown;
                    }
                    if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.South)
                    {
                        BlockVertexData toReturnDown = _blockRampData[BlockHelper.GetBottomRampID(blockMask)].BottomSouthRampVertexData.Copy();
                        BlockVertexData toReturnTop = _blockRampData[BlockHelper.GetTopRampID(blockMask)].TopSouthRampVertexData.Copy();
                        toReturnDown.UpVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.UpIndices = new ushort[0];
                        toReturnTop.DownIndices = new ushort[0];
                        toReturnTop.DownVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.Merge(toReturnTop.Copy());
                        return toReturnDown;
                    }
                    if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.West)
                    {
                        BlockVertexData toReturnDown = _blockRampData[BlockHelper.GetBottomRampID(blockMask)].BottomWestRampVertexData.Copy();
                        BlockVertexData toReturnTop = _blockRampData[BlockHelper.GetTopRampID(blockMask)].TopWestRampVertexData.Copy();
                        toReturnDown.UpVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.UpIndices = new ushort[0];
                        toReturnTop.DownIndices = new ushort[0];
                        toReturnTop.DownVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.Merge(toReturnTop.Copy());
                        return toReturnDown;
                    }
                }
                if (BlockHelper.HasTopRamp(blockMask))
                {
                    if (direction == RampBlockDirection.North)
                    {
                        return _blockRampData[BlockHelper.GetTopRampID(blockMask)].TopNorthRampVertexData.Copy();
                    }
                    if (direction == RampBlockDirection.East)
                    {
                        return _blockRampData[BlockHelper.GetTopRampID(blockMask)].TopEastRampVertexData.Copy();
                    }
                    if (direction == RampBlockDirection.South)
                    {
                        return _blockRampData[BlockHelper.GetTopRampID(blockMask)].TopSouthRampVertexData.Copy();
                    }
                    if (direction == RampBlockDirection.West)
                    {
                        return _blockRampData[BlockHelper.GetTopRampID(blockMask)].TopWestRampVertexData.Copy();
                    }
                }
                if (BlockHelper.HasBottomRamp(blockMask))
                {
                    if (direction == RampBlockDirection.North)
                    {
                        return _blockRampData[BlockHelper.GetBottomRampID(blockMask)].BottomNorthRampVertexData.Copy();
                    }
                    if (direction == RampBlockDirection.East)
                    {
                        return _blockRampData[BlockHelper.GetBottomRampID(blockMask)].BottomEastRampVertexData.Copy();
                    }
                    if (direction == RampBlockDirection.South)
                    {
                        return _blockRampData[BlockHelper.GetBottomRampID(blockMask)].BottomSouthRampVertexData.Copy();
                    }
                    if (direction == RampBlockDirection.West)
                    {
                        return _blockRampData[BlockHelper.GetBottomRampID(blockMask)].BottomWestRampVertexData.Copy();
                    }
                }
            }

            return new BlockVertexData();
        }

        static public RampBlockVertexData GetBlockMaskRampVertexData(BlockMask blockMask)
        {
            if (BlockHelper.IsRampBlock(blockMask))
            {
                // top + bottom needs doing
                if (BlockHelper.HasTopRamp(blockMask))
                    return _blockRampData[BlockHelper.GetTopRampID(blockMask)];
                if (BlockHelper.HasBottomRamp(blockMask))
                    return _blockRampData[BlockHelper.GetBottomRampID(blockMask)];
            }

            return new RampBlockVertexData();
        }

        public static BlockVertexData GetCopyOfBlockMasBlockVertexData(BlockMask blockMask)
        {
            RampBlockVertexData toCopy = GetBlockMaskRampVertexData(blockMask);

            if (BlockHelper.HasTopRamp(blockMask) && BlockHelper.HasBottomRamp(blockMask))
            {
                if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.North)
                {
                    BlockVertexData toReturnDown = toCopy.BottomNorthRampVertexData.Copy();
                    toReturnDown.UpVertices = new VertexPositionNormalTexture[0];
                    toReturnDown.UpIndices = new ushort[0];
                    BlockVertexData toReturnTop = toCopy.TopNorthRampVertexData.Copy();
                    toReturnTop.DownIndices = new ushort[0];
                    toReturnTop.DownVertices = new VertexPositionNormalTexture[0];
                    toReturnDown.Merge(toReturnTop.Copy());
                    return toReturnDown;
                }
                if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.East)
                {
                    BlockVertexData toReturnDown = toCopy.BottomNorthRampVertexData.Copy();
                    toReturnDown.UpVertices = new VertexPositionNormalTexture[0];
                    toReturnDown.UpIndices = new ushort[0];
                    BlockVertexData toReturnTop = toCopy.TopNorthRampVertexData.Copy();
                    toReturnTop.DownIndices = new ushort[0];
                    toReturnTop.DownVertices = new VertexPositionNormalTexture[0];
                    toReturnDown.Merge(toReturnTop.Copy());
                    return toReturnDown;
                }
                if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.South)
                {
                    BlockVertexData toReturn = toCopy.BottomSouthRampVertexData.Copy();
                    toReturn.Merge(toCopy.TopSouthRampVertexData.Copy());
                    return toReturn;
                }
                if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.West)
                {
                    BlockVertexData toReturn = toCopy.BottomWestRampVertexData.Copy();
                    toReturn.Merge(toCopy.TopWestRampVertexData.Copy());
                    return toReturn;
                }
            }

            if (BlockHelper.HasBottomRamp(blockMask))
            {
                if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.North)
                {
                    return toCopy.BottomNorthRampVertexData.Copy();
                }
                if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.East)
                {
                    return toCopy.BottomEastRampVertexData.Copy();
                }
                if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.South)
                {
                    return toCopy.BottomSouthRampVertexData.Copy();
                }
                if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.West)
                {
                    return toCopy.BottomWestRampVertexData.Copy();
                }
            }
            if (BlockHelper.HasTopRamp(blockMask))
            {
                if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.North)
                {
                    return toCopy.TopNorthRampVertexData.Copy();
                }
                if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.East)
                {
                    return toCopy.TopEastRampVertexData.Copy();
                }
                if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.South)
                {
                    return toCopy.TopSouthRampVertexData.Copy();
                }
                if (BlockHelper.GetRampBlockDirection(blockMask) == RampBlockDirection.West)
                {
                    return toCopy.TopWestRampVertexData.Copy();
                }
            }


            return new BlockVertexData();
        }

        static BlockVertexData GetBottomNorthRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            BlockVertexData toReturn = new BlockVertexData();
            toReturn.UpVertices = CalculateBottomNorthUpVertices(topIndex);
            toReturn.UpIndices = RampBlockIndices.DownNorth.UpIndices;
            toReturn.DownVertices = CalculateBottomNorthDownVertices(downIndex);
            toReturn.DownIndices = RampBlockIndices.DownNorth.DownIndices;
            toReturn.NorthVertices = CalculateBottomNorthNorthVertices(northIndex);
            toReturn.NorthIndices = RampBlockIndices.DownNorth.NorthIndices;
            toReturn.EastVertices = CalculateBottomNorthEastVertices(eastIndex);
            toReturn.EastIndices = RampBlockIndices.DownNorth.EastIndices;
            toReturn.SouthVertices = CalculateBottomNorthSouthVertices(southIndex);
            toReturn.SouthIndices = RampBlockIndices.DownNorth.SouthIndices;
            toReturn.WestVertices = CalculateBottomNorthWestVertices(westIndex);
            toReturn.WestIndices = RampBlockIndices.DownNorth.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

        static VertexPositionNormalTexture[] CalculateBottomNorthUpVertices(int topFaceIndex)
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
            topVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));
            topVertices[3] = new VertexPositionNormalTexture(BlockVertices.DownBottomRight, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomNorthDownVertices(int bottomFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateBottomNorthNorthVertices(int northFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateBottomNorthEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int eastRow = (int)Math.Floor((double)eastFaceIndex / 64);
            int eastColumn = eastFaceIndex % 64;

            float eastRowStartX = (eastColumn * textureOffset) + bleedingFix;
            float eastRowEndX = ((eastColumn + 1) * textureOffset) - bleedingFix;
            float eastRowStartY = (eastRow * textureOffset) + bleedingFix;
            float eastRowEndY = ((eastRow + 1) * textureOffset) - bleedingFix;

            eastVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowStartY));
            eastVertices[1] = new VertexPositionNormalTexture(BlockVertices.DownBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowEndY));
            eastVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowEndY));

            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomNorthSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[0];
            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomNorthWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int westRow = (int)Math.Floor((double)westFaceIndex / 64);
            int westColumn = westFaceIndex % 64;

            float westRowStartX = (westColumn * textureOffset) + bleedingFix;
            float westRowEndX = ((westColumn + 1) * textureOffset) - bleedingFix;
            float westRowStartY = (westRow * textureOffset) + bleedingFix;
            float westRowEndY = ((westRow + 1) * textureOffset) - bleedingFix;

            westVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowStartY));
            westVertices[1] = new VertexPositionNormalTexture(BlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowEndY));
            westVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowEndY));

            return westVertices;
        }

        static BlockVertexData GetBottomEastRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            BlockVertexData toReturn = new BlockVertexData();
            toReturn.UpVertices = CalculateBottomEastUpVertices(topIndex);
            toReturn.UpIndices = RampBlockIndices.DownEast.UpIndices;
            toReturn.DownVertices = CalculateBottomEastDownVertices(downIndex);
            toReturn.DownIndices = RampBlockIndices.DownEast.DownIndices;
            toReturn.NorthVertices = CalculateBottomEastNorthVertices(northIndex);
            toReturn.NorthIndices = RampBlockIndices.DownEast.NorthIndices;
            toReturn.EastVertices = CalculateBottomEastEastVertices(eastIndex);
            toReturn.EastIndices = RampBlockIndices.DownEast.EastIndices;
            toReturn.SouthVertices = CalculateBottomEastSouthVertices(southIndex);
            toReturn.SouthIndices = RampBlockIndices.DownEast.SouthIndices;
            toReturn.WestVertices = CalculateBottomEastWestVertices(westIndex);
            toReturn.WestIndices = RampBlockIndices.DownEast.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

        static VertexPositionNormalTexture[] CalculateBottomEastUpVertices(int topFaceIndex)
        {
            VertexPositionNormalTexture[] topVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int topRow = (int)Math.Floor((double)topFaceIndex / 64);
            int topColumn = topFaceIndex % 64;

            float topRowStartX = (topColumn * textureOffset) + bleedingFix;
            float topRowEndX = ((topColumn + 1) * textureOffset) - bleedingFix;
            float topRowStartY = (topRow * textureOffset) + bleedingFix;
            float topRowEndY = ((topRow + 1) * textureOffset) - bleedingFix;

            topVertices[0] = new VertexPositionNormalTexture(BlockVertices.DownTopLeft, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));
            topVertices[1] = new VertexPositionNormalTexture(BlockVertices.UpTopRight, Vector3.Zero, new Vector2(topRowEndX, topRowStartY));
            topVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));
            topVertices[3] = new VertexPositionNormalTexture(BlockVertices.UpBottomRight, Vector3.Zero, new Vector2(topRowStartX, topRowStartY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomEastDownVertices(int bottomFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateBottomEastNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int northRow = (int)Math.Floor((double)northFaceIndex / 64);
            int northColumn = northFaceIndex % 64;

            float northRowStartX = (northColumn * textureOffset) + bleedingFix;
            float northRowEndX = ((northColumn + 1) * textureOffset) - bleedingFix;
            float northRowStartY = (northRow * textureOffset) + bleedingFix;
            float northRowEndY = ((northRow + 1) * textureOffset) - bleedingFix;

            northVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowStartY));
            northVertices[1] = new VertexPositionNormalTexture(BlockVertices.DownTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowEndY));
            northVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowEndY));

            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomEastEastVertices(int eastFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateBottomEastSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int southRow = (int)Math.Floor((double)southFaceIndex / 64);
            int southColumn = southFaceIndex % 64;

            float southRowStartX = (southColumn * textureOffset) + bleedingFix;
            float southRowEndX = ((southColumn + 1) * textureOffset) - bleedingFix;
            float southRowStartY = (southRow * textureOffset) + bleedingFix;
            float southRowEndY = ((southRow + 1) * textureOffset) - bleedingFix;

            southVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowStartY));
            southVertices[1] = new VertexPositionNormalTexture(BlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowEndY));
            southVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowEndY));

            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomEastWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[0];
            return westVertices;
        }

        static BlockVertexData GetBottomSouthRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            BlockVertexData toReturn = new BlockVertexData();
            toReturn.UpVertices = CalculateBottomSouthUpVertices(topIndex);
            toReturn.UpIndices = RampBlockIndices.DownSouth.UpIndices;
            toReturn.DownVertices = CalculateBottomSouthDownVertices(downIndex);
            toReturn.DownIndices = RampBlockIndices.DownSouth.DownIndices;
            toReturn.NorthVertices = CalculateBottomSouthNorthVertices(northIndex);
            toReturn.NorthIndices = RampBlockIndices.DownSouth.NorthIndices;
            toReturn.EastVertices = CalculateBottomSouthEastVertices(eastIndex);
            toReturn.EastIndices = RampBlockIndices.DownSouth.EastIndices;
            toReturn.SouthVertices = CalculateBottomSouthSouthVertices(southIndex);
            toReturn.SouthIndices = RampBlockIndices.DownSouth.SouthIndices;
            toReturn.WestVertices = CalculateBottomSouthWestVertices(westIndex);
            toReturn.WestIndices = RampBlockIndices.DownSouth.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

        static VertexPositionNormalTexture[] CalculateBottomSouthUpVertices(int topFaceIndex)
        {
            VertexPositionNormalTexture[] topVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int topRow = (int)Math.Floor((double)topFaceIndex / 64);
            int topColumn = topFaceIndex % 64;

            float topRowStartX = (topColumn * textureOffset) + bleedingFix;
            float topRowEndX = ((topColumn + 1) * textureOffset) - bleedingFix;
            float topRowStartY = (topRow * textureOffset) + bleedingFix;
            float topRowEndY = ((topRow + 1) * textureOffset) - bleedingFix;

            topVertices[0] = new VertexPositionNormalTexture(BlockVertices.DownTopLeft, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));
            topVertices[1] = new VertexPositionNormalTexture(BlockVertices.DownTopRight, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));
            topVertices[2] = new VertexPositionNormalTexture(BlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(topRowStartX, topRowStartY));
            topVertices[3] = new VertexPositionNormalTexture(BlockVertices.UpBottomRight, Vector3.Zero, new Vector2(topRowEndX, topRowStartY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomSouthDownVertices(int bottomFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateBottomSouthNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[0];


            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomSouthEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int eastRow = (int)Math.Floor((double)eastFaceIndex / 64);
            int eastColumn = eastFaceIndex % 64;

            float eastRowStartX = (eastColumn * textureOffset) + bleedingFix;
            float eastRowEndX = ((eastColumn + 1) * textureOffset) - bleedingFix;
            float eastRowStartY = (eastRow * textureOffset) + bleedingFix;
            float eastRowEndY = ((eastRow + 1) * textureOffset) - bleedingFix;

            eastVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowStartY));
            eastVertices[1] = new VertexPositionNormalTexture(BlockVertices.DownBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowEndY));
            eastVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowEndY));

            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomSouthSouthVertices(int southFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateBottomSouthWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int westRow = (int)Math.Floor((double)westFaceIndex / 64);
            int westColumn = westFaceIndex % 64;

            float westRowStartX = (westColumn * textureOffset) + bleedingFix;
            float westRowEndX = ((westColumn + 1) * textureOffset) - bleedingFix;
            float westRowStartY = (westRow * textureOffset) + bleedingFix;
            float westRowEndY = ((westRow + 1) * textureOffset) - bleedingFix;


            westVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowStartY));
            westVertices[1] = new VertexPositionNormalTexture(BlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowEndY));
            westVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowEndY));

            return westVertices;
        }

        static VertexPositionNormalTexture[] CalculateBottomWestUpVertices(int topFaceIndex)
        {
            VertexPositionNormalTexture[] topVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int topRow = (int)Math.Floor((double)topFaceIndex / 64);
            int topColumn = topFaceIndex % 64;

            float topRowStartX = (topColumn * textureOffset) + bleedingFix;
            float topRowEndX = ((topColumn + 1) * textureOffset) - bleedingFix;
            float topRowStartY = (topRow * textureOffset) + bleedingFix;
            float topRowEndY = ((topRow + 1) * textureOffset) - bleedingFix;

            topVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopLeft, Vector3.Zero, new Vector2(topRowStartX, topRowStartY));
            topVertices[1] = new VertexPositionNormalTexture(BlockVertices.DownTopRight, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));
            topVertices[2] = new VertexPositionNormalTexture(BlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(topRowEndX, topRowStartY));
            topVertices[3] = new VertexPositionNormalTexture(BlockVertices.DownBottomRight, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomWestDownVertices(int bottomFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateBottomWestNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int northRow = (int)Math.Floor((double)northFaceIndex / 64);
            int northColumn = northFaceIndex % 64;

            float northRowStartX = (northColumn * textureOffset) + bleedingFix;
            float northRowEndX = ((northColumn + 1) * textureOffset) - bleedingFix;
            float northRowStartY = (northRow * textureOffset) + bleedingFix;
            float northRowEndY = ((northRow + 1) * textureOffset) - bleedingFix;

            northVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowStartY));
            northVertices[1] = new VertexPositionNormalTexture(BlockVertices.DownTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowEndY));
            northVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowEndY));

            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomWestEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[0];

            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomWestSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int southRow = (int)Math.Floor((double)southFaceIndex / 64);
            int southColumn = southFaceIndex % 64;

            float southRowStartX = (southColumn * textureOffset) + bleedingFix;
            float southRowEndX = ((southColumn + 1) * textureOffset) - bleedingFix;
            float southRowStartY = (southRow * textureOffset) + bleedingFix;
            float southRowEndY = ((southRow + 1) * textureOffset) - bleedingFix;

            southVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowStartY));
            southVertices[1] = new VertexPositionNormalTexture(BlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowEndY));
            southVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowEndY));

            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomWestWestVertices(int westFaceIndex)
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

        static BlockVertexData GetBottomWestRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            BlockVertexData toReturn = new BlockVertexData();
            toReturn.UpVertices = CalculateBottomWestUpVertices(topIndex);
            toReturn.UpIndices = RampBlockIndices.DownWest.UpIndices;
            toReturn.DownVertices = CalculateBottomWestDownVertices(downIndex);
            toReturn.DownIndices = RampBlockIndices.DownWest.DownIndices;
            toReturn.NorthVertices = CalculateBottomWestNorthVertices(northIndex);
            toReturn.NorthIndices = RampBlockIndices.DownWest.NorthIndices;
            toReturn.EastVertices = CalculateBottomWestEastVertices(eastIndex);
            toReturn.EastIndices = RampBlockIndices.DownWest.EastIndices;
            toReturn.SouthVertices = CalculateBottomWestSouthVertices(southIndex);
            toReturn.SouthIndices = RampBlockIndices.DownWest.SouthIndices;
            toReturn.WestVertices = CalculateBottomWestWestVertices(westIndex);
            toReturn.WestIndices = RampBlockIndices.DownWest.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }
        static BlockVertexData GetTopNorthRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            BlockVertexData toReturn = new BlockVertexData();
            toReturn.UpVertices = CalculateTopNorthUpVertices(topIndex);
            toReturn.UpIndices = RampBlockIndices.TopNorth.UpIndices;
            toReturn.DownVertices = CalculateTopNorthDownVertices(downIndex);
            toReturn.DownIndices = RampBlockIndices.TopNorth.DownIndices;
            toReturn.NorthVertices = CalculateTopNorthNorthVertices(northIndex);
            toReturn.NorthIndices = RampBlockIndices.TopNorth.SouthIndices;
            toReturn.EastVertices = CalculateTopNorthEastVertices(eastIndex);
            toReturn.EastIndices = RampBlockIndices.TopNorth.EastIndices;
            toReturn.SouthVertices = CalculateTopNorthSouthVertices(southIndex);
            toReturn.SouthIndices = RampBlockIndices.TopNorth.NorthIndices;
            toReturn.WestVertices = CalculateTopNorthWestVertices(westIndex);
            toReturn.WestIndices = RampBlockIndices.TopNorth.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

        static VertexPositionNormalTexture[] CalculateTopNorthUpVertices(int topFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateTopNorthDownVertices(int bottomFaceIndex)
        {
            VertexPositionNormalTexture[] bottomVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int bottomRow = (int)Math.Floor((double)bottomFaceIndex / 64);
            int bottomColumn = bottomFaceIndex % 64;

            float bottomRowStartX = (bottomColumn * textureOffset) + bleedingFix;
            float bottomRowEndX = ((bottomColumn + 1) * textureOffset) - bleedingFix;
            float bottomRowStartY = (bottomRow * textureOffset) + bleedingFix;
            float bottomRowEndY = ((bottomRow + 1) * textureOffset) - bleedingFix;

            bottomVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowStartY));
            bottomVertices[1] = new VertexPositionNormalTexture(BlockVertices.UpTopRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowStartY));
            bottomVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowEndY));
            bottomVertices[3] = new VertexPositionNormalTexture(BlockVertices.DownBottomRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowEndY));

            return bottomVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopNorthNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[0];
            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopNorthEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[3];
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

            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopNorthSouthVertices(int southFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateTopNorthWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[3];
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

            return westVertices;
        }

        static BlockVertexData GetTopEastRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            BlockVertexData toReturn = new BlockVertexData();
            toReturn.UpVertices = CalculateTopEastUpVertices(topIndex);
            toReturn.UpIndices = RampBlockIndices.TopEast.UpIndices;
            toReturn.DownVertices = CalculateTopEastDownVertices(downIndex);
            toReturn.DownIndices = RampBlockIndices.TopEast.DownIndices;
            toReturn.NorthVertices = CalculateTopEastNorthVertices(northIndex);
            toReturn.NorthIndices = RampBlockIndices.TopEast.NorthIndices;
            toReturn.EastVertices = CalculateTopEastEastVertices(eastIndex);
            toReturn.EastIndices = RampBlockIndices.TopEast.EastIndices;
            toReturn.SouthVertices = CalculateTopEastSouthVertices(southIndex);
            toReturn.SouthIndices = RampBlockIndices.TopEast.SouthIndices;
            toReturn.WestVertices = CalculateTopEastWestVertices(westIndex);
            toReturn.WestIndices = RampBlockIndices.TopEast.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

        static VertexPositionNormalTexture[] CalculateTopEastUpVertices(int topFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateTopEastDownVertices(int bottomFaceIndex)
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
            bottomVertices[1] = new VertexPositionNormalTexture(BlockVertices.UpTopRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowStartY));
            bottomVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowEndY));
            bottomVertices[3] = new VertexPositionNormalTexture(BlockVertices.UpBottomRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowEndY));

            return bottomVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopEastNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[3];
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

            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopEastEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[0];
            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopEastSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[3];
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

            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopEastWestVertices(int westFaceIndex)
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

        static BlockVertexData GetTopSouthRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            BlockVertexData toReturn = new BlockVertexData();
            toReturn.UpVertices = CalculateTopSouthUpVertices(topIndex);
            toReturn.UpIndices = RampBlockIndices.TopSouth.UpIndices;
            toReturn.DownVertices = CalculateTopSouthDownVertices(downIndex);
            toReturn.DownIndices = RampBlockIndices.TopSouth.DownIndices;
            toReturn.NorthVertices = CalculateTopSouthNorthVertices(northIndex);
            toReturn.NorthIndices = RampBlockIndices.TopSouth.NorthIndices;
            toReturn.EastVertices = CalculateTopSouthEastVertices(eastIndex);
            toReturn.EastIndices = RampBlockIndices.TopSouth.EastIndices;
            toReturn.SouthVertices = CalculateTopSouthSouthVertices(southIndex);
            toReturn.SouthIndices = RampBlockIndices.TopSouth.SouthIndices;
            toReturn.WestVertices = CalculateTopSouthWestVertices(westIndex);
            toReturn.WestIndices = RampBlockIndices.TopSouth.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

        static VertexPositionNormalTexture[] CalculateTopSouthUpVertices(int topFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateTopSouthDownVertices(int bottomFaceIndex)
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
            bottomVertices[2] = new VertexPositionNormalTexture(BlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowEndY));
            bottomVertices[3] = new VertexPositionNormalTexture(BlockVertices.UpBottomRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowEndY));

            return bottomVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopSouthNorthVertices(int northFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateTopSouthEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int eastRow = (int)Math.Floor((double)eastFaceIndex / 64);
            int eastColumn = eastFaceIndex % 64;

            float eastRowStartX = (eastColumn * textureOffset) + bleedingFix;
            float eastRowEndX = ((eastColumn + 1) * textureOffset) - bleedingFix;
            float eastRowStartY = (eastRow * textureOffset) + bleedingFix;
            float eastRowEndY = ((eastRow + 1) * textureOffset) - bleedingFix;

            eastVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowStartY));
            eastVertices[1] = new VertexPositionNormalTexture(BlockVertices.UpBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowStartY));
            eastVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowEndY));



            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopSouthSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[0];
            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopSouthWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int westRow = (int)Math.Floor((double)westFaceIndex / 64);
            int westColumn = westFaceIndex % 64;

            float westRowStartX = (westColumn * textureOffset) + bleedingFix;
            float westRowEndX = ((westColumn + 1) * textureOffset) - bleedingFix;
            float westRowStartY = (westRow * textureOffset) + bleedingFix;
            float westRowEndY = ((westRow + 1) * textureOffset) - bleedingFix;

            westVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowStartY));
            westVertices[1] = new VertexPositionNormalTexture(BlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowStartY));

            westVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowEndY));



            return westVertices;
        }

        static VertexPositionNormalTexture[] CalculateTopWestUpVertices(int topFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateTopWestDownVertices(int bottomFaceIndex)
        {
            VertexPositionNormalTexture[] bottomVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int bottomRow = (int)Math.Floor((double)bottomFaceIndex / 64);
            int bottomColumn = bottomFaceIndex % 64;

            float bottomRowStartX = (bottomColumn * textureOffset) + bleedingFix;
            float bottomRowEndX = ((bottomColumn + 1) * textureOffset) - bleedingFix;
            float bottomRowStartY = (bottomRow * textureOffset) + bleedingFix;
            float bottomRowEndY = ((bottomRow + 1) * textureOffset) - bleedingFix;

            bottomVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowStartY));
            bottomVertices[1] = new VertexPositionNormalTexture(BlockVertices.DownTopRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowStartY));
            bottomVertices[2] = new VertexPositionNormalTexture(BlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowEndY));
            bottomVertices[3] = new VertexPositionNormalTexture(BlockVertices.DownBottomRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowEndY));

            return bottomVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopWestNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int northRow = (int)Math.Floor((double)northFaceIndex / 64);
            int northColumn = northFaceIndex % 64;

            float northRowStartX = (northColumn * textureOffset) + bleedingFix;
            float northRowEndX = ((northColumn + 1) * textureOffset) - bleedingFix;
            float northRowStartY = (northRow * textureOffset) + bleedingFix;
            float northRowEndY = ((northRow + 1) * textureOffset) - bleedingFix;

            northVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowStartY));
            northVertices[1] = new VertexPositionNormalTexture(BlockVertices.UpTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowStartY));
            northVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowEndY));

            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopWestEastVertices(int eastFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateTopWestSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int southRow = (int)Math.Floor((double)southFaceIndex / 64);
            int southColumn = southFaceIndex % 64;

            float southRowStartX = (southColumn * textureOffset) + bleedingFix;
            float southRowEndX = ((southColumn + 1) * textureOffset) - bleedingFix;
            float southRowStartY = (southRow * textureOffset) + bleedingFix;
            float southRowEndY = ((southRow + 1) * textureOffset) - bleedingFix;

            southVertices[0] = new VertexPositionNormalTexture(BlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowStartY));
            southVertices[1] = new VertexPositionNormalTexture(BlockVertices.UpBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowStartY));

            southVertices[2] = new VertexPositionNormalTexture(BlockVertices.DownBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowEndY));

            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopWestWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[0];

            return westVertices;
        }

        static BlockVertexData GetTopWestRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            BlockVertexData toReturn = new BlockVertexData();
            toReturn.UpVertices = CalculateTopWestUpVertices(topIndex);
            toReturn.UpIndices = RampBlockIndices.TopWest.UpIndices;
            toReturn.DownVertices = CalculateTopWestDownVertices(downIndex);
            toReturn.DownIndices = RampBlockIndices.TopWest.DownIndices;
            toReturn.NorthVertices = CalculateTopWestNorthVertices(northIndex);
            toReturn.NorthIndices = RampBlockIndices.TopWest.NorthIndices;
            toReturn.EastVertices = CalculateTopWestEastVertices(eastIndex);
            toReturn.EastIndices = RampBlockIndices.TopWest.EastIndices;
            toReturn.SouthVertices = CalculateTopWestSouthVertices(southIndex);
            toReturn.SouthIndices = RampBlockIndices.TopWest.SouthIndices;
            toReturn.WestVertices = CalculateTopWestWestVertices(westIndex);
            toReturn.WestIndices = RampBlockIndices.TopWest.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

    }
}
