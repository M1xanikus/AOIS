using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace truth_table
{
    public class Truth_table
    {
        public Truth_table(string reverse_polish_notation)
        {
            input_str = reverse_polish_notation;
            Form_vars();
            Fill_table();
            Make_index_form();
            Make_sdnf();
            Make_sdnf_num();
            Make_sknf();
            Make_sknf_num();
        }
        private string input_str, sknf = "", sdnf = "", sdnf_num = "", sknf_num = "";
        private int num_of_vars = 0, index_form;
        private List<char> variables= new List<char>();
        private List<List<int>> table = new List<List<int>>();
        Stack<char> stack = new Stack<char>();
        private List<int> Addition_one(List<int> list )
        { 
            List<int> temp = list;
            bool buf = false;
            for (int i = temp.Count - 2; i >= 0 ; i--)
            {
                if (temp[i] == 1 && buf == false && i == temp.Count - 2)
                {
                    temp[i] = 0;
                    buf = true;
                    continue;
                }
                if (temp[i] == 0 && buf == false && i == temp.Count - 2)
                {
                    temp[i] = 1;
                    buf = false;
                    continue;
                }
                if (temp[i] == 0 && buf)
                {
                    temp[i] = 1;
                    buf = false;
                    continue;
                }
                if (temp[i] == 0 && buf == false)
                {
                    temp[i] = 0;
                    continue;
                }
                if (temp[i] == 1 && buf)
                {
                    temp[i] = 0;
                    buf = true;
                    continue;
                }
                if (temp[i] == 1 && buf == false )
                {
                    temp[i] = 1;
                    continue;
                }
                
            }
            return temp;
        }
       
        
        private void Form_vars()
        {
            foreach (var ch in this.input_str)
            {
                if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                {
                    if (!variables.Contains(ch))
                    {
                        variables.Add(ch);
                        num_of_vars++;
                    }

                }
            }

        }
        private void Fill_table()
        {
            List<int> temp = new List<int>();
            for (int i = 0; i < num_of_vars + 1; i++) temp.Insert(0, 0);
            table.Insert(0, temp);
            table[0][num_of_vars] = Calculate_results(table[0]);
            for (int i = 1; i < Math.Pow(2, (double)num_of_vars); i++)
            {
                List<int> buf = new List<int>(table[i - 1]);
                table.Add(Addition_one(buf));
                table[i][num_of_vars] = Calculate_results(table[i]);
            }
        }
        private int Calculate_results(List<int> list)
        {   
            Operations op = new Operations();
            Dictionary<char, int> variable_keys = new Dictionary<char, int>();
                for (int i = 0; i < variables.Count; i++)
                {
                variable_keys.Add(variables[i], list[i]);
                }
            variable_keys.Add('\u0000', 0);
            variable_keys.Add('\u0001', 1);
            foreach (var ch in this.input_str)
            {
                if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                {
                    stack.Push(ch);
                    continue;
                }
                if (ch == '!') // for ! need if
                {
                    char a;
                    a = stack.Pop();
                    stack.Push((char)op.Negation(variable_keys[a]));
                }
                if ((ch == '&'))
                {
                    char a, b;
                    b= stack.Pop();
                    a = stack.Pop();
                    stack.Push((char)op.Conjunction(variable_keys[a], variable_keys[b]));
                }
                if ((ch == '|'))
                {
                    char a, b;
                    b = stack.Pop();
                    a = stack.Pop();
                    stack.Push((char)op.Disjunction(variable_keys[a], variable_keys[b]));
                }
                if ((ch == '>'))
                {
                    char a, b;
                    b = stack.Pop();
                    a = stack.Pop();
                    stack.Push((char)op.Implication(variable_keys[a], variable_keys[b]));
                }
                if ((ch == '~'))
                {
                    char a, b;
                    b = stack.Pop();
                    a = stack.Pop();
                    stack.Push((char)op.Equivalence(variable_keys[a], variable_keys[b]));
                }
            }
        return stack.Pop();
        }
        public void View_table()
        {
            for(int j = 0; j < num_of_vars ; j++)
            {
                Console.Write($"{variables[j]} ");
            }
            Console.WriteLine();
            for (int i = 0; i < Math.Pow(2, (double)num_of_vars); i++)
            {
                for(int j = 0; j < num_of_vars + 1; j++)
                {
                    Console.Write($"{table[i][j]} ");
                }
                Console.Write("\n");
            }
        }
        private void Make_index_form()
        {
            string binary = "";
            for(int i = 0;i < Math.Pow(2, (double)num_of_vars); i++)
            {
                binary = binary.Insert(binary.Length, table[i][num_of_vars].ToString());
            }
            double result = 0;
            double grade = 0;
            for (int i = binary.Length - 1; i >= 0; i--)
            {
                if (binary[i] == '1') { result += Math.Pow(2, grade); }
                grade++;
            }
            if (binary[0] == '1' && grade != 0) { result = 0 - result; }
            this.index_form = (int)result;
        }
        private void Make_sdnf()
        {
            Dictionary<int, char> index_var = new Dictionary<int, char>();
            for (int i = 0; i < variables.Count; i++)
            {
                index_var.Add(i, variables[i]);
            }
            for (int i = 0; i < Math.Pow(2, (double)num_of_vars); i++)
            {
                if (table[i][num_of_vars] == 1)
                {
                    string sdnf_temp = "";
                    sdnf_temp += "(";
                    for (int j = 0; j < num_of_vars;j++)
                    {
                        if (table[i][j] == 0) sdnf_temp += "!" + index_var[j] + "&";
                        else if (table[i][j] == 1) sdnf_temp += index_var[j] + "&";
                    }
                    sdnf_temp = sdnf_temp.Remove(sdnf_temp.LastIndexOf("&"),1);
                    sdnf_temp += ")" + "|";
                    sdnf += sdnf_temp;
                }

            }
            sdnf = sdnf.Remove(sdnf.LastIndexOf("|"),1); 
        }
        private void Make_sdnf_num()
        {
            sdnf_num += "(";
            for (int i = 0;i < Math.Pow(2, (double)num_of_vars); i++)
            {
                if (table[i][num_of_vars] == 1)
                {
                    sdnf_num += i.ToString() + ", ";
                }
            }
            sdnf_num = sdnf_num.Remove(sdnf_num.LastIndexOf(","),1);
            sdnf_num += ")";
            sdnf_num += " |";
        }
        private void Make_sknf()
        {
            Dictionary<int, char> index_var = new Dictionary<int, char>();
            for (int i = 0; i < variables.Count; i++)
            {
                index_var.Add(i, variables[i]);
            }
            for (int i = 0; i < Math.Pow(2, (double)num_of_vars); i++)
            {
                if (table[i][num_of_vars] == 0)
                {
                    string sknf_temp = "";
                    sknf_temp += "(";
                    for (int j = 0; j < num_of_vars; j++)
                    {
                        if (table[i][j] == 1) sknf_temp += "!" + index_var[j] + "|";
                        else if (table[i][j] == 0) sknf_temp += index_var[j] + "|";
                    }
                    sknf_temp = sknf_temp.Remove(sknf_temp.LastIndexOf("|"), 1);
                    sknf_temp += ")" + "&";
                    sknf += sknf_temp;
                }

            }
            sknf = sknf.Remove(sknf.LastIndexOf("&"), 1);
        }
        private void Make_sknf_num()
        {
            sknf_num += "(";
            for (int i = 0; i < Math.Pow(2, (double)num_of_vars); i++)
            {
                if (table[i][num_of_vars] == 0)
                {
                    sknf_num += i.ToString() + ", ";
                }
            }
            sknf_num = sknf_num.Remove(sknf_num.LastIndexOf(","), 1);
            sknf_num += ")";
            sknf_num += " &";
        }
        public int Index_form
        {
            get
            {
                return index_form;
            }
        }
        public string Sdnf
        {
            get
            {
                return sdnf;
            }
        }
        public string Sknf
        {
            get
            {
                return sknf;
            }
        }
        public string Sdnf_num
        {
            get
            {
                return sdnf_num;
            }
        }
        public string Sknf_num
        {
            get
            {
                return sknf_num;
            }
        }

    }
}
