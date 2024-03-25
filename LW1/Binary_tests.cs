using BinaryNumbers;
using Operations;
namespace Binary_test
{
    [TestClass]
    public class Binary_test
    {
        [TestMethod]
        public void TestBinary_addition()
        {
            Binary a1 = new(1);
            Binary b1 = new(2);
            Operation op1 = new();
            Binary c1 = new(op1.Addition(a1,b1));
            Assert.AreEqual(c1.Number,3);

            Binary a2 = new(-1);
            Binary b2 = new(2);
            Operation op2 = new();
            Binary c2 = new(op2.Addition(a2, b2));
            Assert.AreEqual(c2.Number, 1);

            Binary a3 = new(-10);
            Binary b3 = new(2);
            Operation op3 = new();
            Binary c3 = new(op3.Addition(a3, b3));
            Assert.AreEqual(c3.Number, -8);

            Binary a4 = new(-10);
            Binary b4 = new(-2);
            Operation op4 = new();
            Binary c4 = new(op3.Addition(a4, b4));
            Assert.AreEqual(c4.Number, -12);
        }
        public void TestBinary_multiplication()
        {
            
        }
    }
}