using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPS.Data;

namespace JPS
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

        public static T GetOrElse<K, T>(this IDictionary<K, T> dict, K key, T t)
        {
            return dict.TryGet(key).GetOrElse(t);
        }

        public static Option<T> TryGet<K, T>(this IDictionary<K, T> dict, K key)
        {
            if (!dict.ContainsKey(key))
            {
                return Option<T>.None;
            }
            return dict[key].Some();
        }
    }
}
