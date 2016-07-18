using System;
using Aima.Domain.River;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class RiverSample : IExcersice
    {
        public void Run()
        {
            var problem = new RiverProblem();
            var searchMethod = new IterativeDeepingSearch<RiverState>();
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
