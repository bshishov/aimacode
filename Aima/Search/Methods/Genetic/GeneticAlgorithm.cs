using System;
using System.Collections.Generic;
using System.Linq;
using Aima.Search.Methods.Genetic.CrossoverOperators;
using Aima.Search.Methods.Genetic.MutationOperators;
using Aima.Utilities;

namespace Aima.Search.Methods.Genetic
{
    public class GeneticAlgorithm<TAlphabet, TState> : ISearch<TState>
    {
        public double MutationChance = 0.01;
        public double Epsilon = Double.Epsilon;
        public int PopulationSize = 10;
        public int MaxPopulations = 10000;

        private readonly ICrossoverOperator<TAlphabet> _crossoverOperator;
        private readonly IGeneticRepresentation<TAlphabet, TState> _representation;
        private readonly IFitnessFunction<TState> _fitnessFunction;
        private readonly double _target;
        private readonly IMutationOperator<TAlphabet> _mutationOperator;

        public GeneticAlgorithm(IGeneticRepresentation<TAlphabet, TState> representation, 
            IMutationOperator<TAlphabet> mutationOperator,
            IFitnessFunction<TState> fitnessFunction, 
            double targetFitness, 
            ICrossoverOperator<TAlphabet> crossoverOperator)
        {
            _representation = representation;
            _fitnessFunction = fitnessFunction;
            _target = targetFitness;
            _crossoverOperator = crossoverOperator;
            _mutationOperator = mutationOperator;
        }

        public GeneticAlgorithm(IGeneticRepresentation<TAlphabet, TState> representation, 
            IMutationOperator<TAlphabet> mutationOperator, 
            IFitnessFunction<TState> fitnessFunction, 
            double targetFitness)
            : this(representation, 
                  mutationOperator, 
                  fitnessFunction, 
                  targetFitness, 
                  new DefaultCrossoverOperator<TAlphabet>())
        {
        }

        public ISolution<TState> Search(IProblem<TState> problem)
        {
            var rnd = new Random();
            var population = new List<Individual<TAlphabet>>();
            for (var i = 0; i < PopulationSize; i++)
            {
                var rndGenom = _representation.RandomGenome();
                population.Add(new Individual<TAlphabet>()
                {
                    Fitness = _fitnessFunction.Compute(problem, _representation.FromGenome(rndGenom)),
                    Genom = rndGenom
                });
            }

            var populationN = 0;

            while (populationN++ < MaxPopulations)
            {
                var newPopulation = new List<Individual<TAlphabet>>();
                var sumFitness = population.Sum(i => i.Fitness);

                for (var i = 0; i < PopulationSize; i++)
                {
                    Individual<TAlphabet> x = null, y = null;

                    // Random selection of 1st parent
                    while (x == null)
                    {
                        foreach (var individual in population)
                        {
                            var chance = individual.Fitness / sumFitness;
                            if (rnd.NextDouble() < chance)
                            {
                                x = individual;
                                break;
                            }
                        }
                    }

                    // Random selection of 2nd parent
                    while (y == null)
                    {
                        foreach (var individual in population)
                        {
                            var chance = individual.Fitness / sumFitness;
                            if (rnd.NextDouble() < chance)
                            {
                                y = individual;
                                break;
                            }
                        }
                    }

                    var child = _crossoverOperator.Apply(x.Genom, y.Genom);

                    // Mutate with chance
                    if (rnd.NextDouble() < MutationChance)
                        _mutationOperator.Apply(child);

                    var newIndividual = new Individual<TAlphabet>
                    {
                        Fitness = _fitnessFunction.Compute(problem, _representation.FromGenome(child)),
                        Genom = child
                    };

                    if(newIndividual.Fitness >= _target)
                        return new Solution<TState>(new TreeNode<TState>(_representation.FromGenome(newIndividual.Genom)));

                    newPopulation.Add(newIndividual);
                }

                population = newPopulation;
            }

            return new Solution<TState>(new TreeNode<TState>(
                _representation.FromGenome(
                    population.MaxBy(i=>i.Fitness).Genom
                    )));
        }
    }
}
