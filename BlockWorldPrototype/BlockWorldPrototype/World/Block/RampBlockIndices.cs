// -----------------------------------------------------------------------
// <copyright file="RampBlockIndices.cs" company="Microsoft">
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
    public class RampBlockIndices
    {
        public class DownNorth
        {
            public static ushort[] UpIndices =
            {
                0,
                2,
                1,
                1,
                2,
                3
            };

            public static ushort[] DownIndices =
            {
                0,
                1,
                2,
                2,
                1,
                3
            };

            public static ushort[] SouthIndices =
            {
            };

            public static ushort[] NorthIndices =
            {
                1,
                3,
                2,
                1,
                2,
                0
            };

            public static ushort[] EastIndices =
            {
                0,
                1,
                2,
            };

            public static ushort[] WestIndices =
            {
                1,
                0,
                2,
            };
        }

        public class TopNorth
        {
            public static ushort[] UpIndices =
            {
                0,
                2,
                1,
                1,
                2,
                3
            };

            public static ushort[] DownIndices =
            {
                0,
                1,
                2,
                2,
                1,
                3
            };

            public static ushort[] SouthIndices =
            {
            };

            public static ushort[] NorthIndices =
            {
                2,
                3,
                1,
                0,
                2,
                1
            };

            public static ushort[] EastIndices =
            {
                0,
                1,
                2,
            };

            public static ushort[] WestIndices =
            {
                1,
                0,
                2,
            };
        }

        public class TopSouth
        {
            public static ushort[] UpIndices =
            {
                0,
                2,
                1,
                1,
                2,
                3
            };

            public static ushort[] DownIndices =
            {
                0,
                1,
                2,
                2,
                1,
                3
            };

            public static ushort[] SouthIndices =
            {
            };

            public static ushort[] NorthIndices =
            {
                1,
                3,
                2,
                1,
                2,
                0
            };

            public static ushort[] EastIndices =
            {
                0,
                1,
                2,
            };

            public static ushort[] WestIndices =
            {
                1,
                0,
                2,
            };
        }

        public class TopEast
        {
            public static ushort[] UpIndices =
            {
                0,
                2,
                1,
                1,
                2,
                3
            };

            public static ushort[] DownIndices =
            {
                0,
                1,
                2,
                2,
                1,
                3
            };

            public static ushort[] SouthIndices =
            {
                2,
                1,
                0
            };

            public static ushort[] NorthIndices =
            {
                1,
                2,
                0
            };

            public static ushort[] EastIndices =
            {
            };

            public static ushort[] WestIndices =
            {
                1,
                0,
                2,
                2,
                0,
                3
            };
        }

        public class DownEast
        {
            public static ushort[] UpIndices =
            {
                0,
                2,
                1,
                1,
                2,
                3
            };

            public static ushort[] DownIndices =
            {
                0,
                1,
                2,
                2,
                1,
                3
            };

            public static ushort[] SouthIndices =
            {
                0,
                1,
                2
            };

            public static ushort[] NorthIndices =
            {
                0,
                2,
                1
            };

            public static ushort[] EastIndices =
            {
                0,
                1,
                2,
                2,
                3,
                0
            };

            public static ushort[] WestIndices =
            {
            };
        }

        public class DownSouth
        {
            public static ushort[] UpIndices =
            {
                0,
                2,
                1,
                1,
                2,
                3
            };

            public static ushort[] DownIndices =
            {
                0,
                1,
                2,
                2,
                1,
                3
            };

            public static ushort[] SouthIndices =
            {
                3,
                1,
                2,
                2,
                1,
                0
            };

            public static ushort[] NorthIndices =
            {
            };

            public static ushort[] EastIndices =
            {
                0,
                1,
                2
            };

            public static ushort[] WestIndices =
            {
                1,
                0,
                2
            };
        }

        public class TopWest
        {
            public static ushort[] UpIndices =
            {
                0,
                2,
                1,
                1,
                2,
                3
            };

            public static ushort[] DownIndices =
            {
                0,
                1,
                2,
                2,
                1,
                3
            };

            public static ushort[] SouthIndices =
            {
                2,
                1,
                0
            };

            public static ushort[] NorthIndices = 
                {
                    1,
                    2,
                    0
                };

            public static ushort[] EastIndices =
                {
                    0,
                    1,
                    2,
                    2,
                    3,
                    0  
                };

            public static ushort[] WestIndices =
                {
                };
        }

        public class DownWest
        {
            public static ushort[] UpIndices =
            {
                0,
                2,
                1,
                1,
                2,
                3
            };

            public static ushort[] DownIndices =
            {
                2,
                1,
                0,
                3,
                1,
                2
            };

            public static ushort[] SouthIndices =
            {
                0,
                1,
                2
            };

            public static ushort[] NorthIndices =
            {
                0,
                2,
                1
            };

            public static ushort[] EastIndices =
            {
            };

            public static ushort[] WestIndices =
            {
                1,
                0,
                2,
                2,
                0,
                3
            };
        }
    }
}
