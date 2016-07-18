using System;

namespace Aima.AgentSystems
{
    public class Rule<TState> : IRule<TState>
    {
        public IAction Action { get; }
        public bool Matches(TState state)
        {
            return _predicate.Invoke(state);
        }

        private readonly Predicate<TState> _predicate;

        public Rule(Predicate<TState> predicate, IAction action)
        {
            _predicate = predicate;
            Action = action;
        }
    }
}