using System;
using System.Collections.Generic;
using System.Linq;
using Aima.Search.Methods.Genetic.Operators;
using Aima.Utilities;

namespace Aima.Search.Methods.Genetic
{
    public class GeneticAlgorithm<TAlphabet, TState> : ISearch<TState>
    {
        public class Individual
        {
            public TAlphabet[] Genom;
            public double Fitness;
        }

        public double MutationChance = 0.01;
        public double Epsilon = 0.01;
        public int PopulationSize = 10;
        public int MaxPopulations = 10000;

        private readonly ICrossoverOperator<TAlphabet> _crossoverOperator;
        private readonly IGeneticTranslator<TAlphabet, TState> _translator;
        private readonly IFitnessFunction<TState> _fitnessFunction;
        private readonly double _target;

        public GeneticAlgorithm(IGeneticTranslator<TAlphabet, TState> translator, IFitnessFunction<TState> fitnessFunction, double targetFitness, ICrossoverOperator<TAlphabet> crossoverOperator)
        {
            _translator = translator;
            _fitnessFunction = fitnessFunction;
            _target = targetFitness;
            _crossoverOperator = crossoverOperator;
        }

        public GeneticAlgorithm(IGeneticTranslator<TAlphabet, TState> translator, IFitnessFunction<TState> fitnessFunction, double targetFitness)
            : this(translator, fitnessFunction, targetFitness, new DefaultCrossoverOperator<TAlphabet>())
        {
        }

        public ISolution<TState> Search(IProblem<TState> problem)
        {
            var rnd = new Random();
            var population = new List<Individual>();
            for (var i = 0; i < PopulationSize; i++)
            {
                var rndGenom = _translator.RandomGenom();
                population.Add(new Individual()
                {
                    Fitness = _fitnessFunction.Compute(problem, _translator.FromGenom(rndGenom)),
                    Genom = rndGenom
                });
            }

            var populationN = 0;

            while (populationN++ < MaxPopulations)
            {
                var newPopulation = new List<Individual>();
                var sumFitness = population.Sum(i => i.Fitness);

                for (var i = 0; i < PopulationSize; i++)
                {
                    Individual x = null, y = null;

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
                        _translator.Mutate(child);

                    var newIndividual = new Individual
                    {
                        Fitness = _fitnessFunction.Compute(problem, _translator.FromGenom(child)),
                        Genom = child
                    };

                    if(Math.Abs(_target - newIndividual.Fitness) < Epsilon)
                        return new Solution<TState>(new TreeNode<TState>(_translator.FromGenom(newIndividual.Genom)));

                    newPopulation.Add(newIndividual);
                }

                population = newPopulation;
            }

            return new Solution<TState>(new TreeNode<TState>(
                _translator.FromGenom(
                    population.MaxBy(i=>i.Fitness).Genom
                    )));
        }
    }
}
