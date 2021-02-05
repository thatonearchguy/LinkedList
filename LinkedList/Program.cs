using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList
{
    internal static class Program
    {
        private static void Main()
        {
            var workList = new LinkedList<string>
            {
                "Pi",
                "Joe",
                "Fridge",
                "Mind Joe Business",
                "1",
                "2",
                "3",
                "4",
                "23",
                "5",
                "6",
                "7",
                "8",
                "9"
            };
            var testArray = new string[14];
            workList.CopyTo(testArray, 0);
            foreach(var element in testArray) Console.WriteLine(element);
            workList.Remove("Joe");
            workList.Remove("Fridge");
            workList.RemoveAt(5);
            workList.RemoveAt(7);
            workList.Insert(8, "69");
            Console.WriteLine(workList.Contains("69"));
            Console.WriteLine(workList[3]);
            foreach(var element in workList) Console.WriteLine(element);
            workList.Clear();
            Console.WriteLine(workList.Count);
            foreach(var element in workList) Console.WriteLine(element);
        }
    }

    public class LinkedList<T> : IList<T>
    {
        private class Node
        {
            internal Node Next { get; set; }
            internal T Data { get; set; }
            internal Node Previous { get; set; }
            public Node(object data)
            {
                Next = null;
                Previous = null;
                Data = (T) data;
            }
            public Node(object data, Node previousNode)
            {
                Data = (T) data;
                previousNode.Next = this;
                Previous = previousNode;
            }
        }
        private Node _head;
        private Node _tail;

        /// <summary>
        /// Initialises a new instance of the LinkedList class. 
        /// </summary>
        /// <param name="isReadOnly">Set linkedList's write ability, default = false</param>
        public LinkedList(bool isReadOnly = false)
        {
            IsReadOnly = isReadOnly;
            _head = new Node(null);
            _tail = new Node(null);
            Count = 0;
        }

        private int _position = -1;
        
        public bool MoveNext()
        {
            if (_position >= Count - 1) return false;
            _position++;
            return true;
        }

        public void Reset()
        {
            _position = -1;
        }

        public object Current => this[_position];

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = _head;
            for (var i = 0; i < Count; i++)
            {
                yield return currentNode.Data;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (IsReadOnly) throw new AccessViolationException( "Object is read only!");
            if (_head.Data == null)
            {
                _head.Data = item;
                _tail = _head;
            }
            else
            {
                var newNode = new Node(item, _tail);
                _tail = newNode;
            }
            Count++;
        }

        public void Clear()
        {
            if (IsReadOnly) throw new AccessViolationException( "Object is read only!");
            _head = new Node(null);
            _tail = new Node(null);
            Count = 0;
        }

        public bool Contains(T item)
        {
            var currentNode = _head;
            for(var i = 0; i < Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.Data, item)) return true;
                currentNode = currentNode.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var currentNode = _head;
            for (var i = arrayIndex; i < Count; i++)
            {
                array[i] = currentNode.Data;
                currentNode = currentNode.Next;
            }
        }

        public bool Remove(T item)
        {
            if (IsReadOnly) throw new AccessViolationException( "Object is read only!");
            var currentNode = _head;
            for(var i = 0; i < Count-2; i++)
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.Next.Data, item))
                {
                    currentNode.Next = currentNode.Next.Next;
                    Count--;
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        public int Count { get; private set; }
        public bool IsReadOnly { get; }
        public int IndexOf(T item)
        {
            var currentNode = _head;
            for(var i = 0; i < Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.Data, item)) return i;
                currentNode = currentNode.Next;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (IsReadOnly) throw new AccessViolationException( "Object is read only!");
            if (index >= Count) throw new IndexOutOfRangeException();
            var currentNode = _head;
            for(var i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }

            var newNode = new Node(item, currentNode) {Next = currentNode.Next.Next};
            currentNode.Next = newNode;
        }

        public void RemoveAt(int index)
        {
            if (IsReadOnly) throw new AccessViolationException( "Object is read only!");
            if (index >= Count) throw new IndexOutOfRangeException();
            var currentNode = _head;
            for(var i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }
            currentNode.Next = currentNode.Next.Next;
            Count--;
        }

        public T this[int index]
        {
            get
            {
                if (IsReadOnly) throw new AccessViolationException( "Object is read only!");
                if (index >= Count) throw new IndexOutOfRangeException();
                var currentNode = _head;
                for(var i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }

                return currentNode.Next.Data;
            }
            set
            {
                if (IsReadOnly) throw new AccessViolationException( "Object is read only!");
                if (index >= Count) throw new IndexOutOfRangeException();
                var currentNode = _head;
                for(var i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }
                currentNode.Next.Data = value;
            }
        }
    }
    
}