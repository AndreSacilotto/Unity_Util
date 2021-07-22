using System;

namespace Spectra.Singleton
{
    /// <summary>
    /// Singleton that dont need to be created or setted and cannot be changed
    /// </summary>
    /// <typeparam name="T">T is ItSelf</typeparam>
    public abstract class StaticSingleton<T> where T : class, new()
    {
        //Could use lazy
        private static T instance;
        public static T Instance {
            get {
                if (instance == null)
                    instance = new T();
                return instance;
            }
        }

        protected StaticSingleton() { }
    }

}