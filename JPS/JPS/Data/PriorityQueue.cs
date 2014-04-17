using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPS.Data
{
    class PriorityQueue<T>
    {
        private SortedSet<T> rep;
        public PriorityQueue(IComparer<T> comp)
        {
            this.rep =  new SortedSet<T>(comp);
        }
        public bool IsEmpty()
        {
            return rep.Count == 0;
        }
        public T Pop()
        {
            if (rep.Count > 0)
            {
                var result = rep.Min;
                rep.Remove(result);
                return result;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public Option<T> Find(Func<T, bool> pred)
        {
            var found = rep.Where(pred);
            if (found.Count() == 0)
            {
                return Option<T>.None;
            }
            else
            {
                return found.First().Some();
            }

        }

        public void Push(T elt)
        {
            rep.Add(elt);
        }

        public bool Contains(T elt)
        {
            return rep.Contains(elt);
        }

        public void Update(T elt)
        {
            rep.Remove(elt);
            rep.Add(elt);
        }
    }
}
