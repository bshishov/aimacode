namespace Aima.Domain.Vaccum
{
    public interface IVacuumEnviroment
    {
        bool AllClean { get; }
        double AgentScore { get; }
        int CurrentStep { get; }
        void Step();
    }
}