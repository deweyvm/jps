using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LNP.Data;
using System.IO;

namespace LNP
{
    using Point = Tuple<int,int>;
    class Program
    {
        
        static void Main(string[] args)
        {
            var parsed = loadMap();
            var start = parsed.Item1;
            var end = parsed.Item2;
            var array = parsed.Item3;
        }

        static Point parsePoint(string s) 
        {   
            String[] pts = s.Split(',');
            int x = int.Parse(pts[0]);
            int y = int.Parse(pts[1]);
            return Tuple.Create(x, y);
        }

        static Tuple<Point,Point,Array2d<bool>> loadMap()
        {
            const string filepath = "walls.txt";

                var lines = File.ReadAllLines(filepath);
                Point start = parsePoint(lines[0]);
                Point end = parsePoint(lines[1]);
                
                int rows = lines.Length - 2;
                int cols = lines[2].Length;
                var array = Array2d<bool>.Tabulate(cols, rows, 
                    (i, j) => lines[j + 2][i] == 'x'
                );
                return Tuple.Create(start, end, array);
            
        }
    }
}
