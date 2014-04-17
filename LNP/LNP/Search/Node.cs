using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LNP.Search
{
    class Node
    {
        public double f;
        public double g;
        public bool closed;
        public bool opened;
        public Node parent;
        public readonly int x;
        public readonly int y;
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
            return x + y >> 16;
        }
    }
}
