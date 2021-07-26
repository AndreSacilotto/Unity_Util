using System;
using System.Collections.Generic;

namespace Spectra.ObjectPooling
{
    public class ObjectPool<T> : IObjectPool<T> where T : class
    {
        public HashSet<T> pool = new HashSet<T>();
        private Func<T> createFunc;

        public ObjectPool(Func<T> createFunc)
        {
            this.createFunc = createFunc;
        }

        public T Request()
        {
            T item;
            if (pool.Count > 0)
            {
                using var el = pool.GetEnumerator();
                item = el.Current;
                pool.Remove(item);
            }
            else
                item = createFunc();
            return item;
        }

        public bool Return(T item)
        {
            return pool.Add(item);
        }

        public void Clear() => pool.Clear();
    }
}
