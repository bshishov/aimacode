using Aima.AgentSystems;

namespace Aima.Search
{
    public interface ISearch<TState>
        where TState : IState
    {
        ISolution<TState> Search(IProblem<TState> problem);
    }
}