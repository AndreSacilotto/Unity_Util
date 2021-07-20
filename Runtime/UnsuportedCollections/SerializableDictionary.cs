using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Spectra.Collections
{
    [Serializable]
    public class SerializableDictionary<K, V> : IDictionaryWrap<K, V>, ISerializationCallbackReceiver
    {
        [SerializeField] private K[] keys;
        [SerializeField] private V[] values;

        [NonSerialized] private Dictionary<K, V> dict = new Dictionary<K, V>();

#if UNITY_EDITOR
#pragma warning disable IDE0052, CS0414 // Remove unread private members
        [SerializeField] private bool collision;
#pragma warning restore IDE0052, CS0414 // Remove unread private members
#endif

        #region Contructors

        public SerializableDictionary() { }
        public SerializableDictionary(IDictionary<K, V> dictionary) => dict = new Dictionary<K, V>(dictionary);
        public SerializableDictionary(IEqualityComparer<K> comparer) => dict = new Dictionary<K, V>(comparer);

        public SerializableDictionary(int capacity)
        {
            keys = new K[capacity];
            values = new V[capacity];
            dict = new Dictionary<K, V>(capacity);
        }

        public SerializableDictionary(IDictionary<K, V> dictionary, IEqualityComparer<K> comparer) => dict = new Dictionary<K, V>(dictionary, comparer);
        public SerializableDictionary(int capacity, IEqualityComparer<K> comparer)
        {
            keys = new K[capacity];
            values = new V[capacity];
            dict = new Dictionary<K, V>(capacity, comparer);
        }

        #endregion

        #region Props

        public int Count => dict.Count;

        bool ICollection<KeyValuePair<K, V>>.IsReadOnly => (dict as ICollection<KeyValuePair<K, V>>).IsReadOnly;
        bool IDictionary.IsReadOnly => (dict as IDictionary).IsReadOnly;
        bool IDictionary.IsFixedSize => (dict as IDictionary).IsFixedSize;

        bool ICollection.IsSynchronized => (dict as ICollection).IsSynchronized;
        object ICollection.SyncRoot => (dict as ICollection).SyncRoot;

        public Dictionary<K, V>.KeyCollection Keys => dict.Keys;
        public Dictionary<K, V>.ValueCollection Values => dict.Values;

        ICollection<K> IDictionary<K, V>.Keys => dict.Keys;
        ICollection<V> IDictionary<K, V>.Values => dict.Values;

        ICollection IDictionary.Keys => (dict as IDictionary).Keys;

        ICollection IDictionary.Values => (dict as IDictionary).Values;

        IEnumerable<K> IReadOnlyDictionary<K, V>.Keys => dict.Keys;
        IEnumerable<V> IReadOnlyDictionary<K, V>.Values => dict.Values;

        public IEqualityComparer<K> Comparer => dict.Comparer;

        public V this[K key] { get => dict[key]; set => dict[key] = value; }

        object IDictionary.this[object key] { get => (dict as IDictionary)[key]; set => (dict as IDictionary)[key] = value; }

        #endregion

        #region ISerializationCallbackReceiver implementation

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            dict.Clear();
#if UNITY_EDITOR
            collision = false;
#endif
            for (int i = 0; i < keys.Length; i++)
            {
                var contains = dict.ContainsKey(keys[i]);
                if (!contains)
                    dict.Add(keys[i], values[i]);
#if UNITY_EDITOR
                else
                    collision = true;
#endif
            }
        }
        #endregion

        #region Extras
        public void RemoveAll(Func<KeyValuePair<K, V>, bool> match)
        {
            foreach (var el in dict)
                if (!match(el))
                    Remove(el.Key);
        }
        public void ForEach(Action<KeyValuePair<K, V>> action)
        {
            foreach (var pair in dict)
                action(pair);
        }
        public void SetDictionary(K[] newKeys, V[] newvalues)
        {
            keys = newKeys;
            values = newvalues;

            OnAfterDeserialize();
        }
        #endregion

        #region Interfaces

        public void Clear() => dict.Clear();

        public Dictionary<K, V>.Enumerator GetEnumerator() => dict.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => (dict as IEnumerable).GetEnumerator();
        IDictionaryEnumerator IDictionary.GetEnumerator() => (dict as IDictionary).GetEnumerator();
        IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<K, V>>.GetEnumerator() =>
            (dict as ICollection<KeyValuePair<K, V>>).GetEnumerator();

        public void Add(K key, V value) => dict.Add(key, value);
        void ICollection<KeyValuePair<K, V>>.Add(KeyValuePair<K, V> item) => (dict as ICollection<KeyValuePair<K, V>>).Add(item);
        void IDictionary.Add(object key, object value) => (dict as IDictionary).Add(key, value);

        public bool Remove(K key) => dict.Remove(key);
        bool ICollection<KeyValuePair<K, V>>.Remove(KeyValuePair<K, V> item) => (dict as ICollection<KeyValuePair<K, V>>).Remove(item);
        void IDictionary.Remove(object key) => (dict as IDictionary).Remove(key);

        public bool TryGetValue(K key, out V value) => dict.TryGetValue(key, out value);
        public bool ContainsKey(K key) => dict.ContainsKey(key);
        public bool ContainsValue(V value) => dict.ContainsValue(value);

        bool ICollection<KeyValuePair<K, V>>.Contains(KeyValuePair<K, V> item) => (dict as ICollection<KeyValuePair<K, V>>).Contains(item);
        bool IDictionary.Contains(object key) => (dict as IDictionary).Contains(key);

        void ICollection.CopyTo(Array array, int index) => (dict as ICollection).CopyTo(array, index);
        void ICollection<KeyValuePair<K, V>>.CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) => (dict as ICollection<KeyValuePair<K, V>>).CopyTo(array, arrayIndex);

        public void OnDeserialization(object sender) => OnDeserialization(sender);

        public void GetObjectData(SerializationInfo info, StreamingContext context) => GetObjectData(info, context);

        #endregion

    }
}
