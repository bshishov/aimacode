namespace Aima.Search
{
    public interface IHeuristic<in TState>
    {
        double Compute(TState state);
    }
}