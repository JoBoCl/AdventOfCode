using NUnit.Framework;
using CSharpStandardAlgorithms.DataStructures;
using System;

namespace CSharpStandardAlgorithms {
    [TestFixture()]
    public class ArrayQueueTest {

        [Test()]
        public void NewStackIsEmpty() {
            IQueue<int> underTest = new ArrayQueue<int>(1);
            Assert.IsTrue(underTest.IsEmpty());
        }

        [Test()]
        public void SingleItemStackIsFull() {
            IQueue<int> underTest = new ArrayQueue<int>(1);
            Assert.AreEqual(underTest.Size(), 0);
            underTest.Enqueue(1);
            Assert.IsFalse(underTest.IsEmpty());
            Assert.AreEqual(underTest.Size(), 1);
        }

        [Test()]
        public void PoppedItemsInOrder() {
            IQueue<int> underTest = new ArrayQueue<int>(3);
            Assert.AreEqual(underTest.Size(), 0);

            underTest.Enqueue(1);
            underTest.Enqueue(2);
            underTest.Enqueue(3);

            Assert.IsFalse(underTest.IsEmpty());
            Assert.AreEqual(underTest.Size(), 3);

            Assert.AreEqual(underTest.Dequeue(), 1);
            Assert.AreEqual(underTest.Dequeue(), 2);
            Assert.AreEqual(underTest.Dequeue(), 3);
        }
    }
}
