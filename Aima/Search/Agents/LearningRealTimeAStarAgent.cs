using System;
using System.Collections.Generic;
using System.Linq;
using Aima.AgentSystems;
using Aima.Utilities;

namespace Aima.Search.Agents
{
    /// <summary>
    /// A* search-based agent behaviour that learns in realtime by updating
    /// heuristics for each state in space state
    /// </summary>
    public abstract class LearningRealTimeAStarAgent<TState, TPerception> : IAgent<TPerception>
        where TPerception : IPerception
        where TState : class 
    {
        private TState _oldState;
        private IAction _oldAction;
        private readonly Dictionary<TState, double> _h;
        private readonly Dictionary<Tuple<IAction, TState>, TState> _result;
        private readonly IHeuristic<TState> _heuristic;

        protected LearningRealTimeAStarAgent(IHeuristic<TState> heuristic)
        {
            _h = new Dictionary<TState, double>();
            _result = new Dictionary<Tuple<IAction, TState>, TState>();
            _heuristic = heuristic;
        }

        /// <summary>
        /// Identifies current state by given percept and old state
        /// </summary>
        public abstract TState UpdateState(TState oldState, TPerception percept);

        /// <summary>
        /// Checks whether we have reached the goal
        /// </summary>
        public abstract bool GoalTest(TState state);

        public IAction Execute(TPerception perception)
        {
            // Identify state by perception
            var state = UpdateState(_oldState, perception);

            // Check if we have reached the goal
            if (GoalTest(state))
                return null; // TODO: RETURN STOP?

            // Calculate cost estimation for current state (if it is a new state)
            if(!_h.ContainsKey(state))
                _h.Add(state, _heuristic.Compute(state));

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

                // Update heuristic for the old state with minimal LRTACost 
                // of any action available from oldState
                var minStateCost =
                    GetAvailableActions(_oldState)
                        .Min(a => MinimalCostSelector(a, _oldState));
                _h[_oldState] = minStateCost;
            }

            // get action for current state with minimal LRTA cost
            _oldAction = GetAvailableActions(state)
                .MinBy(a => MinimalCostSelector(a, state));

            // current state becomes old
            _oldState = state;

            // return that action
            return _oldAction;
        }

        /// <summary>
        /// A* like cost estimation for given old and new states and action
        /// </summary>
        private double LRTACost(TState oldState, IAction action, TState newState)
        {
            if (newState == null)
                return _heuristic.Compute(oldState);
            return Cost(oldState, action, newState) + _h[newState];
        }

        /// <summary>
        /// This is just a wrapper to properly compute LRTA cost for giver action and state
        /// Here is only for simplifying C# LINQ 
        /// </summary>
        private double MinimalCostSelector(IAction action, TState state)
        {
            var key = new Tuple<IAction, TState>(action, state);
            if (_result.ContainsKey(key))
                return LRTACost(state, action, _result[key]);
            
            return LRTACost(state, action, null);
        }

        /// <summary>
        /// Get actions available from the given state
        /// </summary>
        public abstract IEnumerable<IAction> GetAvailableActions(TState state);

        /// <summary>
        /// Computes cost for step from oldState to newState with given action 
        /// </summary>
        public abstract double Cost(TState oldState, IAction action, TState newState);
    }
}
