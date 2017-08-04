using System;
using System.Collections.Generic;
using System.Linq;
using Aima.AgentSystems;
using Aima.Search;
using Aima.Utilities;

namespace Aima.Domain.SlidingTiles
{
    public class SlidingTilesProblem : IProblem<SlidingTilesState>
    {
        public readonly int N;

        public SlidingTilesProblem(int n = 3, int seed = -1)
        {
            N = n;
            InitialState = RandomState(n, seed);
        }

        public SlidingTilesProblem()
        {
            N = 3;
            InitialState = new SlidingTilesState(N)
            {
                Values = new[] {7, 2, 4, 5, 0, 6, 8, 3, 1},
                ZeroIdx = 5
            };
        }

        public SlidingTilesState InitialState { get; }

        public IEnumerable<Tuple<IAction, SlidingTilesState>> SuccessorFn(SlidingTilesState state)
        {
            var successors = new List<Tuple<IAction, SlidingTilesState>>();

            if (state.ZeroX < state.N - 1)
                successors.Add(new Tuple<IAction, SlidingTilesState>(SlidingTilesActions.Right,
                    state.FromAction(SlidingTilesActions.Right)));

            if (state.ZeroX > 0)
                successors.Add(new Tuple<IAction, SlidingTilesState>(SlidingTilesActions.Left,
                    state.FromAction(SlidingTilesActions.Left)));

            if (state.ZeroY < state.N - 1)
                successors.Add(new Tuple<IAction, SlidingTilesState>(SlidingTilesActions.Bottom,
                    state.FromAction(SlidingTilesActions.Bottom)));

            if (state.ZeroY > 0)
                successors.Add(new Tuple<IAction, SlidingTilesState>(SlidingTilesActions.Top,
                    state.FromAction(SlidingTilesActions.Top)));

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

        public static SlidingTilesState RandomState(int n, int seed = -1)
        {
            var rnd = new Random(seed);
            if (seed == -1)
                rnd = new Random();
            var defVals = new int[n*n];
            for (var i = 0; i < defVals.Length; i++)
                defVals[i] = i;
            var rVals = defVals.OrderBy(x => rnd.Next()).ToArray();

            var id = Array.IndexOf(rVals, 0);

            // TODO: CHECK WHETHER IT SOLVABLE

            return new SlidingTilesState(n)
            {
                Values = rVals,
                ZeroIdx = id
            };
        }
    }
}