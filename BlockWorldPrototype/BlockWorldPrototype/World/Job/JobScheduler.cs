// -----------------------------------------------------------------------
// <copyright file="JobScheduler.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;

namespace BlockWorldPrototype.World.Job
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum JobPriority
    {
        Low = 1,
        Medium = 2,
        High = 4,
        Critical = 8
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JobScheduler
    {
        private Dictionary<int, BaseJob> _allJobs;

        private List<ScheduledJob> _scheduler;

        public JobScheduler()
        {
            _allJobs = new Dictionary<int, BaseJob>();
            _scheduler = new List<ScheduledJob>();
        }

        public void AddJob(BaseJob newJob, JobPriority priority)
        {
            _allJobs.Add(newJob.JID, newJob);
            _scheduler.Add(new ScheduledJob(newJob.JID, priority));
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _scheduler.Count; i++)
            {
                ScheduledJob currentJob = _scheduler[i];
                currentJob.Age += gameTime.ElapsedGameTime.Milliseconds;
                currentJob.Importance = currentJob.Age * (float)currentJob.Priority;

                int newIndex = i;

                for (int j = i - 1; j >= 0; j--)
                {
                    ScheduledJob toCompare = _scheduler[j];
                    if (currentJob.Importance > toCompare.Importance)
                    {
                        newIndex = j;
                    }
                    else
                    {
                        break;
                    }
                }

                _scheduler.RemoveAt(i);
                _scheduler.Insert(newIndex, currentJob);
            }
        }


    }
}
