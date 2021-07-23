using System;
using UnityEngine;
using Spectra.Util;

namespace Spectra.Singleton
{
    /// <summary>
    /// Load SO from Resources on RuntimeInitialize
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SOStaticSingletonResource<T> : ScriptableObject where T : SOStaticSingletonResource<T>
    {
        private static T instance;
        public static T Instance => instance;

        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        protected static void SetInstanceFromResources()
        {
            var assets = UtilResources.LoadAll<T>();
            if (assets.Length > 1)
                throw new Exception($"Multiple SOSingletonResources: " + typeof(T).FullName);
            else if (assets.Length <= 0)
                throw new Exception($"No SOSingletonResources was found in Resources: " + typeof(T).FullName);
            instance = assets[0];
        }
    }
}