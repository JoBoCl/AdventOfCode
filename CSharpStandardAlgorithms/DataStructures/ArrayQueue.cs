using System;
namespace CSharpStandardAlgorithms.DataStructures {
    public class ArrayQueue<T> : IQueue<T> {
        private int maxCapacity;
        private T[] contents;
        private int head = 0;
        private int tail = 0;
        private int size = 0;

        public ArrayQueue(int maxCapacity) {
            contents = new T[maxCapacity];
            this.maxCapacity = maxCapacity;
        }

        public T Dequeue() {
            if (size == 0) {
                throw new InvalidOperationException("Queue already empty");
            }
            if (head == maxCapacity) {
                head = 0;
            }
            size--;
            return contents[head++];
        }

        public void Enqueue(T t) {
            if (size == maxCapacity) {
                throw new InvalidOperationException("Queue already full");
            }
            if (tail == maxCapacity) {
                tail = 0;
            }
            size++;
            contents[tail++] = t;
        }

        public bool IsEmpty() {
            return size == 0;
        }

        public int Size() {
            return size;
        }
    }
}
