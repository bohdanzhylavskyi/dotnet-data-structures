using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private DoublyLinkedList<T> storage = new DoublyLinkedList<T>();

        public T Dequeue()
        {
            if (this.storage.Length == 0)
            {
                throw new InvalidOperationException("'Dequeue' from empty collection is not allowed.");
            }

            return this.storage.RemoveAt(0);
        }

        public void Enqueue(T item)
        {
            this.storage.Add(item);
        }

        public T Pop()
        {
            if (this.storage.Length == 0)
            {
                throw new InvalidOperationException("'Pop' from empty collection is not allowed.");
            }

            return this.storage.RemoveAt(this.storage.Length - 1);
        }

        public void Push(T item)
        {
            this.storage.Add(item);
        }
    }
}
