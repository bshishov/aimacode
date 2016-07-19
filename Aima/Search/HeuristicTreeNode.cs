using Aima.AgentSystems;

namespace Aima.Search
{
    public class HeuristicTreeNode<TState> : TreeNode<TState>
    {
        public double F;

        public HeuristicTreeNode(TState state, double f) : base(state)
        {
            F = f;
        }

        public HeuristicTreeNode(ITreeNode<TState> parent, TState state, double f, IAction action, double stepCost) : base(parent, state, action, stepCost)
        {
            F = f;
        }
    }
}