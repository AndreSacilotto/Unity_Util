using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

using UnityObject = UnityEngine.Object;


namespace Spectra.ObjectPooling
{
    public class GameObjectPool<T> : IObjectPool<T> where T : UnityObject
    {
        private GameObject poolObject;

        public HashSet<T> pool = new HashSet<T>();
        private Func<T> createFunc;

        public GameObjectPool(GameObject poolObject, Func<GameObject ,T> )
        {
            this.poolObject = poolObject;
        }

        #region Create
        private GameObject CreateAdd(GameObject go)
        {
            var item = go.GetComponent<T>();
            //TODO error here
            if (go.activeSelf)
                go.SetActive(false);
            item.Initialize(this as IObjectPool<GameObject>);
            return item;
        }
        public GameObject Create() =>
            CreateAdd(UnityObject.Instantiate(poolObject));
        public GameObject Create(in Vector3 position, in Quaternion quaternion) =>
            CreateAdd(UnityObject.Instantiate(poolObject, position, quaternion));
        public GameObject Create(in Vector3 position, in Quaternion quaternion, Transform parent) =>
            CreateAdd(UnityObject.Instantiate(poolObject, position, quaternion, parent));

        #endregion

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
