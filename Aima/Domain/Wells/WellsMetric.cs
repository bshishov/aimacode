using Aima.Search;
using Aima.Search.Metric;

namespace Aima.Domain.Wells
{
    public class WellsMetric : ISeachMetric<WellsState>
    {
        public double Compute(IProblem<WellsState> problem, ISolution<WellsState> solution)
        {
            var p = (WellsProblem) problem;
            return p.Value(solution.ParentNode.State);
        }
    }
}
