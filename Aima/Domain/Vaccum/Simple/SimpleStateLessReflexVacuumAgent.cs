using Aima.AgentSystems;

namespace Aima.Domain.Vaccum.Simple
{
    public class SimpleStatelessReflexVacuumAgent : IAgent<SimpleVacuumPerception>
    {
        public IAction Execute(SimpleVacuumPerception perception)
        {
            if (perception.Status == "Dirty") return new SimpleAction("Suck");
            if (perception.Location == "A") return new SimpleAction("Right");
            if (perception.Location == "B") return new SimpleAction("Left");

            return null;
        }
    }
}