using System;

namespace Spectra.Singleton
{
    /// <summary>
    /// Definable Singleton.
    /// </summary>
    /// <typeparam name="T">T is ItSelf</typeparam>
    public abstract class ManualSingleton<T> : IManualSingleton where T : ManualSingleton<T>
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