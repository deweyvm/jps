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

        public Option<T> get(int i, int j)
        {
            if (i < 0 || i > cols - 1 || j < 0 || j > rows - 1)
            {
                return new Option<T>(raw[i, j]);
            }
            else
            {
                return Option<T>.None;
            }
        }

        private Array2d(int cols, int rows, T[,] raw)
        {
            this.cols = cols;
            this.rows = rows;
            this.raw = raw;
        }

        public static Array2d<T> Tabulate(int cols, int rows, Func<int, int, T> f)
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
