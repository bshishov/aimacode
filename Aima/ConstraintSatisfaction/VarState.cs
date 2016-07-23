using System.Collections.Generic;

namespace Aima.ConstraintSatisfaction
{
    public class VarState<TValue>
    {
        public readonly List<Assigment<TValue>> Assigments;

        public VarState()
        {
            Assigments = new List<Assigment<TValue>>();
        }

        public VarState(IEnumerable<Assigment<TValue>> assigments)
        {
            Assigments = new List<Assigment<TValue>>(assigments);
        }

        public VarState<TValue> Clone()
        {
            return new VarState<TValue>(Assigments);
        }
    }
}