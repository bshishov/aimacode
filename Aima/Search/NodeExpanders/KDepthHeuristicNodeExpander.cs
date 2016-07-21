using System;
using System.Collections.Generic;
using Aima.Search.Methods;

namespace Aima.Search.NodeExpanders
{
    /// <summary>
    /// Computes heuristic not of 1-depth expanded values, but k-depth using DepthLimitedSearch method
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class KDepthHeuristicNodeExpander<TState> : HeuristicNodeExpander<TState>
    {
        public double Weight = 1.0;
        private readonly DepthLimitedSearch<TState> _dls;

        public KDepthHeuristicNodeExpander(int k, IHeuristic<TState> heuristic)
            : base(heuristic)
        {
            _dls = new DepthLimitedSearch<TState>(k);
        }

        public KDepthHeuristicNodeExpander(int k, Func<TState, double> heuristic)
            : base(heuristic)
        {
            _dls = new DepthLimitedSearch<TState>(k);
        }

        public override IEnumerable<HeuristicTreeNode<TState>> Expand(HeuristicTreeNode<TState> node, IProblem<TState> problem)
        {
            var successors = problem.SuccessorFn(node.State);
            if (successors == null)
                yield break;

            foreach (var successor in successors)
            {
                var action = successor.Item1;
                var state = successor.Item2;
                var localSolution = _dls.Search(problem, state);

                // compute F value of DLS (k) solution
                var f = (2 - Weight) * localSolution.ParentNode.PathCost + Weight * ComputeHeuristic(localSolution.ParentNode.State);
                var newNode = new HeuristicTreeNode<TState>(node, state, f, action,
                    problem.Cost(action, node.State, state));
                yield return newNode;
            }
        }
    }
}