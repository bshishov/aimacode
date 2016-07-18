using Aima.Domain.Obstacles;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class GeometryPath : IExcersice
    {
        public void Run()
        {
            var problem = new ObstaclesProblem();

            Measure.SearchPerformance(problem, new UniformCostSearch<RobotState>());
            Measure.SearchPerformance(problem, new BroadSearch<RobotState>());
            Measure.SearchPerformance(problem, new DepthGraphSearch<RobotState>());
        }
    }
}
