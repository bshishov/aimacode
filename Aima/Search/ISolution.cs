using System.Collections.Generic;
using Aima.AgentSystems;

namespace Aima.Search
{
    public interface ISolution<out TState>
    {
        IEnumerable<IAction> Steps { get; }
        IEnumerable<TState> States { get; }
        ITreeNode<TState> ParentNode { get; }
    }
}