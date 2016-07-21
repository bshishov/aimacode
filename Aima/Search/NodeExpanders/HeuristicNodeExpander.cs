using System;
using System.Collections.Generic;

namespace Aima.Search.NodeExpanders
{
    public abstract class HeuristicNodeExpander<TState> : INodeExpander<HeuristicTreeNode<TState>, TState>
    {
        private readonly Func<TState, double> _func;

        protected HeuristicNodeExpander(IHeuristic<TState> heuristic)
        {
            _func = heuristic.Compute;
        }

        protected HeuristicNodeExpander(Func<TState, double> heuristic)
        {
            _func = heuristic;
        }

        public abstract IEnumerable<HeuristicTreeNode<TState>> Expand(HeuristicTreeNode<TState> node,
            IProblem<TState> problem);

        public double ComputeHeuristic(TState state)
        {
            return _func.Invoke(state);
        }
    }
}