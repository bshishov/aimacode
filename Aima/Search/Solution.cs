using System.Collections.Generic;
using Aima.AgentSystems;

namespace Aima.Search
{
    public class Solution<TState> : ISolution<TState>
    {
        public static Solution<TState> CutOff = new Solution<TState>(null);
        private readonly List<TState> _states = new List<TState>();
        private readonly List<IAction> _steps = new List<IAction>();

        public Solution(ITreeNode<TState> node)
        {
            var n = node;
            while (n != null)
            {
                if (n.Action != null)
                {
                    _steps.Add(n.Action);
                    _states.Add(n.State);
                }
                n = n.ParentNode;
            }

            _steps.Reverse();
            _states.Reverse();
            ParentNode = node;
        }

        public IEnumerable<IAction> Steps => _steps;
        public IEnumerable<TState> States => _states;
        public ITreeNode<TState> ParentNode { get; }
    }
}