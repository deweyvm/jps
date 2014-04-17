using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPS.Data;

namespace JPS.Search
{
    class Node
    {
        public double g;
        public double h;
        public bool closed;
        public bool opened;
        public Option<Node> parent = Option<Node>.None;
        public readonly int x;
        public readonly int y;
        public double f
        {
            get { return g + h; }
        }
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
