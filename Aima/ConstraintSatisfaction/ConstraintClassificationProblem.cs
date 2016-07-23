using System;
using System.Collections.Generic;
using System.Linq;
using Aima.AgentSystems;
using Aima.Search;

namespace Aima.ConstraintSatisfaction
{
    public abstract class ConstraintClassificationProblem<T> : IProblem<VarState<T>>        
    {
        public VarState<T> InitialState { get; } = new VarState<T>();

        private readonly List<Tuple<string, IEnumerable<T>>> _scope;

        protected ConstraintClassificationProblem(IEnumerable<Tuple<string, IEnumerable<T>>> scope)
        {
            _scope = scope.ToList();
        } 

        public IEnumerable<Tuple<IAction, VarState<T>>> SuccessorFn(VarState<T> state)
        {
            Tuple<string, IEnumerable<T>> unassigned = null;
            
            foreach (var tuple in _scope)
            {
                if (state.Assigments.FirstOrDefault(a => a.Name.Equals(tuple.Item1)) == null)
                {
                    unassigned = tuple;
                    break;
                }
            }

            if (unassigned == null)
                yield break;

            foreach (var val in unassigned.Item2)
            {
                var newState = state.Clone();
                var assignment = new Assigment<T>(unassigned.Item1, val);
                newState.Assigments.Add(assignment);
                yield return new Tuple<IAction, VarState<T>>(new AssigmentAction<T>(assignment), newState);
            }
        }

        public bool GoalTest(VarState<T> state)
        {
            return state.Assigments.Count == _scope.Count;
        }

        public virtual double Cost(IAction action, VarState<T> @from, VarState<T> to)
        {
            return 1.0;
        }
    }
}
