using Aima.Domain.TSP;
using Aima.Search.Methods;

namespace Sample.Excersises.Search2
{
    class HeuristicTSP : IExcersice
    {
        public void Run()
        {
            var problem = new TravelingSalespersonProblem(10, 1337);
            
            var heuristic = new TspMstHeuristic(problem);
            //Measure.SearchPerformance(problem, new AStarSearch<TSPState>(heuristic));
            Measure.SearchPerformance(problem, new HillClimbing<TSPState>(new RandomEdgesExpander(heuristic)));
        }
    }
}
