// -----------------------------------------------------------------------
// <copyright file="PathFindercs.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.World.Segments;
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
    public class PathFinder
    {
        public List<Vector3> Path;
        private List<PathNode> _toExpandFromStart;
        private List<PathNode> _toExpandFromGoal;
        private Dictionary<string, PathNode> _expandedNodesFromGoal;
        private Dictionary<string, PathNode> _expandedNodesFromStart;
        public GameWorld GW;
        private Vector3 _goal;
        private Vector3 _start;
        private string _pathFoundKey;

        public PathFinder(GameWorld gameWorld)
        {
            GW = gameWorld;
            _toExpandFromGoal = new List<PathNode>();
            _toExpandFromStart = new List<PathNode>();
            _expandedNodesFromGoal = new Dictionary<string, PathNode>();
            _expandedNodesFromStart = new Dictionary<string, PathNode>();
            Path = new List<Vector3>();
        }

        public bool ExpandNodeFromStart(PathNode node)
        {
            List<PathNode> newNodes = new List<PathNode>();
            _toExpandFromStart.Remove(node);
            AddStartNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.North, node,
                    Vector3.Distance(node.Position + WorldDirection.North, _goal)), newNodes);
            AddStartNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.East, node,
                    Vector3.Distance(node.Position + WorldDirection.East, _goal)), newNodes);
            AddStartNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.South, node,
                    Vector3.Distance(node.Position + WorldDirection.South, _goal)), newNodes);
            AddStartNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.West, node,
                    Vector3.Distance(node.Position + WorldDirection.West, _goal)), newNodes);
            AddStartNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.NorthEast, node,
                    Vector3.Distance(node.Position + WorldDirection.NorthEast, _goal)), newNodes);
            AddStartNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.NorthWest, node,
                    Vector3.Distance(node.Position + WorldDirection.NorthWest, _goal)), newNodes);
            AddStartNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.SouthEast, node,
                    Vector3.Distance(node.Position + WorldDirection.SouthEast, _goal)), newNodes);
            AddStartNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.SouthWest, node,
                    Vector3.Distance(node.Position + WorldDirection.SouthWest, _goal)), newNodes);

            foreach (PathNode pathNode in newNodes)
            {
                if (NodeFromGoalIsExpanded(pathNode))
                {
                    // mark the key that ties the two toegether.
                    _pathFoundKey = pathNode.GetKey();
                    return true;
                }
            }

            foreach (PathNode pathNode in newNodes)
            {
                int indexToAddAt = _toExpandFromStart.Count;

                for (int i = 0; i < _toExpandFromStart.Count; i++)
                {
                    if (pathNode.Heuristic < _toExpandFromStart[i].Heuristic)
                    {
                        indexToAddAt = i;
                        break;
                    }
                }

                _toExpandFromStart.Insert(indexToAddAt, pathNode);
            }

            return false;
        }

        public bool ExpandNodeFromGoal(PathNode node)
        {
            _toExpandFromGoal.Remove(node);
            List<PathNode> newNodes = new List<PathNode>();

            AddGoalNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.North, node,
                    Vector3.Distance(node.Position + WorldDirection.North, _start)), newNodes);
            AddGoalNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.East, node,
                    Vector3.Distance(node.Position + WorldDirection.East, _start)), newNodes);
            AddGoalNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.South, node,
                    Vector3.Distance(node.Position + WorldDirection.South, _start)), newNodes);
            AddGoalNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.West, node,
                    Vector3.Distance(node.Position + WorldDirection.West, _start)), newNodes);
            AddGoalNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.NorthEast, node,
                    Vector3.Distance(node.Position + WorldDirection.NorthEast, _start)), newNodes);
            AddGoalNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.NorthWest, node,
                    Vector3.Distance(node.Position + WorldDirection.NorthWest, _start)), newNodes);
            AddGoalNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.SouthEast, node,
                    Vector3.Distance(node.Position + WorldDirection.SouthEast, _start)), newNodes);
            AddGoalNodeIfUnexpanded(
                new PathNode(node.Position + WorldDirection.SouthWest, node,
                    Vector3.Distance(node.Position + WorldDirection.SouthWest, _start)), newNodes);

            foreach (PathNode pathNode in newNodes)
            {
                if (NodeFromStartIsExpanded(pathNode))
                {
                    // mark the key that ties the two toegether.
                    _pathFoundKey = pathNode.GetKey();
                    return true;
                }
            }

            foreach (PathNode pathNode in newNodes)
            {
                int indexToAddAt = _toExpandFromGoal.Count;

                for (int i = 0; i < _toExpandFromGoal.Count; i++)
                {
                    PathNode currentNode = _toExpandFromGoal[i];
                    if (pathNode.Heuristic < _toExpandFromGoal[i].Heuristic)
                    {
                        indexToAddAt = i;
                        break;
                    }
                }

                _toExpandFromGoal.Insert(indexToAddAt, pathNode);
            }

            return false;
        }

        public bool NodeFromGoalIsExpanded(PathNode node)
        {
            return _expandedNodesFromGoal.ContainsKey(node.GetKey());
        }

        public bool NodeFromStartIsExpanded(PathNode node)
        {
            return _expandedNodesFromStart.ContainsKey(node.GetKey());
        }

        public void AddStartNodeIfUnexpanded(PathNode node, List<PathNode> listToAddTo )
        {
            if (!NodeFromStartIsExpanded(node))
            {
                if (GW.SM.IsLocationWalkable(new SegmentLocation(node.Position)))
                {
                    listToAddTo.Add(node);
                    _expandedNodesFromStart.Add(node.GetKey(), node);
                }
            }
        }

        public void AddGoalNodeIfUnexpanded(PathNode node, List<PathNode> listToAddTo)
        {
            if (!NodeFromGoalIsExpanded(node))
            {
                if (GW.SM.IsLocationWalkable(new SegmentLocation(node.Position)))
                {
                    listToAddTo.Add(node);
                    _expandedNodesFromGoal.Add(node.GetKey(), node);
                }
            }
        }

        public void ExpandEndNode(PathNode node)
        {
            
        }

        public bool GeneratePath(Vector3 startPos, Vector3 goalPos)
        {
            _toExpandFromGoal.Clear();
            _toExpandFromStart.Clear();
            _expandedNodesFromStart.Clear();
            _expandedNodesFromGoal.Clear();
            _pathFoundKey = "nope";
            _goal = goalPos;
            _start = startPos;
            float initialDistance = Vector3.Distance(startPos, goalPos);
            _toExpandFromGoal.Add(new PathNode(goalPos, null, initialDistance));
            _toExpandFromStart.Add(new PathNode(startPos, null, initialDistance));

            while (_toExpandFromGoal.Count > 0 && _toExpandFromStart.Count > 0)
            {
                if (ExpandNodeFromGoal(_toExpandFromGoal[0]))
                {
                    break;
                }

                if (ExpandNodeFromStart(_toExpandFromStart[0]))
                {
                    break;
                }
            }

            if (_pathFoundKey == "nope")
            {
                return false;
            }


            Path = new List<Vector3>();

            PathNode connectingStartNode = _expandedNodesFromStart[_pathFoundKey];
            PathNode connectingGoalNode = _expandedNodesFromGoal[_pathFoundKey];

            while (connectingStartNode.PrevNode != null)
            {
                Path.Add(connectingStartNode.Position);
                connectingStartNode = connectingStartNode.PrevNode;
            }

            Path.Reverse();

            while (connectingGoalNode != null)
            {
                Path.Add(connectingGoalNode.Position);
                connectingGoalNode = connectingGoalNode.PrevNode;
            }

            return true;
        }
    }
}
