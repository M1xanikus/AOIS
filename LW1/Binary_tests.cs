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
            Binary c1 = new(op1.Addition(a1, b1));
            Assert.AreEqual(c1.Number, 3);

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
            Binary c4 = new(op4.Addition(a4, b4));
            Assert.AreEqual(c4.Number, -12);
        }
        [TestMethod]
        public void TestBinary_multiplication()
        {
            Binary a1 = new(1);
            Binary b1 = new(2);
            Operation op1 = new();
            Binary c1 = new(op1.Multiplication(a1, b1));
            Assert.AreEqual(c1.Number, 2);

            Binary a2 = new(-1);
            Binary b2 = new(2);
            Operation op2 = new();
            Binary c2 = new(op2.Multiplication(a2, b2));
            Assert.AreEqual(c2.Number, -2);

            Binary a3 = new(-10);
            Binary b3 = new(2);
            Operation op3 = new();
            Binary c3 = new(op3.Multiplication(a3, b3));
            Assert.AreEqual(c3.Number, -20);

            Binary a4 = new(-10);
            Binary b4 = new(-2);
            Operation op4 = new();
            Binary c4 = new(op4.Multiplication(a4, b4));
            Assert.AreEqual(c4.Number, 20);
        }
        [TestMethod]
        public void TestBinary_Division()
        {

            Binary a1 = new(1);
            Binary b1 = new(2);
            Operation op1 = new();
            Binary_fract c1 = new(op1.Division(a1, b1));
            Assert.AreEqual(c1.Number, 0.5);

            Binary a2 = new(7);
            Binary b2 = new(2);
            Operation op2 = new();
            Binary_fract c2 = new(op2.Division(a2, b2));
            Assert.AreEqual(c2.Number, 3.5);

            Binary a3 = new(10);
            Binary b3 = new(2);
            Operation op3 = new();
            Binary_fract c3 = new(op3.Division(a3, b3));
            Assert.AreEqual(c3.Number, 5);

            Binary a4 = new(5);
            Binary b4 = new(2);
            Operation op4 = new();
            Binary_fract c4 = new(op4.Division(a4, b4));
            Assert.AreEqual(c4.Number, 2.5);
        }
        [TestMethod]
        public void TestBinary_IEEE_addition()
        {
            Binary_IEEE a1 = new(1);
            Binary_IEEE b1 = new(2);
            Operation op1 = new();
            Binary_IEEE c1 = new(op1.Addition_float(a1, b1));
            Assert.AreEqual(c1.Number, 3);

            Binary_IEEE a2 = new((float)1.5);
            Binary_IEEE b2 = new(2);
            Operation op2 = new();
            Binary_IEEE c2 = new(op2.Addition_float(a2, b2));
            Assert.AreEqual(c2.Number, 3.5);

            Binary_IEEE a3 = new((float)0.5);
            Binary_IEEE b3 = new((float)0.5);
            Operation op3 = new();
            Binary_IEEE c3 = new(op3.Addition_float(a3, b3));
            Assert.AreEqual(c3.Number, 1);

            Binary_IEEE a4 = new((float)2.5);
            Binary_IEEE b4 = new((float)3.5);
            Operation op4 = new();
            Binary_IEEE c4 = new(op4.Addition_float(a4, b4));
            Assert.AreEqual(c4.Number, 6);
        }
        [TestMethod]
        public void TestBinary_transfers()
        {
            Binary_fract a1 = new(2);
            Assert.AreEqual(a1.Binary, "0000000001000000");
            Binary_IEEE b2 = new((float)1.5);
            Assert.AreEqual(b2.Binary, "00111111110000000000000000000000");
        }
    }
}