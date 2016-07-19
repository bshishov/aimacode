using System;
using System.Collections.Generic;
using System.Linq;
using Aima.Search;

namespace Aima.Domain.TSP
{
    public class RandomEdgesExpander : HeuristicNodeExpander<TSPState>
    {
        public RandomEdgesExpander(IHeuristic<TSPState> heuristic) : base(heuristic)
        {
        }

        public RandomEdgesExpander(Func<TSPState, double> heuristic) : base(heuristic)
        {
        }

        public override IEnumerable<HeuristicTreeNode<TSPState>> Expand(HeuristicTreeNode<TSPState> node, IProblem<TSPState> problem)
        {
            var p = (TravelingSalespersonProblem)problem;
            var n = p.Cities.Count();

            // if empty path
            if (node.State.Path.Count <= 1)
            {
                var newState = MakeRandomState(p, node.State);

                // return arbitrary path
                return new[]
                {
                    new HeuristicTreeNode<TSPState>(newState, ComputeHeuristic(newState))
                };
            }

            var rnd = new Random();

            // Pick 2 random vertices along the path
            var x = rnd.Next(n - 1);
            var y = x + rnd.Next(n - x);


            // split path 
            var part1 = node.State.Path.GetRange(0, x);
            var part2 = node.State.Path.GetRange(x, y - x);
            var part3 = node.State.Path.GetRange(y, n - y);

            return new[]
            {
                FromCombinedPath(part1, part2, part3),
                FromCombinedPath(part1, part3, part2),
                FromCombinedPath(part2, part1, part3),
                FromCombinedPath(part2, part3, part1),
                FromCombinedPath(part3, part1, part2),
                FromCombinedPath(part3, part2, part1),
            };
        }

        private TSPState MakeRandomState(TravelingSalespersonProblem p, TSPState state)
        {
            var n = p.Cities.Count();
            var rnd = new Random();
            var pathIndicies = new uint[n];

            // fill path with ordered indicies
            for (uint i = 0; i < n; i++)
                pathIndicies[i] = i;

            // return state with shuffled path
            return new TSPState(pathIndicies.OrderBy(i => rnd.NextDouble()).ToList());
        }

        private HeuristicTreeNode<TSPState> FromCombinedPath(List<uint> a, List<uint> b, List<uint> c)
        {
            var r = new List<uint>();
            r.AddRange(a);
            r.AddRange(b);
            r.AddRange(c);
            var newState = new TSPState(r);
            return new HeuristicTreeNode<TSPState>(newState, ComputeHeuristic(newState));
        }
    }
}