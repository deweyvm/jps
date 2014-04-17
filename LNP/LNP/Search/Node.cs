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
        public double h;
        public bool closed;
        public bool opened;
        public Node parent;
        public readonly int x;
        public readonly int y;
        public Tuple<int, int> pos
        {
            get { return Tuple.Create(x, y); }
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
            return x + y >> 16;
        }

        public override string ToString()
        {
            return String.Format("Node {0},{1}", x, y);
        }
    }
}
