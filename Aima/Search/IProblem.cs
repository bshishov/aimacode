using System.Collections.Generic;
using Aima.AgentSystems;
using Aima.Utilities;

namespace Aima.Search
{
    public interface IProblem<TState>
    {
        TState InitialState { get; }
        IEnumerable<Tuple<IAction, TState>> SuccessorFn(TState state);
        bool GoalTest(TState state);
        double Cost(IAction action, TState from, TState to);
    }
}