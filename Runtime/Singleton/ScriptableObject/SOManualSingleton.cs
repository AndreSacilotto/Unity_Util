using System;
using UnityEngine;

namespace Spectra.Singleton
{
    public abstract class SOManualSingleton<T> : ScriptableObject, IManualSingleton where T : SOManualSingleton<T>
    {
        private static T instance;
        public static T Instance => instance;

        public virtual void SetAsSingleton()
        {
            var value = this as T;
            if (instance == null)
                instance = value;
            else if(instance != value)
                throw new SingletonException(GetType());
        }

        public virtual void ResignAsSingleton()
        {
            if (this == instance)
                instance = null;
        }
    }
}