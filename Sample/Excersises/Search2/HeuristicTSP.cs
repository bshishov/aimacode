using System;
using System.Collections.Generic;
using System.Linq;
using Aima.Search;
using Aima.Search.Domain;
using Aima.Search.Methods;
using Aima.Utilities;
using Sample.Excersises.Search;

namespace Sample.Excersises.Search2
{
    class HeuristicTSP : IExcersice
    {
        class TspMstHeuristic : IHeuristic<TravelingSalesPersonState>
        {
            private readonly TravelingSalespersonProblem _problem;

            public TspMstHeuristic(TravelingSalespersonProblem problem)
            {
                _problem = problem;
            }

            public double Compute(TravelingSalesPersonState state)
            {
                var remainGraph = _problem.Graph.DetachedVertices(state.Visited.ToArray());
                return GraphUtilities.KruskalMST(remainGraph).TotalLength();
            }
        }

        public void Run()
        {
            var problem = new TravelingSalespersonProblem(10, 1337);

            Measure.SearchPerformance(problem, new AStarSearch<TravelingSalesPersonState>(new TspMstHeuristic(problem)));
        }
    }
}
