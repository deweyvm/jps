using System;
using System.Linq;
using System.Text;
using JPS.Data;
using System.Collections.Generic;
using JPS.Util;

namespace JPS.Search
{
    class JumpPointSearch
    {
        private Array2d<Node> nodes;
        private Array2d<bool> solid;
        private PriorityQueue<Node> openList;
        private Func<Point,Point, double> heuristic;
        private Func<int, int, Point> p = (i, j) => new Point(i, j);
        private Node endNode;
        class NodeComparer : IComparer<Node> 
        {
            public int Compare(Node x, Node y)
            {
                return Math.Sign(x.f - y.f);
            }
        }

        private List<Point> retracePath(Node end)
        {
            var result = new List<Node>();
            var current = end.Some();
            while (current.Exists(c => c.parent.HasValue))
            {
                current.ForEach(c => {
                    result.Add(c);
                    current = c.parent;
                });
                
            }
            current.ForEach(c => result.Add(c));
            return result.Select(x => x.pos).ToList();
        }
        
        public JumpPointSearch(Array2d<bool> solid, Point start, Point end, Func<Point,Point, double> heuristic)
        {
            this.solid = solid;
            this.heuristic = heuristic;
            this.nodes = solid.Map((i, j, b) => new Node(i, j));
            var startNode = nodes.get(start.x, start.y);
            this.endNode = nodes.get(end.x, end.y);

            this.openList = new PriorityQueue<Node>(new NodeComparer());
            openList.Push(startNode);
            startNode.opened = true;


        }

        public Option<List<Point>> FindPath()
        {
            while (!openList.IsEmpty())
            {
                var node = openList.Pop();
                node.closed = true;

                if (node.Equals(endNode))
                {
                    return Utils.ExpandPath(retracePath(endNode)).Some();
                }
                identifySuccessors(node);
            }
            return Option<List<Point>>.None;
        }

        private void identifySuccessors(Node node)
        {
            var x = node.x;
            var y = node.y;
            var neighbors = findNeighbors(node);
            foreach (var n in neighbors) 
            {
                var jumpPoint = jump(n.x, n.y, x, y);
                jumpPoint.ForEach(point =>
                {
                    var jx = point.x;
                    var jy = point.y;
                    var jumpNode = nodes.get(jx, jy);
                    if (!jumpNode.closed)
                    {
                        var d = Heuristics.Euclidean(node.pos, jumpNode.pos);
                        var ng = node.g + d;
                        if (!jumpNode.opened || ng < jumpNode.g)
                        {
                            jumpNode.g = ng;
                            jumpNode.h = heuristic(jumpNode.pos, endNode.pos);
                            jumpNode.parent = node.Some();

                            if (!jumpNode.opened)
                            {
                                openList.Push(jumpNode);
                                jumpNode.opened = true;
                            }
                            else
                            {
                                openList.Update(jumpNode);
                            }
                        }
                    }
                });
            }
        }

        private bool isWalkable(int i, int j)
        {
            return solid.InRange(i, j) && !solid.get(i, j);
        }

        private Option<Point> jump(int x, int y, int px, int py)
        {
            var dx = x - px;
            var dy = y - py;
            var pt = p(x, y).Some();
            if (!isWalkable(x, y)) return Option<Point>.None;

            if (nodes.get(x, y).pos.Equals(endNode.pos)) return pt;

            if (dx != 0 && dy != 0)
            {
                if ((isWalkable(x - dx, y + dy) && !isWalkable(x - dx, y)) ||
                    (isWalkable(x + dx, y - dy) && !isWalkable(x, y - dy)))
                {
                    return pt;
                }
            }
            else
            {
                if(dx != 0) 
                { 
                    if((isWalkable(x + dx, y + 1) && !isWalkable(x, y + 1)) ||
                        (isWalkable(x + dx, y - 1) && !isWalkable(x, y - 1))) 
                    {
                        return pt;
                    }
                }
                else if ((isWalkable(x + 1, y + dy) && !isWalkable(x + 1, y)) ||
                         (isWalkable(x - 1, y + dy) && !isWalkable(x - 1, y))) 
                {
                    return pt;
                }
                
            }
            

            if (dx != 0 && dy != 0) {
                var jx = jump(x + dx, y, x, y);
                var jy = jump(x, y + dy, x, y);
                if (!jx.HasValue|| !jy.HasValue) {
                    return pt;
                }
            }
            if (isWalkable(x + dx, y) || isWalkable(x, y + dy)) {
                return jump(x + dx, y + dy, x, y);
            }
            return Option<Point>.None;
        }

        private List<Point> getNeighbors8(Point pt)
        {
            var neighbors = new List<Point>();
            for (int i = -1; i <= 1; i += 1)
            {
                for (int j = -1; j <= 1; j += 1)
                {
                    if (!(i == 0 && j == 0))
                    {
                        neighbors.Add(new Point(pt.x + i, pt.y + j));
                    }
                }
            }
            return neighbors;
        }

        private List<Point> findNeighbors(Node node)
        {
            var parent = node.parent;
            var x = node.x;
            var y = node.y;
            var neighbors = new List<Point>();
            Func<int, int, Point> p = (i, j) => new Point(i, j);
            if (!parent.HasValue)
            {
                
                return getNeighbors8(node.pos);
            }

            parent.ForEach(par =>
            {
                var px = par.x;
                var py = par.y;
                var dx = (x - px) / Math.Max(Math.Abs(x - px), 1);
                var dy = (y - py) / Math.Max(Math.Abs(y - py), 1);
                if (dx != 0 && dy != 0)
                {
                    if (isWalkable(x, y + dy))
                    {
                        neighbors.Add(p(x, y + dy));
                    }
                    if (isWalkable(x + dx, y))
                    {
                        neighbors.Add(p(x + dx, y));
                    }
                    if (isWalkable(x, y + dy) || isWalkable(x + dx, y))
                    {
                        neighbors.Add(p(x + dx, y + dy));
                    }
                    if (!isWalkable(x - dx, y) && isWalkable(x, y + dy))
                    {
                        neighbors.Add(p(x - dx, y + dy));
                    }
                    if (!isWalkable(x, y - dy) && isWalkable(x + dx, y))
                    {
                        neighbors.Add(p(x + dx, y - dy));
                    }
                }
                else if (dx == 0 && isWalkable(x, y + dy))
                {
                    neighbors.Add(p(x, y + dy));
                    if (!isWalkable(x + 1, y))
                    {
                        neighbors.Add(p(x + 1, y + dy));
                    }
                    if (!isWalkable(x - 1, y))
                    {
                        neighbors.Add(p(x - 1, y + dy));
                    }
                }
                else if (isWalkable(x + dx, y))
                {
                    neighbors.Add(p(x + dx, y));
                    if (!isWalkable(x, y + 1))
                    {
                        neighbors.Add(p(x + dx, y + 1));
                    }
                    if (!isWalkable(x, y - 1))
                    {
                        neighbors.Add(p(x + dx, y - 1));
                    }
                }

                
            });
            return neighbors;
        }
    }
}
