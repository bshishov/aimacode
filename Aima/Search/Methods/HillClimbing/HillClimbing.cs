﻿using System;
using Aima.Search.NodeExpanders;

namespace Aima.Search.Methods.HillClimbing
{
    /// <summary>
    ///     The hill climbing algorithm, which is the most basic local search technique
    ///     Finds local minimum of heuristic function in state space
    /// </summary>
    public class HillClimbing<TState> : HeuristicSearch<TState>
    {
        private readonly IHillClimbingStrategy<TState> _strategy;

        public HillClimbing(IHillClimbingStrategy<TState> strategy, HeuristicNodeExpander<TState> expander)
            : base(expander)
        {
            _strategy = strategy;
            _strategy.SearchNodeChanged += node => SearchNodeChanged?.Invoke(node);
        }

        public HillClimbing(IHillClimbingStrategy<TState> strategy, IHeuristic<TState> heuristic)
            : this(strategy, new DefaultHeuristicNodeExpander<TState>(heuristic))
        {
        }

        public HillClimbing(IHillClimbingStrategy<TState> strategy, Func<TState, double> heuristic)
            : this(strategy, new DefaultHeuristicNodeExpander<TState>(heuristic))
        {
        }

        public HillClimbing(HeuristicNodeExpander<TState> expander)
            : this(new SteepestAscentHillClimbingStrategy<TState>(),
                expander)
        {
        }

        public HillClimbing(Func<TState, double> heuristic)
            : this(new SteepestAscentHillClimbingStrategy<TState>(),
                new DefaultHeuristicNodeExpander<TState>(heuristic))
        {
        }

        public HillClimbing(IHeuristic<TState> heuristic)
            : this(new SteepestAscentHillClimbingStrategy<TState>(),
                new DefaultHeuristicNodeExpander<TState>(heuristic))
        {
        }

        public override ISolution<TState> Search(IProblem<TState> problem)
        {
            var current = new HeuristicTreeNode<TState>(problem.InitialState,
                Expander.ComputeHeuristic(problem.InitialState));
            return new Solution<TState>(_strategy.Climb(current, problem, Expander));
        }

        public override event Action<ITreeNode<TState>> SearchNodeChanged;
    }
}