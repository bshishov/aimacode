using System.Collections.Generic;
using System.Linq;
using Aima.AgentSystems;
using Aima.Search.Queue;

namespace Aima.Search.Methods
{
    public class UniformCostSearch<TState, TQueue> : ISearch<TState>
        where TState : IState
        where TQueue : IQueue<ITreeNode<TState>>, new()
    {
        public ISolution<TState> Search(IProblem<TState> problem)
        {
            var explored = new HashSet<TState>();
            var frontier = new TQueue();
            frontier.Put(new TreeNode<TState>(problem.InitialState));

            while (true)
            {
                if (frontier.IsEmpty)
                    return null;

                var node = frontier.Take();
                if (problem.GoalTest(node.State))
                    return new Solution<TState>(node);

                // if not a solution
                explored.Add(node.State);

                // for each successor
                foreach (var successor in SearchUtilities.Expand(node, problem))
                {
                    // check whether node with this state is existed
                    var existed = frontier.FirstOrDefault(s => s.State.Equals(successor.State));
                    
                    // if node is explored and not existed
                    if (!explored.Contains(successor.State) && existed == null)
                        frontier.Put(successor);
                    // if node is existed in frontier and its more expensive that current node 
                    // than replace it
                    else if(existed != null && existed.PathCost > successor.PathCost)
                    {
                        frontier.Remove(existed);
                        frontier.Put(successor);
                    }
                }
            }
        }
    }

    public class UniformCostSearch<TState> : UniformCostSearch<TState, FIFOQueue<ITreeNode<TState>>>
        where TState : IState
    {
    }
}