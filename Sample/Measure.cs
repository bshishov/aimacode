using System;
using System.Diagnostics;
using System.Linq;
using Aima.AgentSystems;
using Aima.Search;

namespace Sample
{
    public static class Measure
    {
        public static void SearchPerformance<TState>(IProblem<TState> problem, ISearch<TState> method, string name="")
            where TState : IState
        {
            if(!string.IsNullOrEmpty(name))
                Console.WriteLine(name);

            Console.WriteLine("Solving problem {0} using method {1}", problem.GetType().Name, method.GetType().Name);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var solution = method.Search(problem);
            stopwatch.Stop();

            if (solution != null)
            {
                Console.WriteLine("Solution found!");
                Console.WriteLine("Time:\t{0}", stopwatch.Elapsed);
                Console.WriteLine("Cost:\t{0:F}", solution.ParentNode.PathCost);

                var stepsCount = solution.Steps.Count();
                Console.WriteLine("Steps:\t{0}", stepsCount);
                Console.WriteLine("");

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