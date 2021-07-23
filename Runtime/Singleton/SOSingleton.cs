using System;
using UnityEngine;

namespace Spectra.Singleton
{
    public abstract class SOSingleton<T> : ScriptableObject, IAsSingleton where T : SOSingleton<T>
    {
        private static T instance;
        public static T Instance => instance;

        protected virtual void OnEnable() => SetAsSingleton();
        public void SetAsSingleton()
        {
            var value = this as T;
            if (Instance == null)
                instance = value;
            else if (Instance != value)
                throw new Exception($"One Instance of { GetType().FullName } already exists");
        }

        public void ResignAsSingleton()
        {
            if (this == Instance)
                instance = null;
        }
    }
}