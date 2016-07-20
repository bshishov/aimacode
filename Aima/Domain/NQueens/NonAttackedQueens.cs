using System;
using Aima.Search;
using Aima.Search.Methods.Genetic;
using Aima.Search.Metric;

namespace Aima.Domain.NQueens
{
    public class NonAttackedQueens : IFitnessFunction<QueensPath>, ISeachMetric<QueensPath>
    {
        private readonly AttackedQueens _heuristic;

        public NonAttackedQueens()
        {
            _heuristic = new AttackedQueens();
        }

        public double Compute(IProblem<QueensPath> problem, QueensPath state)
        {
            return ((NQueensProblem)problem).N - _heuristic.Compute(state);
        }

        public double Compute(IProblem<QueensPath> problem, ISolution<QueensPath> solution)
        {
            return Compute(problem, solution.ParentNode.State);
        }
    }
}
