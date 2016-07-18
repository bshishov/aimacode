using System;
using System.Linq;

namespace Aima.Domain.SlidingTiles
{
    public static class SlidingTilesHeuristics
    {
        /// <summary>
        /// Calculates summary distance between tile position and its goal
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static double ManhattanDistance(SlidingTilesState state)
        {
            var res = 0;
            for (var i = 0; i < state.Values.Length; i++)
            {
                // real position = i
                var goalx = i % state.N;
                var goaly = i / state.N;

                var px = state.Values[i] % state.N;
                var py = state.Values[i] / state.N;

                res += Math.Abs(goalx - px) + Math.Abs(goaly - py);
            }
            return res;
        }

        public static double MisplacesTiles(SlidingTilesState state)
        {
            return state.Values.Where((t, i) => t != i).Count();
        }

        public static double H1H2Compound(SlidingTilesState state)
        {
            return Math.Max(ManhattanDistance(state), MisplacesTiles(state));
        }

        /// <summary>
        /// LinearConflict Conflict Tiles Definition: Two tiles tj and tk are in a linear conflict 
        /// if tj and tk are in the same line, 
        /// the goal positions of tj and tk are both in that line, 
        /// tj is to the right of tk,
        /// goal position of tj is to the left of the goal position of tk.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static double LinearConflict(SlidingTilesState state)
        {
            // TODO: CHECK TWICE!!!
            var res = 0;
            // horizontal lines
            for (var line = 0; line < state.N; line++)
            {
                for (var j = 0; j < state.N; j++)
                {
                    var ji = line * state.N + j; // index of tile J
                    var jx = state.Values[ji] % state.N; // position in line of a tile J
                    var jgx = ji % state.N; // goal position X of a tile J
                    var jgy = ji / state.N; // goal position Y of a tile J

                    // jg and kg must be on the same line
                    if (jgy != line)
                        continue;

                    for (var k = 0; k < state.N; k++)
                    {
                        var ki = line * state.N + k;
                        var kx = state.Values[ki] % state.N;
                        var kgx = ki % state.N;
                        var kgy = ki / state.N;

                        // jg and kg must be on the same line
                        if (kgy != line)
                            continue;

                        if (jx > kx && jgx < kgx)
                            res += 2;
                    }
                }
            }

            // vertical lines
            for (var line = 0; line < state.N; line++)
            {
                for (var j = 0; j < state.N; j++)
                {
                    var ji = j * state.N + line; // index of tile J
                    var jx = state.Values[ji] / state.N; // position in line of a tile J
                    var jgx = ji % state.N; // goal position X of a tile J
                    var jgy = ji / state.N; // goal position Y of a tile J

                    // jg and kg must be on the same line
                    if (jgy != line)
                        continue;

                    for (var k = 0; k < state.N; k++)
                    {
                        var ki = k * state.N + line;
                        var kx = state.Values[ki] / state.N;
                        var kgx = ki % state.N;
                        var kgy = ki / state.N;

                        // jg and kg must be on the same line
                        if (kgy != line)
                            continue;

                        if (jx > kx && jgx < kgx)
                            res += 2;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// h = The number of steps it would take to solve the problem if it was possible to swap any tile with the "space". 
        /// This heuristic also known as Gaschnig's heuristic. 
        /// It is an admissible heuristic, since it underestimates the distance function of the problem and gives a closer approximation of the distance.
        /// </summary>
        /// <param name="problem"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static double NMaxSwap(SlidingTilesState state)
        {
            var swaps = 0;
            var b = state.ZeroIdx;
            var v = new int[state.N * state.N];
            state.Values.CopyTo(v, 0);
            while (!Check(v))
            {
                if (v[b] != b)
                {
                    var x = Array.IndexOf(v, b);
                    v[x] = 0;
                    v[b] = b;
                    b = x;
                    swaps++;
                }
                else // swap any misplaced tile
                {
                    var misplaced = 0;
                    for (misplaced = 1; misplaced < v.Length; misplaced++)
                    {
                        if (misplaced != v[misplaced])
                            break;
                    }

                    v[b] = v[misplaced];
                    v[misplaced] = 0;
                    b = misplaced;
                    swaps++;
                }
            }
            return swaps;
        }

        private static bool Check(int[] v)
        {
            for (var i = 0; i < v.Length; i++)
            {
                if (i != v[i])
                    return false;
            }

            return true;
        }
    }
}