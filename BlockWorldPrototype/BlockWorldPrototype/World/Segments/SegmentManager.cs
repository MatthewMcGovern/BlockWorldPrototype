// -----------------------------------------------------------------------
// <copyright file="SegmentManager.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.Core;
using BlockWorldPrototype.Core.Graphics;
using BlockWorldPrototype.World.Block;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BlockWorldPrototype.World.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SegmentManager
    {
        public Segment[,] Segments;
        private Segment _nullSegment;
        public Vector2 VectorSegment;
        public Vector3 ActiveSegment;
        public GraphicsDevice Device;

        private Random _rand;

        public SegmentManager(GraphicsDevice device)
        {
            _rand = new Random();
            Device = device;
            VectorSegment = Vector2.Zero;
            ActiveSegment = Vector3.Zero;

            Segments = new Segment[WorldGlobal.WorldSegmentsSize.X, WorldGlobal.WorldSegmentsSize.Z];

            for (int x = 0; x < Segments.GetLength(0); x++)
            {
                for (int z = 0; z < Segments.GetLength(1); z++)
                {
                    Segments[x, z] = new Segment(Device, this, new Vector2(x, z));
                }
            }

            _nullSegment = new Segment(device, this, new Vector2(-1, -1));
            _nullSegment.SetNull();
        }

        public void LoadContent(ContentManager content, Effect effect)
        {
            //_BlockHighlight = new ImRenderBasic(content.Load<Model>("Models/HighLight"), effect);
            //_BlockHighlightBlocked = new ImRenderBasic(content.Load<Model>("Models/HighLight_Blocked"), effect);
        }

        // GETS
        public Segment GetSegmentAt(SegmentLocation segmentLocation)
        {
            if (IsLocationInRange(segmentLocation))
                return Segments[segmentLocation.SegmentX, segmentLocation.SegmentZ];

            return _nullSegment;
        }

        public SegmentRender GetRenderSegmentAt(SegmentLocation segmentLocation)
        {
            return GetSegmentAt(segmentLocation).RenderSegments[segmentLocation.RenderSegmentIndex];
        }

        public BlockMask GetBlockMaskAt(SegmentLocation segmentLocation)
        {
            if (IsLocationInRange(segmentLocation))
                return
                    GetRenderSegmentAt(segmentLocation).Blocks[
                        segmentLocation.BlockX, segmentLocation.RenderSegmentBlockMaskIndex, segmentLocation.BlockZ];

            return BlockHelper.BlockMasks.Null;
        }

        // SETS
        public void ClearItemsObstacleFlag(SegmentLocation segmentLocation)
        {
            GetRenderSegmentAt(segmentLocation).Blocks[
                segmentLocation.BlockX, segmentLocation.RenderSegmentBlockMaskIndex, segmentLocation.BlockZ] =
                BlockHelper.BlockMasks.Air;

        }

        public void SetBlockMaskAt(SegmentLocation segmentLocation, BlockMask blockMask)
        {
            if (IsLocationInRange(segmentLocation))
            {
                // can't place block on obstructed air as its an item!
                if (GetBlockMaskAt(segmentLocation) == BlockHelper.BlockMasks.AirBlocked)
                    return;

                // can't delete block with item above it!
                if (blockMask == BlockHelper.BlockMasks.Air)
                    if (GetBlockMaskAt(segmentLocation.TranslateAndClone(new Vector3(0, 1, 0))) ==
                        BlockHelper.BlockMasks.AirBlocked)
                        return;

                GetRenderSegmentAt(segmentLocation).Blocks[
                    segmentLocation.BlockX, segmentLocation.RenderSegmentBlockMaskIndex, segmentLocation.BlockZ] =
                    blockMask;

                // Mark all around it as dirty.
                SetLocationDirty(segmentLocation);
                SetLocationDirty(segmentLocation.TranslateAndClone(WorldDirection.Up));
                SetLocationDirty(segmentLocation.TranslateAndClone(WorldDirection.Down));
                SetLocationDirty(segmentLocation.TranslateAndClone(WorldDirection.North));
                SetLocationDirty(segmentLocation.TranslateAndClone(WorldDirection.East));
                SetLocationDirty(segmentLocation.TranslateAndClone(WorldDirection.South));
                SetLocationDirty(segmentLocation.TranslateAndClone(WorldDirection.West));
            }
        }

        public void SetFlagAt(SegmentLocation segmentLocation, BlockMask flag)
        {
            if (IsLocationInRange(segmentLocation))
                SetBlockMaskAt(segmentLocation, GetBlockMaskAt(segmentLocation) | flag);
        }

        public void RemoveFlagAt(SegmentLocation segmentLocation, BlockMask flag)
        {
            if (IsLocationInRange(segmentLocation))
                SetBlockMaskAt(segmentLocation, GetBlockMaskAt(segmentLocation) & ~flag);
        }

        public void SetLocationDirty(SegmentLocation segmentLocation)
        {
            if (IsLocationInRange(segmentLocation))
                GetRenderSegmentAt(segmentLocation).Dirty = true;
        }

        public void SetLocationObstructed(SegmentLocation segmentLocation)
        {
            if (IsLocationInRange(segmentLocation))
                SetFlagAt(segmentLocation, BlockMask.IsObstacle);
        }

        public void SetLocationPassable(SegmentLocation segmentLocation)
        {
            if (IsLocationInRange(segmentLocation))
                RemoveFlagAt(segmentLocation, BlockMask.IsObstacle);
        }

        // QUERIES
        public bool IsLocationBlockedExcludeItems(SegmentLocation segmentLocation)
        {
            if (GetBlockMaskAt(segmentLocation) == BlockHelper.BlockMasks.AirBlocked)
                return false;
            else
                return IsLocationObstructed(segmentLocation);
        }

        public bool IsLocationObstructed(SegmentLocation segmentLocation)
        {
            if (IsLocationInRange(segmentLocation))
                return BlockHelper.IsBlockAnObstacle(GetBlockMaskAt(segmentLocation));

            return true;
        }

        public bool DoesLocationObscureDirection(SegmentLocation segmentLocation, Vector3 direction)
        {
            if (IsLocationInRange(segmentLocation))
                return BlockHelper.DoesBlockMaskObscureFromDirection(GetBlockMaskAt(segmentLocation), direction);

            return false;
        }

        public bool IsLocationInRange(SegmentLocation segmentLocation)
        {
            if (segmentLocation.WorldLocation.X < 0 || segmentLocation.WorldLocation.Y < 0 ||
                segmentLocation.WorldLocation.Z < 0)
                return false;
            if (segmentLocation.WorldLocation.X >= Segments.GetLength(0) * WorldGlobal.RenderSegmentSize.X ||
                segmentLocation.WorldLocation.Z >= Segments.GetLength(1) * WorldGlobal.RenderSegmentSize.Z ||
                segmentLocation.WorldLocation.Y >= WorldGlobal.SegmentSize.Y)
                return false;

            return true;
        }

        public bool IsLocationWalkable(SegmentLocation segmentLocation)
        {
            if (IsLocationInRange(segmentLocation))
            {
                if (IsLocationObstructed(segmentLocation))
                {
                    return false;
                }

                BlockMask blockBelow = GetBlockMaskAt(segmentLocation.TranslateAndClone(WorldDirection.Down));

                if (BlockHelper.IsBlock(blockBelow) || BlockHelper.HasTopRamp(blockBelow))
                {
                    return true;
                }
            }

            return false;
        }

        // UPDATES
        public void Update(GameTime gameTime)
        {
            for (int x = 0; x < Segments.GetLength(0); x++)
            {
                for (int z = 0; z < Segments.GetLength(1); z++)
                {
                    Segments[x, z].Update(gameTime);
                }
            }
        }





        // DRAWING
        public void DrawBlocks()
        {
            for (int x = 0; x < Segments.GetLength(0); x++)
            {
                for (int z = 0; z < Segments.GetLength(1); z++)
                {
                    Segments[x, z].DrawBlocks();
                }
            }
        }
    }
}
