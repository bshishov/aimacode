using System.Collections.Generic;
using System.Linq;

namespace Aima.AgentSystems.Agents
{
    public abstract class ModelBasedReflexAgent<TPerception, TState> : IAgent<TPerception>
        where TPerception : IPerception
    {
        protected List<IRule<TState>> Rules = new List<IRule<TState>>();
        private TState _lastState;
        private IAction _lastAction;

        public abstract TState UpdateState(TState lastState, IAction lastAction, TPerception percept);

        public IAction Execute(TPerception perception)
        {
            var state = UpdateState(_lastState, _lastAction, perception);
            var rule = MatchRule(state);
            _lastState = state;
            _lastAction = rule?.Action;
            return rule?.Action;
        }

        private IRule<TState> MatchRule(TState state)
        {
            return Rules.FirstOrDefault(r => r.Matches(state));
        }
    }
}