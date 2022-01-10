using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    // adapted from https://stackoverflow.com/a/13776636
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private const int InitialCapacity = 0;
        private const int GrowFactor = 2;
        private const int MinGrow = 1;

        private T[] _heap = new T[InitialCapacity];

        public int Count { get; private set; } = 0;
        public int Capacity { get; private set; } = InitialCapacity;

        public PriorityQueue() { }

        public PriorityQueue(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                if (Count == Capacity)
                    Grow();

                _heap[Count++] = item;
            }

            for (int i = Parent(Count - 1); i >= 0; i--)
                BubbleDown(i);
        }

        public void Enqueue(T item)
        {
            if (Count == Capacity)
                Grow();

            _heap[Count++] = item;
            BubbleUp(Count - 1);
        }

        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is empty");
            return _heap[0];
        }

        public T Dequeue()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is empty");
            T ret = _heap[0];
            Count--;
            Swap(Count, 0);
            BubbleDown(0);
            return ret;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _heap.Take(Count).GetEnumerator();
        }

        private bool Dominates(T x, T y)
        {
            return x.CompareTo(y) <= 0;
        }

        private void BubbleUp(int i)
        {
            if (i == 0 || Dominates(_heap[Parent(i)], _heap[i]))
                return; //correct domination (or root)

            Swap(i, Parent(i));
            BubbleUp(Parent(i));
        }

        private void BubbleDown(int i)
        {
            int dominatingNode = Dominating(i);
            if (dominatingNode == i) return;
            Swap(i, dominatingNode);
            BubbleDown(dominatingNode);
        }

        private int Dominating(int i)
        {
            int dominatingNode = i;
            dominatingNode = GetDominating(YoungChild(i), dominatingNode);
            dominatingNode = GetDominating(OldChild(i), dominatingNode);

            return dominatingNode;
        }

        private int GetDominating(int newNode, int dominatingNode)
        {
            if (newNode < Count && !Dominates(_heap[dominatingNode], _heap[newNode]))
                return newNode;
            else
                return dominatingNode;
        }

        private void Swap(int i, int j)
        {
            T tmp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = tmp;
        }

        private static int Parent(int i)
        {
            return (i + 1) / 2 - 1;
        }

        private static int YoungChild(int i)
        {
            return (i + 1) * 2 - 1;
        }

        private static int OldChild(int i)
        {
            return YoungChild(i) + 1;
        }

        private void Grow()
        {
            int newCapacity = Capacity * GrowFactor + MinGrow;
            var newHeap = new T[newCapacity];
            Array.Copy(_heap, newHeap, Capacity);
            _heap = newHeap;
            Capacity = newCapacity;
        }
    }
}
