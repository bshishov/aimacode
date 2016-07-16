namespace Aima.AgentSystems.Vacuum.Simple
{
    public class SimpleVacuumPerception : IPerception, IState
    {
        public string Location { get; set; }
        public string Status { get; set; }
    }
}