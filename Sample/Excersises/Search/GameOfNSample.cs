using System;
using System.Linq;
using Aima.Search.Domain;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class GameOfNSample : IExcersice
    {
        public void Run()
        {
            var problem = new SlidingTilesProblem();
            Console.WriteLine(problem.InitialState);

            var searchMethod = new BroadGraphSearch<SlidingTilesState>();
            //var searchMethod = new DepthGraphSearch<State>();
            var solution = searchMethod.Search(problem);

            if (solution != null)
            {
                Console.WriteLine("Solution found in {0} steps", solution.Steps.Count());
                /*
                foreach (var action in solution.Steps)
                {
                    Console.WriteLine(action.Name);
                }

                var node = solution.ParentNode;
                while (node != null)
                {
                    if(node.Action != null)
                        Console.WriteLine(node.Action);
                    Console.WriteLine(node.State);
                    node = node.ParentNode;
                }*/
            }
            else
            {
                Console.WriteLine("No solution");
            }
        }
    }
}
