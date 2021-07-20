using System.Collections;
using System.Collections.Generic;

namespace Spectra.Unsuported.Collections
{
    internal interface IStackWrap<T> : IReadOnlyCollection<T>, ICollection
    {
        void Clear();
        bool Contains(T item);
        void CopyTo(T[] array, int arrayIndex);
        new Stack<T>.Enumerator GetEnumerator();
        T Peek();
        T Pop();
        void Push(T item);
        T[] ToArray();
        void TrimExcess();
    }
}