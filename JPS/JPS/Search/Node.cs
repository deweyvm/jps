using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPS.Data;

namespace JPS.Search
{
    /// <summary>
    /// Priority queue node containing a position and f score.
    /// </summary>
    class Node
    {
        public double f;
        public readonly int x;
        public readonly int y;
        public Point pos
        {
            get { return new Point(x, y); }
        }
        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Node)) return false;
            Node other = (Node)obj;
            return other.x == x && other.y == y;
        }

        public override int GetHashCode()
        {
            return x + (y >> 16);
        }
    }
}
