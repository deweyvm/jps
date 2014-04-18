using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPS.Data
{
    /// <summary>
    /// A priority queue.
    /// </summary>
    /// <typeparam name="T">The contained element.</typeparam>
    class PriorityQueue<T>
    {
        private SortedSet<T> rep;
        /// <summary>
        /// Create a new queue which uses the given comparer to compare elements.
        /// </summary>
        /// <param name="comp"></param>
        public PriorityQueue(IComparer<T> comp)
        {
            this.rep =  new SortedSet<T>(comp);
        }

        /// <summary>
        /// Returns true if this is empty.
        /// </summary>
        public bool IsEmpty()
        {
            return rep.Count == 0;
        }

        /// <summary>
        /// Returns the minimum element from this. If the queue is empty, 
        /// an exception is raised.
        /// </summary>
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

        /// <summary>
        /// Adds the given element to this.
        /// </summary>
        public void Push(T elt)
        {
            rep.Add(elt);
        }

        /// <summary>
        /// Returns true if this contains the given element.
        /// </summary>
        public bool Contains(T elt)
        {
            return rep.Contains(elt);
        }

        /// <summary>
        /// Updates the priority of the given element.
        /// </summary>
        public void Update(T elt)
        {
            rep.Remove(elt);
            rep.Add(elt);
        }
    }
}
