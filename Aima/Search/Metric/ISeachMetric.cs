namespace Aima.Search.Metric
{
    public interface ISeachMetric<TState>
    {
        double Compute(IProblem<TState> problem, ISolution<TState> solution);
    }
}