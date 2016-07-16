namespace Aima.AgentSystems
{
    public interface IRule<in TState>
        where TState : IState
    {
        IAction Action { get; }
        bool Matches(TState state);
    }
}