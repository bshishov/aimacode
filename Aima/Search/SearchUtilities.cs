using System.Collections.Generic;

namespace Aima.Search
{
    public static class SearchUtilities
    {
        public static IEnumerable<ITreeNode<TState>> Expand<TState>(ITreeNode<TState> node, IProblem<TState> problem)
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
