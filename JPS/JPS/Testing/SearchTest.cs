using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using JPS.Util;
using JPS.Search;

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
            testFile(getTests("tests/pos"), c => c > 0);
            testFile(getTests("tests/neg"), c => c == 0);
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

        private void testFile(List<string> tests, Func<int, bool> pred)
        {
            tests.ForEach (t => {
                var parsed = Loader.LoadWalls(t);
                var start = parsed.Item1;
                var end = parsed.Item2;
                var array = parsed.Item3;
                var search = new JumpPointSearch(array, start, end, Heuristics.Euclidean);
                var path = search.FindPath();
                array.Print(start, end, x => x, path);
                Utils.Assert(pred(path.Count), t);
            });
        }
    }
}
