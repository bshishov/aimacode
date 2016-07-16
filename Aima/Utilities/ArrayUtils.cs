using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aima.AgentSystems.Vacuum.Grid;

namespace Aima.Utilities
{
    public static class ArrayUtils
    {
        public static bool ContentEquals<T>(this T[,] arr, T[,] other) where T : IComparable
        {
            if (arr.GetLength(0) != other.GetLength(0) ||
                arr.GetLength(1) != other.GetLength(1))
                return false;
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    if (arr[i, j].CompareTo(other[i, j]) != 0)
                        return false;
            return true;
        }

        public static int ComputeHash<T>(this T[,] array, int w, int h)
        {
            var hash = 17;
            for (int i = 0; i < w; i++)
            {
                for (var j = 0; j < h; j++)
                {
                    hash = hash * 31 + array[i, j].GetHashCode();
                }
            }
            return hash;
        }

        public static bool IsClear(this CellState[,] array, int w, int h)
        {
            for (var i = 0; i < w; i++)
            {
                for (var j = 0; j < h; j++)
                {
                    if (array[i, j] == CellState.Dirty)
                        return false;
                }
            }
            return true;
        }
    }
}
