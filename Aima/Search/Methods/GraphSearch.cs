using System.Collections.Generic;
using Aima.AgentSystems;
using Aima.Search.Queue;

namespace Aima.Search.Methods
{
    public class GraphSearch<TState, TQueue> : ISearch<TState>
        where TState : IState
        where TQueue : IQueue<ITreeNode<TState>>, new()
    {
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