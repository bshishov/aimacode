using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Aima.Search;
using Aima.Search.Metric;

namespace Sample
{
    public static class Measure
    {
        class SearchResult
        {
            public TimeSpan Time;
            public bool Success;
        }

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

        public static void SearchTries<TState>(int tries, IProblem<TState> problem, ISearch<TState> method, ISeachMetric<TState> metric, Predicate<double> successIfMetric, string tag = "", bool verbose = false)
        {
            Console.WriteLine("Solving {0} using {1} {2} time", problem.GetType().Name, method.GetType().Name, tries);
            var results = new List<SearchResult>();
            
            for (var i = 0; i < tries; i++)
            {
                if (verbose)
                {
                    if (string.IsNullOrEmpty(tag))
                        Console.WriteLine("Solving {0} using {1}", problem.GetType().Name, method.GetType().Name);
                    else
                        Console.WriteLine("Solving {0} using {1} ({2})", problem.GetType().Name, method.GetType().Name,
                            tag);
                }

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var solution = method.Search(problem);
                stopwatch.Stop();

                var success = false;

                if (solution != null)
                {
                    var mvalue = metric.Compute(problem, solution);
                    if (successIfMetric.Invoke(mvalue))
                    {
                        if(verbose)
                            Console.WriteLine("{0}: {1:F} (success)", metric.GetType().Name, mvalue);
                        success = true;
                    }
                    else
                    {
                        if (verbose)
                            Console.WriteLine("{0}: {1:F} (failrue)", metric.GetType().Name, mvalue);
                    }


                }
                
                results.Add(new SearchResult
                {
                    Success = success,
                    Time = stopwatch.Elapsed
                });

                
            }

            Console.WriteLine();
            Console.WriteLine("\tSuccess rate:\t{0:F}%", 100 * results.Count(r => r.Success) / (double)tries);
            Console.WriteLine("\tMin time:\t{0}", results.Min(r => r.Time));
            Console.WriteLine("\tMax time:\t{0}", results.Max(r => r.Time));
            Console.WriteLine("\tAvg time:\t{0}", TimeSpan.FromMilliseconds(results.Average(r => r.Time.Milliseconds)));
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}