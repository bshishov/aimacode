using System.Linq;

namespace Aima.Search.Metric
{
    public class StepsCountMetric<TState> : ISeachMetric<TState>
    {
        public double Compute(IProblem<TState> problem, ISolution<TState> solution)
        {
            return solution.Steps.Count();
        }
    }
}