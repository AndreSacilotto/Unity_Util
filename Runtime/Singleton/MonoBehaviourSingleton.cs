using UnityEngine;

namespace Spectra.Singleton
{
    [DisallowMultipleComponent]
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour, ISetAsSingleton where T : MonoBehaviour
    {
        protected static T instance;
        public static T Instance => instance;

        protected virtual void Awake() => SetAsInstance();

        public virtual void SetAsInstance()
        {
            var value = this as T;
            if (instance == null)
                instance = value;
            else if (instance != value)
                Destroy(value);
        }

        protected virtual void OnDestroy()
        {
            if (this == instance)
                instance = null;
        }
    }
}
