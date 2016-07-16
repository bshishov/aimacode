using System;
using System.Collections.Generic;
using System.Linq;
using Aima.AgentSystems;
using Aima.Search;
using Aima.Search.Methods;
using Aima.Utilities;

namespace Sample.Excersises.Search
{
    class GeometryPath : IExcersice
    {
        class State : IState
        {
            public int VertexId;

            public override int GetHashCode()
            {
                return VertexId.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                var s = obj as State;
                if (s != null)
                    return s.VertexId == VertexId;
                return base.Equals(obj);
            }
        }

        class GeometryProblem : IProblem<State>
        {
            public State InitialState { get; }

            private readonly List<IShape> _obstacles = new List<IShape>();
            private readonly List<Line> _edges;
            private readonly List<Vector2> _points;

            public GeometryProblem()
            {
                InitialState = new State {VertexId = 0};

                _obstacles.Add(new Rectangle(10, 10, 100, 100));
                _obstacles.Add(new Rectangle(150, 150, 100, 100));
                
                GeometryUtilities.CreateGraph(new List<Vector2> {new Vector2(0,0), new Vector2(270,270)}, _obstacles, out _points, out _edges);
            }

            public IEnumerable<Tuple<IAction, State>> SuccessorFn(State state)
            {
                var p = _points[state.VertexId];

                var successors = _edges.Where(e => e.A.Equals(p))
                    .Select(e => new Tuple<IAction, State>(new SimpleAction("MOVETO " + e.B), new State { VertexId = _points.IndexOf(e.B)}))
                    .ToList();
                successors.AddRange(_edges.Where(e => e.B.Equals(p))
                    .Select(e => new Tuple<IAction, State>(new SimpleAction("MOVETO " + e.A), new State { VertexId = _points.IndexOf(e.A) }))
                    .ToList());

                return successors;
            }

            public bool GoalTest(State state)
            {
                return state.VertexId == 1;
            }

            public double Cost(IAction action, State @from, State to)
            {
                var a = _points[from.VertexId];
                var b = _points[to.VertexId];
                return (b - a).Length;
            }
        }

        public void Run()
        {
            var problem = new GeometryProblem();
            var searchMethod = new UniformCostSearch<State>();
            var solution = searchMethod.Search(problem);

            if (solution != null)
            {
                Console.WriteLine("SOLUTION FOUND!");
                foreach (var action in solution.Steps)
                {
                    Console.WriteLine(action.Name);
                }
            }
            else
            {
                Console.WriteLine("No solution");
            }
        }
    }
}
