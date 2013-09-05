// -----------------------------------------------------------------------
// <copyright file="JobManager.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.Remoting.Messaging;

namespace BlockWorldPrototype.World.Job
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JobManager
    {
        public List<BaseJob> _jobs;

        public JobManager ()
        {
            _jobs = new List<BaseJob>();
        }

        public void AddJob(BaseJob job)
        {
            _jobs.Add(job);
        }

        public BaseJob GetNextJob()
        {
            _jobs[0].Claimed = true;
            return _jobs[0];

        }
    }
}
