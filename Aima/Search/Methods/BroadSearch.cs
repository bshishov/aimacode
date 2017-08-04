using Aima.Search.Queue;

namespace Aima.Search.Methods
{
    public class BroadSearch<TState> : TreeSearch<TState, FIFOQueue<ITreeNode<TState>>>
    {
    }
}