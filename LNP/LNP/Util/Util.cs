using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LNP.Util
{
    using Point = Tuple<int,int>;
    using LNP.Search;
    static class Util
    {
        public static List<Point> Bresenham(Point p0, Point p1)
        {
            var limit = Heuristics.Manhattan(p0, p1);
            Console.WriteLine(limit);
            var line = new List<Point>();
            var x0 = p0.Item1;
            var y0 = p0.Item2;
            var x1 = p1.Item1;
            var y1 = p1.Item2;

            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);

            var sx = (x0 < x1) ? 1 : -1;
            var sy = (y0 < y1) ? 1 : -1;

            var err = dx - dy;

            var x = x0;
            var y = y0;

            for (int p = 0; p < limit + 10; p += 1)
            {
                line.Add(Tuple.Create(x, y));

                if (x == x1 && y == y1)
                {
                    return line;
                }

                var e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y += sy;
                }
            }

            throw new Exception("programmer error");


        }

        public static List<Point> ExpandPath(List<Point> path)
        {
            var expanded = new List<List<Point>>();
            /*if (path.Count <= 2)
            {
                return path;
            }*/

            for (int k = 0; k < path.Count - 1; k += 1)
            {
                var p0 = path[k];
                var p1 = path[k + 1];
                var interpolated = Bresenham(p0, p1);
                expanded.Add(interpolated);
            }
            //expanded.Add(new List<Point>(path[path.Count - 1]));
            return expanded.SelectMany(x => x).ToList();
        }
    }
}
