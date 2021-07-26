using System.Collections.Generic;

namespace Spectra.ObjectPooling
{
    public interface IGameObjectPool<T> : IEnumerable<T> where T : class
    {
        T Request();
        bool Return(T item);

        void Clear();
    }


}
