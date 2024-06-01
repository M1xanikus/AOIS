using System;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AP;
namespace Menu
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Associative_Processor processor = new Associative_Processor();
            processor.Display();
            Console.WriteLine();

            processor.SortTable();
            processor.Display();
            Console.WriteLine();

            string start = "101";
            processor.Adding(start);
            processor.Display();
            Console.WriteLine(processor.GetRow(9));
            Console.WriteLine();

            string word1 = processor.GetRow(0);
            Console.WriteLine(word1);
            Console.WriteLine();
            string word2 = processor.GetRow(1);
            Console.WriteLine(word2);
            // Console.WriteLine(processor.GetAddressColumn(0));
            Console.WriteLine();
            //processor.ComputeDisparity(word1, word2, 0);
            Console.WriteLine();
            processor.Display();
            processor.ComputeEquivalence(word1, word2, 1);
            Console.WriteLine(processor.GetRow(1));
            Console.WriteLine("aasad");
            string word11 = processor.GetRow(0);
            Console.WriteLine(word1);
            Console.WriteLine();
            string word22 = processor.GetRow(1);
            Console.WriteLine(word2);
            processor.ProhibitSecond(word1, word2, 4);
            Console.WriteLine();
            processor.Display();
            word11 = processor.GetRow(0);
            word22 = processor.GetRow(1);
            Console.WriteLine(word22);
            processor.ImplSecondToFirst(word1, word2, 2);
            Console.WriteLine(processor.GetRow(2));
            Console.WriteLine();

            processor.Display();
            Console.WriteLine(processor.GetAddressColumn(3));
            Console.WriteLine();
        }
    }
}
