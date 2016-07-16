using System;
using System.Collections.Generic;
using System.Linq;
using Aima.AgentSystems;
using Aima.Utilities;

namespace Aima.Search.Domain
{
    public class RobotState : IState
    {
        public int VertexId;

        public override int GetHashCode()
        {
            return VertexId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var s = obj as RobotState;
            if (s != null)
                return s.VertexId == VertexId;
            return base.Equals(obj);
        }
    }

    public class ObstaclesProblem : IProblem<RobotState>
    {
        public RobotState InitialState { get; }

        private readonly List<IShape> _obstacles = new List<IShape>();
        private readonly List<Line> _edges;
        private readonly List<Vector2> _points;

        public ObstaclesProblem()
        {
            InitialState = new RobotState { VertexId = 0 };

            _obstacles.Add(new Rectangle(10, 10, 100, 100));
            _obstacles.Add(new Rectangle(150, 150, 100, 100));

            GeometryUtilities.CreateGraph(new List<Vector2> { new Vector2(0, 0), new Vector2(270, 270) }, _obstacles, out _points, out _edges);
        }

        public IEnumerable<Tuple<IAction, RobotState>> SuccessorFn(RobotState state)
        {
            var p = _points[state.VertexId];

            var successors = _edges.Where(e => e.A.Equals(p))
                .Select(e => new Tuple<IAction, RobotState>(new SimpleAction("MOVETO " + e.B), new RobotState { VertexId = _points.IndexOf(e.B) }))
                .ToList();
            successors.AddRange(_edges.Where(e => e.B.Equals(p))
                .Select(e => new Tuple<IAction, RobotState>(new SimpleAction("MOVETO " + e.A), new RobotState { VertexId = _points.IndexOf(e.A) }))
                .ToList());

            return successors;
        }

        public bool GoalTest(RobotState state)
        {
            return state.VertexId == 1;
        }

        public double Cost(IAction action, RobotState @from, RobotState to)
        {
            var a = _points[from.VertexId];
            var b = _points[to.VertexId];
            return (b - a).Length;
        }
    }
}
