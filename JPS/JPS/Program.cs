using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPS.Data;
using System.IO;
using JPS.Search;
using JPS.Testing;

namespace JPS
{
    class Program
    {
        static void Main(string[] args)
        {
            runTests();
            var parsed = Loader.LoadWalls("walls.txt");//loadMap();
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
        }

        static void runTests()
        {
            new TestManager(new List<Test>(new Test[] {
                new PointTest(),
                new SearchTest()
            })).RunAll();
            Console.WriteLine("Done Tests");
            Console.ReadLine();
            System.Environment.Exit(0);
        }

        
    }
}
