﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityObject = UnityEngine.Object;


namespace Spectra.ObjectPooling
{
    [Serializable]
    public class GameObjectPool<T> : IGameObjectPool<T> where T : Component, IGameObjectPoolItem<T>
    {
        private GameObject poolObject;
        private Transform poolParent;

        [SerializeField, Attributes.ReadOnly]
        public List<T> pool = new List<T>();
        public HashSet<T> currentPool = new HashSet<T>();

        public GameObjectPool(GameObject poolObject, int initialSize = 0, Transform poolParent = null)
        {
            this.poolObject = poolObject;
            this.poolParent = poolParent;
            for (int i = 0; i < initialSize; i++)
                currentPool.Add(InstantiateObject());
        }

        private T InstantiateObject()
        {
            var go = poolParent == null ? UnityObject.Instantiate(poolObject) : UnityObject.Instantiate(poolObject, poolParent);
            var item = go.GetComponent<T>();
            pool.Add(item);
            item.OnCreate(this);
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
            item.OnTakeFromPool();
            return item;
        }

        public bool Return(T item)
        {
            var canReturn = currentPool.Add(item);
            if (canReturn)
                item.OnReturnToPool();
            return canReturn;
        }

        public void Clear()
        {
            foreach (var item in pool)
                item.OnDestroyFromPool();
            pool.Clear();
            currentPool.Clear();
        }

        public void SetParent(Transform parent)
        {
            if(parent != null)
            {
                poolParent = parent;
                foreach (var item in pool)
                    item.transform.SetParent(parent);
            }
        }

        public IEnumerator<T> GetEnumerator() => pool.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }


}
