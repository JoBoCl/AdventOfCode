using System;
namespace CSharpStandardAlgorithms.DataStructures {
    public interface IQueue<T> {
        bool IsEmpty();
        T Dequeue();
        void Enqueue(T t);
        int Size();
    }
}
