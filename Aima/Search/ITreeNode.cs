using Aima.AgentSystems;

namespace Aima.Search
{
    public interface ITreeNode<out TState>
    {
        TState State { get; }
        ITreeNode<TState> ParentNode { get; }
        IAction Action { get; }
        double PathCost { get; }
        int Depth { get; }
    }
}