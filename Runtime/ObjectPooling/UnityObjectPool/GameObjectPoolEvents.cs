using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityObject = UnityEngine.Object;


namespace Spectra.ObjectPooling
{
    [Serializable]
    public class GameObjectPoolEvents<T> : IGameObjectPool<T> where T : Component
    {
        [SerializeField, Attributes.ReadOnly]
        public List<T> pool = new List<T>();
        public HashSet<T> currentPool = new HashSet<T>();

        public event Func<T> CreateFunc;
        public event Action<T> OnTakeFromPool;
        public event Action<T> OnReturnToPool;
        public event Action<T> OnDestroyFromPool;

        public GameObjectPoolEvents(Func<T> createFunc, int initialSize = 0, Action<T> onTakeFromPool = null, Action<T> onReturnToPool = null, Action<T> onDestroyFromPool = null)
        {
            CreateFunc = createFunc;
            OnTakeFromPool = onTakeFromPool;
            OnReturnToPool = onReturnToPool;
            OnDestroyFromPool = onDestroyFromPool;
            for (int i = 0; i < initialSize; i++)
                currentPool.Add(createFunc());
        }

        private T InstantiateObject()
        {
            var item = CreateFunc();
            pool.Add(item);
            return item;
        }

        public T Request()
        {
            T item;
            if (currentPool.Count > 0)
            {
                using var el = currentPool.GetEnumerator();
                el.MoveNext();
                item = el.Current;
                currentPool.Remove(item);
            }
            else
                item = InstantiateObject();
            OnTakeFromPool?.Invoke(item);
            return item;
        }

        public bool Return(T item)
        {
            var canReturn = currentPool.Add(item);
            if (canReturn)
                OnReturnToPool?.Invoke(item);
            return canReturn;
        }

        public void Clear()
        {
            foreach (var item in pool)
                OnDestroyFromPool?.Invoke(item);
            pool.Clear();
            currentPool.Clear();
        }

        public void SetParent(Transform parent)
        {
            if(parent != null)
                foreach (var item in pool)
                    item.transform.SetParent(parent);
        }

        public IEnumerator<T> GetEnumerator() => pool.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => pool.GetEnumerator();

    }
}
