using Aima.AgentSystems;

namespace Aima.Search
{
    public interface ITreeNode<out TState>
        where TState : IState
    {
        TState State { get; }
        ITreeNode<TState> ParentNode { get; }
        IAction Action { get; }
        double PathCost { get; }
        int Depth { get; }
    }
}