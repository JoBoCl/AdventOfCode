using System;
namespace CSharpStandardAlgorithms.DataStructures {
    public class ArrayStack<T> : IStack<T> {
        int maxSize;
        T[] contents;
        int size = 0;
        public ArrayStack(int maxSize) {
            this.maxSize = maxSize;
            this.contents = new T[maxSize];
        }

        public bool IsEmpty() {
            return size == 0;
        }
        public T Pop() {
            if (size == 0) {
                throw new InvalidOperationException("Empty stack");
            }
            return contents[--size];
        }

        public T Peak() {
            if (size == 0) {
                throw new InvalidOperationException("Empty stack");
            }
            return contents[size - 1];
        }

        public void Push(T t) {
            if (size == maxSize) {
                throw new InvalidOperationException("Cannot add item to full stack.");
            }
            this.contents[size++] = t;
        }

        public int Size() {
            return size;
        }

    }
}
