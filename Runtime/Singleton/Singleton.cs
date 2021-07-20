using System;

namespace Spectra.Singleton
{
    public abstract class Singleton<T> where T : class
    {
        public static T Instance { get; private set; }

        public Singleton()
        {
            SetInstance(this as T);
        }

        public void SetInstance(T value)
        {
            if (Instance == null)
                Instance = value;
            else if (Instance != value)
                throw new Exception($"One Instance of {GetType().Name} already exists");
        }
    }
}