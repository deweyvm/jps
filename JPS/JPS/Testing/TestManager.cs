using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPS.Util;

namespace JPS.Testing
{
    class TestManager
    {
        private readonly IEnumerable<Test> tests;
        public TestManager(IEnumerable<Test> tests)
        {
            this.tests = tests;
        }

        public void RunAll(bool fatalFail)
        {
            var fail = false;
            foreach (Test t in tests)
            {
                try
                {
                    t.Run();
                    Console.WriteLine("PASS: " + t.Name());
                }
                catch (AssertionError e)
                {
                    Console.WriteLine("FAIL: " + t.Name() + ": " + e.Message);
                    fail = true;
                }
            }
            if (fail && fatalFail)
            {
                throw new Exception("Test failure.");
            }
        }

    }
}
