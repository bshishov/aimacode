using System;

namespace Aima.AgentSystems.Vacuum.Simple
{
    public class SimpleVacuumEnviroment : ISingleAgentEnviroment<SimpleVacuumPerception>, IVacuumEnviroment
    {
       public double AgentScore { get; private set; } = 0;
        public IAgent<SimpleVacuumPerception> Agent { get; }

        public bool AllClean => !_adirty && !_bdirty;
        public uint Width { get; } = 10;
        public uint Height { get; } = 10;
        public int CurrentStep => _step;



        private string _agentRealPosition;
        private bool _adirty = false;
        private bool _bdirty = false;
        private int _step;

        public SimpleVacuumEnviroment(IAgent<SimpleVacuumPerception> agent, string initialState = "A", bool ADirty = false, bool BDirty = true)
        {
            Agent = agent;
            _agentRealPosition = "A";
            _adirty = ADirty;
            _bdirty = BDirty;
        }
        
        public void Step()
        {
            var action = Agent.Execute(new SimpleVacuumPerception
            {
                Location = _agentRealPosition,
                Status = AgentStatus()
            });
            DoAction(action);
            _step++;

            if (_adirty)
                AgentScore++;
            if (_bdirty)
                AgentScore++;
        }

        private string AgentStatus()
        {
            if (_agentRealPosition == "A")
                return _adirty ? "Dirty" : "Clean";

            if (_agentRealPosition == "B")
                return _bdirty ? "Dirty" : "Clean";

            return null;
        }

        private void DoAction(IAction action)
        {
            // DoNothing
            if(action == null)
                return;

            Console.WriteLine("[{0}] {1}", _step, action.Name);

            if (action.Equals(VacuumActions.Left))
            {
                if (_agentRealPosition == "B")
                {
                    _agentRealPosition = "A";
                    AgentScore--;
                }
            }

            if (action.Equals(VacuumActions.Right))
            {
                if (_agentRealPosition == "A")
                {
                    _agentRealPosition = "B";
                    AgentScore--;
                }
            }

            if (action.Equals(VacuumActions.Suck))
            {
                if (_agentRealPosition == "A" && _adirty)
                {
                    _adirty = false;
                }

                if (_agentRealPosition == "B" && _bdirty)
                {
                    _bdirty = false;
                }
            }
        }
    }
}
