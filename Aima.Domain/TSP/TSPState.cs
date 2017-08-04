using System.Collections.Generic;
using System.Text;

namespace Aima.Domain.TSP
{
    public class TSPState
    {
        public readonly List<uint> Path;

        public TSPState()
        {
            Path = new List<uint>();
        }

        public TSPState(List<uint> path)
        {
            Path = path;
        }

        public uint CityId => Path[Path.Count - 1];

        public override int GetHashCode()
        {
            var sb = new StringBuilder();
            foreach (var u in Path)
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
            var sb = new StringBuilder();
            foreach (var u in Path)
            {
                sb.AppendFormat("{0} > ", u);
            }
            return sb.ToString();
        }
    }
}