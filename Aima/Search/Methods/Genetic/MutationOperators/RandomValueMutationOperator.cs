using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aima.Search.Methods.Genetic.MutationOperators
{
    public class RandomValueMutationOperator<TAlphabet> : IMutationOperator<TAlphabet>
    {
        private readonly Func<TAlphabet[], TAlphabet> _func;
        private readonly Random _random;

        public RandomValueMutationOperator(Func<TAlphabet[], TAlphabet> rnd)
        {
            _func = rnd;
            _random = new Random();
        }

        public RandomValueMutationOperator(int seed, Func<TAlphabet[], TAlphabet> rnd)
        {
            _func = rnd;
            _random = new Random(seed);
        }

        public void Apply(TAlphabet[] genome)
        {
            var position = _random.Next(genome.Length);
            genome[position] = _func.Invoke(genome);
        }
    }
}
