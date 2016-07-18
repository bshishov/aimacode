using Aima.AgentSystems;

namespace Aima.Search
{
    public interface ISearch<TState>
    {
        ISolution<TState> Search(IProblem<TState> problem);
    }
}