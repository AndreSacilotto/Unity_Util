using UnityEngine;

namespace Spectra.Singleton
{
    [DisallowMultipleComponent]
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; protected set; }

        public virtual void SetInstance()
        {
            var value = this as T;
            if (Instance == null)
                Instance = value;
            else if (Instance != value)
                Destroy(value);
        }

        protected virtual void Awake() => SetInstance();

        protected virtual void OnDestroy()
        {
            if (this == Instance)
                Instance = null;
        }
    }
}
