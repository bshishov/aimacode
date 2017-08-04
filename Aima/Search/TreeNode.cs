using Aima.AgentSystems;

namespace Aima.Search
{
    public class TreeNode<TState> : ITreeNode<TState>
    {
        // Initial
        public TreeNode(TState state)
        {
            State = state;
            Action = null;
            ParentNode = null;
            PathCost = 0;
            Depth = 0;
        }

        public TreeNode(ITreeNode<TState> parent, TState state, IAction action, double stepCost)
        {
            ParentNode = parent;
            Action = action;
            PathCost = parent.PathCost + stepCost;
            State = state;
            Depth = parent.Depth + 1;
        }

        public TState State { get; }
        public ITreeNode<TState> ParentNode { get; }
        public IAction Action { get; }
        public double PathCost { get; }
        public int Depth { get; }

        public override string ToString()
        {
            return $"[{PathCost}] {State}";
        }
    }
}