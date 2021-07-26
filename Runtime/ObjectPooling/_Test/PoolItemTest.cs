using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Spectra.ObjectPooling
{
    public class PoolItemTest : MonoBehaviour, IGameObjectPoolItem<PoolItemTest>
    {
        private IGameObjectPool<PoolItemTest> myPool;

        public IGameObjectPool<PoolItemTest> PoolIBelongTo => myPool;

        public void OnCreate(GameObjectPool<PoolItemTest> pool)
        {
            myPool = pool;
            Debug.Log("Create");
        }

        public void OnDestroyFromPool()
        {
            Destroy(gameObject);
            Debug.Log("Destry");
        }

        public void OnReturnToPool()
        {
            Debug.Log("OnReturnToPool");
        }

        public void OnTakeFromPool()
        {
            Debug.Log("OnTakeFromPool");
            Task.Delay(1000).ContinueWith(t => myPool.Return(this));
        }

    }
}
