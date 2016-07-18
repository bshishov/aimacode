using System;
using System.Collections.Generic;
using System.Linq;
using Aima.Search.Methods.Genetic;

namespace Aima.Domain.TSP
{
   /// <summary>
    /// Path representation.
    /// Translates path to genom the most obvious way. 
    /// Each number in genom is vertex id of path.
    /// </summary>
    public class TspGeneticTranslator : IGeneticTranslator<uint, TSPState>
    {
        private readonly int _size;
        private readonly Random _random = new Random();

        public TspGeneticTranslator(TravelingSalespersonProblem problem)
        {
            _size = problem.Cities.Count();
        }

        public uint[] RandomGenom()
        {
            var pathIndicies = new uint[_size];

            // fill path with ordered indicies
            for (uint i = 0; i < _size; i++)
                pathIndicies[i] = i;

            return pathIndicies.OrderBy(i => _random.NextDouble()).ToArray();
        }

        public TSPState FromGenom(uint[] genom)
        {
            return new TSPState(new List<uint>(genom));
        }

        public uint[] FromState(TSPState state)
        {
            return state.Path.ToArray();
        }

        public void Mutate(uint[] genom)
        {
            // Random swap
            var x = _random.Next(_size - 1);
            var y = _random.Next(x, _size);

            // swap
            var old = genom[x];
            genom[x] = genom[y];
            genom[y] = old;
        }
    }
}
