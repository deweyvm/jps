using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using JPS.Util;
using JPS.Search;
using JPS.Data;

namespace JPS.Testing
{
    class SearchTest : Test
    {
        public string Name()
        {
            return "Search";
        }

        public void Run()
        {
            testFile(getTests("tests/pos"), c => c.HasValue);
            testFile(getTests("tests/neg"), c => !c.HasValue);
        }

        private List<string> getTests(string folder)
        {
            var result = new List<string>();
            for (int i = 0; ; i += 1)
            {
                var pathname = string.Format("{0}/{1}.txt", folder, i);
                if (File.Exists(pathname))
                {
                    result.Add(pathname);
                }
                else
                {
                    return result;
                }
            }
        }

        private void testFile(List<string> tests, Func<Option<List<Point>>, bool> pred)
        {
            tests.ForEach (t => {
                //todo: make sure path doesnt hit any impassable blocks and that it actually validly leads from one tile to an adjacent tile
                var parsed = Loader.LoadWalls(t);
                var start = parsed.Item1;
                var end = parsed.Item2;
                var array = parsed.Item3;
                var search = new JumpPointSearch(array, start, end, Heuristics.Euclidean);
                var path = search.FindPath();
                array.Print(start, end, x => x, path);
                
                Console.WriteLine("--------------------------------------");
                Utils.Assert(pred(path), t);
            });
        }
    }
}
