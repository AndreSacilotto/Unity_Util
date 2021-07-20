using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Spectra.Unsuported.Collections
{
    public interface IHashSetWrap<T> : ISet<T>, IReadOnlyCollection<T>, IDeserializationCallback, ISerializable
    {
        IEqualityComparer<T> Comparer { get; }

        void CopyTo(T[] array, int arrayIndex, int count);
        void CopyTo(T[] array);
        int RemoveWhere(Predicate<T> match);
        void TrimExcess();
    }
}