using Aima.Search;
using Aima.Utilities;

namespace Aima.Domain.TSP
{
    public class TspMstHeuristic : IHeuristic<TSPState>
    {
        private readonly TravelingSalespersonProblem _problem;

        public TspMstHeuristic(TravelingSalespersonProblem problem)
        {
            _problem = problem;
        }

        public double Compute(TSPState state)
        {
            var remainGraph = _problem.Map.DetachedVertices(state.Path);
            return GraphUtilities.KruskalMST(remainGraph).TotalLength();
        }
    }
}