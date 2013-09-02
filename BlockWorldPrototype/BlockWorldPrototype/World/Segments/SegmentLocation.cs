// -----------------------------------------------------------------------
// <copyright file="SegmentLocation.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;

namespace BlockWorldPrototype.World.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SegmentLocation
    {
        public Vector3 WorldLocation;
        public int SegmentX;
        public int SegmentZ;
        public int BlockX;
        public int BlockY;
        public int BlockZ;
        public int RenderSegmentIndex;
        public int RenderSegmentBlockMaskIndex;

        public SegmentLocation(Vector3 worldLocation)
        {
            WorldLocation = worldLocation;
            SegmentX = (int)Math.Floor((double)worldLocation.X / WorldGlobal.SegmentSize.X);
            SegmentZ = (int)Math.Floor((double)worldLocation.Z / WorldGlobal.SegmentSize.Z);
            BlockX = (int)worldLocation.X % WorldGlobal.SegmentSize.X;
            BlockY = (int)worldLocation.Y % WorldGlobal.SegmentSize.Y;
            BlockZ = (int)worldLocation.Z % WorldGlobal.SegmentSize.Z;

            RenderSegmentIndex = (int)Math.Floor((double)(worldLocation.Y / WorldGlobal.RenderSegmentSize.Y));
            RenderSegmentBlockMaskIndex = (int)worldLocation.Y % WorldGlobal.RenderSegmentSize.Y;
        }

        public SegmentLocation TranslateAndClone(Vector3 distance)
        {
            return new SegmentLocation(WorldLocation + distance);
        }
    }
}
