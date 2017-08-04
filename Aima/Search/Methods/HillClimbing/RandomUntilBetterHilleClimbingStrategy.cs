using System;
using Aima.Search.NodeExpanders;
using Aima.Utilities;

namespace Aima.Search.Methods.HillClimbing
{
    /// <summary>
    ///     Assuming that expander returns random successors,
    ///     strategy is to try find better ones
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class RandomUntilBetterHilleClimbingStrategy<TState> : IHillClimbingStrategy<TState>
    {
        private readonly int _maxTries;

        public RandomUntilBetterHilleClimbingStrategy(int maxTries = 10)
        {
            _maxTries = maxTries;
        }

        public event Action<ITreeNode<TState>> SearchNodeChanged;

        public HeuristicTreeNode<TState> Climb(HeuristicTreeNode<TState> initial, IProblem<TState> problem,
            HeuristicNodeExpander<TState> expander)
        {
            var current = initial;
            var tryN = 0;
            while (tryN < _maxTries)
            {
                SearchNodeChanged?.Invoke(current);

                // get neighbor with lowest computed heuristic
                var neighbor = expander.Expand(current, problem).MinBy(n => n.F);

                // if we are probably at local minimum then genrate more successors
                // wish we are lucky
                if (neighbor.F >= current.F)
                {
                    current = expander.Expand(current, problem).MinBy(n => n.F);
                    tryN++;
                }
                else // Neighbor is good, keep climbing
                {
                    current = neighbor;
                    tryN = 0;
                }
            }

            return current;
        }
    }
}