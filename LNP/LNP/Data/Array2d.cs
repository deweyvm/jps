using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LNP.Data
{
    class Array2d<T>
    {
        public readonly int cols;
        public readonly int rows;
        private T[,] raw;

        public T get(int i, int j)
        {
            if (i < 0 || i > cols - 1 || j < 0 || j > rows - 1)
            {
                return raw[i, j];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        private Array2d(int cols, int rows, T[,] raw)
        {
            this.cols = cols;
            this.rows = rows;
            this.raw = raw;
        }

        public Array2d<K> Map<K>(Func<int, int, T, K> f)
        {
            return Tabulate<K>(cols, rows, (i, j) => f(i, j, get(i, j)));
        }

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
    }
}
