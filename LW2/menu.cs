using System;
using System.Globalization;
using System.Security.Principal;
using Rev_Pol_Not;
using truth_table;
namespace Menu
{
    class Menu
    {
        static void Main(string[] args)
        {
            Reverse_polish_notation a = new("(a|b)&!c");
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