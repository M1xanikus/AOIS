using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rev_Pol_Not
{
    public class Reverse_polish_notation
    {
        public Reverse_polish_notation(string input_str)
        {
            this.input_str = input_str;
            Make_RPN();
        }
        private string input_str = "";
        private string output_str = "";
        Stack<char> stack = new Stack<char>();
        private int Get_priority(char op)
        {
            try
            {
                switch (op)
                {
                    case '!':
                        return 5;
                    case '&':
                        return 4;
                    case '|':
                        return 3;
                    case '>':
                        return 2;
                    case '~':
                        return 1;
                    case '(':
                        return 0;
                    default:
                        throw new ArgumentException($"There is no such operation!\nOperation you used: {op}");
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Исключение: {e.Message}");
                Console.WriteLine($"Метод: {e.TargetSite}");
                return -1;
            }
        }
        private void  Make_RPN()
        {
            char ch_;
            foreach (var ch in this.input_str)
            {
                if (ch == '-') continue;
                if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                {
                    output_str += ch;
                    continue;
                }
                if (ch == '(')
                {
                    stack.Push(ch);
                    continue;
                }
                if (ch == ')')
                {
                    char temp = stack.Pop();
                    while (temp != '(')
                    {
                        output_str += temp;
                        temp = stack.Pop();
                    }
                    continue;
                }
                if (ch == '!' || ch == '&' || ch == '|' || ch == '>' || ch == '~')
                {
                    char c;
                    if (stack.TryPeek(out c))
                    { 
                        while (Get_priority(ch) <= Get_priority(stack.Peek()))
                        {
                            output_str += stack.Pop();
                        }
                        stack.Push(ch);
                        continue;
                    }
                    stack.Push(ch);
                    continue;
                }

            }
            while (stack.TryPop(out ch_))
                {
                    output_str += ch_;
                }
           
        }
        public string Output_str
        {
            get
            {
                return output_str;
            }
        }
    }
}
