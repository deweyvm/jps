using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPS.Testing
{
    class TestManager
    {
        private readonly IEnumerable<Test> tests;
        public TestManager(IEnumerable<Test> tests)
        {
            this.tests = tests;
        }

        public void RunAll()
        {
            foreach (Test t in tests)
            {
                try
                {
                    t.Run();
                    Console.WriteLine("PASS: " + t.Name());
                }
                catch (Exception e)
                {
                    Console.WriteLine("FAIL: " + t.Name() + ": " + e.Message);
                }
            }
        }
    }
}
