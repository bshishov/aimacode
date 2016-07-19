using Aima.Domain.TSP;
using Aima.Search.Methods;
using Aima.Search.Methods.Genetic;
using Aima.Search.Methods.Genetic.Operators;
using Aima.Search.Methods.HillClimbing;
using Aima.Search.Methods.SimulatedAnnealing;

namespace Sample.Excersises.Search2
{
    class HeuristicTSP : IExcersice
    {
        public void Run()
        {
            //var problem = new TravelingSalespersonProblem(11, 1339);
            var problem = new TravelingSalespersonProblem(10, 101);

            var heuristic = new TspMstHeuristic(problem);

            //Measure.SearchPerformance(problem, new DepthSearch<TSPState>(), metric: new TspMetric());
            //Measure.SearchPerformance(problem, new UniformCostSearch<TSPState>(), metric: new TspMetric());

            Measure.SearchPerformance(problem, new AStarSearch<TSPState>(heuristic), metric: new TspMetric());
            Measure.SearchPerformance(problem, new RecursiveBestFirstSearch<TSPState>(heuristic), metric: new TspMetric());

            

            
            /*
            Measure.SearchPerformance(problem, new SimulatedAnnealing<TSPState>(
                new RandomEdgesExpander(heuristic)), "Simulated annealing", metric: new TspMetric());

            Measure.SearchPerformance(problem, new HillClimbing<TSPState>(
                new SteepestAscentHillClimbingStrategy<TSPState>(),
                new RandomEdgesExpander(heuristic)), "Steepest ascent", metric: new TspMetric());

            Measure.SearchPerformance(problem, new HillClimbing<TSPState>(
                new RandomUntilBetterHilleClimbingStrategy<TSPState>(), 
                new RandomEdgesExpander(heuristic)), "Random until better", metric: new TspMetric());

            Measure.SearchPerformance(problem, new HillClimbing<TSPState>(
                new StochasticHillClimbingStrategy<TSPState>(), 
                new RandomEdgesExpander(heuristic)), "Stochastic", metric: new TspMetric());

            var genetinc = new GeneticAlgorithm<uint, TSPState>(new TspGeneticTranslator(problem),
                new TspFitnessFunction(), 1000, new PmxOperator<uint>());

            Measure.SearchPerformance(problem, genetinc, metric: new TspMetric());*/
        }
    }
}
