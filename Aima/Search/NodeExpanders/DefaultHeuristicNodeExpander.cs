using System;
using System.Collections.Generic;

namespace Aima.Search.NodeExpanders
{
    public class DefaultHeuristicNodeExpander<TState> : HeuristicNodeExpander<TState>
    {
        public double Weight = 1.0;

        public DefaultHeuristicNodeExpander(IHeuristic<TState> heuristic)
            : base(heuristic)
        {
        }

        public DefaultHeuristicNodeExpander(Func<TState, double> heuristic)
            : base(heuristic)
        {
        }

        public override IEnumerable<HeuristicTreeNode<TState>> Expand(HeuristicTreeNode<TState> node,
            IProblem<TState> problem)
        {
            var successors = problem.SuccessorFn(node.State);
            if (successors == null)
                yield break;

            foreach (var successor in successors)
            {
                var action = successor.Item1;
                var state = successor.Item2;
                var f = (2 - Weight)*node.PathCost + Weight*ComputeHeuristic(state);
                var newNode = new HeuristicTreeNode<TState>(node, state, f, action,
                    problem.Cost(action, node.State, state));
                yield return newNode;
            }
        }
    }
}