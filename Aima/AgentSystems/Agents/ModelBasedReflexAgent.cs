using System.Collections.Generic;
using System.Linq;

namespace Aima.AgentSystems.Agents
{
    public abstract class ModelBasedReflexAgent<TPerception, TState> : IAgent<TPerception>
        where TPerception : IPerception
    {
        private IAction _lastAction;
        private TState _lastState;
        protected List<IRule<TState>> Rules = new List<IRule<TState>>();

        public IAction Execute(TPerception perception)
        {
            var state = UpdateState(_lastState, _lastAction, perception);
            var rule = MatchRule(state);
            _lastState = state;
            _lastAction = rule?.Action;
            return rule?.Action;
        }

        public abstract TState UpdateState(TState lastState, IAction lastAction, TPerception percept);

        private IRule<TState> MatchRule(TState state)
        {
            return Rules.FirstOrDefault(r => r.Matches(state));
        }
    }
}