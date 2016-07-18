using System;
using System.Collections.Generic;

namespace Aima.Search
{
    public interface INodeExpander<TNode, TState>
        where TNode : ITreeNode<TState>
    {
        IEnumerable<TNode> Expand(TNode node, IProblem<TState> problem);
    }

    public class DefaultNodeExpander<TState> : INodeExpander<TreeNode<TState>, TState>
    {
        public IEnumerable<TreeNode<TState>> Expand(TreeNode<TState> node, IProblem<TState> problem)
        {
            var successors = problem.SuccessorFn(node.State);
            if (successors == null)
                yield break;

            foreach (var successor in successors)
            {
                var action = successor.Item1;
                var result = successor.Item2;
                yield return new TreeNode<TState>(node, result, action, problem.Cost(action, node.State, result));
            }
        }
    }

    public abstract class HeuristicNodeExpander<TState> : INodeExpander<HeuristicTreeNode<TState>, TState>
    {
        private readonly Func<TState, double> _func;

        protected HeuristicNodeExpander(IHeuristic<TState> heuristic)
        {
            _func = heuristic.Compute;
        }

        protected HeuristicNodeExpander(Func<TState, double> heuristic)
        {
            _func = heuristic;
        }

        public abstract IEnumerable<HeuristicTreeNode<TState>> Expand(HeuristicTreeNode<TState> node,
            IProblem<TState> problem);

        public double ComputeHeuristic(TState state)
        {
            return _func.Invoke(state);
        }
    }

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

        public override IEnumerable<HeuristicTreeNode<TState>> Expand(HeuristicTreeNode<TState> node, IProblem<TState> problem)
        {
            var successors = problem.SuccessorFn(node.State);
            if (successors == null)
                yield break;

            foreach (var successor in successors)
            {
                var action = successor.Item1;
                var state = successor.Item2;
                var newNode = new HeuristicTreeNode<TState>(node, state, action,
                    problem.Cost(action, node.State, state))
                {
                    Heuristic = (2 - Weight) * node.PathCost + Weight * ComputeHeuristic(state)
                };
                yield return newNode;
            }
        }
    }
}