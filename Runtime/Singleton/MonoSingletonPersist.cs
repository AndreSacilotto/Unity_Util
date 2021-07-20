using UnityEngine;

namespace Spectra.Singleton
{
    [DisallowMultipleComponent]
    public abstract class MonoSingletonPersist<T> : MonoSingleton<T> where T : MonoBehaviour
    {
        public override void SetInstance()
        {
            var value = this as T;
            if (Instance == null)
            {
                Instance = value;
                DontDestroyOnLoad(value);
            }
            else if (Instance != value)
                Destroy(value);
        }
    }

}