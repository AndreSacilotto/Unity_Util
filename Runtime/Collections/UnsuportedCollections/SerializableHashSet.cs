using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Spectra.Collections
{
    [Serializable]
    public class SerializableHashSet<T> : IHashSetWrap<T>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<T> list = new List<T>();
        [NonSerialized] private HashSet<T> hash = new HashSet<T>();

#if UNITY_EDITOR
#pragma warning disable IDE0052, CS0414 // Remove unread private members
        [SerializeField] private bool collision;
#pragma warning restore IDE0052, CS0414 // Remove unread private members
#endif

        #region Contructors

        public SerializableHashSet() => hash = new HashSet<T>();
        public SerializableHashSet(IEnumerable<T> collection) => hash = new HashSet<T>(collection);
        public SerializableHashSet(IEqualityComparer<T> comparer) => hash = new HashSet<T>(comparer);
        public SerializableHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer) => hash = new HashSet<T>(collection, comparer);

        #endregion

        public int Count => hash.Count;

        public bool IsReadOnly => (hash as ICollection<T>).IsReadOnly;

        public IEqualityComparer<T> Comparer => hash.Comparer;

        #region Unity Serialization

        //Save the hash to list
        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        //Load the hash from list
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            hash.Clear();
#if UNITY_EDITOR
            collision = false;
#endif
            foreach (var el in list)
            {
                var contains = hash.Add(el);
#if UNITY_EDITOR
                if (!contains)
                    collision = true;
#endif
            }
        }

        #endregion

        #region Interface Implementation

        public bool Add(T item) => hash.Add(item);
        void ICollection<T>.Add(T item) => (hash as ICollection<T>).Add(item);

        public void ExceptWith(IEnumerable<T> other) => hash.ExceptWith(other);

        public void IntersectWith(IEnumerable<T> other) => hash.IntersectWith(other);

        public bool IsProperSubsetOf(IEnumerable<T> other) => hash.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<T> other) => hash.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<T> other) => hash.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<T> other) => hash.IsSupersetOf(other);

        public bool Overlaps(IEnumerable<T> other) => hash.Overlaps(other);

        public bool SetEquals(IEnumerable<T> other) => hash.SetEquals(other);

        public void SymmetricExceptWith(IEnumerable<T> other) => hash.SymmetricExceptWith(other);

        public void UnionWith(IEnumerable<T> other) => hash.UnionWith(other);

        public void Clear() => hash.Clear();

        public bool Contains(T item) => hash.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => hash.CopyTo(array, arrayIndex);
        public void CopyTo(T[] array, int arrayIndex, int count) => hash.CopyTo(array, arrayIndex, count);
        public void CopyTo(T[] array) => hash.CopyTo(array);

        public bool Remove(T item) => hash.Remove(item);

        public IEnumerator<T> GetEnumerator() => hash.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (hash as IEnumerable).GetEnumerator();

        public void OnDeserialization(object sender) => hash.OnDeserialization(sender);

        public void GetObjectData(SerializationInfo info, StreamingContext context) => hash.GetObjectData(info, context);

        public int RemoveWhere(Predicate<T> match) => hash.RemoveWhere(match);

        public void TrimExcess() => hash.TrimExcess();
        #endregion
    }
}