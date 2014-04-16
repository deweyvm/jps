using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LNP.Data
{
    class Queue<T>
    {
        private SortedSet<T> rep;
        public Queue(IComparer<T> comp)
        {
            this.rep =  new SortedSet<T>(comp);
        }
        public Option<T> Pop()
        {
            if (rep.Count > 0)
            {
                var result = rep.Min;
                rep.Remove(result);
                return result.Some();
            }
            else
            {
                return Option<T>.None;
            }
        }

        public void Push(T elt)
        {
            rep.Add(elt);
        }
    }
}
