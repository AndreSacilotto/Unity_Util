using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spectra.Unsuported.Collections
{
    [Serializable]
    public class SerializableQueue<T> : IQueueWrap<T>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<T> list = new List<T>();
        [NonSerialized] private Queue<T> queue = new Queue<T>();

        public int Count => queue.Count;

        bool ICollection.IsSynchronized => (queue as ICollection).IsSynchronized;

        object ICollection.SyncRoot => (queue as ICollection).SyncRoot;

        #region Unity Serialization
        public void OnAfterDeserialize() { }

        public void OnBeforeSerialize()
        {
            queue.Clear();
            foreach (var el in list)
                queue.Enqueue(el);
        }
        #endregion

        #region Interface
        public void Clear() => queue.Clear();

        public bool Contains(T item) => queue.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => queue.CopyTo(array, arrayIndex);

        void ICollection.CopyTo(Array array, int index) => (queue as ICollection).CopyTo(array, index);

        public T Dequeue() => queue.Dequeue();

        public void Enqueue(T item) => queue.Enqueue(item);

        public T Peek() => queue.Peek();

        public T[] ToArray() => queue.ToArray();

        public void TrimExcess() => queue.Clear();

        public Queue<T>.Enumerator GetEnumerator() => queue.GetEnumerator();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => (queue as IEnumerable<T>).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => (queue as IEnumerable).GetEnumerator();
        #endregion
    }

}