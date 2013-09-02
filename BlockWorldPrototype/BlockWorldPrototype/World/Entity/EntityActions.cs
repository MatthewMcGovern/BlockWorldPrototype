// -----------------------------------------------------------------------
// <copyright file="EntityActions.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BlockWorldPrototype.World.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EntityActions
    {
        public List<Action<BaseEntity>> _actions;

        public EntityActions()
        {
            _actions = new List<Action<BaseEntity>>();    
        }

        public void AddAction(Action<BaseEntity> action)
        {
            _actions.Add(action);
        }

        public void ApplyAll(BaseEntity baseEntity)
        {
            foreach (Action<BaseEntity> action in _actions)
            {
                action(baseEntity);
            }
        }

        public bool HasActions()
        {
            return _actions.Count >= 1;
        }
    }
}
