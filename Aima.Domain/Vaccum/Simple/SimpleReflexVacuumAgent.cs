using Aima.AgentSystems;
using Aima.AgentSystems.Agents;

namespace Aima.Domain.Vaccum.Simple
{
    public class SimpleReflexVacuumAgent : SimpleReflexAgent<SimpleVacuumPerception, SimpleVacuumPerception>
    {
        public SimpleReflexVacuumAgent()
        {
            Rules.Add(new Rule<SimpleVacuumPerception>(s => s.Status == "Dirty", VacuumActions.Suck));
            Rules.Add(new Rule<SimpleVacuumPerception>(s => s.Location == "A", VacuumActions.Right));
            Rules.Add(new Rule<SimpleVacuumPerception>(s => s.Location == "B", VacuumActions.Left));
        }

        public override SimpleVacuumPerception InterpretInput(SimpleVacuumPerception percept)
        {
            return percept;
        }
    }
}