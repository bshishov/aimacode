using System.Collections.Generic;
using System.Linq;
using Aima.Search.Queue;

namespace Aima.Search.Methods
{
    public class UniformCostSearch<TState> : ISearch<TState>
    {
        public ISolution<TState> Search(IProblem<TState> problem)
        {
            // TODO: OPTIMIZE PERFORMANCE!!!
            var openSet = new SortedQueue<ITreeNode<TState>>(new TreeNodeCostComparer<TState>());
            openSet.Put(new TreeNode<TState>(problem.InitialState));
            var closedSet = new HashSet<TState>();

            while (true)
            {
                if (openSet.IsEmpty)
                    return null;

                var node = openSet.Take();

                if (problem.GoalTest(node.State))
                    return new Solution<TState>(node);

                // if not a solution
                closedSet.Add(node.State);

                // for each successor
                foreach (var successor in SearchUtilities.Expand(node, problem))
                {
                    // check whether node with this state is existed
                    var existed = openSet.FirstOrDefault(s => s.State.Equals(successor.State));
                    
                    // if node is explored and not existed
                    if (!closedSet.Contains(successor.State) && existed == null)
                        openSet.Put(successor);
                    // if node is existed in frontier and its more expensive that current node 
                    // than replace it
                    else if(existed != null && existed.PathCost > successor.PathCost)
                    {
                        openSet.Remove(existed);
                        openSet.Put(successor);
                    }
                }
            }
        }
    }
}