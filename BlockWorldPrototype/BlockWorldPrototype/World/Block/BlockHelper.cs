// -----------------------------------------------------------------------
// <copyright file="BlockHelper.cs" company="Microsoft">
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
    public static class BlockHelper
    {
        public static class BlockMasks
        {
            public static BlockMask Empty = BlockMask.Empty;
            public static BlockMask Null = BlockMask.IsObstacle;
            public static BlockMask Air = BlockMask.Type1;
            public static BlockMask AirBlocked = BlockMask.Type1 | BlockMask.IsObstacle;
            public static BlockMask Debug = BlockMask.IsObstacle | BlockMask.Type1 | BlockMask.Data1;
            public static BlockMask Soil = BlockMask.IsObstacle | BlockMask.Type1 | BlockMask.Data2;
            public static BlockMask Stone = BlockMask.IsObstacle | BlockMask.Type1 | BlockMask.Data1 | BlockMask.Data2;
        }

        public static class RampBlockMasks
        {
            public static class Top
            {
                public static BlockMask Air = BlockMask.Type2;
                public static BlockMask Debug = BlockMask.IsObstacle | BlockMask.Type2 | BlockMask.Data7;
                public static BlockMask Soil = BlockMask.IsObstacle | BlockMask.Type2 | BlockMask.Data8;
            }

            public static class Bottom
            {
                public static BlockMask Air = BlockMask.Type2;
                public static BlockMask Debug = BlockMask.IsObstacle | BlockMask.Type2 | BlockMask.Data3;
                public static BlockMask Soil = BlockMask.IsObstacle | BlockMask.Type2 | BlockMask.Data4;
            }
        }

        public static bool IsBlock(BlockMask blockMask)
        {
            return blockMask.HasFlag(BlockMask.Type1 & ~BlockMask.Type2);
        }

        public static bool IsRampBlock(BlockMask blockMask)
        {
            return blockMask.HasFlag(BlockMask.Type2 & ~BlockMask.Type1);
        }

        public static bool HasBottomRamp(BlockMask blockMask)
        {
            return (blockMask.HasFlag(BlockMask.Data3) || blockMask.HasFlag(BlockMask.Data4) ||
                    blockMask.HasFlag(BlockMask.Data5) || blockMask.HasFlag(BlockMask.Data6));
        }

        public static bool HasTopRamp(BlockMask blockMask)
        {
            return (blockMask.HasFlag(BlockMask.Data7) || blockMask.HasFlag(BlockMask.Data8) || blockMask.HasFlag(BlockMask.Data9) ||
                     blockMask.HasFlag(BlockMask.Data10));
        }

        public static bool HasBothRamps(BlockMask blockMask)
        {
            bool toReturn = HasBottomRamp(blockMask);
            return toReturn && HasTopRamp(blockMask);
        }

        public static int GetBlockID(BlockMask blockMask)
        {
            int id = (int)(blockMask & ~BlockMask.IsObstacle & ~BlockMask.Type1 & ~BlockMask.Type2) >> 3;

            return id;
        }

        public static int GetBottomRampID(BlockMask blockMask)
        {
            int id = (int)(blockMask & ~BlockMask.IsObstacle & ~BlockMask.Type1 & ~BlockMask.Type2 & ~BlockMask.Data1 & ~BlockMask.Data2 & ~BlockMask.Data7 & ~BlockMask.Data8 & ~BlockMask.Data9 & ~BlockMask.Data10) >> 5;

            return id;
        }

        public static int GetTopRampID(BlockMask blockMask)
        {
            int id = (int)(blockMask & ~BlockMask.IsObstacle & ~BlockMask.Type1 & ~BlockMask.Type2 & ~BlockMask.Data1 & ~BlockMask.Data2 & ~BlockMask.Data3 & ~BlockMask.Data4 & ~BlockMask.Data5 & ~BlockMask.Data6) >> 9;

            return id;
        }

        public static BlockMask GetRampBlockDirection(BlockMask blockMask)
        {
            if (blockMask.HasFlag(RampBlockDirection.West))
                return RampBlockDirection.West;
            if (blockMask.HasFlag(RampBlockDirection.East))
                return RampBlockDirection.East;
            if (blockMask.HasFlag(RampBlockDirection.South))
                return RampBlockDirection.South;

            return RampBlockDirection.North;
        }

        public static BlockMask RotateRampTo(BlockMask blockMask, BlockMask direction)
        {
            // unset direction
            blockMask = blockMask | ~BlockMask.Data1 | ~BlockMask.Data2;
            // set new direciton
            blockMask = blockMask | direction;

            return blockMask;
        }

        public static bool DoesBlockMaskAObscureMaskBFace(BlockMask blockMaskA, BlockMask blockMaskB,
            Vector3 fromDirection)
        {

            // Air and null blocks are see through
            if (blockMaskA == BlockMasks.AirBlocked || blockMaskA == BlockMasks.Air || blockMaskA == BlockMasks.Null)
                return false;

            // Blocks always obscure.
            if (IsBlock(blockMaskA))
                return true;

            // A top+bottom ramp is the same as a block
            if (HasTopRamp(blockMaskA) && HasBottomRamp(blockMaskA))
                return true;

            // bottom ramps block the opposite direction
            if (HasBottomRamp(blockMaskA))
            {
                if (GetRampBlockDirection(blockMaskA) == RampBlockDirection.North && fromDirection == WorldDirection.South)
                    return true;
                if (GetRampBlockDirection(blockMaskA) == RampBlockDirection.East && fromDirection == WorldDirection.West)
                    return true;
                if (GetRampBlockDirection(blockMaskA) == RampBlockDirection.South && fromDirection == WorldDirection.North)
                    return true;
                if (GetRampBlockDirection(blockMaskA) == RampBlockDirection.West && fromDirection == WorldDirection.East)
                    return true;
            }

            if (HasTopRamp(blockMaskA))
            {

                // top ramps block the same direction
                if (GetRampBlockDirection(blockMaskA) == RampBlockDirection.North && fromDirection == WorldDirection.North)
                    return true;
                if (GetRampBlockDirection(blockMaskA) == RampBlockDirection.East && fromDirection == WorldDirection.East)
                    return true;
                if (GetRampBlockDirection(blockMaskA) == RampBlockDirection.South && fromDirection == WorldDirection.South)
                    return true;
                if (GetRampBlockDirection(blockMaskA) == RampBlockDirection.West && fromDirection == WorldDirection.West)
                    return true;
            }


            // top ramps block matching bottom ramps from Sides
            if (HasTopRamp(blockMaskA) && HasTopRamp(blockMaskB))
            {
                if (GetRampBlockDirection(blockMaskA) == GetRampBlockDirection(blockMaskB))
                {
                    if (GetRampBlockDirection(blockMaskA) == RampBlockDirection.North || GetRampBlockDirection(blockMaskA) == RampBlockDirection.South)
                    {
                        if (fromDirection == WorldDirection.East || fromDirection == WorldDirection.West)
                        {
                            return true;
                        }
                        return false;
                    }
                    if (GetRampBlockDirection(blockMaskA) == RampBlockDirection.East || GetRampBlockDirection(blockMaskA) == RampBlockDirection.West)
                    {
                        if (fromDirection == WorldDirection.North || fromDirection == WorldDirection.South)
                        {
                            return true;
                        }
                        return false;
                    }
                }

                return false;
            }

            // bottom ramps block matching bottom ramps from Sides
            if (HasBottomRamp(blockMaskA) && HasBottomRamp(blockMaskB))
            {
                if (GetRampBlockDirection(blockMaskA) == GetRampBlockDirection(blockMaskB))
                {
                    if (GetRampBlockDirection(blockMaskA) == RampBlockDirection.North || GetRampBlockDirection(blockMaskA) == RampBlockDirection.South)
                    {
                        if (fromDirection == WorldDirection.East || fromDirection == WorldDirection.West)
                        {
                            return true;
                        }
                        return false;
                    }
                    if (GetRampBlockDirection(blockMaskA) == RampBlockDirection.East || GetRampBlockDirection(blockMaskA) == RampBlockDirection.West)
                    {
                        if (fromDirection == WorldDirection.North || fromDirection == WorldDirection.South)
                        {
                            return true;
                        }
                        return false;
                    }
                }

                return false;
            }

            // top ramps block up
            if (HasBottomRamp(blockMaskA))
            {
                return (fromDirection == WorldDirection.Up);
            }

            // bottom ramps block down
            if (HasTopRamp(blockMaskA))
            {
                return (fromDirection == WorldDirection.Down);
            }

            return false;
        }
        public static bool DoesBlockMaskObscureFromDirection(BlockMask blockMask, Vector3 fromDirection)
        {
            if (blockMask == BlockMasks.Air || blockMask == BlockMasks.AirBlocked)
            {
                return false;
            }
            if (IsBlock(blockMask))
            {
                return true;
            }
            if (IsRampBlock(blockMask))
            {
                if (HasBottomRamp(blockMask) && HasTopRamp(blockMask))
                {
                    return true;
                }
                else if (HasBottomRamp(blockMask))
                {
                    if (fromDirection == WorldDirection.Down)
                    {
                        return true;
                    }
                    switch (GetRampBlockDirection(blockMask))
                    {
                        case RampBlockDirection.North:
                            if (fromDirection == WorldDirection.North)
                                return true;
                            return false;
                        case RampBlockDirection.East:
                            if (fromDirection == WorldDirection.East)
                                return true;
                            return false;
                        case RampBlockDirection.South:
                            if (fromDirection == WorldDirection.South)
                                return true;
                            return false;
                        case RampBlockDirection.West:
                            if (fromDirection == WorldDirection.West)
                                return true;
                            return false;
                    }
                }
                else if (HasTopRamp(blockMask))
                {
                    if (fromDirection == WorldDirection.Up)
                    {
                        return true;
                    }
                    switch (GetRampBlockDirection(blockMask))
                    {
                        case RampBlockDirection.North:
                            if (fromDirection == WorldDirection.South)
                                return true;
                            return false;
                        case RampBlockDirection.East:
                            if (fromDirection == WorldDirection.West)
                                return true;
                            return false;
                        case RampBlockDirection.South:
                            if (fromDirection == WorldDirection.North)
                                return true;
                            return false;
                        case RampBlockDirection.West:
                            if (fromDirection == WorldDirection.East)
                                return true;
                            return false;
                    }
                }
            }
            return false;
        }

        public static bool IsBlockAnObstacle(BlockMask blockMask)
        {
            return blockMask.HasFlag(BlockMask.IsObstacle);
        }
    }
}
