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
            var closed = new HashSet<TState>();
            var fringe = new TQueue();
            fringe.Put(new TreeNode<TState>(problem.InitialState));

            while (true)
            {
                if (fringe.IsEmpty)
                    return null;

                var node = fringe.Take();
                if (problem.GoalTest(node.State))
                    return new Solution<TState>(node);

                if (!closed.Contains(node.State))
                {
                    closed.Add(node.State);
                    fringe.Put(SearchUtilities.Expand(node, problem));
                }
            }
        }
    }
}