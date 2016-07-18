using System.Collections.Generic;

namespace Aima.Search
{
    public class TreeNodeCostComparer<TState> : IComparer<ITreeNode<TState>>
    {
        public int Compare(ITreeNode<TState> x, ITreeNode<TState> y)
        {
            return x.PathCost.CompareTo(y.PathCost);
        }
    }
}