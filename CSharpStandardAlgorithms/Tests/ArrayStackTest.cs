using NUnit.Framework;
using CSharpStandardAlgorithms.DataStructures;
using System;

namespace CSharpStandardAlgorithms {
    [TestFixture()]
    public class ArrayStackTest {

        [Test()]
        public void NewStackIsEmpty() {
            IStack<int> underTest = new ArrayStack<int>(1);
            Assert.IsTrue(underTest.IsEmpty());
        }

        [Test()]
        public void SingleItemStackIsFull() {
            IStack<int> underTest = new ArrayStack<int>(1);
            Assert.AreEqual(underTest.Size(), 0);
            underTest.Push(1);
            Assert.IsFalse(underTest.IsEmpty());
            Assert.AreEqual(underTest.Size(), 1);
        }

        [Test()]
        public void PoppedItemsInReverseOrder() {
            IStack<int> underTest = new ArrayStack<int>(3);
            Assert.AreEqual(underTest.Size(), 0);

            underTest.Push(1);
            underTest.Push(2);
            underTest.Push(3);

            Assert.IsFalse(underTest.IsEmpty());
            Assert.AreEqual(underTest.Size(), 3);

            Assert.AreEqual(underTest.Pop(), 3);
            Assert.AreEqual(underTest.Pop(), 2);
            Assert.AreEqual(underTest.Pop(), 1);
        }
    }
}
