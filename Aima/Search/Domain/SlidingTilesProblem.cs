using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aima.AgentSystems;

namespace Aima.Search.Domain
{
    public static class SlidingTilesActions
    {
        public static readonly IAction Top = new SimpleAction("TOP");
        public static readonly IAction Left = new SimpleAction("LEFT");
        public static readonly IAction Right = new SimpleAction("RIGHT");
        public static readonly IAction Bottom = new SimpleAction("BOTTOM");
    }

    public class SlidingTilesState : IState
    {
        public static int N = 4;
        public int[] Values = new int[N * N];

        public int ZeroX = 0;
        public int ZeroY = 0;

        public int ZeroIdx
        {
            get { return ToIndex(ZeroX, ZeroY); }
            set
            {
                ZeroX = value % N;
                ZeroY = value / N;
            }
        }

        public SlidingTilesState FromAction(IAction action)
        {
            var s = Clone();
            var swapTarget = -1;

            if (action.Equals(SlidingTilesActions.Left))
                swapTarget = ToIndex(ZeroX - 1, ZeroY);
            if (action.Equals(SlidingTilesActions.Right))
                swapTarget = ToIndex(ZeroX + 1, ZeroY);
            if (action.Equals(SlidingTilesActions.Top))
                swapTarget = ToIndex(ZeroX, ZeroY - 1);
            if (action.Equals(SlidingTilesActions.Bottom))
                swapTarget = ToIndex(ZeroX, ZeroY + 1);

            var old = s.Values[swapTarget];
            s.Values[swapTarget] = 0;
            s.Values[ZeroIdx] = old;
            s.ZeroIdx = swapTarget;

            return s;
        }

        private static int ToIndex(int x, int y)
        {
            return y * N + x;
        }

        private SlidingTilesState Clone()
        {
            var newState = new SlidingTilesState()
            {
                ZeroX = ZeroX,
                ZeroY = ZeroY,
            };
            Values.CopyTo(newState.Values, 0);
            return newState;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < N; j++)
                {
                    sb.AppendFormat("{0}\t", Values[ToIndex(j, i)]);
                }

                sb.Append("\n");
            }
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            var sb = new StringBuilder(Values.Length);
            for (var i = 0; i < Values.Length; i++)
            {
                sb.Append((char)Values[i]);
            }
            return sb.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var s = obj as SlidingTilesState;
            if (s != null)
                return s.Values.SequenceEqual(Values);
            return base.Equals(obj);
        }
    }

    public class SlidingTilesProblem : IProblem<SlidingTilesState>
    {
        public SlidingTilesState InitialState { get; }

        public SlidingTilesProblem()
        {
            InitialState = new SlidingTilesState
            {

                Values = new[] { 7, 2, 4, 5, 0, 6, 8, 3, 1, 9, 10, 11, 12, 13, 14, 15 },
                //Values = new[] { 7, 2, 4, 5, 0, 6, 8, 3, 1 },
                //Values = new[] { 3, 2, 1, 0 },
                ZeroX = 1,
                ZeroY = 1
            };
        }

        public IEnumerable<Tuple<IAction, SlidingTilesState>> SuccessorFn(SlidingTilesState state)
        {
            var successors = new List<Tuple<IAction, SlidingTilesState>>();

            if (state.ZeroX < SlidingTilesState.N - 1)
                successors.Add(new Tuple<IAction, SlidingTilesState>(SlidingTilesActions.Right, state.FromAction(SlidingTilesActions.Right)));

            if (state.ZeroX > 0)
                successors.Add(new Tuple<IAction, SlidingTilesState>(SlidingTilesActions.Left, state.FromAction(SlidingTilesActions.Left)));

            if (state.ZeroY < SlidingTilesState.N - 1)
                successors.Add(new Tuple<IAction, SlidingTilesState>(SlidingTilesActions.Bottom, state.FromAction(SlidingTilesActions.Bottom)));

            if (state.ZeroY > 0)
                successors.Add(new Tuple<IAction, SlidingTilesState>(SlidingTilesActions.Top, state.FromAction(SlidingTilesActions.Top)));

            return successors;
        }

        public bool GoalTest(SlidingTilesState state)
        {
            //Console.SetCursorPosition(0, State.N + 1);
            //Console.WriteLine(state);
            //Thread.Sleep(1000);

            for (var i = 0; i < state.Values.Length; i++)
            {
                if (state.Values[i] != i)
                    return false;
            }

            return true;
        }

        public double Cost(IAction action, SlidingTilesState @from, SlidingTilesState to)
        {
            return 1;
        }
    }
}
