using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aima.AgentSystems;
using Aima.Utilities;

namespace Aima.Search.Domain
{
    public struct City
    {
        public string Name;
        public Vector2 Position;

        public override string ToString()
        {
            return $"{Name} {Position}";
        }
    }

    public struct TravelingSalesPersonAction : IAction
    {
        public uint CityId;
        public string Name => CityId.ToString();
    }

    public class TravelingSalesPersonState : IState
    {
        public uint CityId { get; }
        public readonly HashSet<uint> Visited;

        public TravelingSalesPersonState(uint city)
        {
            CityId = city;
            Visited = new HashSet<uint>() { city };
        }

        public TravelingSalesPersonState(uint city, HashSet<uint> lastVisited)
        {
            CityId = city;
            Visited = new HashSet<uint>(lastVisited) { city };
        }

        public TravelingSalesPersonState FromVisit(uint city)
        {
            return new TravelingSalesPersonState(city, Visited);
        }

        public override int GetHashCode()
        {
            var sb = new StringBuilder();
            sb.Append((char) CityId);
            foreach (var u in Visited)
            {
                sb.Append((char)u);
            }
            return sb.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var s = obj as TravelingSalesPersonState;
            if (s != null)
                return GetHashCode().Equals(s.GetHashCode());
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return $"{CityId} (Visited: {Visited.Count})";
        }
    }

    public class TravelingSalespersonProblem : IProblem<TravelingSalesPersonState>
    {
        public IEnumerable<City> Cities => _cities;
        public WeightedGraph Graph { get; }

        private readonly City[] _cities;

        public TravelingSalespersonProblem(City[] cities)
        {
            _cities = cities;
            Graph = new WeightedGraph(cities.Length);

            for (uint i = 0; i < cities.Length; i++)
            {
                for (uint j = 0; j < cities.Length; j++)
                {
                    Graph.AddEdge(i, j, (_cities[i].Position - _cities[j].Position).Length );
                }
            }
        }

        public TravelingSalespersonProblem(int n, int seed = -1) : this(RandomCities(n, seed))
        {
        }

        public TravelingSalesPersonState InitialState => new TravelingSalesPersonState(0);

        public IEnumerable<Tuple<IAction, TravelingSalesPersonState>> SuccessorFn(TravelingSalesPersonState state)
        {
            return 
                Graph.Edges(state.CityId)
                    .Where(e => !state.Visited.Contains(e.To))
                    .Select(edge => new Tuple<IAction, TravelingSalesPersonState>(
                        new TravelingSalesPersonAction {CityId = edge.To},
                        new TravelingSalesPersonState(edge.To, state.Visited)
                        ));
        }

        public bool GoalTest(TravelingSalesPersonState state)
        {
            return state.Visited.Count == _cities.Length;
        }

        public double Cost(IAction action, TravelingSalesPersonState @from, TravelingSalesPersonState to)
        {
            return Graph.Distance(from.CityId, to.CityId);
        }

        public static City[] RandomCities(int n, int seed = -1)
        {
            var rnd = new Random(seed);
            if(seed == -1)
                rnd = new Random();

            
            var cities = new City[n];

            for (var i = 0; i < n; i++)
            {
                cities[i] = new City()
                {
                    Name = $"City {i}",
                    Position = new Vector2((float)rnd.NextDouble(), (float)rnd.NextDouble())
                };
            }

            return cities;
        }
    }
}
