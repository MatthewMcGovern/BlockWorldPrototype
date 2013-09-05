// -----------------------------------------------------------------------
// <copyright file="EntityJob.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.World.Entity;

namespace BlockWorldPrototype.World.Job
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EntityJob : BaseJob
    {
        public EntitySchematic SchematicRequired;
        public int EntityID;

        public EntityJob(EntitySchematic required) : base()
        {
            SchematicRequired = required;
            EntityID = -1;
        }
    }
}
