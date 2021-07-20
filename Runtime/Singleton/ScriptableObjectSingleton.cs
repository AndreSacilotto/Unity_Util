using System;
using UnityEngine;

namespace Spectra.Singleton
{
    public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            SetInstance();
        }

        public void SetInstance()
        {
            var value = this as T;
            if (Instance == null)
                Instance = value;
            else if (Instance != value)
                throw new Exception($"One Instance of {GetType().FullName} already exists");
        }

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            name = UtilSO.GetSOFileNameByID(this);
        }
#endif

    }

}