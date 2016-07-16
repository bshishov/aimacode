namespace Aima.AgentSystems.Vacuum
{
    public interface IVacuumEnviroment
    {
        bool AllClean { get; }
        double AgentScore { get; }
        void Step();
        int CurrentStep { get; }
    }
}
