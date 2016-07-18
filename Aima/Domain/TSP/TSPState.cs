using System.Collections.Generic;
using System.Text;

namespace Aima.Domain.TSP
{
    public class TSPState
    {
        public List<uint> Path;
        public uint CityId => Path[Path.Count - 1];

        public TSPState(uint startFrom = 0)
        {
            Path = new List<uint>() { startFrom };
        }

        public TSPState(List<uint> path)
        {
            Path = path;
        }

        public override int GetHashCode()
        {
            var sb = new StringBuilder();
            sb.Append((char)CityId);
            foreach (var u in Path)
            {
                sb.Append((char)u);
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