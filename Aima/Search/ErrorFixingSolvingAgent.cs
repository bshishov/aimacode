using System;
using System.Linq;
using Aima.AgentSystems;
using Aima.Search.Queue;

namespace Aima.Search
{
    public abstract class ErrorFixingSolvingAgent<TState, TPerception> : IAgent<TPerception>
        where TPerception : IPerception
    {
        private readonly ISearch<TState> _searchMethod;
        private IProblem<TState> _problem;
        private readonly IQueue<IAction> _actions;
        private readonly IQueue<TState> _expectedStates;
        private TState _state;
        private TState _lastExpectedState;
        private TState _goal;
        

        protected ErrorFixingSolvingAgent(ISearch<TState> searchMethod)
        {
            _searchMethod = searchMethod;
            _actions = new FIFOQueue<IAction>();
            _expectedStates = new FIFOQueue<TState>();
        }

        public IAction Execute(TPerception perception)
        {
            _state = UpdateState(_state, perception);

            if (_lastExpectedState != null && !_lastExpectedState.Equals(_state))
            {
                Console.WriteLine("[AGENT]\tUnexpected state!!!");
                _actions.Clear();
                _expectedStates.Clear();
                _lastExpectedState = default(TState);
            }

            if (_actions.IsEmpty)
            {
                _goal = FormulateGoal(_state);
                _problem = FormulateProblem(_state, _goal);
                var solution = _searchMethod.Search(_problem);
                if (solution == null)
                {
                    Console.WriteLine("[AGENT]\tNo solution. Halt!");
                    return null;
                }

                var steps = solution.Steps.ToList();
                _actions.Put(steps);
                _expectedStates.Put(solution.States);

                Console.WriteLine("[AGENT]\tFound solution in {0} steps", steps.Count);
            }

            var action = _actions.Take();
            _lastExpectedState = _expectedStates.Take();
            return action;
        }

        public abstract TState UpdateState(TState state, TPerception percept);
        public abstract TState FormulateGoal(TState state);
        public abstract IProblem<TState> FormulateProblem(TState state, TState goal);
    }
}