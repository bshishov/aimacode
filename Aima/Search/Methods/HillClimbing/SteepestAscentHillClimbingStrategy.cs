using System;
using Aima.Search.NodeExpanders;
using Aima.Utilities;

namespace Aima.Search.Methods.HillClimbing
{
    /// <summary>
    /// Just takes a successor with minimal heuristic value
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class SteepestAscentHillClimbingStrategy<TState> : IHillClimbingStrategy<TState>
    {
        public event Action<ITreeNode<TState>> SearchNodeChanged;

        public HeuristicTreeNode<TState> Climb(HeuristicTreeNode<TState> initial, IProblem<TState> problem, HeuristicNodeExpander<TState> expander)
        {
            var current = initial;
            while (true)
            {
                SearchNodeChanged?.Invoke(current);

                // get neighbor with lowest computed heuristic
                var neighbor = expander.Expand(current, problem).MinBy(n => n.F);

                // if we are at local minimum then return solution
                if (neighbor.F >= current.F)
                    return current;

                // otherwise keep climbing
                current = neighbor;
            }
        }
    }
}