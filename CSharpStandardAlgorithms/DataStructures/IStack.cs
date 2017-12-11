using System;
namespace CSharpStandardAlgorithms.DataStructures {
    public interface IStack<T> {
        bool IsEmpty();
        T Pop();
        T Peak();
        void Push(T t);
        int Size();
    }
}
