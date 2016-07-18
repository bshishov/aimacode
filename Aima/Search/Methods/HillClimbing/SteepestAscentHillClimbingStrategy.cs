using Aima.Utilities;

namespace Aima.Search.Methods.HillClimbing
{
    /// <summary>
    /// Just takes a successor with minimal heuristic value
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class SteepestAscentHillClimbingStrategy<TState> : IHillClimbingStrategy<TState>
    {
        public HeuristicTreeNode<TState> Climb(HeuristicTreeNode<TState> initial, IProblem<TState> problem, HeuristicNodeExpander<TState> expander)
        {
            var current = initial;
            while (true)
            {
                // get neighbor with lowest computed heuristic
                var neighbor = expander.Expand(current, problem).MinBy(n => n.Heuristic);

                // if we are at local minimum then return solution
                if (neighbor.Heuristic >= current.Heuristic)
                    return current;

                // otherwise keep climbing
                current = neighbor;
            }
        }
    }
}