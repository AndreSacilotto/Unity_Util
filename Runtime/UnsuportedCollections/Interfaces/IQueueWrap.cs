using System.Collections;
using System.Collections.Generic;

namespace Spectra.Collections
{
    public interface IQueueWrap<T> : IReadOnlyCollection<T>, ICollection
    {
        void Clear();
        bool Contains(T item);
        void CopyTo(T[] array, int arrayIndex);
        T Dequeue();
        void Enqueue(T item);
        new Queue<T>.Enumerator GetEnumerator();
        T Peek();
        T[] ToArray();
        void TrimExcess();
    }

}