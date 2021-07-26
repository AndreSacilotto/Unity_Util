using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spectra.ObjectPooling
{
    [Serializable]
    public class ObjectPoolSerializable<T> : IObjectPool<T> where T : class
    {
        [SerializeField]
        public List<T> pool;
        private Func<T> createFunc;

        public ObjectPoolSerializable(Func<T> createFunc)
        {
            this.createFunc = createFunc;
            pool = new List<T>();
        }

        public ObjectPoolSerializable(Func<T> createFunc, int capacity)
        {
            this.createFunc = createFunc;
            pool = new List<T>(capacity);
        }

        public T Request()
        {
            T item;
            if (pool.Count > 0)
            {
                item = pool[0];
                pool.RemoveAt(0);
            }
            else
                item = createFunc();
            return item;
        }

        public bool Return(T item)
        {
            if (!pool.Contains(item))
            {
                pool.Add(item);
                return true;
            }
            return false;
        }

        public void Clear() => pool.Clear();
    }
}
