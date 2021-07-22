using System;
using UnityEngine;

namespace Spectra.Singleton
{
    public abstract class SOSingleton<T> : ScriptableObject, ISetAsSingleton where T : SOSingleton<T>
    {
        private static T instance;
        public static T Instance => instance;

        protected virtual void OnEnable() => SetAsInstance();
        public void SetAsInstance()
        {
            var value = this as T;
            if (Instance == null)
                instance = value;
            else if (Instance != value)
                throw new Exception($"One Instance of { GetType().FullName } already exists");
        }

        protected virtual void OnDestroy()
        {
            if(this == Instance)
                instance = null;
        }
    }
}