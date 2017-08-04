using System;

namespace Aima.Search
{
    public interface ISearchNodeNotifier<TState>
    {
        event Action<ITreeNode<TState>> SearchNodeChanged;
    }
}