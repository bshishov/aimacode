using System;
using System.Linq;
using Aima.Search;
using Aima.Search.Domain;
using Aima.Search.Methods;
using Aima.Utilities;

namespace Sample.Excersises.Search2
{
    class HeuristicTSP : IExcersice
    {
        class TspMstHeuristic : IHeuristic<TravelingSalespersonProblem, TravelingSalesPersonState>
        {
            public double Compute(TravelingSalespersonProblem problem, TravelingSalesPersonState state)
            {
                var remainGraph = problem.Graph.DetachedVertices(state.Visited.ToArray());
                return GraphUtilities.KruskalMST(remainGraph).TotalLength();
            }
        }

        public void Run()
        {
            var problem = new TravelingSalespersonProblem(20, 1);
            var searchMethod = new AStarSearch<TravelingSalespersonProblem, TravelingSalesPersonState>(new TspMstHeuristic());
            var solution = searchMethod.Search(problem);

            if (solution != null)
            {
                foreach (var action in solution.Steps)
                {
                    Console.WriteLine(action.Name);
                }
            }
            else
            {
                Console.WriteLine("No solution");
            }
        }
    }
}
