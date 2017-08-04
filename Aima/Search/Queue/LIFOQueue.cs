using System.Collections.Generic;

namespace Aima.Search.Queue
{
    public class LIFOQueue<T> : LinkedList<T>, IQueue<T>
    {
        public bool IsEmpty => Count == 0;
        public new T First => Last.Value;

        public T Take()
        {
            var result = Last.Value;
            RemoveLast();
            return result;
        }

        public void Put(T element)
        {
            AddLast(element);
        }

        public void Put(IEnumerable<T> elements)
        {
            foreach (var element in elements)
            {
                Put(element);
            }
        }

        public new void Remove(T element)
        {
            ((LinkedList<T>) this).Remove(element);
        }
    }
}