using UnityEngine;

namespace Spectra.Singleton
{
    [DisallowMultipleComponent]
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour, IAsSingleton where T : MonoBehaviour
    {
        protected static T instance;
        public static T Instance => instance;

        protected virtual void Awake() => SetAsSingleton();

        public virtual void SetAsSingleton()
        {
            var value = this as T;
            if (instance == null)
                instance = value;
            else if (instance != value)
                Destroy(value);
        }

        void IAsSingleton.ResignAsSingleton()
        {
            if (this == instance)
                instance = null;
        }

        protected virtual void OnDestroy() => (this as IAsSingleton).ResignAsSingleton();
    }
}
