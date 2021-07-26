using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Spectra.ObjectPooling
{
    [Serializable]
    public class ObjectPoolFixed<T> : IObjectPool<T> where T : class
    {
        public List<T> pool = new List<T>();
        private Func<T> createFunc;
        private int maxCapacity;

        public ObjectPoolFixed(Func<T> createFunc, int capacity, bool initialize = false)
        {
            this.createFunc = createFunc;
            pool = new List<T>(capacity);
            maxCapacity = capacity;
            if (initialize)
                for (int i = 0; i < capacity; i++)
                    pool.Add(createFunc());
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
            if (pool.Count+1 < maxCapacity && !pool.Contains(item))
            {
                pool.Add(item);
                return true;
            }
            return false;
        }

        public void Clear() => pool.Clear();

        public void SetCapacity(int capacity)
        {
            if(pool.Count < capacity)
            {
                pool.Capacity = capacity;
                maxCapacity = capacity;
            }
            throw new Exception("Cant set a lower capacity than actual capacity");
        }
    }
}
