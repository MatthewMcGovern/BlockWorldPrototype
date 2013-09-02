// -----------------------------------------------------------------------
// <copyright file="RampBlockDirection.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BlockWorldPrototype.World.Block
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class RampBlockDirection
    {
        public const BlockMask North = BlockMask.Empty;
        public const BlockMask East = BlockMask.Data1;
        public const BlockMask South = BlockMask.Data2;
        public const BlockMask West = BlockMask.Data2 | BlockMask.Data1;
    }
}
