using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPS.Data;
using System.IO;

namespace JPS
{
    class Loader
    {
        static Point parsePoint(string s)
        {
            String[] pts = s.Split(',');
            int x = int.Parse(pts[0]);
            int y = int.Parse(pts[1]);
            return new Point(x, y);
        }

        public static Tuple<Point, Point, Array2d<bool>> LoadWalls(string filepath)
        {
            var lines = File.ReadAllLines(filepath);
            Point start = parsePoint(lines[0]);
            Point end = parsePoint(lines[1]);

            int rows = lines.Length - 2;
            int cols = lines[2].Length;
            var array = Array2d<bool>.Tabulate(cols, rows,
                (i, j) => lines[j + 2][i] == 'x'
            );
            return Tuple.Create(start, end, array);

        }
    }
}
