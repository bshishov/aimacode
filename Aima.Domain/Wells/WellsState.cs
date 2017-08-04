using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aima.Domain.TSP;

namespace Aima.Domain.Wells
{
    public class WellsState
    {
        public readonly List<int> WellsPositions;

        public WellsState()
        {
            WellsPositions = new List<int>();
        }

        public WellsState(List<int> path)
        {
            WellsPositions = path;
        }

        public override int GetHashCode()
        {
            var sb = new StringBuilder();
            foreach (var u in WellsPositions)
            {
                sb.Append((char) u);
            }
            return sb.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var s = obj as TSPState;
            if (s != null)
                return GetHashCode().Equals(s.GetHashCode());
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return string.Join(", ", WellsPositions.Select(p => p.ToString()).ToArray());
        }
    }
}