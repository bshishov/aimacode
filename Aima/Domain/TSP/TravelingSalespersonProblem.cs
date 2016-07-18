using System;
using System.Collections.Generic;
using Aima.AgentSystems;
using Aima.Search;
using Aima.Utilities;

namespace Aima.Domain.TSP
{
    public class TravelingSalespersonProblem : IProblem<TSPState>
    {
        public IEnumerable<City> Cities => _cities;
        private readonly City[] _cities;

        public WeightedGraph Map { get; set; }

        public TravelingSalespersonProblem(City[] cities)
        {
            _cities = cities;

            
            Map = new WeightedGraph(cities.Length);

            for (uint i = 0; i < cities.Length; i++)
            {
                for (uint j = 0; j < cities.Length; j++)
                {
                    Map.AddEdge(i, j, (_cities[i].Position - _cities[j].Position).Length );
                }
            }

            InitialState = new TSPState(0);
        }


        public TravelingSalespersonProblem(int n, int seed = -1) : this(RandomCities(n, seed))
        {
        }

        public TSPState InitialState { get; }

        public IEnumerable<Tuple<IAction, TSPState>> SuccessorFn(TSPState state)
        {
            var availableVertices = new List<uint>();
            for (uint i = 0; i < _cities.Length; i++)
            {
                availableVertices.Add(i);
            }

            foreach (var v in state.Path)
            {
                availableVertices.Remove(v);
            }

            foreach (var availableVertex in availableVertices)
            {
                var newPath = state.Path.GetRange(0, state.Path.Count);
                newPath.Add(availableVertex);
                var newState = new TSPState(newPath);
                yield return new Tuple<IAction, TSPState>(
                    new TravelingSalesPersonAction { CityId = availableVertex }, 
                    newState);
            }
        }

        public bool GoalTest(TSPState state)
        {
            return state.Path.Count == _cities.Length - 1;
        }

        public double Cost(IAction action, TSPState @from, TSPState to)
        {
            return Map.Distance(from.CityId, to.CityId);
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
