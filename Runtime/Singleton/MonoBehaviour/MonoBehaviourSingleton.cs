using System;
using UnityEngine;

namespace Spectra.Singleton
{
    [DisallowMultipleComponent]
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour, IDisposable where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance => instance;

        public virtual void Awake()
        {
            var value = this as T;
            if (instance == null)
                instance = value;
            else if (instance != value)
                throw new SingletonException(GetType());
        }

        public virtual void Dispose()
        {
            if (this == instance)
                instance = null;
        }

        public virtual void OnDestroy() => Dispose();
    }
}
