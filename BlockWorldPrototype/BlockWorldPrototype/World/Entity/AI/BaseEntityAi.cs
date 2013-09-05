// -----------------------------------------------------------------------
// <copyright file="BaseEntityAI.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.World.Entity.Property;
using BlockWorldPrototype.World.Job;
using Microsoft.Xna.Framework;

namespace BlockWorldPrototype.World.Entity.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaseEntityAi : BaseEntity
    {
        private PathFinder _pathFinder;
        private float thinkingtime = 1000f;
        private float thoughttime = 0f;
        private List<BaseJob> _assignedJobs;
        private BaseJob _activeJob;

        public BaseEntityAi(GameWorld gameworld, EntitySchematic entitySchem, Vector3 location)
            : base(gameworld, entitySchem, location)
        {
            _assignedJobs = new List<BaseJob>();
            _pathFinder = new PathFinder(gameworld);
        }

        public void Update(GameTime gameTime)
        {
            thoughttime += gameTime.ElapsedGameTime.Milliseconds;

            if (_assignedJobs.Count == 0)
            {
                _assignedJobs.Add(GW.JM.GetNextJob());
                _activeJob = _assignedJobs[0];
            }

            if (_activeJob != null)
            {
                if (_pathFinder.Path.Count == 0)
                {
                    if (_activeJob is EntityJob)
                    {
                        EntityJob activeEntityJob = _activeJob as EntityJob;
                        if (activeEntityJob.EntityID == -1)
                        {
                            activeEntityJob.EntityID = GW.EC.GetClosestEntityByType(Position,
                                activeEntityJob.SchematicRequired).ID;
                        }
                        else
                        {
                            if (GW.EC.GetByID(activeEntityJob.EntityID).Position == Position)
                            {
                                GW.EC.GetByID(activeEntityJob.EntityID).GetProperty<Harvestable>().Harvest(GW.EC.GetByID(activeEntityJob.EntityID));
                                activeEntityJob.EntityID = -1;
                            }
                            else
                            {
                                _pathFinder.GeneratePath(Position, GW.EC.GetByID(activeEntityJob.EntityID).Position);
                            }
                        }
                    }
                }
            }
            

            if (thoughttime > thinkingtime)
            {
                // act!
                thoughttime = thoughttime - thinkingtime;

                if (_pathFinder.Path.Count > 0)
                {
                    Vector3 nextLocation = _pathFinder.Path[0];
                    _pathFinder.Path.RemoveAt(0);
                    if (nextLocation - Position == WorldDirection.North)
                    {
                        MoveNorth();
                    }
                    else if (nextLocation - Position == WorldDirection.East)
                    {
                        MoveEast();
                    }
                    else if (nextLocation - Position == WorldDirection.South)
                    {
                        MoveSouth();
                    }
                    else if (nextLocation - Position == WorldDirection.West)
                    {
                        MoveWest();
                    }
                    else if (nextLocation - Position == WorldDirection.NorthEast)
                    {
                        MoveNorthEast();
                    }
                    else if (nextLocation - Position == WorldDirection.NorthWest)
                    {
                        MoveNorthWest();
                    }
                    else if (nextLocation - Position == WorldDirection.SouthEast)
                    {
                        MoveSouthEast();
                    }
                    else if (nextLocation - Position == WorldDirection.SouthWest)
                    {
                        MoveSouthWest();
                    }
                }
            }
        }

        public void MoveNorth()
        {
            Position += WorldDirection.North;
            Rotation = WorldRotation.North;
        }
        public void MoveEast()
        {
            Position += WorldDirection.East;
            Rotation = WorldRotation.East;
        }
        public void MoveWest()
        {
            Position += WorldDirection.West;
            Rotation = WorldRotation.West;
        }
        public void MoveSouth()
        {
            Position += WorldDirection.South;
            Rotation = WorldRotation.South;
        }
        public void MoveNorthEast()
        {
            Position += WorldDirection.NorthEast;
            Rotation = WorldRotation.NorthEast;
        }
        public void MoveNorthWest()
        {
            Position += WorldDirection.NorthWest;
            Rotation = WorldRotation.NorthWest;
        }
        public void MoveSouthEast()
        {
            Position += WorldDirection.SouthEast;
            Rotation = WorldRotation.SouthEast;
        }
        public void MoveSouthWest()
        {
            Position += WorldDirection.SouthWest;
            Rotation = WorldRotation.SouthWest;
        }
    }
}
