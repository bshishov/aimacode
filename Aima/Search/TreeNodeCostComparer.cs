using System.Collections.Generic;
using Aima.AgentSystems;

namespace Aima.Search
{
    public class TreeNodeCostComparer<TState> : IComparer<ITreeNode<TState>>
        where TState : IState
    {
        public int Compare(ITreeNode<TState> x, ITreeNode<TState> y)
        {
            return x.PathCost.CompareTo(y.PathCost);
        }
    }
}