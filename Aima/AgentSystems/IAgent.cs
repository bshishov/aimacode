namespace Aima.AgentSystems
{
    public interface IAgent<in TPErception>
        where TPErception : IPerception
    {
        IAction Execute(TPErception perception);
    }
}