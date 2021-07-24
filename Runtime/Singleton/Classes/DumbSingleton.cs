using System;

namespace Spectra.Singleton
{
    public abstract class DumbSingleton<T> where T : DumbSingleton<T>, new()
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
        protected DumbSingleton() { }
    }

}