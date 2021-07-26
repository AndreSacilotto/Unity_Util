namespace Spectra.ObjectPooling
{
    public interface IObjectPool<T> where T : class
    {
        T Request();
        bool Return(T item);

        void Clear();
    }


}
