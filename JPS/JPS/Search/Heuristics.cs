using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPS.Search
{
    using Point = Tuple<int, int>;
    static class Heuristics
    {
        public static double Euclidean(Point p0, Point p1)
        {
            Func<int,int,int> sq = (i, j) => (i - j) * (i - j);
            return Math.Sqrt(sq(p0.Item1, p1.Item1) + sq(p0.Item2, p1.Item2));
        }

        public static double Manhattan(Point p0, Point p1)
        {
            return Math.Abs(p1.Item1 - p0.Item1) + Math.Abs(p1.Item2 - p0.Item2);
        }
    }
}
