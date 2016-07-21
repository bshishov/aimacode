using System;
using System.Collections.Generic;
using System.Linq;
using Aima.Search.NodeExpanders;

namespace Aima.Search.Methods
{
    public class RecursiveBestFirstSearch<TState> : HeuristicSearch<TState>
    {
        public class RBFSExpander<T> : HeuristicNodeExpander<T>
        {
            public RBFSExpander(IHeuristic<T> heuristic)
                : base(heuristic)
            {
            }

            public RBFSExpander(Func<T, double> heuristic)
                : base(heuristic)
            {
            }

            public override IEnumerable<HeuristicTreeNode<T>> Expand(HeuristicTreeNode<T> node, IProblem<T> problem)
            {
                var successors = problem.SuccessorFn(node.State);
                if (successors == null)
                    yield break;

                foreach (var successor in successors)
                {
                    var action = successor.Item1;
                    var state = successor.Item2;

                    var f = Math.Max(node.PathCost + ComputeHeuristic(state), node.F); // like in book, not optimal solution (but close)
                    //var f = Math.Max(node.PathCost + ComputeHeuristic(state), node.PathCost + ComputeHeuristic(node.State)); //custom (node.F remains unchanged from search), better than A* in some cases (WTF)
                    //var f = node.PathCost + ComputeHeuristic(state); // can be better than A* (WTF) in some cases
                    
                    yield return new HeuristicTreeNode<T>(node, state, f, action,
                        problem.Cost(action, node.State, state));
                }
            }
        }

        public RecursiveBestFirstSearch(IHeuristic<TState> heuristic) 
            : base(new RBFSExpander<TState>(heuristic))
        {
        }

        public RecursiveBestFirstSearch(Func<TState, double> heuristic) 
            : base(new RBFSExpander<TState>(heuristic))
        {
        }

        public override ISolution<TState> Search(IProblem<TState> problem)
        {
            return new Solution<TState>(
                RBFS(problem, new HeuristicTreeNode<TState>(problem.InitialState, Expander.ComputeHeuristic(problem.InitialState)), Double.PositiveInfinity).Item1);
        }

        private Tuple<HeuristicTreeNode<TState>, double> RBFS(IProblem<TState> problem, HeuristicTreeNode<TState> node, double fLimit)
        {
            if (problem.GoalTest(node.State))
                return new Tuple<HeuristicTreeNode<TState>, double>(node, fLimit);

            var successors = Expand(node, problem).ToList();
            if(successors.Count == 0)
                return new Tuple<HeuristicTreeNode<TState>, double>(null, Double.PositiveInfinity);

            successors.Sort((n1, n2) => n1.F.CompareTo(n2.F));

            while (true)
            {
                var best = successors[0];
                if (best.F > fLimit)
                    return new Tuple<HeuristicTreeNode<TState>, double>(null, best.F);

                // second lowest f-value among heuristics
                var fSibling = successors.Count == 1 ? double.PositiveInfinity : successors[1].F;
                
                var result = RBFS(problem, best, Math.Min(fLimit, fSibling));
                best.F = result.Item2;

                if (result.Item1 != null)
                    return result;

                successors.Sort((n1, n2) => n1.F.CompareTo(n2.F));
            }
        }
    }
}
