// -----------------------------------------------------------------------
// <copyright file="Harvestable.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.Core.Debug;

namespace BlockWorldPrototype.World.Entity.Behaviour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Harvestable : BaseBehaviour
    {
        public Harvestable()
        {
            
        }

        public void Harvest(BaseEntity baseEntity)
        {
            if (HasActions())
            {
                _actions.ApplyAll(baseEntity);
            }
            else
            {
                DebugLog.Log("Tried to harvest non-harvestable item: " + baseEntity.Schematic.Name + " (ID: " + baseEntity.ID+")", DebugMessageType.Error);
            }
        }
    }
}
