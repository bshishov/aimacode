using Aima.AgentSystems;

namespace Aima.Search
{
    public class HeuristicTreeNode<TState> : TreeNode<TState>
    {
        public double Heuristic;

        public HeuristicTreeNode(TState state) : base(state)
        {
        }

        public HeuristicTreeNode(ITreeNode<TState> parent, TState state, IAction action, double stepCost) : base(parent, state, action, stepCost)
        {
        }
    }
}