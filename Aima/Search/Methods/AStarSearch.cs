using System;
using System.Collections.Generic;
using System.Linq;
using Aima.Search.NodeExpanders;
using Aima.Search.Queue;

namespace Aima.Search.Methods
{
    /// <summary>
    /// A* search implementation with closed set (like GraphSearch)
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class AStarSearch<TState> : HeuristicSearch<TState>
    {
        public double Weight = 1.0;

        public AStarSearch(HeuristicNodeExpander<TState> expander) : base(expander)
        {
        }


        public AStarSearch(IHeuristic<TState> heuristic) : base(heuristic)
        {
        }

        public AStarSearch(Func<TState, double> heuristic) : base(heuristic)
        {
        }

        public override ISolution<TState> Search(IProblem<TState> problem)
        {
            var openSet = new SortedQueue<HeuristicTreeNode<TState>>(new HeuristicComparer<TState>());
            openSet.Put( new HeuristicTreeNode<TState>(problem.InitialState, Expander.ComputeHeuristic(problem.InitialState)));
            var closedSet = new HashSet<TState>();

            while (true)
            {
                if (openSet.IsEmpty)
                    return null;

                var node = openSet.Take();

                if (problem.GoalTest(node.State))
                    return new Solution<TState>(node);

                closedSet.Add(node.State);

                foreach (var successor in Expand(node, problem))
                {
                    // check whether node with this state is existed
                    var existed = openSet.FirstOrDefault(s => s.State.Equals(successor.State));
                    
                    // if node is explored and not existed
                    if (!closedSet.Contains(successor.State) && existed == null)
                        openSet.Put(successor);
                    // if node is existed in frontier and its more expensive that current node 
                    // than replace it
                    else if (existed != null && existed.PathCost > successor.PathCost)
                    {
                        openSet.Remove(existed);
                        openSet.Put(successor);
                    }
                }
            }
        }
    }
}
