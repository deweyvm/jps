using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPS.Data
{
    /// <summary>
    /// A container containing zero or one elements.
    /// </summary>
    public sealed class Option<T>
    {
        public readonly bool HasValue;
        public readonly T Value;
        public static readonly Option<T> None = new Option<T>();

        /// <summary>
        /// Equivalent to the Some constructor. Creates an Option containing the 
        /// given value.
        /// </summary>
        public Option(T value)
        {
            this.Value = value;
            this.HasValue = true;
        }

        private Option()
        {
            this.HasValue = false;
        }

        /// <summary>
        /// Perform an action on the contained value, or do nothing if None.
        /// </summary>
        public void ForEach(Action<T> action)
        {
            if (this.HasValue)
            {
                action(this.Value);
            }
        }

        /// <summary>
        /// Test if the contained value satisfies a predicate, or false if None.
        /// </summary>
        public bool Exists(Func<T, bool> pred)
        {
            return Map(pred).GetOrElse(false);
        }

        /// <summary>
        /// Get the contained value or the given value if None.
        /// </summary>
        public T GetOrElse(T t)
        {
            if (this.HasValue)
            {
                return this.Value;
            }
            else
            {
                return t;
            }
        }

        /// <summary>
        /// Map the contained value with the given function. Has no effect if None.
        /// </summary>
        public Option<R> Map<R>(Func<T, R> f)
        {
            if (this.HasValue)
            {
                return new Option<R>(f(this.Value));
            }
            else
            {
                return Option<R>.None;
            }
        }


    }
}
