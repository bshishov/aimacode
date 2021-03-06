﻿using System;
using System.Linq;
using Aima.AgentSystems;
using Aima.Search.Queue;

namespace Aima.Search.Agents
{
    public abstract class SimpleProblemSolvingAgent<TState, TPerception> : IAgent<TPerception>
        where TPerception : IPerception
    {
        private readonly IQueue<IAction> _actions;
        private readonly ISearch<TState> _searchMethod;
        private TState _goal;
        private IProblem<TState> _problem;
        private TState _state;

        protected SimpleProblemSolvingAgent(ISearch<TState> searchMethod)
        {
            _searchMethod = searchMethod;
            _actions = new FIFOQueue<IAction>();
        }

        public IAction Execute(TPerception perception)
        {
            _state = UpdateState(_state, perception);

            if (_actions.IsEmpty)
            {
                _goal = FormulateGoal(_state);
                _problem = FormulateProblem(_state, _goal);
                var solution = _searchMethod.Search(_problem);
                if (solution == null || !solution.Steps.Any())
                {
                    Console.WriteLine("[AGENT]\tNo solution. Halt!");
                    return null;
                }

                var steps = solution.Steps.ToList();
                _actions.Put(steps);
                Console.WriteLine("[AGENT]\tFound solution in {0} steps", steps.Count);
            }

            return _actions.Take();
        }

        public abstract TState UpdateState(TState state, TPerception percept);
        public abstract TState FormulateGoal(TState state);
        public abstract IProblem<TState> FormulateProblem(TState state, TState goal);
    }
}