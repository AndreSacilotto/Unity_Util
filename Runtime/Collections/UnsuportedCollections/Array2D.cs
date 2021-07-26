using System;
using System.Collections;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Spectra.Collections
{
    [Serializable]
    public class Array2D<T> : ICollection, IEnumerable, IStructuralComparable, IStructuralEquatable, ICloneable
    {
        [SerializeField] T[] internalArray;
        [SerializeField] int columns;
        [SerializeField] int rows;

        public Array2D(int columns, int rows)
        {
            this.columns = columns;
            this.rows = rows;
            internalArray = new T[columns * rows];
        }
        public Array2D(int size) : this(size, size) { }

        public Array2D(T[,] array) : this(array.GetLength(0), array.GetLength(1))
        {
            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                    this[x, y] = array[x, y];
        }

        public Array2D(T[] array, int columns, int rows)
        {
            if (array.Length != columns * rows)
                throw new Exception("The array1D need to have the same size");
            this.columns = columns;
            this.rows = rows;
            internalArray = new T[columns * rows];

            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                    this[x, y] = array[GetIndex(x, y)];
        }

        #region Props

        public int Length => internalArray.Length;
        public int Width => rows;
        public int Height => columns;

        public ReadOnlyCollection<T> GetCollection => Array.AsReadOnly(internalArray);

        int ICollection.Count => ((ICollection)internalArray).Count;

        bool ICollection.IsSynchronized => internalArray.IsSynchronized;

        object ICollection.SyncRoot => internalArray.SyncRoot;

        #endregion

        #region EXTRAS

        public T[,] GetArray2D()
        {
            var arr = new T[columns, rows];
            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                    arr[x, y] = this[x, y];
            return arr;
        }

        public void ForEach(Action<T, int, int> act)
        {
            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                    act(this[x, y], x, y);
        }

        #endregion

        #region Interfaces
        int IStructuralComparable.CompareTo(object other, IComparer comparer) =>
            ((IStructuralComparable)internalArray).CompareTo(other, comparer);

        void ICollection.CopyTo(Array array, int index) => internalArray.CopyTo(array, index);

        IEnumerator IEnumerable.GetEnumerator() => internalArray.GetEnumerator();

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer) => ((IStructuralEquatable)internalArray).Equals(other, comparer);

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) => ((IStructuralEquatable)internalArray).GetHashCode(comparer);

        public object Clone() => new Array2D<T>(internalArray, columns, rows);

        #endregion

        public int GetIndex(int x, int y) => x * rows + y;
        public T this[int x, int y]
        {
            get => internalArray[x * rows + y];
            set => internalArray[x * rows + y] = value;
        }
    }
}