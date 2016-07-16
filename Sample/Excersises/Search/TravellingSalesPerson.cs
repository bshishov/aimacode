using System;
using System.Collections.Generic;
using System.Linq;
using Aima.AgentSystems;
using Aima.Search;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class TravellingSalesPerson : IExcersice
    {
        class State : IState
        {
            public string LastVisited { get; }
            public readonly HashSet<string> Visited;

            public State(string city)
            {
                LastVisited = city;
                Visited= new HashSet<string>() { city };
            }
            
            public State(string city, HashSet<string> lastVisited)
            {
                LastVisited = city;
                Visited = new HashSet<string>(lastVisited) { city };
            }

            public State FromVisit(string city)
            {
                return new State(city, Visited);
            }

            public override int GetHashCode()
            {
                return LastVisited.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                var s = obj as State;
                if (s != null)
                    return LastVisited.Equals(s.LastVisited);
                return base.Equals(obj);
            }
        }

        class Problem : IProblem<State>
        {
            public readonly Dictionary<Tuple<string, string>, int> Distances = new Dictionary<Tuple<string, string>, int>();
            public readonly Dictionary<string, List<string>> Neighbours = new Dictionary<string, List<string>>();

            public Problem()
            {
                Distances.Add(new Tuple<string, string>("Arad", "Zerind"), 75);
                Distances.Add(new Tuple<string, string>("Zerind", "Oradea"), 71);
                Distances.Add(new Tuple<string, string>("Oradea", "Sibiu"), 151);
                Distances.Add(new Tuple<string, string>("Arad", "Sibiu"), 140);
                Distances.Add(new Tuple<string, string>("Sibiu", "Rimnicu Vilcea"), 80);
                Distances.Add(new Tuple<string, string>("Sibiu", "Fagaras"), 99);
                Distances.Add(new Tuple<string, string>("Fagaras", "Bucharest"), 211);
                Distances.Add(new Tuple<string, string>("Arad", "Timisoara"), 118);
                Distances.Add(new Tuple<string, string>("Timisoara", "Lugoj"), 111);
                Distances.Add(new Tuple<string, string>("Lugoj", "Mehadia"), 70);
                Distances.Add(new Tuple<string, string>("Mehadia", "Drobeta"), 75);
                Distances.Add(new Tuple<string, string>("Drobeta", "Craiova"), 120);
                Distances.Add(new Tuple<string, string>("Craiova", "Rimnicu Vilcea"), 146);
                Distances.Add(new Tuple<string, string>("Craiova", "Pitesti"), 138);
                Distances.Add(new Tuple<string, string>("Rimnicu Vilcea", "Pitesti"), 97);
                Distances.Add(new Tuple<string, string>("Pitesti", "Bucharest"), 101);
                Distances.Add(new Tuple<string, string>("Bucharest", "Giurgiu"), 90);
                Distances.Add(new Tuple<string, string>("Bucharest", "Urziceni"), 85);
                Distances.Add(new Tuple<string, string>("Urziceni", "Vaslui"), 142);
                Distances.Add(new Tuple<string, string>("Vaslui", "Iasi"), 92);
                Distances.Add(new Tuple<string, string>("Iasi", "Neamt"), 87);
                Distances.Add(new Tuple<string, string>("Urziceni", "Hirsova"), 98);
                Distances.Add(new Tuple<string, string>("Hirsova", "Eforie"), 86);

                foreach (var routes in Distances.Keys)
                {
                    if (!Neighbours.ContainsKey(routes.Item1))
                        Neighbours.Add(routes.Item1, new List<string>());

                    if (!Neighbours.ContainsKey(routes.Item2))
                        Neighbours.Add(routes.Item2, new List<string>());

                    Neighbours[routes.Item1].Add(routes.Item2);
                    Neighbours[routes.Item2].Add(routes.Item1);
                }

                foreach (var route in Distances.Keys.ToList())
                {
                    var dist = Distances[route];
                    Distances.Add(new Tuple<string, string>(route.Item2, route.Item1), dist);
                }
            }

            public State InitialState => new State("Arad");

            public IEnumerable<Tuple<IAction, State>> SuccessorFn(State state)
            {
                return Neighbours[state.LastVisited]
                    .Where(s => !state.Visited.Contains(s))
                    .Select(city => new Tuple<IAction, State>(new SimpleAction(city), new State(city, state.Visited)))
                    .ToList();
            }

            public bool GoalTest(State state)
            {
                return state.LastVisited == "Neamt" && state.Visited.Contains("Drobeta");
            }

            public double Cost(IAction action, State @from, State to)
            {
                return Distances[new Tuple<string,string>(from.LastVisited, to.LastVisited)];
            }
        }

        public void Run()
        {
            //var searchMethod = new DepthLimitedSearch<State>(20);
            //var searchMethod = new DepthGraphSearch<State>();
            var searchMethod = new UniformCostSearch<State>();
            var solution = searchMethod.Search(new Problem());

            if (solution != null)
            {
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
