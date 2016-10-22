using System;
using Aima.Search;
using Aima.Search.Methods.Genetic;

namespace Aima.Domain.Wells
{
    public class WellsFitnessFunction : IFitnessFunction<WellsState>
    {
        private readonly double _max;

        public WellsFitnessFunction(double max = 1.0)
        {
            _max = max;
        }

        public double Compute(IProblem<WellsState> problem, WellsState state)
        {
            var p = (WellsProblem)problem;
            return p.Value(state) / _max;
        }
    }
}
