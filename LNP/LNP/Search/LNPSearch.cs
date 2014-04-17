using System;
using System.Linq;
using System.Text;
using LNP.Data;

namespace LNP.Search
{
    using Point = Tuple<int, int>;
    using System.Collections.Generic;
    class LNPSearch
    {
        private Array2d<Node> nodes;
        private Array2d<bool> solid;
        private PriorityQueue<Node> openList;
        private Func<Point,Point, float> heuristic;
        private Func<int, int, Tuple<int, int>> t = (i, j) => Tuple.Create(i, j);
        private Node endNode;
        class NodeComparer : System.Collections.Generic.IComparer<Node> 
        {
            public int Compare(Node x, Node y)
            {
                return Math.Sign(x.f - y.f);
            }
        }

        private List<Node> retracePath(Node end)
        {
            return new System.Collections.Generic.List<Node>();
        }
        
        public LNPSearch(Array2d<bool> solid, Point start, Point end, Func<Point,Point, float> heuristic)
        {
            this.solid = solid;
            this.nodes = solid.Map((i, j, b) => new Node(i, j));
            var startNode = nodes.get(start.Item1, start.Item2);
            this.endNode = nodes.get(end.Item1, end.Item2);

            this.openList = new PriorityQueue<Node>(new NodeComparer());
            openList.Push(startNode);
            startNode.opened = true;


        }

        public List<Node> FindPath(Node endNode)
        {
            
            while (!openList.IsEmpty())
            {
                var node = openList.Pop();
                node.closed = true;

                if (node == endNode)
                {
                    return retracePath(endNode);
                }
                identifySuccessors(node);

            }
            return new List<Node>();
        }

        private void identifySuccessors(Node node)
        {
            var x = node.x;
            var y = node.y;
            var neighbors = findNeighbors(node);
            foreach (var n in neighbors) 
            {
                var jumpPoint = jump(n.Item1, n.Item2, x, y);
                if (jumpPoint != null)
                {
                    var jx = jumpPoint.Item1;
                    var jy = jumpPoint.Item2;
                }
            }
        }

        private Point jump(int x, int y, int px, int py)
        {
            var dx = x - px;
            var dy = y - py;
            var pt = t(x, y);
            if (!solid.get(x, y)) return null;

            if (nodes.get(x, y).Equals(endNode)) return pt;

            if (dx != 0 && dy != 0)
            {
                if ((solid.get(x - dx, y + dy) && !solid.get(x - dx, y)) ||
                     (solid.get(x + dx, y - dy) && !solid.get(x, y - dy)))
                {
                    return pt;
                }
                else
                {
                    if ((solid.get(x + 1, y + dy) && !solid.get(x + 1, y)) ||
                       (solid.get(x - 1, y + dy) && !solid.get(x - 1, y))) 
                    {
                        return pt;
                    }
                }
            }

            if (dx != 0 && dy != 0) {
                var jx = jump(x + dx, y, x, y);
                var jy = jump(x, y + dy, x, y);
                if (jx != null || jy != null) {
                    return pt;
                }
            }
            if (solid.get(x + dx, y) || solid.get(x, y + dy)) {
                return jump(x + dx, y + dy, x, y);
            }
            return null;
        }

        private List<Point> findNeighbors(Node node)
        {
            var parent = node.parent;
            var x = node.x;
            var y = node.y;
            var neighbors = new List<Point>();
            Func<int, int, Tuple<int, int>> t = (i, j) => Tuple.Create(i, j);
            if (parent != null)
            {
                var px = parent.x;
                var py = parent.y;
                var dx = (x - px) / Math.Max(Math.Abs(x - px), 1);
                var dy = (y - py) / Math.Max(Math.Abs(y - py), 1);
                if (dx != 0 && dy != 0)
                {
                    if (solid.get(x, y + dy)) {
                        neighbors.Add(t(x, y + dy));
                    }
                    if (solid.get(x + dx, y))
                    {
                        neighbors.Add(t(x + dx, y));
                    }
                    if (solid.get(x, y + dy) || solid.get(x + dx, y))
                    {
                        neighbors.Add(t(x + dx, y + dy));
                    }
                    if (!solid.get(x - dx, y) && solid.get(x, y + dy))
                    {
                        neighbors.Add(t(x - dx, y + dy));
                    }
                    if (!solid.get(x, y - dy) && solid.get(x + dx, y))
                    {
                        neighbors.Add(t(x + dx, y - dy));
                    }
                }
                else
                {
                    if (dx == 0)
                    {
                        if (solid.get(x, y + dy))
                        {
                            if (solid.get(x, y + dy))
                            {
                                neighbors.Add(t(x, y + dy));
                            }
                            if (!solid.get(x + 1, y))
                            {
                                neighbors.Add(t(x + 1, y + dy));
                            }
                            if (!solid.get(x - 1, y))
                            {
                                neighbors.Add(t(x - 1, y + dy));
                            }
                        }
                    }
                    else 
                    { 
                        if (solid.get(x + dx, y)) {
                            if (solid.get(x + dx, y)) {
                                neighbors.Add(t(x + dx, y));
                            }
                            if (!solid.get(x, y + 1)) {
                                neighbors.Add(t(x + dx, y + 1));
                            }
                            if (!solid.get(x, y - 1)) {
                                neighbors.Add(t(x + dx, y - 1));
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = -1; i <= 1; i += 1) 
                {
                    for (int j = -1; j <= 1; j += 1)
                    {
                        if (!(i == 0 && j == 0))
                        {
                            neighbors.Add(t(x + i, y + j));
                        }
                    }
                }
            }
            return neighbors;
        }
    }
}
