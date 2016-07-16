using System;
using System.Collections.Generic;
using Aima.AgentSystems;

namespace Aima.Search
{
    public interface IProblem<TState>
        where TState : IState
    {
        TState InitialState { get; }
        IEnumerable<Tuple<IAction, TState>> SuccessorFn(TState state);
        bool GoalTest(TState state);
        double Cost(IAction action, TState from, TState to);
    }
}
