using System;
using System.Collections.Generic;
using System.Linq;

namespace Aima.Search.Methods.Genetic.Selections
{
    public class FitnessProportionateSelection<TAlphabet, TState> : ISelection<TAlphabet, TState>
    {
        public IEnumerable<Individual<TAlphabet>> Select(IEnumerable<Individual<TAlphabet>> population, IFitnessFunction<TState> fitnessFunction)
        {
            // TODO: IMPLEMENT (BUT DESIGN INTERFACE FIRST)
            var sumFitness = population.Sum(i => i.Fitness);
            return population.ToList().OrderBy(individual => individual.Fitness/sumFitness);
        }
    }
}
