using System.Collections.Generic;
using System.Text;

namespace Aima.Domain.NQueens
{
    public class QueensPath
    {
        public readonly List<uint> Path;

        public QueensPath()
        {
            Path = new List<uint>();
        }

        public QueensPath(List<uint> path)
        {
            Path = path;
        }

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
            var s = obj as QueensPath;
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