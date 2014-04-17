using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPS.Data
{
    public sealed class Option<T>
    {
        public readonly bool HasValue;
        public readonly T Value;
        public static readonly Option<T> None = new Option<T>();

        public Option(T value)
        {
            this.Value = value;
            this.HasValue = true;
        }

        private Option()
        {
            this.HasValue = false;
        }

        public Option<R> Map<R>(Func<T, R> converter)
        {
            if (this.HasValue)
            {
                return new Option<R>(converter(this.Value));
            }
            else
            {
                return Option<R>.None;
            }
        }


    }
}
