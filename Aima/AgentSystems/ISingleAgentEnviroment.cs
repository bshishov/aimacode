namespace Aima.AgentSystems
{
    public interface ISingleAgentEnviroment<in TPerception>
        where TPerception : IPerception
    {
        IAgent<TPerception> Agent { get; }
        double AgentScore { get; }
        void Step();
    }
}