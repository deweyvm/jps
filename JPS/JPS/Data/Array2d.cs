using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPS.Data
{
    class Array2d<T>
    {
        public readonly int cols;
        public readonly int rows;
        private T[,] raw;

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

        public Array2d<K> Map<K>(Func<int, int, T, K> f)
        {
            return Tabulate<K>(cols, rows, (i, j) => f(i, j, get(i, j)));
        }

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
