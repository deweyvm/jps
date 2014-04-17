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

        public void ForEach(Action<T> action)
        {
            if (this.HasValue)
            {
                action(this.Value);
            }
        }

        public bool Exists(Func<T, bool> pred)
        {
            return Map(pred).GetOrElse(false);
        }

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
