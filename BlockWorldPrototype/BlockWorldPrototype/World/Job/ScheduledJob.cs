// -----------------------------------------------------------------------
// <copyright file="ScheduledJobSegment.cs" company="Microsoft">
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
    public class ScheduledJob
    {
        public int JID;
        public JobPriority Priority;
        public float Age;
        public float Importance;

        public ScheduledJob(int jobID, JobPriority priorty)
        {
            JID = jobID;
            Priority = priorty;
        }
    }
}
