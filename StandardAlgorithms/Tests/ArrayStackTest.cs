using Microsoft.VisualStudio.TestTools.UnitTesting;

using StandardAlgorithms.DataStructures;

namespace StandardAlgorithmTests {
  
    [TestClass]
    public class ArrayStackTest {
    
        [TestMethod]
        public void NewStackIsEmpty() {
            IStack<int> underTest = new ArrayStack<int>(1);
            Assert.IsTrue(underTest.IsEmpty());
        }
    }
}
