using Aima.Search;
using Aima.Search.Methods.Genetic;

namespace Aima.Domain.TSP
{
    public class TspFitnessFunction : IFitnessFunction<TSPState>
    {
        public double Compute(IProblem<TSPState> problem, TSPState state)
        {
            var p = (TravelingSalespersonProblem)problem;

            var pathCost = 0.0;
            for (var i = 0; i < state.Path.Count - 1; i++)
            {
                pathCost += p.Map.Distance(state.Path[i], state.Path[i + 1]);
            }
            return 100 / pathCost;
        }
    }
}