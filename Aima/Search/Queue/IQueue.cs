using System.Collections.Generic;

namespace Aima.Search.Queue
{
    public interface IQueue<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }
        T First { get; }
        T Take();
        void Put(T element);
        void Put(IEnumerable<T> elements);
        void Remove(T element);
        void Clear();
    }
}