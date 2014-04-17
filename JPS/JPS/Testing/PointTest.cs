using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using JPS.Data;
using JPS.Util;

namespace JPS.Testing
{
    class PointTest : Test
    {
        public string Name()
        {
            return "Point";
        }

        public void Run()
        {
            Utils.Assert(new Point(1, 1).Equals(new Point(1, 1)), "Structural Equality");
            Utils.Assert(!new Point(1, 1).Equals(new Point(1, 2)), "Different Points inequal");
            Utils.Assert(new Point(1, 2).x == 1, "x getter");
            Utils.Assert(new Point(1, 2).y == 2, "y getter");
        }
    }
}
