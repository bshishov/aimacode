using Aima.AgentSystems;

namespace Aima.Search
{
    public interface IHeuristic<in TProblem, in TState>
         where TState : IState
         where TProblem : IProblem<TState>
    {
        double Compute(TProblem problem, TState state);
    }

    public abstract class HeuristicSearch<TProblem, TState> : ISearch<TState>
        where TState : IState
        where TProblem : IProblem<TState>
    {
        private readonly IHeuristic<TProblem, TState> _heuristic;

        protected HeuristicSearch(IHeuristic<TProblem, TState> heuristic)
        {
            _heuristic = heuristic;
        }

        public abstract ISolution<TState> Search(IProblem<TState> problem);

        public double ComputeHeuristic(TProblem problem, TState state)
        {
            return _heuristic.Compute(problem, state);
        }
    }
}
