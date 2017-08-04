using System;
using System.Linq;
using Aima.Search.NodeExpanders;

namespace Aima.Search.Methods.HillClimbing
{
    /// <summary>
    ///     Will take successors with less heuristic value,
    ///     the less heuristic is the higher the chance
    ///     Assuming that expander returns random successors
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class StochasticHillClimbingStrategy<TState> : IHillClimbingStrategy<TState>
    {
        public event Action<ITreeNode<TState>> SearchNodeChanged;

        public HeuristicTreeNode<TState> Climb(HeuristicTreeNode<TState> initial, IProblem<TState> problem,
            HeuristicNodeExpander<TState> expander)
        {
            var rnd = new Random();
            var current = initial;
            while (true)
            {
                SearchNodeChanged?.Invoke(current);

                var goodNeighbors = expander.Expand(current, problem).Where(n => n.F < current.F).ToList();

                // No good neighbors at all nothing to select from
                if (goodNeighbors.Count == 0)
                    return current;

                var sumHeuristic = goodNeighbors.Sum(n => n.F) + 1.0;

                HeuristicTreeNode<TState> neighbor = null;

                // Random selection
                while (neighbor == null)
                {
                    foreach (var n in goodNeighbors)
                    {
                        // the less heuristic is the higher is the chance
                        if (rnd.NextDouble() < 1.0 - n.F/sumHeuristic)
                        {
                            neighbor = n;
                            break;
                        }
                    }
                }

                // if we are at local minimum then return solution
                if (neighbor.F >= current.F)
                    return current;

                // otherwise keep climbing
                current = neighbor;
            }
        }
    }
}