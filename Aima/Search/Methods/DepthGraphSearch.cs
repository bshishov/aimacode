using Aima.AgentSystems;
using Aima.Search.Queue;

namespace Aima.Search.Methods
{
    public class DepthGraphSearch<TState> : GraphSearch<TState, LIFOQueue<ITreeNode<TState>>>
    {
    }
}