using System;
using System.Diagnostics;
using System.Linq;
using Aima.Search;
using Aima.Search.Metric;

namespace Sample
{
    public static class Measure
    {
        public static void SearchPerformance<TState>(IProblem<TState> problem, ISearch<TState> method, string name="", ISeachMetric<TState> metric = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Solving {0} using {1}", problem.GetType().Name, method.GetType().Name);
            }
            else
            {
                Console.WriteLine("Solving {0} using {1} ({2})", problem.GetType().Name, method.GetType().Name, name);
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var solution = method.Search(problem);
            stopwatch.Stop();

            if (solution != null)
            {
                Console.WriteLine("Solution found!");
                if(metric != null)
                    Console.WriteLine("Metric:\t{0:F} ({1})", metric.Compute(problem, solution), metric.GetType().Name);
                Console.WriteLine("Time:\t{0}", stopwatch.Elapsed);
                Console.WriteLine("Cost:\t{0:F}", solution.ParentNode.PathCost);

                var stepsCount = solution.Steps.Count();
                Console.WriteLine("Steps:\t{0}", stepsCount);
                Console.WriteLine("");

                Console.WriteLine("State:");
                Console.WriteLine(solution.ParentNode.State);


                Console.WriteLine("\nPath:");
                if (stepsCount < 100)
                {
                    foreach (var action in solution.Steps)
                    {
                        Console.Write(action.Name);
                        Console.Write(" > ");
                    }
                }
                else
                {
                    foreach (var action in solution.Steps.Take(100))
                    {
                        Console.Write(action.Name);
                        Console.Write(" > ");
                    }

                    Console.Write("... (rest of solution omitted)");
                }
                Console.Write("\n");
            }
            else
            {
                Console.WriteLine("No solution");
            }

            Console.WriteLine("\n");
        }
    }
}