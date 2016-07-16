using System;
using System.Collections.Generic;
using System.Linq;
using Aima.AgentSystems;

namespace Aima.Search.Domain
{
    public static class RiverActions
    {
        public static readonly IAction Move1AToRight = new SimpleAction("Move 1xA To Right");
        public static readonly IAction Move2AToRight = new SimpleAction("Move 2xA To Right");

        public static readonly IAction Move1BToRight = new SimpleAction("Move 1xB To Right");
        public static readonly IAction Move2BToRight = new SimpleAction("Move 2xB To Right");

        public static readonly IAction MoveBothToRight = new SimpleAction("Move AB To Right");
    }

    public class RiverState : IState
    {
        public int LeftA { get; set; }
        public int LeftB { get; set; }
        public int RightA { get; set; }
        public int RightB { get; set; }

        public bool IsValid => LeftA >= LeftB && RightA >= RightB;
    }

    public class RiverProblem : IProblem<RiverState>
    {
        public RiverState InitialState => new RiverState()
        {
            LeftA = 3,
            LeftB = 3
        };

        public IEnumerable<Tuple<IAction, RiverState>> SuccessorFn(RiverState state)
        {
            var successors = new List<Tuple<IAction, RiverState>>();

            if (state.LeftA >= 1)
                successors.Add(new Tuple<IAction, RiverState>(RiverActions.Move1AToRight, new RiverState()
                {
                    LeftA = state.LeftA - 1,
                    LeftB = state.LeftB,
                    RightA = state.RightA + 1,
                    RightB = state.RightB,
                }));

            if (state.LeftA >= 2)
                successors.Add(new Tuple<IAction, RiverState>(RiverActions.Move2AToRight, new RiverState()
                {
                    LeftA = state.LeftA - 2,
                    LeftB = state.LeftB,
                    RightA = state.RightA + 2,
                    RightB = state.RightB,
                }));

            if (state.LeftB >= 1)
                successors.Add(new Tuple<IAction, RiverState>(RiverActions.Move1BToRight, new RiverState()
                {
                    LeftA = state.LeftA,
                    LeftB = state.LeftB - 1,
                    RightA = state.RightA,
                    RightB = state.RightB + 1,
                }));

            if (state.LeftB >= 2)
                successors.Add(new Tuple<IAction, RiverState>(RiverActions.Move2BToRight, new RiverState()
                {
                    LeftA = state.LeftA,
                    LeftB = state.LeftB - 2,
                    RightA = state.RightA,
                    RightB = state.RightB + 2,
                }));


            if (state.LeftB >= 1 && state.LeftA >= 1)
                successors.Add(new Tuple<IAction, RiverState>(RiverActions.MoveBothToRight, new RiverState()
                {
                    LeftA = state.LeftA - 1,
                    LeftB = state.LeftB - 1,
                    RightA = state.RightA + 1,
                    RightB = state.RightB + 1,
                }));

            return successors.Where(i => i.Item2.IsValid).ToList();
        }

        public bool GoalTest(RiverState state)
        {
            return state.RightA == 3 && state.RightB == 3;
        }

        public double Cost(IAction action, RiverState @from, RiverState to)
        {
            return 1;
        }
    }
}
