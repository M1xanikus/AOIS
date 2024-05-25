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
        {//(((!A)&((!B)&C))|(((!A)&B)&C)|((!A)&(B&(!C)))|(A&(B&(!C))))

                    Reverse_polish_notation a = new("((A|B)&(!C))");
                    Console.WriteLine(a.Output_str);
                    Truth_table b = new(a.Output_str);
           
                    Console.WriteLine("СДНФ исходное: " + b.Sdnf);
                    Console.WriteLine("Склеянное СДНФ: " + b.Sticked_sdnf);
                    Console.WriteLine("Минимизированное СДНФ расчетно-табличным: " + b.Min_sdnf_by_table);
                    Console.WriteLine("СКНФ исходное: " + b.Sknf);
                    Console.WriteLine("Склеянное СКНФ: " + b.Sticked_sknf);
                    Console.WriteLine("Минимизированное СКНФ расчетно-табличным: " + b.Min_sknf_by_table);
            Console.WriteLine("Минимизированное СДНФ расчётным: "+b.Min_sdnf_by_counting);
            Console.WriteLine("Минимизированное СКНФ расчётным: "+b.Min_sknf_by_counting);
            Console.WriteLine("Минимизированное СДНФ картой Карно: " + b.Min_sdnf_Karno);
            Console.WriteLine("Минимизированное СКНФ картой Карно: " + b.Min_sknf_Karno);


            //b.View_table();
            //Console.WriteLine(b.Index_form);
            //Console.WriteLine(b.Sdnf);
            //Console.WriteLine(b.Sdnf_num);
            //Console.WriteLine(b.Sknf);
            //Console.WriteLine(b.Sknf_num);
            //Console.WriteLine(b.Sknf);
            //Console.WriteLine(b.Sticked_sknf);



        }
        
    }
}