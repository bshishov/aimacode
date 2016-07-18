namespace Aima.AgentSystems
{
    public interface IRule<in TState>
    {
        IAction Action { get; }
        bool Matches(TState state);
    }
}