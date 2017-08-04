using System;
using System.Collections.Generic;
using System.Linq;
using Aima.Utilities;

namespace Aima.Search.Methods.Genetic.SelectionOperators
{
    public class FitnessProportionateSelection<TAlphabet, TState> : ISelectionOperator<TAlphabet, TState>
    {
        private readonly Random _rnd;

        public FitnessProportionateSelection()
        {
            _rnd = new Random();
        }

        public FitnessProportionateSelection(int seed)
        {
            _rnd = new Random(seed);
        }

        public IEnumerable<Tuple<Individual<TAlphabet>, Individual<TAlphabet>>> SelectPairs(
            List<Individual<TAlphabet>> population, IFitnessFunction<TState> fitnessFunction)
        {
            var sumFitness = population.Sum(i => i.Fitness);
            for (var i = 0; i < population.Count; i++)
            {
                Individual<TAlphabet> x = null, y = null;

                // Random selection of 1st parent
                while (x == null)
                {
                    foreach (var individual in population)
                    {
                        var chance = individual.Fitness/sumFitness;
                        if (_rnd.NextDouble() < chance)
                        {
                            x = individual;
                            break;
                        }
                    }
                }

                // Random selection of 2nd parent
                while (y == null)
                {
                    foreach (var individual in population)
                    {
                        var chance = individual.Fitness/sumFitness;
                        if (_rnd.NextDouble() < chance)
                        {
                            y = individual;
                            break;
                        }
                    }
                }
                yield return new Tuple<Individual<TAlphabet>, Individual<TAlphabet>>(x, y);
            }
        }
    }
}