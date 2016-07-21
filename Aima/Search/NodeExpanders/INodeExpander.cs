using System.Collections.Generic;

namespace Aima.Search.NodeExpanders
{
    public interface INodeExpander<TNode, TState>
        where TNode : ITreeNode<TState>
    {
        IEnumerable<TNode> Expand(TNode node, IProblem<TState> problem);
    }
}