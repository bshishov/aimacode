using Aima.Search;

namespace Aima.Domain.Obstacles
{
    public class StraightDistance : IHeuristic<RobotState>
    {
        private readonly PathFindingProblem _problem;

        public StraightDistance(PathFindingProblem problem)
        {
            _problem = problem;
        }

        public double Compute(RobotState state)
        {
            return _problem.DistanceToTarget(state);
        }
    }
}
