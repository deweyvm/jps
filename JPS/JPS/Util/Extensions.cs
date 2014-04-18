using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPS.Data;

namespace JPS
{
    /// <summary>
    /// Useful extension methods
    /// </summary>
    static class Extensions
    {
        /// <summary>
        /// Creates a sequence containing the integers 0 to max - 1.
        /// </summary>
        public static IEnumerable<int> Range(this int max)
        {
            return Enumerable.Range(0, max);
        }

        /// <summary>
        /// Returns a new Option containing the given value.
        /// </summary>
        public static Option<T> Some<T>(this T anything)
        {
            return new Option<T>(anything);
        }

        /// <summary>
        /// Gets an element from a dictionary or the given value if the key is 
        /// not currently a member of the dictionary. The element is not added 
        /// to the dictionary.
        /// </summary>
        public static T GetOrElse<K, T>(this IDictionary<K, T> dict, K key, T t)
        {
            return dict.TryGet(key).GetOrElse(t);
        }

        /// <summary>
        /// If the given key is contained in dict, returns Some(dict[key]). 
        /// Otherwise None.
        /// </summary>
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
