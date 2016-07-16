using System;
using Aima.Search.Domain;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class GeometryPath : IExcersice
    {
        public void Run()
        {
            var searchMethod = new UniformCostSearch<RobotState>();
            var solution = searchMethod.Search(new ObstaclesProblem());

            if (solution != null)
            {
                Console.WriteLine("SOLUTION FOUND!");
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
