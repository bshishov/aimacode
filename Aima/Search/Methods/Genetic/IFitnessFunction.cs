namespace Aima.Search.Methods.Genetic
{
    public interface IFitnessFunction<TState>
    {
        double Compute(IProblem<TState> problem, TState state);
    }
}