using Aima.Domain.Obstacles;
using Aima.Search.Methods;
using Aima.Search.Methods.HillClimbing;
using Aima.Search.NodeExpanders;

namespace Sample.Excersises.Search2
{
    class PathFinding : IExcersice
    {
        public void Run()
        {
            var problem = new PathFindingProblem();

            Measure.SearchPerformance(problem, new UniformCostSearch<RobotState>());
            Measure.SearchPerformance(problem, new BroadSearch<RobotState>());
            Measure.SearchPerformance(problem, new DepthGraphSearch<RobotState>());


            // HEURISTIC
            var heuristic = new StraightDistance(problem);
            Measure.SearchPerformance(problem, new HillClimbing<RobotState>(new KDepthHillClimbingStrategy<RobotState>(3), heuristic), "DLS(k) Strategy");
            Measure.SearchPerformance(problem, new HillClimbing<RobotState>(new KDepthHeuristicNodeExpander<RobotState>(3, heuristic)), "DLS(k) Expander");
            Measure.SearchPerformance(problem, new RecursiveBestFirstSearch<RobotState>(heuristic));
            Measure.SearchPerformance(problem, new AStarSearch<RobotState>(heuristic));
        }
    }
}
