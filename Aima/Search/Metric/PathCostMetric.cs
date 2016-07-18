namespace Aima.Search.Metric
{
    public class PathCostMetric<TState> : ISeachMetric<TState>
    {
        public double Compute(IProblem<TState> problem, ISolution<TState> solution)
        {
            return solution.ParentNode.PathCost;
        }
    }
}