using System;
using System.Linq;
using Aima.Search.Methods.Genetic;

namespace Aima.Domain.TSP
{
   /// <summary>
    /// Path representation.
    /// Translates path to genome the most obvious way. 
    /// Each number in genome is vertex id of path.
    /// </summary>
    public class TspGeneticRepresentation : IGeneticRepresentation<uint, TSPState>
    {
        private readonly int _size;
        private readonly Random _random = new Random();

        public TspGeneticRepresentation(TravelingSalespersonProblem problem)
        {
            _size = problem.Cities.Count();
        }

        public uint[] RandomGenome()
        {
            var pathIndicies = new uint[_size];

            // fill path with ordered indicies
            for (uint i = 0; i < _size; i++)
                pathIndicies[i] = i;

            return pathIndicies.OrderBy(i => _random.NextDouble()).ToArray();
        }

        public TSPState FromGenome(uint[] genome)
        {
            return new TSPState(genome.ToList());
        }

        public uint[] FromState(TSPState state)
        {
            return state.Path.ToArray();
        }
    }
}
