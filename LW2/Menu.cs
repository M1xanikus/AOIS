using System;
using System.Globalization;
using System.Security.Principal;
using rev_pol_not;
using truth_table;
namespace Menu
{
    class Menu
    {
        static void Main(string[] args)
        {
            Reverse_polish_notation a = new("(a|b)&!c");
            Console.WriteLine(a.Output_str);
            Truth_table b = new(a.Output_str);
            b.View_table();
            Console.WriteLine(b.Index_form);
            Console.WriteLine(b.Sdnf);
            Console.WriteLine(b.Sdnf_num);
            Console.WriteLine(b.Sknf);
            Console.WriteLine(b.Sknf_num);
        }
        
    }
}
