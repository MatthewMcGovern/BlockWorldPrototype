// -----------------------------------------------------------------------
// <copyright file="BaseJob.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BlockWorldPrototype.World.Job
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaseJob
    {
        public bool Claimed;
        public int JID;

        public BaseJob()
        {
            Claimed = false;
            JID = WorldGlobal.GetNextJobID();
        }
    }
}
