using Aima.AgentSystems;
using Aima.Search.Queue;

namespace Aima.Search.Methods
{
    public class TreeSearch<TState, TQueue> : ISearch<TState>
        where TState : IState
        where TQueue : IQueue<ITreeNode<TState>>, new()
    {
        public ISolution<TState> Search(IProblem<TState> problem)
        {
            var frontier = new TQueue();
            frontier.Put(new TreeNode<TState>(problem.InitialState));

            while (true)
            {
                if (frontier.IsEmpty)
                    return null;

                var node = frontier.Take();
                if(problem.GoalTest(node.State))
                    return new Solution<TState>(node);
                
                frontier.Put(SearchUtilities.Expand(node, problem));
            }
        }


    }
}