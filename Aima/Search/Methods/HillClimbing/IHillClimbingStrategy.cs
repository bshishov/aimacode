using System;
using Aima.Search.NodeExpanders;

namespace Aima.Search.Methods.HillClimbing
{
    public interface IHillClimbingStrategy<TState>
    {
        event Action<ITreeNode<TState>> SearchNodeChanged;

        HeuristicTreeNode<TState> Climb(HeuristicTreeNode<TState> initial, IProblem<TState> problem,
            HeuristicNodeExpander<TState> expander);
    }
}