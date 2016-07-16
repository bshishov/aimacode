using System.Collections.Generic;
using Aima.AgentSystems;

namespace Aima.Search
{
    public static class SearchUtilities
    {
        public static IEnumerable<ITreeNode<TState>> Expand<TState>(ITreeNode<TState> node, IProblem<TState> problem)
            where TState : IState
        {
            var nodes = new List<ITreeNode<TState>>();
            var successors = problem.SuccessorFn(node.State);
            if (successors == null)
                return nodes;

            foreach (var successor in successors)
            {
                var action = successor.Item1;
                var result = successor.Item2;
                nodes.Add(new TreeNode<TState>(node, result, action, problem.Cost(action, node.State, result)));
            }

            return nodes;
        }
    }
}
