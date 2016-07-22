using System;
using System.Collections.Generic;
using Aima.Search.Methods.Genetic.CrossoverOperators;
using Aima.Search.Methods.Genetic.MutationOperators;
using Aima.Search.Methods.Genetic.SelectionOperators;
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
        private readonly ISelectionOperator<TAlphabet, TState> _selectionOperator;

        public GeneticAlgorithm(IGeneticRepresentation<TAlphabet, TState> representation,
            ISelectionOperator<TAlphabet, TState> selectionOperator,
            IMutationOperator<TAlphabet> mutationOperator,
            IFitnessFunction<TState> fitnessFunction, 
            double targetFitness, 
            ICrossoverOperator<TAlphabet> crossoverOperator)
        {
            _representation = representation;
            _selectionOperator = selectionOperator;
            _fitnessFunction = fitnessFunction;
            _target = targetFitness;
            _crossoverOperator = crossoverOperator;
            _mutationOperator = mutationOperator;
        }

        public GeneticAlgorithm(IGeneticRepresentation<TAlphabet, TState> representation,
            ISelectionOperator<TAlphabet, TState> selectionOperator,
            IMutationOperator<TAlphabet> mutationOperator, 
            IFitnessFunction<TState> fitnessFunction, 
            double targetFitness)
            : this(representation,
                  selectionOperator,
                  mutationOperator, 
                  fitnessFunction, 
                  targetFitness, 
                  new DefaultCrossoverOperator<TAlphabet>())
        {
        }

        public GeneticAlgorithm(IGeneticRepresentation<TAlphabet, TState> representation,
            IMutationOperator<TAlphabet> mutationOperator,
            IFitnessFunction<TState> fitnessFunction,
            double targetFitness)
            : this(representation,
                  new FitnessProportionateSelection<TAlphabet, TState>(), 
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
                
                var selection = _selectionOperator.SelectPairs(population, _fitnessFunction);

                foreach (var pair in selection)
                {
                    var child = _crossoverOperator.Apply(pair.Item1.Genom, pair.Item2.Genom);

                    // Mutate with chance
                    if (rnd.NextDouble() < MutationChance)
                        _mutationOperator.Apply(child);

                    // create new individual
                    var newIndividual = new Individual<TAlphabet>
                    {
                        Fitness = _fitnessFunction.Compute(problem, _representation.FromGenome(child)),
                        Genom = child
                    };

                    // if individual fits enough
                    if (newIndividual.Fitness >= _target)
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
