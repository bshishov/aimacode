using Aima.Domain.Obstacles;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class PathFinding : IExcersice
    {
        public void Run()
        {
            var problem = new PathFindingProblem();

            Measure.SearchPerformance(problem, new UniformCostSearch<RobotState>());
            Measure.SearchPerformance(problem, new BroadSearch<RobotState>());
            Measure.SearchPerformance(problem, new DepthGraphSearch<RobotState>());
        }
    }
}
