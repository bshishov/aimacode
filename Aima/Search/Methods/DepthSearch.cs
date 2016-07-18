using Aima.AgentSystems;
using Aima.Search.Queue;

namespace Aima.Search.Methods
{
    public class DepthSearch<TState> : TreeSearch<TState, LIFOQueue<ITreeNode<TState>>>
    {
    }
}