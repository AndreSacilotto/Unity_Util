using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spectra.Collections
{
    [Serializable]
    public class SerializableStack<T> : IStackWrap<T>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<T> list = new List<T>();
        [NonSerialized] private Stack<T> stack = new Stack<T>();

        public int Count => stack.Count;

        bool ICollection.IsSynchronized => (stack as ICollection).IsSynchronized;

        object ICollection.SyncRoot => (stack as ICollection).SyncRoot;

        #region Unity Serialization

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            stack.Clear();
            foreach (var el in list)
                stack.Push(el);
        }

        #endregion

        #region Interfaces
        public T Peek() => stack.Peek();

        public T Pop() => stack.Pop();

        public void Push(T item) => stack.Pop();

        public T[] ToArray() => stack.ToArray();

        public void TrimExcess() => stack.TrimExcess();

        public Stack<T>.Enumerator GetEnumerator() => stack.GetEnumerator();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => (stack as IEnumerable<T>).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => (stack as IEnumerable).GetEnumerator();

        public void Clear() => stack.Clear();

        public bool Contains(T item) => stack.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => stack.CopyTo(array, arrayIndex);
        void ICollection.CopyTo(Array array, int index) => (stack as ICollection).CopyTo(array, index);

        #endregion
    }
}