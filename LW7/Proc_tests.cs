using AP;
using System.Diagnostics;
namespace Proc_tests
{
    [TestClass]
    public class AssociativeProcessorTests
    {
        private Associative_Processor ap;

        [TestInitialize]
        public void Setup()
        {
            ap = new Associative_Processor();
            
        }

        
        [TestMethod]
        public void Test_Make_Binary()
        {   
            int value = 5;
            string expected = "0000000000000101";
            string result = ap.Make_Binary(value);
            Assert.AreEqual(expected, result);
        }

    

        [TestMethod]
        public void Test_Make_Decimal()
        {
            ap.Display();
            string binary = "0000000000000101";
            int expected = 5;
            int result = ap.Make_Decimal(binary);
            Assert.AreEqual(expected, result);
        }

        
        [TestMethod]
        public void Test_GetRow()
        {
            string expected = "1111100001110000";
            string result = ap.GetRow(0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_AddRow()
        {
            string word = "1111111111111111";
            ap.AddRow(0, word);
            string result = ap.GetRow(0);
            Assert.AreEqual(word, result);
        }

        [TestMethod]
        public void Test_GetAddressColumn()
        {
            string expected = "1000100010010010";
            string result = ap.GetAddressColumn(0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_ComputeDisparity()
        {
            string firstWord = "1111000011110000";
            string secondWord = "0000111100001111";
            string expected = "0000000000000000";
            ap.ComputeDisparity(firstWord, secondWord, 0);
            string result = ap.GetRow(0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_ComputeEquivalence()
        {
            string firstWord = "0000000000001001";
            string secondWord = "0000000000111000";
            string expected = "0000000000110001";
            ap.ComputeEquivalence(firstWord, secondWord, 1);
            string result = ap.GetRow(1);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_ProhibitSecond()
        {
            string firstWord = "0000000000001001";
            string secondWord = "0000000000111000";
            string expected = "1111111111001111";
            ap.ProhibitSecond(firstWord, secondWord, 1);
            string result = ap.GetRow(1);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_ImplSecondToFirst()
        {
            string firstWord = "0000000000001001";
            string secondWord = "0000000000110001";
            string expected = "0000000000110000";
            ap.ImplSecondToFirst(firstWord, secondWord, 2);
            string result = ap.GetRow(2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Sort_()
        {
            Associative_Processor ap2 = new Associative_Processor();
            ap2.SortTable();
            string start = "101";
            string expected = "0011000101010010";
            ap2.Adding(start);
            Assert.AreEqual(expected, ap2.GetRow(9));
        }
    }
}
