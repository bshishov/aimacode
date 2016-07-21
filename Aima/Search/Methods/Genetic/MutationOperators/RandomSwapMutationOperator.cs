using System;

namespace Aima.Search.Methods.Genetic.MutationOperators
{
    /// <summary>
    /// Randomly swaps two elements in genome
    /// </summary>
    /// <typeparam name="TAlphabet"></typeparam>
    public class RandomSwapMutationOperator<TAlphabet> : IMutationOperator<TAlphabet>
    {
        private readonly Random _random;

        public RandomSwapMutationOperator()
        {
            _random = new Random();
        }

        public RandomSwapMutationOperator(int seed)
        {
            _random = new Random(seed);
        }

        public void Apply(TAlphabet[] genome)
        {
            // Random swap
            var x = _random.Next(genome.Length - 1);
            var y = _random.Next(x, genome.Length);

            // swap
            var old = genome[x];
            genome[x] = genome[y];
            genome[y] = old;
        }
    }
}
