using truth_table;
using rev_pol_not;
namespace Test_truth_table
{
    [TestClass]
    public class Test_truth_table
    {
        [TestMethod]
        public void TestRPN()
        {
            Reverse_polish_notation a = new("(a~b)");
            Assert.AreEqual(a.Output_str, "ab~");
            Reverse_polish_notation b = new("!a");
            Assert.AreEqual(b.Output_str, "a!");
            Reverse_polish_notation c = new("a&b");
            Assert.AreEqual(c.Output_str, "ab&");
            Reverse_polish_notation d = new("a|b");
            Assert.AreEqual(d.Output_str, "ab|");
            Reverse_polish_notation e = new("a->b");
            Assert.AreEqual(e.Output_str, "ab>");
        }
        [TestMethod]
        public void TestTruthTable() 
        {
            Reverse_polish_notation f = new("(a|b)&!c");
            Assert.AreEqual(f.Output_str, "ab|c!&");
            Truth_table table = new(f.Output_str);
            Assert.AreEqual(table.Index_form, 42);
            Assert.AreEqual(table.Sdnf, "(!a&b&!c)|(a&!b&!c)|(a&b&!c)");
            Assert.AreEqual(table.Sdnf_num, "(2, 4, 6 ) |");
            Assert.AreEqual(table.Sknf, "(a|b|c)&(a|b|!c)&(a|!b|!c)&(!a|b|!c)&(!a|!b|!c)");
            Assert.AreEqual(table.Sknf_num, "(0, 1, 3, 5, 7 ) &");
        }
    }
}