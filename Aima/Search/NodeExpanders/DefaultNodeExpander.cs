using System.Collections.Generic;

namespace Aima.Search.NodeExpanders
{
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
}