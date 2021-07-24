using System;
using System.Reflection;

namespace Spectra.Singleton
{
    public abstract class LazySingleton<T> where T : LazySingleton<T>
    {
        private static readonly Lazy<T> instance = new Lazy<T>(CreateInstance);
        public static T Instance => instance.Value;

        private static T CreateInstance()
        {
            var constructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            return (T)constructor.Invoke(null);
        }
        protected LazySingleton() { }
    }
}