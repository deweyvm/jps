using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPS.Data;

namespace JPS.Search
{
    /// <summary>
    /// Contains different search heuristics.
    /// </summary>
    static class Heuristics
    {
        public static double Euclidean(Point p0, Point p1)
        {
            Func<int,int,int> sq = (i, j) => (i - j) * (i - j);
            return Math.Sqrt(sq(p0.x, p1.x) + sq(p0.y, p1.y));
        }

        public static double Manhattan(Point p0, Point p1)
        {
            return Math.Abs(p1.x - p0.x) + Math.Abs(p1.y - p0.y);
        }
    }
}
