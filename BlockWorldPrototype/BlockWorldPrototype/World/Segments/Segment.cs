// -----------------------------------------------------------------------
// <copyright file="Segment.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.Core;
using BlockWorldPrototype.World.Block;
using Microsoft.Xna.Framework;
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
    public class Segment
    {
        public SegmentManager ParentSegmentManager;
        public SegmentRender[] RenderSegments;
        public GraphicsDevice Device;
        public bool IsLoaded;
        public Vector2 Position;

        public Segment(GraphicsDevice device, SegmentManager segmentManager, Vector2 position)
        {
            ParentSegmentManager = segmentManager;
            IsLoaded = false;
            Device = device;
            Position = position;
            LoadSegment();
        }

        public void SetNull()
        {
            foreach (var imRenderSegment in RenderSegments)
            {
                imRenderSegment.Fill(BlockHelper.BlockMasks.Null);
            }
        }

        public void LoadSegment()
        {
            RenderSegments = new SegmentRender[WorldGlobal.NoOfRenderSegments];
            for (int i = 0; i < WorldGlobal.NoOfRenderSegments; i++)
            {
                RenderSegments[i] = new SegmentRender(Device, this, i);
            }

            RenderSegments[0].Fill(BlockHelper.BlockMasks.Soil);
            IsLoaded = true;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < WorldGlobal.NoOfRenderSegments; i++)
            {
                RenderSegments[i].Update(gameTime);
            }
        }
        public void UpdateDrawModules()
        {
            // Get all the vertices required and stick it in a draw module.
            for (int i = 0; i < RenderSegments.Length; i++)
            {
                RenderSegments[i].UpdateDrawModule();
            }
        }

        public void DrawBlocks()
        {
            for (int i = 0; i < RenderSegments.Length; i++)
            {
                RenderSegments[i].DrawBlocks();
            }
        }
    }
}
