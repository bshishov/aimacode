using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aima.AgentSystems;
using Aima.Search;

namespace Aima.Domain.Wells
{
    public class WellsProblem : IProblem<WellsState>
    {
        public readonly int WellsNumber;
        public readonly int Width;
        public readonly int Height;

        private readonly Func<int, int, double> _func;

        public WellsState InitialState { get; }

        public WellsProblem(int n, int sizeX, int sizeY, Func<int, int, double> func)
        {
            WellsNumber = n;
            Width = sizeX;
            Height = sizeY;
            _func = func;
        }

        public IEnumerable<Tuple<IAction, WellsState>> SuccessorFn(WellsState wellsState)
        {
            return null;
        }

        public bool GoalTest(WellsState wellsState)
        {
            return false;
        }

        public double Cost(IAction action, WellsState @from, WellsState to)
        {
            return 1;
        }

        public int ToIndex(int x, int y)
        {
            return y * Width + x;
        }

        public void FromIndex(int index, out int x, out int y)
        {
            x = index % Width;
            y = index / Height;
        }

        public double Value(int index)
        {
            int x;
            int y;
            FromIndex(index, out x, out y);

            return _func(x, y);
        }

        public double Value(WellsState state)
        {
            return state.WellsPositions.Sum(wellsPosition => Value(wellsPosition));
        }
    }
}
