using System.Collections.Generic;
using Aima.AgentSystems;

namespace Aima.Search
{
    public interface ISolution<out TState>
        where TState : IState
    {
        IEnumerable<IAction> Steps { get; }
        IEnumerable<TState> States { get; }
        ITreeNode<TState> ParentNode { get; }
    }
}