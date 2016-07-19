using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Aima.AgentSystems;
using Aima.Search;
using Aima.Search.Agents;
using Aima.Search.Methods;
using Aima.Utilities;

namespace Sample.Excersises.Search
{
    class Robot : IExcersice
    {
        public class MoveAction : IAction
        {
            public Vector2 Target { get; }
            public string Name { get; }

            public MoveAction(Vector2 target)
            {
                Target = target;
                Name = target.ToString();
            }

            public override int GetHashCode()
                => Name.GetHashCode();

            public override string ToString() 
                => Name;
        }

        public class RobotPerception : IPerception
        {
            public IEnumerable<Vector2> Vertices { get; }

            public RobotPerception(IEnumerable<Vector2> vertices)
            {
                Vertices = vertices;
            }
        }

        public class RoboInnertState : IState
        {
            public int VertexId { get; }

            public RoboInnertState(int vertexId)
            {
                VertexId = vertexId;
            }

            public override bool Equals(object obj)
            {
                var s = obj as RoboInnertState;
                if (s != null)
                    return s.VertexId == VertexId;
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return VertexId.GetHashCode();
            }
        }

        public class RobotPathProblem : IProblem<RoboInnertState>
        {
            public RoboInnertState InitialState { get; }
            public RoboInnertState TargetState { get; }

            private readonly List<Vector2> _vertices;
            private readonly List<Line> _edges;

            public RobotPathProblem(RoboInnertState initial, RoboInnertState target, List<Vector2> vertices, List<Line> edges)
            {
                InitialState = initial;
                TargetState = target;
                _vertices = vertices;
                _edges = edges;
            }

            public IEnumerable<Tuple<IAction, RoboInnertState>> SuccessorFn(RoboInnertState state)
            {
                var p = _vertices[state.VertexId];

                var successors = _edges.Where(e => e.A.Equals(p))
                    .Select(e => new Tuple<IAction, RoboInnertState>(new MoveAction(e.B), new RoboInnertState(_vertices.IndexOf(e.B))))
                    .ToList();
                successors.AddRange(_edges.Where(e => e.B.Equals(p))
                    .Select(e => new Tuple<IAction, RoboInnertState>(new MoveAction(e.A), new RoboInnertState(_vertices.IndexOf(e.A))))
                    .ToList());

                return successors;
            }

            public bool GoalTest(RoboInnertState state)
            {
                if (state == null)
                    return false;
                return state.Equals(TargetState);
            }

            public double Cost(IAction action, RoboInnertState @from, RoboInnertState to)
            {
                if (from == null)
                    return 1;

                var a = _vertices[from.VertexId];
                var b = _vertices[to.VertexId];
                return (b - a).Length;
            }
        }

        public class RobotAgent : ErrorFixingSolvingAgent<RoboInnertState, RobotPerception>
        {
            private readonly List<Vector2> _mapVertices;
            private readonly List<Line> _mapEdges;
            

            public RobotAgent(List<Vector2> mapVertices, List<Line> mapEdges) 
                : base(new UniformCostSearch<RoboInnertState>())
            {
                _mapVertices = mapVertices;
                _mapEdges = mapEdges;
            }

            public override RoboInnertState FormulateGoal(RoboInnertState state)
            {
                var i = Enviroment.TargetVertexId;

                Console.WriteLine("[ROBOT]\tNew goal: vertex {0} {1}", i, _mapVertices[i]);
                return new RoboInnertState(i);
            }

            public override IProblem<RoboInnertState> FormulateProblem(RoboInnertState state, RoboInnertState goal)
            {
                return new RobotPathProblem(state, goal, _mapVertices, _mapEdges);
            }

