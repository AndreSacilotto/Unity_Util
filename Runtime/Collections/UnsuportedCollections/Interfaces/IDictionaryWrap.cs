using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Spectra.Collections
{
    public interface IDictionaryWrap<K, V> : IDictionary<K, V>, IDictionary, IReadOnlyDictionary<K, V>, IDeserializationCallback, ISerializable
    {
        new Dictionary<K, V>.KeyCollection Keys { get; }
        new Dictionary<K, V>.ValueCollection Values { get; }

        IEqualityComparer<K> Comparer { get; }
        new Dictionary<K, V>.Enumerator GetEnumerator();
        bool ContainsValue(V value);
    }
}
