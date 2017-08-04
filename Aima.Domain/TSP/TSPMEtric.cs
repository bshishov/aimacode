using Aima.Search;
using Aima.Search.Metric;

namespace Aima.Domain.TSP
{
    public class TspMetric : ISeachMetric<TSPState>
    {
        public double Compute(IProblem<TSPState> problem, ISolution<TSPState> solution)
        {
            var p = (TravelingSalespersonProblem) problem;
            var state = solution.ParentNode.State;

            var pathCost = 0.0;
            for (var i = 0; i < state.Path.Count - 1; i++)
            {
                pathCost += p.Map.Distance(state.Path[i], state.Path[i + 1]);
            }
            return pathCost;
        }
    }
}