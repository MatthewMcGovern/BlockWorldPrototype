// -----------------------------------------------------------------------
// <copyright file="BlockMask.cs" company="Microsoft">
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
    [Flags]
    public enum BlockMask
    {
        Empty = 0,
        IsObstacle = 1,
        Type1 = 2,
        Type2 = 4,
        Data1 = 8,
        Data2 = 16,
        Data3 = 32,
        Data4 = 64,
        Data5 = 128,
        Data6 = 256,
        Data7 = 512,
        Data8 = 1024,
        Data9 = 2048,
        Data10 = 4096,
    }
}
