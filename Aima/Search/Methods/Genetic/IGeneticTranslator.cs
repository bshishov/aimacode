using System;

namespace Aima.Search.Methods.Genetic
{
    public interface IGeneticTranslator<TAlphabet,TState>
    {
        TAlphabet[] RandomGenom();
        TState FromGenom(TAlphabet[] genom);
        TAlphabet[] FromState(TState state);
        void Mutate(TAlphabet[] genom);
    }
}