            public override RoboInnertState UpdateState(RoboInnertState state, RobotPerception percept)
            {
                var p = percept.Vertices.ToArray();
                if (p.Length == 0)
                {
                    Console.WriteLine("[ROBOT]\tCan't see a thing!");
                    return null;
                }


                Console.WriteLine("[ROBOT]\tI see {0} vertices. Where am I now?", percept.Vertices.Count());

                if (p.Length == 1)
                {
                    if (state == null)
                    {
                        Console.WriteLine("[ROBOT]\tI dont' know where i was before");
                        return null;
                    }
                    
                    var vertex = _mapVertices.FirstOrDefault(v => v.Equals(_mapVertices[state.VertexId] + p[0]));
                    if (vertex == null)
                    {
                        Console.WriteLine("[ROBOT]\tWell'p can't identify");
                        return null;
                    }
                       
                    return new RoboInnertState(_mapVertices.IndexOf(vertex));
                }
                
                var percepted = new HashSet<Vector2>();

                foreach (var vertex in percept.Vertices)
                    percepted.Add(vertex);

                // where am 
                for (var i = 0; i < _mapVertices.Count; i++)
                {
                    var v = _mapVertices[i];

                    var neighbours = _mapEdges.Where(e => e.A.Equals(v)).Select(e => e.B).ToList();
                    neighbours.AddRange(_mapEdges.Where(e => e.B.Equals(v)).Select(e => e.A));

                    neighbours = neighbours.Select(n => n - v).ToList();

                    if (percepted.IsSupersetOf(neighbours))
                    {
                        Console.WriteLine("[ROBOT]\tI'm at vertex {0}, {1}", i, v);
                        return new RoboInnertState(i);
                    }
                }

                Console.WriteLine("[ROBOT]\tWell'p can't identify");
                return state;
            }
        }

        public class Enviroment : ISingleAgentEnviroment<RobotPerception>
        {
            public IAgent<RobotPerception> Agent { get; }
            public double AgentScore => _score;
            public static int TargetVertexId = 1;
            public static readonly int ScorePerGoal = 1000;

            private readonly List<Vector2> _vertices;
            private readonly List<Line> _edges;
            private readonly List<IShape> _obstacles;

            private Vector2 _robotPosition;
            private double _score = 0;
            private readonly Random _random = new Random();

            public Enviroment()
            {
                _robotPosition = new Vector2();
                _obstacles = new List<IShape>();

                for (int i = 0; i < 10; i++)
                {
                    _obstacles.Add(new Rectangle(
                        _random.Next(-1000, 1000), 
                        _random.Next(-1000, 1000), 
                        _random.Next(10, 200),
                        _random.Next(10, 200)
                    ));
                }

                GeometryUtilities.CreateGraph(new List<Vector2>(), _obstacles, out _vertices, out _edges);
                _robotPosition = _vertices[_random.Next(_vertices.Count)];
                Agent = new RobotAgent(_vertices, _edges);
                SetGoal();
            }

            public void Step()
            {
                var action = Agent.Execute(CreatePerception());
                PerfromAction(action);
                if (_robotPosition == _vertices[TargetVertexId])
                {
                    _score += ScorePerGoal;
                    Console.WriteLine("[ENV]\tGoal reached!\tScore = {0}", _score);

                    SetGoal();
                }

                Thread.Sleep(100);
            }

            private RobotPerception CreatePerception()
            {
                var vertices = new List<Vector2>();

                foreach (var vertex in _vertices)
                {
                    var intersects = _obstacles.Any(shape => GeometryUtilities.Intersects(_robotPosition, vertex, shape));

                    if(!intersects)
                        vertices.Add(vertex - _robotPosition);
                }

                return new RobotPerception(vertices);
            }

            private void SetGoal()
            {
                TargetVertexId = _random.Next(_vertices.Count);
                Console.WriteLine("[ENV]\tNew goal. Vertex {0}, {1}", TargetVertexId, _vertices[TargetVertexId]);

                // RESET GOAL
                if(_vertices[TargetVertexId] == _robotPosition)
                    SetGoal();
            }

            private void PerfromAction(IAction action)
            {
                var move = action as MoveAction;
                if (move != null)
                {
                    if (_random.NextDouble() > 0.7)
                    {
                        // TODO: CHECK obstacles
                        var penalty = (move.Target - _robotPosition).Length;
                        _robotPosition = move.Target;
                        _score -= penalty;
                        Console.WriteLine("[ENV]\tMoving to {0}\tPenalty = {1}", move.Target, penalty);
                    }
                    else
                    {
                        Console.WriteLine("[ENV]\tThanks Murphys Law. Failed to move :D");
                    }
                }
            }
        }

        public void Run()
        {
            var env = new Enviroment();
            while (true)
            {
                env.Step();
                Console.WriteLine("[STEP]\tScore = {0}", env.AgentScore);
            }
        }
    }
}
