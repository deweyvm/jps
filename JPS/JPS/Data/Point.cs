using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPS.Data
{
    class Point
    {
        public readonly int x;
        public readonly int y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point)) return false;
            Point other = (Point)obj;
            return other.x == x && other.y == y;
        }

        public override int GetHashCode()
        {
            return x + (y >> 16);
        }

        public override string ToString()
        {
            return String.Format("Point {0},{1}", x, y);
        }
    }
}
