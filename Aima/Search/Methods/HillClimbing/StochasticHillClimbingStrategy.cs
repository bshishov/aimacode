using System;
using System.Linq;

namespace Aima.Search.Methods.HillClimbing
{
    /// <summary>
    /// Will take successors with less heuristic value, 
    /// the less heuristic is the higher the chance
    /// Assuming that expander returns random successors
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class StochasticHillClimbingStrategy<TState> : IHillClimbingStrategy<TState>
    {
        public HeuristicTreeNode<TState> Climb(HeuristicTreeNode<TState> initial, IProblem<TState> problem, HeuristicNodeExpander<TState> expander)
        {
            var rnd = new Random();
            var current = initial;
            while (true)
            {
                var goodNeighbors = expander.Expand(current, problem).Where(n => n.Heuristic < current.Heuristic).ToList();

                // No good neighbors at all nothing to select from
                if (goodNeighbors.Count == 0)
                    return current;

                var sumHeuristic = goodNeighbors.Sum(n => n.Heuristic) + 1.0;

                HeuristicTreeNode<TState> neighbor = null;

                // Random selection
                while (neighbor == null)
                {
                    foreach (var n in goodNeighbors)
                    {
                        // the less heuristic is the higher is the chance
                        if (rnd.NextDouble() < 1.0 - (n.Heuristic / sumHeuristic))
                        {
                            neighbor = n;
                            break;
                        }
                    }
                }

                // if we are at local minimum then return solution
                if (neighbor.Heuristic >= current.Heuristic)
                    return current;

                // otherwise keep climbing
                current = neighbor;
            }
        }
    }
}