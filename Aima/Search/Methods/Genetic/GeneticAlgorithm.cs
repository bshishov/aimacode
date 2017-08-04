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
        private readonly ICrossoverOperator<TAlphabet> _crossoverOperator;
        private readonly IMutationOperator<TAlphabet> _mutationOperator;
        private readonly IGeneticRepresentation<TAlphabet, TState> _representation;
        private readonly ISelectionOperator<TAlphabet, TState> _selectionOperator;
        private readonly double _target;
        public double Epsilon = double.Epsilon;
        public int MaxPopulations = 10000;
        public double MutationChance = 0.01;
        public int PopulationSize = 10;

        public GeneticAlgorithm(IGeneticRepresentation<TAlphabet, TState> representation,
            ISelectionOperator<TAlphabet, TState> selectionOperator,
            IMutationOperator<TAlphabet> mutationOperator,
            IFitnessFunction<TState> fitnessFunction,
            double targetFitness,
            ICrossoverOperator<TAlphabet> crossoverOperator)
        {
            _representation = representation;
            _selectionOperator = selectionOperator;
            FitnessFunction = fitnessFunction;
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

        public int CurrentPopulation { get; private set; }

        public IFitnessFunction<TState> FitnessFunction { get; }

        public ISolution<TState> Search(IProblem<TState> problem)
        {
            var rnd = new Random();
            var population = new List<Individual<TAlphabet>>();
            for (var i = 0; i < PopulationSize; i++)
            {
                var rndGenom = _representation.RandomGenome();
                population.Add(new Individual<TAlphabet>
                {
                    Fitness = FitnessFunction.Compute(problem, _representation.FromGenome(rndGenom)),
                    Genom = rndGenom
                });
            }

            CurrentPopulation = 0;

            while (CurrentPopulation++ < MaxPopulations)
            {
                var newPopulation = new List<Individual<TAlphabet>>();

                var selection = _selectionOperator.SelectPairs(population, FitnessFunction);

                foreach (var pair in selection)
                {
                    var child = _crossoverOperator.Apply(pair.Item1.Genom, pair.Item2.Genom);

                    // Mutate with chance
                    if (rnd.NextDouble() < MutationChance)
                        _mutationOperator.Apply(child);

                    // create new individual
                    var newIndividual = new Individual<TAlphabet>
                    {
                        Fitness = FitnessFunction.Compute(problem, _representation.FromGenome(child)),
                        Genom = child
                    };

                    // if individual fits enough
                    if (newIndividual.Fitness >= _target)
                    {
                        var winner = new TreeNode<TState>(_representation.FromGenome(newIndividual.Genom));
                        SearchNodeChanged?.Invoke(winner);
                        return
                            new Solution<TState>(winner);
                    }

                    newPopulation.Add(newIndividual);
                }

                population = newPopulation;

                var best = population.MaxBy(ind => ind.Fitness);
                // Notify that new child is selected
                SearchNodeChanged?.Invoke(new TreeNode<TState>(_representation.FromGenome(best.Genom)));
            }

            return new Solution<TState>(new TreeNode<TState>(
                _representation.FromGenome(
                    population.MaxBy(i => i.Fitness).Genom
                    )));
        }

        public event Action<ITreeNode<TState>> SearchNodeChanged;
    }
}