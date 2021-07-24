using UnityEngine;

namespace Spectra.Singleton
{
    [DisallowMultipleComponent]
    public abstract class MonoBehaviourManualSingleton<T> : MonoBehaviour, IManualSingleton where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance => instance;

        private void SetInstance(T value, bool dontDestroyOnLoad)
        {
            instance = value;
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(instance);
        }

        public virtual void SetAsSingleton(bool dontDestroyOnLoad = false, bool destroyOld = false, bool destroyIfInvalid = true)
        {
            var value = this as T;
            if (instance == null)
                SetInstance(value, dontDestroyOnLoad);
            else if (instance != value)
            {
                if (destroyOld)
                {
                    var old = instance;
                    SetInstance(value, dontDestroyOnLoad);
                    Destroy(old);
                }
                else if (destroyIfInvalid)
                    Destroy(value);
            }
        }

        public void ResignAsSingleton(bool destroyIfSingleton = true)
        {
            if (this == instance)
            {
                var old = instance;
                instance = null;
                if (destroyIfSingleton)
                    Destroy(old);
            }
        }

        void IManualSingleton.SetAsSingleton() => SetAsSingleton(false, false, true);

        void IManualSingleton.ResignAsSingleton() => ResignAsSingleton(true);
    }
}
