using System;

namespace Spectra.Extensions
{
    /// <summary>
    /// Coordenates = width(x) : height(y) famous Cartesian plane <br/>
    /// Uses row-major order
    /// </summary>
    public static class ArrayRowMajor
    {
        /// <summary> Create a 2D row major array from a 1D array</summary>
        public static T[,] FromArray1D<T>(T[] array1D, int width, int height)
        {
            if (array1D.Length != width * height)
                throw new Exception("The array1D need to have the same size");

            var array2D = new T[width, height];
            for (int y = 0, i = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    array2D[x, y] = array1D[i++];
            return array2D;
        }

        /// <summary> Flatten the coordenates to a 1D array </summary>
        public static T[] ToArray1D<T>(T[,] array2D)
        {
            var array1D = new T[array2D.Length];
            for (int y = 0, i = 0; y < array2D.GetLength(1); y++)
                for (int x = 0; x < array2D.GetLength(0); x++)
                    array1D[i++] = array2D[x, y];
            return array1D;
        }

        public static void Populate<T>(T[,] array2D, Func<int, int, T> action)
        {
            for (int y = 0; y < array2D.GetLength(1); y++)
                for (int x = 0; x < array2D.GetLength(0); x++)
                    array2D[x, y] = action(x, y);
        }

        public static void ForEach(int width, int height, Action<int, int> action)
        {
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    action(x, y);
        }

        public static void ForEach<T>(T[,] array2D, Action<T, int, int> action)
        {
            for (int y = 0; y < array2D.GetLength(1); y++)
                for (int x = 0; x < array2D.GetLength(0); x++)
                    action(array2D[x, y], x, y);
        }

        public static void ForEach<T>(T[,] array2D, Action<int, int> action)
        {
            for (int y = 0; y < array2D.GetLength(1); y++)
                for (int x = 0; x < array2D.GetLength(0); x++)
                    action(x, y);
        }

        public static void ForEach<T>(T[,] array2D, Action<T> action)
        {
            for (int y = 0; y < array2D.GetLength(1); y++)
                for (int x = 0; x < array2D.GetLength(0); x++)
                    action(array2D[x, y]);
        }

    }
}
