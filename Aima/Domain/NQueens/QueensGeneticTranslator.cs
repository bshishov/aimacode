using System;
using System.Linq;
using Aima.Search.Methods.Genetic;

namespace Aima.Domain.NQueens
{
    public class QueensGeneticTranslator : IGeneticTranslator<uint, QueensPath>
    {
        private readonly int _size;
        private readonly Random _random = new Random();

        public QueensGeneticTranslator(int deckSize)
        {
            _size = deckSize;
        }

        public uint[] RandomGenom()
        {
            var pathIndicies = new uint[_size];

            // fill path with ordered indicies
            for (uint i = 0; i < _size; i++)
                pathIndicies[i] = i;

            return pathIndicies.OrderBy(i => _random.NextDouble()).ToArray();
        }

        public QueensPath FromGenom(uint[] genom)
        {
            return new QueensPath(genom.ToList());
        }

        public uint[] FromState(QueensPath state)
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
