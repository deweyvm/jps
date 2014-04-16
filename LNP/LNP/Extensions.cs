using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LNP.Data;

namespace LNP
{
    static class Extensions
    {
        public static IEnumerable<int> Range(this int max)
        {
            return Enumerable.Range(0, max);
        }
        public static Option<T> Some<T>(this T anything)
        {
            return new Option<T>(anything);
        }
    }
}
