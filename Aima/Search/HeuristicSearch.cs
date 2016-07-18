using System;
using System.Collections.Generic;

namespace Aima.Search
{
    public abstract class HeuristicSearch<TState> : ISearch<TState>
    {
        public readonly HeuristicNodeExpander<TState> Expander;
        
        protected HeuristicSearch(HeuristicNodeExpander<TState> expander)
        {
            Expander = expander;
        }

        protected HeuristicSearch(IHeuristic<TState> heuristic)
            : this(new DefaultHeuristicNodeExpander<TState>(heuristic))
        {
        }

        protected HeuristicSearch(Func<TState, double> heuristic)
            : this(new DefaultHeuristicNodeExpander<TState>(heuristic))
        {
        }

        public abstract ISolution<TState> Search(IProblem<TState> problem);

        protected IEnumerable<HeuristicTreeNode<TState>> Expand(HeuristicTreeNode<TState> node, IProblem<TState> problem)
        {
            return Expander.Expand(node, problem);
        }
    }
}
