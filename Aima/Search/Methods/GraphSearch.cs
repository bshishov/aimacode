using System;
using System.Collections.Generic;
using Aima.Search.Queue;

namespace Aima.Search.Methods
{
    public class GraphSearch<TState, TQueue> : ISearch<TState>
        where TQueue : IQueue<ITreeNode<TState>>, new()
    {
        public event Action<ITreeNode<TState>> SearchNodeChanged;

        public ISolution<TState> Search(IProblem<TState> problem)
        {
            var closedSet = new HashSet<TState>();
            var openSet = new TQueue();
            openSet.Put(new TreeNode<TState>(problem.InitialState));

            while (true)
            {
                if (openSet.IsEmpty)
                    return null;

                var node = openSet.Take();

                // Notify subscribers that node is changed
                SearchNodeChanged?.Invoke(node);

                if (problem.GoalTest(node.State))
                    return new Solution<TState>(node);

                if (!closedSet.Contains(node.State))
                {
                    closedSet.Add(node.State);
                    openSet.Put(SearchUtilities.Expand(node, problem));
                }
            }
        }
    }
}