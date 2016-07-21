using Aima.Domain.NQueens;
using Aima.Search.Methods;
using Aima.Search.Methods.Genetic;
using Aima.Search.Methods.Genetic.CrossoverOperators;
using Aima.Search.Methods.Genetic.MutationOperators;
using Aima.Search.Methods.HillClimbing;

namespace Sample.Excersises.Search2
{
    class NQueens : IExcersice
    {
        public void Run()
        {
            var problem = new NQueensProblem(7);
            var attackedQueens = new AttackedQueens();
            var nonAttackedQueens = new NonAttackedQueens();
            
            Measure.SearchPerformance(problem, new AStarSearch<QueensPath>(attackedQueens), metric: attackedQueens);
            Measure.SearchPerformance(problem, new RecursiveBestFirstSearch<QueensPath>(attackedQueens), metric: attackedQueens);
            Measure.SearchPerformance(problem, new RecursiveBestFirstSearch<QueensPath>(attackedQueens), metric: attackedQueens);
            Measure.SearchPerformance(problem, new HillClimbing<QueensPath>(attackedQueens), metric: attackedQueens);

            var geneticAlgorithm = new GeneticAlgorithm<uint, QueensPath>(
                new QueensGeneticRepresentation(problem.N),
                new RandomSwapMutationOperator<uint>(), 
                nonAttackedQueens, problem.N, new PmxOperator<uint>())
            {
                MaxPopulations = 10000
            };
            Measure.SearchPerformance(problem, geneticAlgorithm, metric: attackedQueens);
            
            Measure.SearchTries(100, problem, geneticAlgorithm, attackedQueens, q => q < 1.0);
            Measure.SearchTries(100, problem, new HillClimbing<QueensPath>(new StochasticHillClimbingStrategy<QueensPath>(), attackedQueens), attackedQueens, q => q < 1.0, "Stochastic climbing");
        }
    }
}
