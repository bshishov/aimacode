using System;
using System.Linq;
using Aima.Search.NodeExpanders;
using Aima.Utilities;

namespace Aima.Search.Methods.HillClimbing
{
    /// <summary>
    ///     While climbing computes heuristic not of 1-depth expanded values, but k-depth using DepthLimitedSearch method
    ///     This helps avoid local minimums of basic HillClimbing
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class KDepthHillClimbingStrategy<TState> : IHillClimbingStrategy<TState>
    {
        private readonly DepthLimitedSearch<TState> _dls;

        public KDepthHillClimbingStrategy(int k)
        {
            _dls = new DepthLimitedSearch<TState>(k);
        }

        public event Action<ITreeNode<TState>> SearchNodeChanged;

        public HeuristicTreeNode<TState> Climb(HeuristicTreeNode<TState> initial, IProblem<TState> problem,
            HeuristicNodeExpander<TState> expander)
        {
            var current = initial;
            while (true)
            {
                SearchNodeChanged?.Invoke(current);

                var neighbors = expander.Expand(current, problem).ToList();
                foreach (var node in neighbors)
                {
                    var localSolution = _dls.Search(problem, node.State);
                    node.F = localSolution.ParentNode.PathCost +
                             expander.ComputeHeuristic(localSolution.ParentNode.State);
                }

                // get neighbor with lowest computed heuristic
                var neighbor = neighbors.MinBy(n => n.F);

                // if we are at local minimum then return solution
                if (neighbor.F >= current.F)
                    return current;

                // otherwise keep climbing
                current = neighbor;
            }
        }
    }
}