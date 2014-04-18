using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPS.Data
{
    /// <summary>
    /// A wrapper for a two dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of the contained element.</typeparam>
    class Array2d<T>
    {
        public readonly int cols;
        public readonly int rows;
        private T[,] raw;

        /// <summary>
        /// Get an element at the given index. 
        /// Throws an exception if the indices are not in range.
        /// </summary>
        public T get(int i, int j)
        {
            if (!InRange(i, j))
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                return raw[i, j];
            }
        }

        /// <summary>
        /// Returns true if the given indices are within the bounds of the array.
        /// </summary>
        public bool InRange(int i, int j)
        {
            return i >= 0 && i <= cols - 1 && j >= 0 && j <= rows - 1;
        }

        private Array2d(int cols, int rows, T[,] raw)
        {
            this.cols = cols;
            this.rows = rows;
            this.raw = raw;
        }

        /// <summary>
        /// Create a new Array2d by applying f to each element and index in `this`.
        /// </summary>
        public Array2d<K> Map<K>(Func<int, int, T, K> f)
        {
            return Tabulate<K>(cols, rows, (i, j) => f(i, j, get(i, j)));
        }

        /// <summary>
        /// Perform an action on each element and its index.
        /// </summary>
        public void ForEach(Action<int, int, T> f)
        {
            foreach (int i in cols.Range())
            {
                foreach (int j in rows.Range())
                {
                    f(i, j, get(i, j));
                }
            }
        }

        /// <summary>
        /// Create a new Array2d from a raw 2d array.
        /// </summary>
        public static Array2d<T> FromArray<T>(int cols, int rows, T[,] elts)
        {
            return new Array2d<T>(cols, rows, elts);
        }

        /// <summary>
        /// Create a new Array2d from a generating function.
        /// </summary>
        public static Array2d<T> Tabulate<T>(int cols, int rows, Func<int, int, T> f)
        {
            T[,] raw = new T[cols,rows];
            foreach (int i in cols.Range()) 
            {
                foreach (int j in rows.Range()) 
                {
                    raw[i, j] = f(i, j);
                }
            }
            return new Array2d<T>(cols, rows, raw);
        }

        /// <summary>
        /// Print the current array and path.
        /// </summary>
        /// <param name="start">The start node.</param>
        /// <param name="end">The end node.</param>
        /// <param name="f">Whether or not an element is "solid".</param>
        /// <param name="path">The path from start to end or None.</param>
        public void Print(Point start, Point end, Func<T, bool> f, Option<List<Point>> path)
        {

            foreach (int j in rows.Range())
            {
                foreach (int i in cols.Range())
                {
                    char c;
                    
                    if (start.x == i && start.y == j)
                    {
                        c = 'S';
                    }
                    else if (end.x == i && end.y == j)
                    {
                        c = 'F';
                    }
                    else if (path.Exists(p => p.Contains(new Point(i, j))))
                    {
                        c = 'O';
                    }
                    else if (f(get(i, j)))
                    {
                        c = 'X';
                    }
                    else
                    {
                        c = ' ';
                    }
                    Console.Write(c);
                }
                Console.WriteLine();
            }
        }
    }
}
