using System;

namespace Aima.Search.Methods
{
    public class IterativeDeepingSearch<TState> : ISearch<TState>
    {
        public static readonly int MaxDepth = 1000;

        public ISolution<TState> Search(IProblem<TState> problem)
        {
            for (var i = 0; i < MaxDepth; i++)
            {
                var search = new DepthLimitedSearch<TState>(i);

                // Subscribe to active node changed event, and pass it
                if (SearchNodeChanged != null)
                    search.SearchNodeChanged += node => SearchNodeChanged?.Invoke(node);

                var res = search.Search(problem);

                // If no result that this is a failure
                if (res == null)
                    return null;

                // If not a cutoff than this is a result
                if (!res.Equals(Solution<TState>.CutOff))
                    return res;
            }

            // No results found in MaxDepth
            return null;
        }

        public event Action<ITreeNode<TState>> SearchNodeChanged;
    }
}