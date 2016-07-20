using System;
using System.Collections.Generic;
using System.Linq;
using Aima.AgentSystems;
using Aima.Search;

namespace Aima.Domain.NQueens
{
    public class NQueensProblem : IProblem<QueensPath>
    {
        public readonly int N;
        public QueensPath InitialState { get; }

        public NQueensProblem(int n)
        {
            InitialState = new QueensPath();
            N = n;
        }

        public NQueensProblem(List<uint> path)
        {
            InitialState = new QueensPath(path);
            N = path.Count;
        }

        public IEnumerable<Tuple<IAction, QueensPath>> SuccessorFn(QueensPath state)
        {
            var availablePositions = new List<uint>();
            for (uint i = 0; i < N; i++)
            {
                availablePositions.Add(i);
            }

            foreach (var v in state.Path)
            {
                availablePositions.Remove(v);
            }

            foreach (var availableVertex in availablePositions)
            {
                var newPath = state.Path.GetRange(0, state.Path.Count);
                newPath.Add(availableVertex);
                var newState = new QueensPath(newPath);
                yield return new Tuple<IAction, QueensPath>(
                    new NQueensAction(availableVertex),
                    newState);
            }
        }

        public bool GoalTest(QueensPath state)
        {
            if (state.Path.Count != N)
                return false;

            var path = state.Path.ToArray();
            for (var i = 0; i < N - 1; i++)
            {
                for (var j = i + 1; j < N; j++)
                {
                    if (!TestQueens(i, (int)path[i], j, (int)path[j]))
                        return false;
                }
            }

            return true;
        }

        public static bool TestQueens(int x1, int y1, int x2, int y2)
        {
            // vertical
            if (x1 == x2)
                return false;

            // horisontal
            if (y1 == y2)
                return false;

            // positive diag
            if (y2 > y1 && x2 - x1 == y2 - y1)
                return false;

            // negative diag
            if (y2 < y1 && x2 - x1 == y1 - y2)
                return false;

            return true;
        }

        public double Cost(IAction action, QueensPath @from, QueensPath to)
        {
            // each placement of queen costs 1
            return 1.0;
        }
    }
}
