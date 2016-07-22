using System;
using System.Collections.Generic;

namespace Aima.Search.Methods.Genetic.SelectionOperators
{
    public interface ISelectionOperator<TAlphabet, TState>
    {
        // TODO: DESIGN INTERFACE
        IEnumerable<Tuple<Individual<TAlphabet>, Individual<TAlphabet>>> SelectPairs(List<Individual<TAlphabet>> population,
            IFitnessFunction<TState> fitnessFunction);
    }
}
