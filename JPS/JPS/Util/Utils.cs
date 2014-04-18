using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPS.Search;
using JPS.Data;

namespace JPS.Util
{
    class AssertionError : Exception
    {
        public AssertionError(string message) : base(message)
        {
            
        }
    }

    static class Utils
    {
        /// <summary>
        /// An assertion which raises an exception rather than 
        /// opening a pop-up window.
        /// </summary>
        /// <param name="pred"></param>
        /// <param name="message"></param>
        public static void Assert(bool pred, string message)
        {
            if (!pred) throw new AssertionError(message);
        }

        /// <summary>
        /// Traces from p0 to p1 using Bresenham's line algorithm.
        /// </summary>
        public static List<Point> Bresenham(Point p0, Point p1)
        {
            var limit = Heuristics.Manhattan(p0, p1);
            var line = new List<Point>();
            var x0 = p0.x;
            var y0 = p0.y;
            var x1 = p1.x;
            var y1 = p1.y;

            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);

            var sx = (x0 < x1) ? 1 : -1;
            var sy = (y0 < y1) ? 1 : -1;

            var err = dx - dy;

            var x = x0;
            var y = y0;

            for (int p = 0; p < limit + 10; p += 1)
            {
                line.Add(new Point(x, y));

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

        /// <summary>
        /// Expands a jump-point containing path to a full path where every
        /// successive point is adjacent to the last.
        /// </summary>
        public static List<Point> ExpandPath(List<Point> path)
        {
            var expanded = new List<List<Point>>();

            for (int k = 0; k < path.Count - 1; k += 1)
            {
                var p0 = path[k];
                var p1 = path[k + 1];
                var interpolated = Bresenham(p0, p1);
                expanded.Add(interpolated.Take(interpolated.Count - 1).ToList());
            }

            var result = expanded.SelectMany(x => x).ToList();
            result.Add(path[path.Count - 1]);
            result.Reverse();
            return result.ToList();
        }
    }
}
