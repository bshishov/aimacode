using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aima.Search
{
    public interface ISearchNodeNotifier<out TState>
    {
        event Action<ITreeNode<TState>> SearchNodeChanged;
    }
}
