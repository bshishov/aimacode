using System.Collections;
using System.Collections.Generic;

namespace Aima.Search.Queue
{
    class SortedQueue<T> : IQueue<T>
    {
        private readonly IComparer<T> _comparer;
        private readonly List<T> _list;
        
        public SortedQueue(IComparer<T> comparer)
        {
            _list = new List<T>();
            _comparer = comparer;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public bool IsEmpty => _list.Count == 0;

        public T First => _list[0];

        public T Take()
        {
            var v = _list[0];
            _list.RemoveAt(0);
            return v;
        }

        public void Put(T element)
        {
            // assuming that the list is always sorted
            for (var i = 0; i < _list.Count; i++)
            {
                if (_comparer.Compare(_list[i], element) >= 0)
                {
                    _list.Insert(i, element);
                    return;
                }
            }

            _list.Insert(_list.Count, element);
        }

        public void Put(IEnumerable<T> elements)
        {
            foreach (var element in elements)
            {
                this.Put(element);
            }
        }

        public void Remove(T element)
        {
            _list.Remove(element);
        }

        public void Clear()
        {
            _list.Clear();
        }
    }
}
