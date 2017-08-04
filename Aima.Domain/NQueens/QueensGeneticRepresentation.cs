using System;
using System.Linq;
using Aima.Search.Methods.Genetic;

namespace Aima.Domain.NQueens
{
    public class QueensGeneticRepresentation : IGeneticRepresentation<uint, QueensPath>
    {
        private readonly Random _random = new Random();
        private readonly int _size;

        public QueensGeneticRepresentation(int deckSize)
        {
            _size = deckSize;
        }

        public uint[] RandomGenome()
        {
            var pathIndicies = new uint[_size];

            // fill path with ordered indicies
            for (uint i = 0; i < _size; i++)
                pathIndicies[i] = i;

            return pathIndicies.OrderBy(i => _random.NextDouble()).ToArray();
        }

        public QueensPath FromGenome(uint[] genome)
        {
            return new QueensPath(genome.ToList());
        }

        public uint[] FromState(QueensPath state)
        {
            return state.Path.ToArray();
        }
    }
}