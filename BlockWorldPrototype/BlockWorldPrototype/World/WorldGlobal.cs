// -----------------------------------------------------------------------
// <copyright file="WorldGlobal.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BlockWorldPrototype.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class WorldGlobal
    {
        public static class ItemIDs
        {
            public static int Tree = 0;
        }

        public static class WorldSegmentsSize
        {
            public static int X = 10;
            public static int Z = 10;
        }

        public static class SegmentSize
        {
            public static int X = 32;
            public static int Y = 64;
            public static int Z = 32;
        }

        public static class RenderSegmentSize
        {
            public static int X = SegmentSize.X;
            public static int Y = 4;
            public static int Z = SegmentSize.Z;
        }

        public static int[] RenderSegmentIndices;
        public static int[] RenderBlockMaskIndices;

        public static int _uniqueEntityID;
        public static int _uniqueJobID;

        public static void Init()
        {
            _uniqueEntityID = 0;
            _uniqueEntityID = 0;
            RenderSegmentIndices = new int[SegmentSize.Y];
            RenderBlockMaskIndices = new int[SegmentSize.Y];

            for (int i = 0; i < SegmentSize.Y; i++)
            {
                RenderSegmentIndices[i] = (int)Math.Floor((double)(i/RenderSegmentSize.Y));
            }

            for (int y = 0; y < SegmentSize.Y; y++)
            {
                RenderBlockMaskIndices[y] = y%RenderSegmentSize.Y;
            }
        }

        public static int NoOfRenderSegments = (int)Math.Ceiling((double)SegmentSize.Y/RenderSegmentSize.Y);

        public static int GetNextEntityID()
        {
            return _uniqueEntityID++;
        }

        public static int GetNextJobID()
        {
            return _uniqueJobID++;
        }
    }
}
