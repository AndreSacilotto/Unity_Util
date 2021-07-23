using UnityEngine;

namespace Spectra.Singleton
{
    [DisallowMultipleComponent]
    public abstract class MonoBehaviourSingletonPersist<T> : MonoBehaviourSingleton<T> where T : MonoBehaviour
    {
        public override void SetAsSingleton()
        {
            var value = this as T;
            if (instance == null)
            {
                instance = value;
                DontDestroyOnLoad(value);
            }
            else if (instance != value)
                Destroy(value);
        }
    }

}