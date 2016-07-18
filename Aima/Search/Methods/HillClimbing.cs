using System;
using Aima.Utilities;

namespace Aima.Search.Methods
{
    /// <summary>
    /// The hill climbing algorithm, which is the most basic local search technique
    /// Finds local minimum of heuristic function in state space
    /// </summary>
    public class HillClimbing<TState> : HeuristicSearch<TState>
    {
        public HillClimbing(HeuristicNodeExpander<TState> expander)
            : base(expander)
        {
        }

        public HillClimbing(IHeuristic<TState> heuristic) : base(heuristic)
        {
        }

        public HillClimbing(Func<TState, double> heuristic) : base(heuristic)
        {
        }

        public override ISolution<TState> Search(IProblem<TState> problem)
        {
            var current = new HeuristicTreeNode<TState>(problem.InitialState)
            {
                Heuristic = Expander.ComputeHeuristic(problem.InitialState)
            };

            while (true)
            {
                // get neighbor with lowest computed heuristic
                var neighbor = Expand(current, problem).MinBy(n => n.Heuristic);

                // if we are at local minimum then return solution
                if (neighbor.Heuristic >= current.Heuristic)
                    return new Solution<TState>(current);

                // otherwise keep climbing
                current = neighbor;
            }
        }
    }
}
