using System.Collections.Generic;
using System.Linq;
using Aima.AgentSystems;
using Aima.Search.Queue;

namespace Aima.Search.Methods
{
    public class AStarSearch<TProblem, TState> : HeuristicSearch<TProblem, TState>
        where TState : IState
        where TProblem : IProblem<TState>
    {
        public AStarSearch(IHeuristic<TProblem, TState> heuristic) : base(heuristic)
        {
        }

        public override ISolution<TState> Search(IProblem<TState> problem)
        {
            var frontier = new List<HeuristicTreeNode<TState>> {new HeuristicTreeNode<TState>(problem.InitialState)};

            while (true)
            {
                if (frontier.Count == 0)
                    return null;
                
                var node = frontier.First();
                frontier.RemoveAt(0);

                if (problem.GoalTest(node.State))
                    return new Solution<TState>(node);
                
                frontier.AddRange(Expand(node, (TProblem)problem));
                frontier.Sort((n1, n2) => n1.Heuristic.CompareTo(n2.Heuristic));
            }
        }

        private IEnumerable<HeuristicTreeNode<TState>> Expand(ITreeNode<TState> node, TProblem problem)
        {
            var successors = problem.SuccessorFn(node.State);
            if (successors == null)
                yield break;

            foreach (var successor in successors)
            {
                var action = successor.Item1;
                var state = successor.Item2;
                var newNode = new HeuristicTreeNode<TState>(node, state, action,
                    problem.Cost(action, node.State, state))
                {
                    Heuristic = node.PathCost + ComputeHeuristic(problem, state)
                };
                yield return newNode;
            }
        }
    }
}
