using System;
using UnityEngine;

namespace Spectra.Singleton
{
    /// <summary>
    /// Load SO from Resources
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SOSingletonResource<T> : ScriptableObject where T : SOSingletonResource<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = GetInstanceFromResources();
                return instance;
            }
        }

        private static T GetInstanceFromResources()
        {
            var assets = Util.UtilResources.LoadAll<T>();
            if (assets.Length > 1)
                throw new SingletonException($"Multiple SOSingletonResources: " + nameof(T));
            else if (assets.Length <= 0)
                throw new SingletonException($"No SOSingletonResources was found in Resources: " + nameof(T));
           return assets[0];
        }
    }
}