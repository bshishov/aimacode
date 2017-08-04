using Aima.Search;
using Aima.Search.Metric;

namespace Aima.Domain.NQueens
{
    public class AttackedQueens : IHeuristic<QueensPath>, ISeachMetric<QueensPath>
    {
        public double Compute(QueensPath state)
        {
            var attacked = 0.0;
            for (var i = 0; i < state.Path.Count - 1; i++)
            {
                for (var j = i + 1; j < state.Path.Count; j++)
                {
                    if (!NQueensProblem.TestQueens(i, (int) state.Path[i], j, (int) state.Path[j]))
                        attacked += 1.0;
                }
            }

            return attacked;
        }

        public double Compute(IProblem<QueensPath> problem, ISolution<QueensPath> solution)
        {
            return Compute(solution.ParentNode.State);
        }
    }
}