using Aima.AgentSystems;
using Aima.Search.Queue;

namespace Aima.Search.Methods
{
    public class BroadGraphSearch<TState> : GraphSearch<TState, FIFOQueue<ITreeNode<TState>>>
    {
    }
}