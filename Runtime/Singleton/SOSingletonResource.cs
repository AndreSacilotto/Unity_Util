using System;
using UnityEngine;

namespace Spectra.Singleton
{
    public abstract class SOSingletonResource<T> : ScriptableObject where T : SOSingletonResource<T>
    {
        private static T instance;
        public static T Instance
        {
            get {
                if (instance == null)
                    instance = GetInstanceFromResources();
                return instance;
            }
        }

        private static T GetInstanceFromResources()
        {
            var assets = Resources.LoadAll<T>("");
            if (assets.Length > 1)
                throw new Exception($"Multiple SOSingletonResources: " + typeof(T).FullName);
            else if (assets.Length <= 0)
                throw new Exception($"No SOSingletonResources was found in Resources: " + typeof(T).FullName);
            return assets[0];
        }

        public static void UnloadResource()
        {
            instance = null;
            Resources.UnloadAsset(instance);
        }
    }
}