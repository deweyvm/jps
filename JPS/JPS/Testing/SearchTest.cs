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
            testFiles(getTests("tests/pos"), (c, start, end, solid) => c.Exists(path => {
                var reachesEnd = 
                    path.Count == 0 || 
                    (path[0].Equals(start) && path[path.Count - 1].Equals(end));
                var clearPath = path.All(p => !solid.get(p.x, p.y));
                return reachesEnd && clearPath;
            }));
            testFiles(getTests("tests/neg"), (c, start, end, solid) => !c.HasValue);
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

        private void testFiles(List<string> tests, Func<Option<List<Point>>, Point, Point, Array2d<bool>, bool> pred)
        {
            tests.ForEach (t => {
                var parsed = Loader.LoadWalls(t);
                var start = parsed.Item1;
                var end = parsed.Item2;
                var array = parsed.Item3;
                var search = new JumpPointSearch(array, start, end, Heuristics.Euclidean);
                var path = search.FindPath();
                array.Print(start, end, x => x, path);
                
                Console.WriteLine("--------------------------------------");
                Utils.Assert(pred(path, start, end, array), t);
            });
        }
    }
}
