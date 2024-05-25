using rev_pol_not;
using truth_table;
using BinaryNumbers;
namespace Menu
{
    class Menu
    {
        static void Main(string[] args)
        {
            Reverse_polish_notation a = new("(((a|b)|p)&(!(((a&b)|(a&p))|(b&p)))|((a&b)&p))");
            Reverse_polish_notation b = new("(((a&b)|(a&p))|(b&p))");
            Truth_table ex1 = new Truth_table(a.Output_str);
            Truth_table ex2 = new Truth_table(b.Output_str);
            ex1.View_table();
            Console.WriteLine(ex1.Sdnf);
            Console.WriteLine(ex1.Min_sdnf_by_counting);
            ex2.View_table();
            Console.WriteLine(ex2.Sdnf);
            Console.WriteLine(ex2.Min_sdnf_by_counting);
            Binary num = new(5);
            Console.WriteLine(num.Straight_binary.Remove(0,12));
            D8421 ex = new(num.Straight_binary.Remove(0, 12));
            ex.Print_tables();
            ex.Minimize();
            Console.WriteLine($"E: {ex.min_sdnf_1}");
            Console.WriteLine($"F: { ex.min_sdnf_2}");
            Console.WriteLine($"G: {ex.min_sdnf_3}");
            Console.WriteLine($"H: {ex.min_sdnf_4}");
        }
    }
}