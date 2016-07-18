using System;
using System.Linq;
using Aima.Domain.SlidingTiles;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class SlidingTiles : IExcersice
    {
        public void Run()
        {
            var problem = new SlidingTilesProblem(3, 123124);
            Console.WriteLine(problem.InitialState);

            Measure.SearchPerformance(problem, new DepthGraphSearch<SlidingTilesState>());
            Measure.SearchPerformance(problem, new BroadGraphSearch<SlidingTilesState>());
        }
    }
}
