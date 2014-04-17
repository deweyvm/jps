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
            Console.ReadLine();
        }

        static void runTests()
        {
            new TestManager(new List<Test>(new Test[] {
                new PointTest(),
                new SearchTest()
            })).RunAll(true);
            Console.WriteLine("Done Tests");
        }

        
    }
}
