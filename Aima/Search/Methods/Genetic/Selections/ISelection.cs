using System.Collections.Generic;

namespace Aima.Search.Methods.Genetic.Selections
{
    interface ISelection<TAlphabet, TState>
    {
        // TODO: DESIGN INTERFACE
        IEnumerable<Individual<TAlphabet>> Select(IEnumerable<Individual<TAlphabet>> population,
            IFitnessFunction<TState> fitnessFunction);
    }
}
