﻿// -----------------------------------------------------------------------
// <copyright file="Behaviour.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.Remoting;

namespace BlockWorldPrototype.World.Entity.Behaviour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaseBehaviour
    {
        protected EntityActions _actions;

        public BaseBehaviour()
        {
            _actions = new EntityActions();
        }

        public void AddAction(Action<BaseEntity> action)
        {
            _actions.AddAction(action);
        }

        public bool HasActions()
        {
            return _actions.HasActions();
        }

        public void Activate(BaseEntity baseEntity)
        {
            _actions.ApplyAll(baseEntity);
        }
    }
}
