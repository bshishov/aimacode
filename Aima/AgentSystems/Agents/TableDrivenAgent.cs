using System.Collections.Generic;

namespace Aima.AgentSystems.Agents
{
    internal class TableDrivenAgent<T> : IAgent<T>
        where T : IPerception
    {
        private readonly List<T> _percepts = new List<T>();
        private readonly Dictionary<IEnumerable<T>, IAction> _table = new Dictionary<IEnumerable<T>, IAction>();

        public IAction Execute(T perception)
        {
            _percepts.Add(perception);

            IAction action;
            if (_table.TryGetValue(_percepts, out action))
                return action;

            return null;
        }

        public void Add(IEnumerable<T> percepts, IAction action)
        {
            _table.Add(percepts, action);
        }
    }
}