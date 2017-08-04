using System.Collections.Generic;

namespace Aima.Search.Queue
{
    public class FIFOQueue<T> : LinkedList<T>, IQueue<T>
    {
        public bool IsEmpty => Count == 0;
        public new T First => ((LinkedList<T>) this).First.Value;

        public T Take()
        {
            var result = ((LinkedList<T>) this).First.Value;
            RemoveFirst();
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