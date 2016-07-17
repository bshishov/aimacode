using System;
using Aima.AgentSystems;

namespace Aima.Search
{
    public interface IHeuristic<in TState>
         where TState : IState
    {
        double Compute(TState state);
    }

    public abstract class HeuristicSearch<TState> : ISearch<TState>
        where TState : IState
    {
        private readonly Func<TState, double> _func;

        protected HeuristicSearch(IHeuristic<TState> heuristic)
        {
            _func = heuristic.Compute;
        }

        protected HeuristicSearch(Func<TState, double> heuristic)
        {
            _func = heuristic;
        }

        public abstract ISolution<TState> Search(IProblem<TState> problem);

        public double ComputeHeuristic(TState state)
        {
            return _func.Invoke(state);
        }
    }
}
