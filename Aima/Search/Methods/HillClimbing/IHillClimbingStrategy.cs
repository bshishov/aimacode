namespace Aima.Search.Methods.HillClimbing
{
    public interface IHillClimbingStrategy<TState>
    {
        HeuristicTreeNode<TState> Climb(HeuristicTreeNode<TState> initial, IProblem<TState> problem, HeuristicNodeExpander<TState> expander);
    }
}