using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPS.Data;

namespace JPS.Search
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
        public Point pos
        {
            get { return new Point(x, y); }
        }
        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
