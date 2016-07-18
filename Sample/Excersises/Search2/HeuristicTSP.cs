using Aima.Domain.TSP;
using Aima.Search.Methods;
using Aima.Search.Methods.Genetic;
using Aima.Search.Methods.Genetic.Operators;

namespace Sample.Excersises.Search2
{
    class HeuristicTSP : IExcersice
    {
        public void Run()
        {
            var problem = new TravelingSalespersonProblem(8, 1339);
            
            var heuristic = new TspMstHeuristic(problem);
            
            Measure.SearchPerformance(problem, new DepthSearch<TSPState>(), metric: new TspMetric());
            Measure.SearchPerformance(problem, new UniformCostSearch<TSPState>(), metric: new TspMetric());
            Measure.SearchPerformance(problem, new AStarSearch<TSPState>(heuristic), metric: new TspMetric());
            Measure.SearchPerformance(problem, new HillClimbing<TSPState>(new RandomEdgesExpander(heuristic)), metric: new TspMetric());

            var genetinc = new GeneticAlgorithm<uint, TSPState>(new TspGeneticTranslator(problem),
                new TspFitnessFunction(), 1000, new PmxOperator<uint>());

            Measure.SearchPerformance(problem, genetinc, metric: new TspMetric());
        }
    }
}
