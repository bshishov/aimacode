using System.Collections.Generic;
using System.Linq;
using Aima.AgentSystems;
using Aima.Utilities;

namespace Aima.Search.Agents
{
    /// <summary>
    ///     Agent that search in online mode whic explores the enviroment using depth search
    ///     Can be applied only when each state can be backtracked
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TPerception"></typeparam>
    public abstract class OnlineDFSAgent<TState, TPerception> : IAgent<TPerception>
        where TPerception : IPerception
    {
        private readonly Dictionary<Tuple<IAction, TState>, TState> _result;
        private readonly Dictionary<TState, List<TState>> _unbacktracked;
        private readonly Dictionary<TState, List<IAction>> _unexplored;
        private IAction _oldAction;
        private TState _oldState;

        protected OnlineDFSAgent()
        {
            _result = new Dictionary<Tuple<IAction, TState>, TState>();
            _unexplored = new Dictionary<TState, List<IAction>>();
            _unbacktracked = new Dictionary<TState, List<TState>>();
        }

        public IAction Execute(TPerception perception)
        {
            var state = UpdateState(_oldState, perception);

            // Check if we have reached the goal
            if (GoalTest(state))
                return null; // TODO: RETURN STOP?

            // if state is actually a new state
            if (!state.Equals(_oldState))
            {
                _unexplored.Add(state, GetAvailableActions(state).ToList());
            }

            // If there was an old state
            if (_oldState != null)
            {
                // Add current state to result dict
                // So we mark that we can reach current state from oldState with oldAction
                var key = new Tuple<IAction, TState>(_oldAction, _oldState);
                if (_result.ContainsKey(key))
                    _result[key] = state;
                else
                    _result.Add(key, state);

                // add backtracking (from old state to state)
                _unbacktracked.Prepend(_oldState, state);
            }

            IAction action;

            // if state is not unexplored
            if (_unexplored.IsEmpty(state))
            {
                // Nothing to backtrack to
                if (_unbacktracked.IsEmpty(state))
                    return null; // Nothing to do

                // get state to backtrack to
                var backtrackTo = _unbacktracked.Pop(state);

                // find action in result that can lead to that state
                action =
                    _result.FirstOrDefault(pair => pair.Key.Item2.Equals(state) && pair.Value.Equals(backtrackTo))
                        .Key.Item1;
            }
            else
            {
                // let's find some action that can lead to unexplored state
                action = _unexplored.Pop(state);
            }

            _oldState = state;
            _oldAction = action;
            return action;
        }

        /// <summary>
        ///     Identifies current state by given percept and old state
        /// </summary>
        public abstract TState UpdateState(TState oldState, TPerception percept);

        /// <summary>
        ///     Checks whether we have reached the goal
        /// </summary>
        public abstract bool GoalTest(TState state);

        /// <summary>
        ///     Get actions available from the given state
        /// </summary>
        public abstract IEnumerable<IAction> GetAvailableActions(TState state);
    }
}