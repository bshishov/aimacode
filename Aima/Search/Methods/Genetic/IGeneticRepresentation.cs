namespace Aima.Search.Methods.Genetic
{
    public interface IGeneticRepresentation<TAlphabet,TState>
    {
        TAlphabet[] RandomGenome();
        TState FromGenome(TAlphabet[] genome);
        TAlphabet[] FromState(TState state);
    }
}