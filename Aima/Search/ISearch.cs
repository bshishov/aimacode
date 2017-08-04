namespace Aima.Search
{
    public interface ISearch<TState> : ISearchNodeNotifier<TState>
    {
        ISolution<TState> Search(IProblem<TState> problem);
    }
}