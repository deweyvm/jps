using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPS.Data;
using System.IO;

namespace JPS
{
    using JPS.Search;
    class Program
    {
        static void Main(string[] args)
        {
            var parsed = loadMap();
            var start = parsed.Item1;
            var end = parsed.Item2;
            var array = parsed.Item3;
            var search = new JumpPointSearch(array, start, end, Heuristics.Euclidean);
            var path = search.FindPath();

            array.Print(start, end, x => x, path);
            Console.WriteLine("Printing all nodes:");
            foreach (var node in path)
            {
                Console.WriteLine(node);
            }
            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        static Point parsePoint(string s) 
        {   
            String[] pts = s.Split(',');
            int x = int.Parse(pts[0]);
            int y = int.Parse(pts[1]);
            return new Point(x, y);
        }

        static Tuple<Point,Point,Array2d<bool>> loadMap()
        {
            const string filepath = "walls.txt";

                var lines = File.ReadAllLines(filepath);
                Point start = parsePoint(lines[0]);
                Point end = parsePoint(lines[1]);
                
                int cols = lines.Length - 2;
                int rows = lines[2].Length;
                var array = Array2d<bool>.Tabulate(cols, rows, 
                    (i, j) => lines[i + 2][j] == 'x'
                );
                return Tuple.Create(start, end, array);
            
        }
    }
}
