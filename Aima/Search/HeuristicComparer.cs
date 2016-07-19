using System.Collections.Generic;

namespace Aima.Search
{
    /// <summary>
    /// Compares to heuristic nodes according to its computed heuristic value
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    class HeuristicComparer<TState> : IComparer<HeuristicTreeNode<TState>>
    {
        public int Compare(HeuristicTreeNode<TState> x, HeuristicTreeNode<TState> y)
        {
            return x.F.CompareTo(y.F);
        }
    }
}