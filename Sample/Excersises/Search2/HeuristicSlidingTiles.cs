using Aima.Domain.SlidingTiles;
using Aima.Search.Methods;

namespace Sample.Excersises.Search2
{
    class HeuristicSlidingTiles : IExcersice
    {
        public void Run()
        {
            var problem = new SlidingTilesProblem(3, 123124);
            //var problem = new SlidingTilesProblem(4, 1340);

            Measure.SearchPerformance(problem, new BroadGraphSearch<SlidingTilesState>(), "Broad graph search");

            Measure.SearchPerformance(problem, new AStarSearch<SlidingTilesState>(SlidingTilesHeuristics.ManhattanDistance), "Manhattan Distance heuristic");
            Measure.SearchPerformance(problem, new AStarSearch<SlidingTilesState>(SlidingTilesHeuristics.H1H2Compound), "Compound heuristics");
            Measure.SearchPerformance(problem, new AStarSearch<SlidingTilesState>(SlidingTilesHeuristics.NMaxSwap), "NMax Swap (Gaschnig's) heuristic");
            Measure.SearchPerformance(problem, new AStarSearch<SlidingTilesState>(SlidingTilesHeuristics.MisplacesTiles), "Misplaced tiles heuristic");
            Measure.SearchPerformance(problem, new AStarSearch<SlidingTilesState>(SlidingTilesHeuristics.LinearConflict), "Linear conflict heuristic");
        }
    }
}
