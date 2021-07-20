using System;
using UnityEngine;

namespace Spectra.Singleton
{
    public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T>
    {
        public static T Instance { get; private set; }

        public void SetInstance()
        {
            var value = this as T;
            if (Instance == null)
                Instance = value;
            else if (Instance != value)
                throw new Exception($"One Instance of {GetType().FullName} already exists");
        }
    }

}