using System.Collections.Generic;
using System.Linq;

namespace Aima.AgentSystems.Agents
{
    public abstract class SimpleReflexAgent<TPerception, TState> : IAgent<TPerception>
        where TPerception : IPerception
    {
        protected List<IRule<TState>> Rules = new List<IRule<TState>>();

        public IAction Execute(TPerception perception)
        {
            var state = InterpretInput(perception);
            var rule = MatchRule(state);
            return rule.Action;
        }

        public abstract TState InterpretInput(TPerception percept);

        private IRule<TState> MatchRule(TState state)
        {
            return Rules.FirstOrDefault(r => r.Matches(state));
        }
    }
}