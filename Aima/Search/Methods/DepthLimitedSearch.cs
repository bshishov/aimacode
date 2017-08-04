using System;

namespace Aima.Search.Methods
{
    public class DepthLimitedSearch<TState> : ISearch<TState>
    {
        private readonly int _limit;

        public DepthLimitedSearch(int limit)
        {
            _limit = limit;
        }

        public ISolution<TState> Search(IProblem<TState> problem)
        {
            return Search(new TreeNode<TState>(problem.InitialState), problem, _limit);
        }

        public event Action<ITreeNode<TState>> SearchNodeChanged;

        public ISolution<TState> Search(IProblem<TState> problem, TState state)
        {
            return Search(new TreeNode<TState>(state), problem, _limit);
        }

        public ISolution<TState> Search(ITreeNode<TState> node, IProblem<TState> problem, int limit)
        {
            SearchNodeChanged?.Invoke(node);

            if (problem.GoalTest(node.State))
                return new Solution<TState>(node);

            if (node.Depth >= limit)
            {
                return Solution<TState>.CutOff;
            }

            var cutoffOccured = false;
            foreach (var successor in SearchUtilities.Expand(node, problem))
            {
                var result = Search(successor, problem, limit);

                // If failed than go next
                if (result == null)
                    continue;

                // If this is cutoff then cutoff occured
                if (result.Equals(Solution<TState>.CutOff))
                {
                    cutoffOccured = true;
                    continue;
                }

                // If not a cutoff and not a failure than this is a result
                return result;
            }

            // All non-failed successors cutoff -> return a cutoff
            if (cutoffOccured)
                return Solution<TState>.CutOff;

            return null;
        }
    }
}