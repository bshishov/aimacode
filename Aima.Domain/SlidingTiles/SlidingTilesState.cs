using System.Linq;
using System.Text;
using Aima.AgentSystems;

namespace Aima.Domain.SlidingTiles
{
    public class SlidingTilesState : IState
    {
        public readonly int N;
        public int[] Values;

        public int ZeroX;
        public int ZeroY;

        public SlidingTilesState(int n)
        {
            N = n;
            Values = new int[N*N];
        }

        public int ZeroIdx
        {
            get { return ToIndex(ZeroX, ZeroY); }
            set
            {
                ZeroX = value%N;
                ZeroY = value/N;
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

        private int ToIndex(int x, int y)
        {
            return y*N + x;
        }

        private SlidingTilesState Clone()
        {
            var newState = new SlidingTilesState(N)
            {
                ZeroX = ZeroX,
                ZeroY = ZeroY
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
                    sb.AppendFormat("{0} ", Values[ToIndex(j, i)]);
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
                sb.Append((char) Values[i]);
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
}