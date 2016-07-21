using System;
using System.Collections.Generic;
using System.Linq;

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

        public static void Append<T1, T2>(this Dictionary<T1, List<T2>> dict, T1 key, T2 value)
        {
            if (!dict.ContainsKey(key))
                dict.Add(key, new List<T2> { value });
            else
            {
                if (dict[key] == null)
                    dict[key] = new List<T2> { value };
                else
                    dict[key].Add(value);
            }
        }

        public static void Prepend<T1, T2>(this Dictionary<T1, List<T2>> dict, T1 key, T2 value)
        {
            if (!dict.ContainsKey(key))
                dict.Add(key, new List<T2> { value });
            else
            {
                if (dict[key] == null)
                    dict[key] = new List<T2> { value };
                else
                    dict[key].Insert(0, value);
            }
        }

        public static bool IsEmpty<T1, T2>(this Dictionary<T1, List<T2>> dict, T1 key)
        {
            if (!dict.ContainsKey(key))
                return true;

            if (dict[key] == null)
                return true;

            return dict[key].Count == 0;
        }

        public static T2 Pop<T1, T2>(this Dictionary<T1, List<T2>> dict, T1 key)
        {
            if (!dict.ContainsKey(key))
                throw new InvalidOperationException("Nothing to pop");

            var list = dict[key];
            if (list == null || list.Count == 0)
                throw new InvalidOperationException("Nothing to pop");

            var val = list.First();
            list.RemoveAt(0);
            return val;
        }
    }
}
