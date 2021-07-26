using UnityEngine;

namespace Spectra.ObjectPooling
{
    public interface IGameObjectPoolItem<T> where T : Component, IGameObjectPoolItem<T>
    {
        IGameObjectPool<T> PoolIBelongTo { get; }

        void OnCreate(GameObjectPool<T> pool);

        void OnTakeFromPool();

        void OnReturnToPool();

        void OnDestroyFromPool();
    }


}
