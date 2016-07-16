using System;
using Aima.Search.Domain;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class TravellingSalesPerson : IExcersice
    {
        public void Run()
        {
            //var searchMethod = new DepthLimitedSearch<TravelingSalesPersonState>(20);
            var searchMethod = new DepthGraphSearch<TravelingSalesPersonState>();
            //var searchMethod = new UniformCostSearch<TravelingSalesPersonState>();
            var solution = searchMethod.Search(new TravelingSalespersonProblem(20));

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
