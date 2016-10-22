using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aima.Domain.Wells;
using Aima.Search.Methods.Genetic;
using Aima.Search.Methods.Genetic.CrossoverOperators;
using Aima.Search.Methods.Genetic.MutationOperators;
using Aima.Search.Methods.Genetic.SelectionOperators;

namespace Sample.Excersises
{
    class WellsSearch : IExcersice
    {
        public void Run()
        {
            var problem = new WellsProblem(10, 10, 10, FieldFunction);
            var fittness = new WellsFitnessFunction();
            var metric = new WellsMetric();
            var rnd = new Random();

            var geneticAlgorithm = new GeneticAlgorithm<int, WellsState>(
                new WellsGeneticRepresentation(problem), 
                new FitnessProportionateSelection<int, WellsState>(),
                new RandomValueMutationOperator<int>((genome) => problem.ToIndex(rnd.Next(problem.Width), rnd.Next(problem.Height))),
                fittness, 0.9, new DefaultCrossoverOperator<int>())
            {
                MaxPopulations = 100000,
                MutationChance = 0.03,
            };

            geneticAlgorithm.SearchNodeChanged += node => Console.WriteLine(node.State);
            Measure.SearchPerformance(problem, geneticAlgorithm, metric: metric);

            //Measure.SearchTries(100, problem, geneticAlgorithm, metric, q => q < 1.0);
        }

        private double FieldFunction(int x, int y)
        {
            return y * y;
            //Math.Cos(x/2.0) + Math.Sin(y/2.0) + 2.0;
        }
    }
}
