using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedListNode<T>
    {
        public T Value;
        public DoublyLinkedListNode<T> Next;
        public DoublyLinkedListNode<T> Prev;

        public DoublyLinkedListNode(T value)
        {
            Value = value;
        }
    }

    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private DoublyLinkedListNode<T> head;
        private DoublyLinkedListNode<T> tail;

        public int Length
        {
            get
            {
                if (head == null)
                {
                    return 0;
                }

                var current = head;
                var count = 1;

                while (current.Next != null)
                {
                    count++;
                    current = current.Next;
                }

                return count;
            }
        }

        public void Add(T e)
        {
            var newNode = new DoublyLinkedListNode<T>(e);

            if (head == null)
            {
                head = tail = newNode; 
            } else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
        }

        public void AddAt(int index, T e)
        {
            var newNode = new DoublyLinkedListNode<T>(e);

            if (head == null)
            {
                Add(e);
                return;
            }

            if (index == 0)
            {
                head.Prev = newNode;
                newNode.Next = head;
                head = newNode;

                return;
            }

            if (index == this.Length)
            {
                newNode.Prev = tail;
                tail.Next = newNode;
                tail = newNode;

                return;
            }

            var node = NodeAt(index);
            newNode.Prev = node.Prev;
            newNode.Next = node;
            node.Prev = newNode;

            if (newNode.Prev != null)
            {
                newNode.Prev.Next = newNode;
            }
        }

        public T ElementAt(int index)
        {
            var node = NodeAt(index);
            return node.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnumerator<T>(this);
        }

        public void Remove(T item)
        {
            Func<DoublyLinkedListNode<T>, int, bool> predicate = (DoublyLinkedListNode<T> node, int index) => node.Value.Equals(item);

            RemoveNode(predicate);

        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException("");
            }


            Func<DoublyLinkedListNode<T>, int, bool> predicate = (DoublyLinkedListNode<T> node, int nodeIndex) => nodeIndex == index;

            var removed = RemoveNode(predicate);
            return removed!.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DoublyLinkedListEnumerator<T>(this);
        }

        private DoublyLinkedListNode<T> NodeAt(int index)
        {
            if (head == null)
            {
                throw new IndexOutOfRangeException("List is empty");
            }

            if (index < 0)
            {
                throw new IndexOutOfRangeException("Index can't be less than 0");
            }

            var current = head;
            var currentIndex = 0;

            while (current != null)
            {
                if (currentIndex == index)
                {
                    return current;
                }

                currentIndex++;
                current = current.Next;
            }

            throw new IndexOutOfRangeException("Index out of range");
        }

        private DoublyLinkedListNode<T> RemoveNode(Func<DoublyLinkedListNode<T>, int, bool> predicate)
        {
            var current = head;
            var index = 0;

            while (current != null)
            {
                if (predicate(current, index))
                {
                    if (current == head && current == tail)
                    {
                        head = tail = null;
                    } else if (current == head)
                    {
                        head = current.Next;

                        if (current.Next != null)
                        {
                            current.Next.Prev = null;
                        }
                    } else if (current == tail)
                    {
                        tail = current.Prev;

                        if (current.Prev != null)
                        {
                            current.Prev.Next = null;
                        }
                    } else
                    {
                        current.Prev.Next = current.Next;
                    }

                    return current;
                }

                current = current.Next;
                index++;
            }

            return null;
        }

        private class DoublyLinkedListEnumerator<T> : IEnumerator<T>
        {
            private DoublyLinkedList<T> list;
            private DoublyLinkedListNode<T> currentNode;

            public DoublyLinkedListEnumerator(DoublyLinkedList<T> list)
            {
                this.list = list;
            }

            public T Current => currentNode.Value;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                var nextNode = currentNode != null
                    ? currentNode.Next
                    : list.head;

                if (nextNode == null)
                {
                    return false;
                }

                currentNode = nextNode;

                return true;
            }

            public void Reset()
            {
                currentNode = this.list.head;
            }
        }
    }
}
