using System;

namespace Spectra.Singleton
{
    /// <summary>
    /// Singleton that will try to be setted in the constructor and can be changed after that.
    /// </summary>
    /// <typeparam name="T">T is ItSelf</typeparam>
    public abstract class Singleton<T> : IAsSingleton, IDisposable where T : Singleton<T>
    {
        private static T instance;
        public static T Instance => instance;

        public Singleton() => SetAsSingleton();

        public void SetAsSingleton()
        {
            var value = this as T;
            if (instance == null)
                instance = value;
            else if (Instance != value)
                throw new Exception($"One Instance of {typeof(T).FullName} already exists");
        }

        public void ResignAsSingleton()
        {
            if (this == instance)
                instance = null;
        }

        public void Dispose() => ResignAsSingleton();
    }
}