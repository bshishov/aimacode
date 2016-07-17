using System.Collections.Generic;
using Aima.AgentSystems;

namespace Aima.Search
{
    /// <summary>
    /// Compares to heuristic nodes according to its computed heuristic value
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    class HeuristicComparer<TState> : IComparer<HeuristicTreeNode<TState>>
        where TState : IState
    {
        public int Compare(HeuristicTreeNode<TState> x, HeuristicTreeNode<TState> y)
        {
            return x.Heuristic.CompareTo(y.Heuristic);
        }
    }
}