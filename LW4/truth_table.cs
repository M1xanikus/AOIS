using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using rev_pol_not;
namespace truth_table
{
    public class Truth_table
    {
        public Truth_table(string reverse_polish_notation)
        {
            input_str = reverse_polish_notation;
            Form_vars();
            Fill_table(ref this.table, this.input_str);
            Make_sdnf();
            stack.Clear();
            Stick_sdnf();
            Make_sticked_sdnf();
            Minimize_by_counting_method(sticked_sdnf);
        }
        public Truth_table(List<List<int>> table)
        {
           this.table = table;
            for (char i = 'A'; i <= 'D'; i++)
                variables.Add(i);
            num_of_vars = variables.Count;
            Make_sdnf();
            Stick_sdnf();
            Make_sticked_sdnf();
            Minimize_by_counting_method(sticked_sdnf);
        }


        private string input_str, sknf = "", sdnf = "", sdnf_num = "", sknf_num = "", sticked_sdnf = "", sticked_sknf = "", min_sdnf_by_table = "", min_sknf_by_table = "", min_sdnf_by_counting = "",min_sknf_by_counting = "";
        private int num_of_vars = 0, index_form;
        private List<char> variables = new List<char>();
        private List<List<int>> table = new List<List<int>>();
        private List<List<string>> table_for_method = new();
        Stack<char> stack = new Stack<char>();
        private List<int> Addition_one(List<int> list)
        {
            List<int> temp = list;
            bool buf = false;
            for (int i = temp.Count - 2; i >= 0; i--)
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
                if (temp[i] == 1 && buf == false)
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
                if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || ch == '1' || ch == '0') //added 0 1
                {
                    if (!variables.Contains(ch))
                    {
                        variables.Add(ch);
                        num_of_vars++;
                    }

                }
            }

        }
        private void Fill_table(ref List<List<int>> input_table, string input_string)
        {
            List<int> temp = new List<int>();
            for (int i = 0; i < num_of_vars + 1; i++) temp.Insert(0, 0);
            input_table.Insert(0, temp);
            input_table[0][num_of_vars] = Calculate_results(input_table[0],input_string);
            if (table.Count != 10)
            {
                for (int i = 1; i < Math.Pow(2, (double)num_of_vars); i++)// ебаные 16 вместо 10, рот ебал
                {
                    List<int> buf = new List<int>(input_table[i - 1]);// так а шо делать, если у меня код остальной ломается
                    input_table.Add(Addition_one(buf));
                    input_table[i][num_of_vars] = Calculate_results(input_table[i], input_string);
                }
            }
            else
            {
                for (int i = 1; i < 10; i++)// ебаные 16 вместо 10, рот ебал
                {
                    List<int> buf = new List<int>(input_table[i - 1]);// так а шо делать, если у меня код остальной ломается
                    input_table.Add(Addition_one(buf));
                    input_table[i][num_of_vars] = Calculate_results(input_table[i], input_string);
                }
            }
        }
        private int Calculate_results(List<int> list , string input_string)
        {
            Operations op = new Operations();
            Dictionary<char, int> variable_keys = new Dictionary<char, int>();
            for (int i = 0; i < variables.Count; i++)
            {
                variable_keys.Add(variables[i], list[i]);
            }
            variable_keys.Add('\u0000', 0);
            variable_keys.Add('\u0001', 1);
            foreach (var ch in input_string)
            {
                if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || ch == '0' || ch == '1') // added 0 1
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
                    b = stack.Pop();
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
            return variable_keys[stack.Pop()];
        }
        public void View_table()
        {
            for (int j = 0; j < num_of_vars; j++)
            {
                Console.Write($"{variables[j]} ");
            }
            Console.WriteLine();
            for (int i = 0; i < Math.Pow(2, (double)num_of_vars); i++)
            {
                for (int j = 0; j < num_of_vars + 1; j++)
                {
                    Console.Write($"{table[i][j]} ");
                }
                Console.Write("\n");
            }
        }
        private void Make_index_form()
        {
            string binary = "";
            for (int i = 0; i < Math.Pow(2, (double)num_of_vars); i++)
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
            this.index_form = (int)result;
        }
        private void Make_sdnf()
        {
            Dictionary<int, char> index_var = new Dictionary<int, char>();
            for (int i = 0; i < variables.Count; i++)
            {
                index_var.Add(i, variables[i]);
            }
            for (int i = 0; i < table.Count; i++)
            {
                if (table[i][num_of_vars] == 1)
                {
                    string sdnf_temp = "";
                    sdnf_temp += "(";
                    for (int j = 0; j < num_of_vars; j++)
                    {
                        if (table[i][j] == 0) sdnf_temp += "!" + index_var[j] + "&";
                        else if (table[i][j] == 1) sdnf_temp += index_var[j] + "&";
                    }
                    sdnf_temp = sdnf_temp.Remove(sdnf_temp.LastIndexOf("&"), 1);
                    sdnf_temp += ")" + "|";
                    sdnf += sdnf_temp;
                }

            }
            if (sdnf != "")
                sdnf = sdnf.Remove(sdnf.LastIndexOf("|"), 1);
        }
        private void Make_sdnf_num()
        {
            sdnf_num += "(";
            for (int i = 0; i < Math.Pow(2, (double)num_of_vars); i++)
            {
                if (table[i][num_of_vars] == 1)
                {
                    sdnf_num += i.ToString() + ", ";
                }
            }
            if (sdnf_num != "(")
            {
                sdnf_num = sdnf_num.Remove(sdnf_num.LastIndexOf(","), 1);
                sdnf_num += ")";
                sdnf_num += " |";
            }
            if (sdnf_num == "(") { sdnf_num = sdnf_num.Remove(sdnf_num.LastIndexOf("("), 1); }
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
            if (sknf != "")
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
            if (sknf_num != "(")
            {
                sknf_num = sknf_num.Remove(sknf_num.LastIndexOf(","), 1);
                sknf_num += ")";
                sknf_num += " &";
            }
            if (sknf_num == "(") { sknf_num = sknf_num.Remove(sknf_num.LastIndexOf("("), 1); }


        }


        private List<string> Make_lexemes(string str_sdnf_or_sknf)
        {
            int counter = 0;
            List<string> lexemes_ = new List<string>();
            for (int i = 0; i < str_sdnf_or_sknf.Length; i++)
            {

                if (str_sdnf_or_sknf[i] == '!')
                {
                    lexemes_[counter] = lexemes_[counter].Insert(lexemes_[counter].Length, "0");
                    i++;
                    continue;
                }
                if ((str_sdnf_or_sknf[i] >= 'a' && str_sdnf_or_sknf[i] <= 'z') || (str_sdnf_or_sknf[i] >= 'A' && str_sdnf_or_sknf[i] <= 'Z') && str_sdnf_or_sknf[i - 1] != '!')
                {
                    lexemes_[counter] = lexemes_[counter].Insert(lexemes_[counter].Length, "1");
                    continue;
                }
                if (str_sdnf_or_sknf[i] == '&') continue;
                if (str_sdnf_or_sknf[i] == '|') continue;
                if (str_sdnf_or_sknf[i] == ')')
                {
                    lexemes_[counter] = lexemes_[counter].Insert(lexemes_[counter].Length, str_sdnf_or_sknf[i].ToString());
                    counter++;
                    continue;
                }
                if (str_sdnf_or_sknf[i] == '(')
                    lexemes_.Add("");
                lexemes_[counter] = lexemes_[counter].Insert(0, str_sdnf_or_sknf[i].ToString());
            }
            return lexemes_;
        }
        private void Stick_lexemes(ref List<string> lexemes_, List<Tuple<int, int>> tuple_list)
        {
            int first, second, lex_length = lexemes_[0].Length, orig_lex_count = lexemes_.Count;
            for (int i = 0; i < tuple_list.Count; i++)
            {
                string new_lex = "(";
                first = tuple_list[i].Item1;
                second = tuple_list[i].Item2;
                for (int j = 1; j < lex_length; j++)
                {

                    if (lexemes_[first][j] == ')' && lexemes_[second][j] == ')')
                    {
                        new_lex = new_lex.Insert(new_lex.Length, ")");
                        lexemes_.Add(new_lex); // make skleika
                        break;
                    }
                    if (lexemes_[first][j] == 'X' && lexemes_[second][j] == 'X') new_lex = new_lex.Insert(new_lex.Length, "X");
                    if (lexemes_[first][j] == '1' && lexemes_[second][j] == '1')
                    {
                        new_lex = new_lex.Insert(new_lex.Length, "1");
                        continue;
                    }
                    if (lexemes_[first][j] == '0' && lexemes_[second][j] == '0')
                    {
                        new_lex = new_lex.Insert(new_lex.Length, "0");
                        continue;
                    }
                    if ((lexemes_[first][j] == '1' && lexemes_[second][j] == '0') || (lexemes_[first][j] == '0' && lexemes_[second][j] == '1'))
                    {
                        new_lex = new_lex.Insert(new_lex.Length, "X");
                    }
                }
            }
            lexemes_.RemoveRange(0, orig_lex_count);
        }
        private void Print_min_sdnf_phase(List<string> lexemes_)
        {
            sticked_sdnf = "";
            for (int i = 0; i < lexemes_.Count; i++)
            {
                sticked_sdnf += "|" + lexemes_[i];

            }
            sticked_sdnf = sticked_sdnf.Remove(0, 1);
           
        }
        private void Print_min_sknf_phase(List<string> lexemes_)
        {
            sticked_sknf = "";
            for (int i = 0; i < lexemes_.Count; i++)
            {
                sticked_sknf += "&" + lexemes_[i];

            }
            sticked_sknf = sticked_sknf.Remove(0, 1);
            
        }
        private List<int> Find_the_odd_lexs(List<Tuple<int, int>> tuple_list, int lex_count)
        {
            List<int> the_odd_lexs = [];
            for (int i = 0; i < lex_count; i++)
            {
                for (int j = 0; j < tuple_list.Count; j++)
                {
                    if (i == tuple_list[j].Item1 || i == tuple_list[j].Item2) break;
                    if (j == tuple_list.Count - 1) the_odd_lexs.Add(i);
                }


            }
            return the_odd_lexs;
        }
        private void Compare_and_stick_lexemes_sdnf(ref List<string> lexemes_, ref List<string> lexs_in_the_end_, int counter)
        {

            List<Tuple<int, int>> tuple_list = [];

            int lex_count = lexemes_.Count, length_lex = lexemes_[0].Length, counter_vars = 0, k = 1, vars_need_to_stick = lexemes_[0].Length - counter;
            bool make_new_lexeme = false;
            for (int i = 0; i < lex_count - 1; i++)
            {

                for (int j = k; j < lex_count; j++)
                {

                    for (int l = 1; l < length_lex; l++)
                    {
                        if ((lexemes_[i][l] == '1' || lexemes_[i][l] == '0') && lexemes_[j][l] == 'X' || (lexemes_[j][l] == '1' || lexemes_[j][l] == '0') && lexemes_[i][l] == 'X')
                            break;
                        if (counter_vars == vars_need_to_stick)
                        {
                            make_new_lexeme = !make_new_lexeme; // make skleika
                            break;
                        }
                        if (lexemes_[i][l] == 'X' && lexemes_[j][l] == 'X') continue;


                        if (lexemes_[i][l] == ')' && lexemes_[j][l] == ')') break;
                        if (lexemes_[i][l] == '1' && lexemes_[j][l] == '1')
                        {
                            counter_vars++;
                            continue;
                        }
                        if (lexemes_[i][l] == '0' && lexemes_[j][l] == '0')
                        {
                            counter_vars++;
                            continue;
                        }
                        if ((lexemes_[i][l] == '1' && lexemes_[j][l] == '0') || (lexemes_[i][l] == '0' && lexemes_[j][l] == '1')) continue;
                    }
                    counter_vars = 0;
                    if (make_new_lexeme)
                    {
                        tuple_list.Add(Tuple.Create(i, j)); // pairs of original lexemes
                        make_new_lexeme = !make_new_lexeme;
                    }

                }
                k++;
            }

            if (tuple_list.Count == 0)
            {
                for (int i = 0; i < lexs_in_the_end_.Count; i++)
                {
                    lexemes_.Add(lexs_in_the_end_[i]);
                }
                Print_min_sdnf_phase(lexemes_);
                throw new Exception("Склеиваний больше нет!");
            }
            else
            {
                List<int> odd_lexs = Find_the_odd_lexs(tuple_list, lex_count);
                if (odd_lexs.Count == 0) { Console.WriteLine("\n Несклеянных лексем нет!\n"); }
                else
                {
                    for (int i = 0; i < odd_lexs.Count; i++)
                    {
                        lexs_in_the_end_.Add(lexemes_[odd_lexs[i]]);
                    }
                }
                Stick_lexemes(ref lexemes_, tuple_list);

                Print_min_sdnf_phase(lexemes_);
            }

        }
        private void Stick_sdnf()
        {
            try
            {
                if (sdnf != "")
                {
                    int counter = 3;
                    List<string> lexemes = new(Make_lexemes(Sdnf));
                    List<string> lexemes_in_the_end = [];
                    while (true)
                    {
                        
                        Compare_and_stick_lexemes_sdnf(ref lexemes, ref lexemes_in_the_end, counter);
                        counter++;
                    }
                }
                else
                {
                    throw new Exception("СДНФ нет");
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }
        private void Make_sticked_sdnf()
        {
            int counter_vars = 0;
            string pre_min_sdnf = "";
            for (int i = 0; i < sticked_sdnf.Length; i++)
            {
                if (sticked_sdnf[i] == '(')
                {
                    pre_min_sdnf = pre_min_sdnf.Insert(pre_min_sdnf.Length, "(");
                }
                if (sticked_sdnf[i] == '1')
                {
                    pre_min_sdnf += variables[counter_vars];
                    counter_vars++;
                    continue;
                }
                if (sticked_sdnf[i] == '0')
                {
                    pre_min_sdnf += "!" + variables[counter_vars];
                    counter_vars++;
                    continue;
                }
                if (sticked_sdnf[i] == 'X')
                {
                    counter_vars++;
                    continue;
                }
                if (sticked_sdnf[i] == ')')
                {
                    pre_min_sdnf = pre_min_sdnf.Insert(pre_min_sdnf.Length, ")|");
                    counter_vars = 0;
                }
            }
            pre_min_sdnf = pre_min_sdnf.Remove(pre_min_sdnf.Length - 1, 1);
            sticked_sdnf = pre_min_sdnf;
        }
        private void Compare_and_stick_lexemes_sknf(ref List<string> lexemes_, ref List<string> lexs_in_the_end_, int counter)
        {
            List<Tuple<int, int>> tuple_list = [];

            int lex_count = lexemes_.Count, length_lex = lexemes_[0].Length, counter_vars = 0, k = 1, vars_need_to_stick = lexemes_[0].Length - counter;
            bool make_new_lexeme = false;
            for (int i = 0; i < lex_count - 1; i++)
            {

                for (int j = k; j < lex_count; j++)
                {

                    for (int l = 1; l < length_lex; l++)
                    {
                        if ((lexemes_[i][l] == '1' || lexemes_[i][l] == '0') && lexemes_[j][l] == 'X' || (lexemes_[j][l] == '1' || lexemes_[j][l] == '0') && lexemes_[i][l] == 'X')
                            break;
                        if (counter_vars == vars_need_to_stick)
                        {
                            make_new_lexeme = !make_new_lexeme; // make skleika
                            break;
                        }
                        if (lexemes_[i][l] == 'X' && lexemes_[j][l] == 'X') continue;


                        if (lexemes_[i][l] == ')' && lexemes_[j][l] == ')') break;
                        if (lexemes_[i][l] == '1' && lexemes_[j][l] == '1')
                        {
                            counter_vars++;
                            continue;
                        }
                        if (lexemes_[i][l] == '0' && lexemes_[j][l] == '0')
                        {
                            counter_vars++;
                            continue;
                        }
                        if ((lexemes_[i][l] == '1' && lexemes_[j][l] == '0') || (lexemes_[i][l] == '0' && lexemes_[j][l] == '1')) continue;
                    }
                    counter_vars = 0;
                    if (make_new_lexeme)
                    {
                        tuple_list.Add(Tuple.Create(i, j)); // pairs of original lexemes
                        make_new_lexeme = !make_new_lexeme;
                    }

                }
                k++;
            }

            if (tuple_list.Count == 0)
            {
                for (int i = 0; i < lexs_in_the_end_.Count; i++)
                {
                    lexemes_.Add(lexs_in_the_end_[i]);
                }
                Print_min_sknf_phase(lexemes_);
                throw new Exception("Склеиваний больше нет!");
            }
            else
            {
                List<int> odd_lexs = Find_the_odd_lexs(tuple_list, lex_count);
                if (odd_lexs.Count == 0) { Console.WriteLine("\n Несклеянных лексем нет!\n"); }
                else
                {
                    for (int i = 0; i < odd_lexs.Count; i++)
                    {
                        lexs_in_the_end_.Add(lexemes_[odd_lexs[i]]);
                    }
                }
                Stick_lexemes(ref lexemes_, tuple_list);

                Print_min_sknf_phase(lexemes_);
            }

        }
        private void Stick_sknf()
        {
            try
            {
                if (sknf != "")
                {
                    int counter = 3;
                    List<string> lexemes = new(Make_lexemes(Sknf));
                    List<string> lexemes_in_the_end = [];
                    while (true)
                    {
                        Console.WriteLine("Фаза склеивания СКНФ");
                        Compare_and_stick_lexemes_sknf(ref lexemes, ref lexemes_in_the_end, counter);
                        counter++;
                    }
                }
                else
                {
                    throw new Exception("СКНФ нет");
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        private void Make_sticked_sknf()
        {
            int counter_vars = 0;
            string pre_min_sknf = "";
            for (int i = 0; i < sticked_sknf.Length; i++)
            {
                if (sticked_sknf[i] == '(')
                {
                    pre_min_sknf = pre_min_sknf.Insert(pre_min_sknf.Length, "(");
                }
                if (sticked_sknf[i] == '1')
                {
                    pre_min_sknf += variables[counter_vars];
                    counter_vars++;
                    continue;
                }
                if (sticked_sknf[i] == '0')
                {
                    pre_min_sknf += "!" + variables[counter_vars];
                    counter_vars++;
                    continue;
                }
                if (sticked_sknf[i] == 'X')
                {
                    counter_vars++;
                    continue;
                }
                if (sticked_sknf[i] == ')')
                {
                    pre_min_sknf = pre_min_sknf.Insert(pre_min_sknf.Length, ")&");
                    counter_vars = 0;
                }
            }
            pre_min_sknf = pre_min_sknf.Remove(pre_min_sknf.Length - 1, 1);
            sticked_sknf = pre_min_sknf;
        }


        private List<string> Make_lexemes_for_table(string str_sdnf_or_sknf)
        {
            int counter = 0;
            List<string> lexemes_ = new List<string>();
            for (int i = 0; i < str_sdnf_or_sknf.Length; i++)
            {

                if (str_sdnf_or_sknf[i] == '!')
                {
                    lexemes_[counter] = lexemes_[counter].Insert(lexemes_[counter].Length, $"{str_sdnf_or_sknf[i]}{str_sdnf_or_sknf[i + 1]}");
                    i++;
                    continue;
                }
                if ((str_sdnf_or_sknf[i] >= 'a' && str_sdnf_or_sknf[i] <= 'z') || (str_sdnf_or_sknf[i] >= 'A' && str_sdnf_or_sknf[i] <= 'Z') && str_sdnf_or_sknf[i - 1] != '!')
                {
                    lexemes_[counter] = lexemes_[counter].Insert(lexemes_[counter].Length, str_sdnf_or_sknf[i].ToString());
                    continue;
                }
                if (str_sdnf_or_sknf[i] == '&') continue;
                if (str_sdnf_or_sknf[i] == '|') continue;
                if (str_sdnf_or_sknf[i] == ')')
                {
                    lexemes_[counter] = lexemes_[counter].Insert(lexemes_[counter].Length, str_sdnf_or_sknf[i].ToString());
                    counter++;
                    continue;
                }
                if (str_sdnf_or_sknf[i] == '(')
                    lexemes_.Add("");
                lexemes_[counter] = lexemes_[counter].Insert(0, str_sdnf_or_sknf[i].ToString());
            }
            return lexemes_;
        }
        private bool Contains_in(string a, string b)
        {

            List<string> vars = new();
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == '!')
                    if ((a[i + 1] >= 'a' && a[i + 1] <= 'z') || (a[i + 1] >= 'A' && a[i + 1] <= 'Z'))
                    {
                        vars.Add(a[i].ToString() + a[i + 1].ToString());
                        i++;
                        continue;
                    }
                if (i != 0)
                    if ((a[i] >= 'a' && a[i] <= 'z') || (a[i] >= 'A' && a[i] <= 'Z') && a[i - 1] != '!')
                        vars.Add(a[i].ToString());
                if (i == 0 && (a[i] >= 'a' && a[i] <= 'z') || (a[i] >= 'A' && a[i] <= 'Z'))
                    vars.Add(a[i].ToString());
            }
            for (int i = 0; i < vars.Count; i++)
            {
                if (vars[i].Length != 1 && b.Contains(vars[i]))
                    continue;
                if (vars[i].Length != 1 && !b.Contains(vars[i]))
                    return false;
                if (vars[i].Length == 1)
                    if (b.Contains("!" + vars[i]))
                        return false;

            }
            return true;

        }
        private List<List<string>> Make_table_for_method(string sdnf_or_sknf, string sticked_sdnf_or_sknf)
        {
            List<string> temp = [];
            List<string> lexemes = new List<string>(Make_lexemes_for_table(sdnf_or_sknf));
            List<string> sticked = new List<string>(Make_lexemes_for_table(sticked_sdnf_or_sknf));
            List<List<string>> table = new();
            for (int i = 0; i < lexemes.Count + 1; i++)
                temp.Add("");

            for (int i = 0; i < sticked.Count + 1; i++)
            {
                List<string> buf = new(temp);
                table.Add(buf);
            }
            for (int i = 1, j = 0; i < table[0].Count; j++, i++)
                table[0][i] = lexemes[j];
            for (int i = 1, j = 0; i < table.Count; j++, i++)
                table[i][0] = sticked[j];
            for (int i = 1; i < table[0].Count; i++)// fix
            {
                for (int j = 1; j < table.Count; j++)
                {
                    string buf_sticked = table[j][0].Trim(['(', ')']), buf_lex = table[0][i].Trim(['(', ')']);
                    if (Contains_in(buf_sticked, buf_lex))
                        table[j][i] = "1";
                    else table[j][i] = "0";

                }
            }
            return table;
        }
        private void Comparing_and_deleting_useless(ref List<string> lexemes)
        {

            int k = 2, counter_one = 0, index = -1;
            for (int i = 1; i < table_for_method.Count; i++)
            {

                for (int j = k; j < table_for_method.Count; j++)
                {
                    if (table_for_method[i].SequenceEqual(table_for_method[j]))
                    {
                        table_for_method.Remove(table_for_method[j]);
                        j--;
                    }
                }
                k++;
            }
            for (int j = 1; j < table_for_method[0].Count; j++)
            {
                for (int i = 1; i < table_for_method.Count; i++)
                {
                    if (table_for_method[i][j] == "1")
                    {
                        counter_one++;
                        index = i;
                    }
                    if (counter_one > 1)
                        break;
                    if (i == table_for_method.Count - 1 && counter_one == 1)
                        lexemes.Add(table_for_method[index][0]);
                }
                index = -1;
                counter_one = 0;
            }
        }
        private void Print_table_for_min()
        {
            Console.Write("     ");
            for (int i = 0; i < table_for_method[0].Count; i++)
                Console.Write("   " + table_for_method[0][i]);
            Console.WriteLine();
            for (int i = 1; i < table_for_method.Count; i++)
            {
                for (int j = 0; j < table_for_method[i].Count; j++)
                {
                    Console.Write("\t" + table_for_method[i][j]);
                }
                Console.WriteLine();
            }
        }
        private void Minimize_by_сounting_table_method(string sdnf_or_sknf, string sticked_sdnf_or_sknf)
        {
            List<string> new_lexemes = new();
            table_for_method = new(Make_table_for_method(sdnf_or_sknf, sticked_sdnf_or_sknf));

            Comparing_and_deleting_useless(ref new_lexemes);

            if (sticked_sdnf_or_sknf == sticked_sdnf)
            {
                Console.WriteLine("\tТаблица для СДНФ:");
                Print_table_for_min();
                for (int i = 0; i < new_lexemes.Count; i++)
                {
                    min_sdnf_by_table += "|" + new_lexemes[i];
                }
                min_sdnf_by_table = min_sdnf_by_table.Remove(0, 1);
            }
            else
            {
                Console.WriteLine("\tТаблица для СКНФ:");
                Print_table_for_min();
                for (int i = 0; i < new_lexemes.Count; i++)
                {
                    min_sknf_by_table += "&" + new_lexemes[i];
                }
                min_sknf_by_table = min_sknf_by_table.Remove(0, 1);
            }

        }



    private void Revive_sticked(ref string sticked, string sticked_sdnf_or_sknf)
        {
            char op, neg_op;
            if (sticked_sdnf_or_sknf == sticked_sdnf)
            {
                op = '&';
                neg_op = '|';
            }
            else 
            {
                op = '|';
                neg_op = '&';
            }
            for (int i = 1; i < sticked.Length-1; i++)
            {
                if (((sticked[i+1] >= 'a' && sticked[i+1] <= 'z') || (sticked[i+1] >= 'A' && sticked[i+1] <= 'Z')) && sticked[i - 1] != '(' && sticked[i] == '!')
                {
                    sticked = sticked.Insert(i, op.ToString());
                    i++;
                    continue;
                }
                    if (((sticked[i] >= 'a' && sticked[i] <= 'z') || (sticked[i] >= 'A' && sticked[i] <= 'Z')) && sticked[i-1] != '(' && sticked[i - 1] != '!')
                {
                    sticked = sticked.Insert(i, op.ToString());
                    i++;
                    continue;
                }

            }
            int index_left , index_right ;
            for (int i = 1; i < sticked.Length - 1; i++)
            {
                index_left = i;
                index_right = i;
                if (sticked[i] == op)
                {
                    if (sticked[i - 1] == ')')
                        continue;
                    if ((sticked[i - 1] >= 'a' && sticked[i - 1] <= 'z') || (sticked[i - 1] >= 'A' && sticked[i - 1] <= 'Z'))
                    {
                        index_left--;
                        if (i - 2 > 0)
                        {
                            if (sticked[i - 2] == '!')
                                index_left--;
                        }
                    }
                    if (((sticked[i + 1] >= 'a' && sticked[i + 1] <= 'z') || (sticked[i + 1] >= 'A' && sticked[i + 1] <= 'Z')) || sticked[i + 1] == '!')
                    {
                        index_right++;
                        if (sticked[i + 1] == '!')
                            index_right++;
                    }
                    sticked = sticked.Insert(index_left - 1, "(");
                    i++;
                    sticked = sticked.Insert(index_right + 2, ")");
                    i++;
                }
                
                
            }
            for (int i = 1; i < sticked.Length - 1; i++)
            {
                if (sticked[i] == '!')
                {
                    sticked = sticked.Insert(i, "(");
                    i++;
                    sticked = sticked.Insert(i +2, ")");
                    i++;
                }
            }
            if (sticked[0] == neg_op)
            {
                sticked = sticked.Remove(0, 1);
            }
            if (sticked[sticked.Length-1] == neg_op)
            {
                sticked = sticked.Remove(sticked.Length-1, 1);
            }
            else if(sticked[0] != neg_op && sticked[sticked.Length - 1] != neg_op)
            {
                for (int i = 0; i < sticked.Length; i++)
                {
                    if(i != sticked.Length-1)
                        if (sticked[i] == neg_op && (sticked[i + 1] == neg_op || sticked[i-1] == neg_op))
                            sticked = sticked.Remove(i, 1);
                }
            }
            sticked = sticked.Insert(0, "(");
            sticked = sticked.Insert(sticked.Length, ")");
        }
    private bool Check_truth_tables_for_equality(List<List<int>> orig_table, List<List<int>> compared_table)
        {
            for (int i = 0; i < orig_table.Count; i++)
            {
                if (orig_table[i][variables.Count] != compared_table[i][variables.Count])
                    return true;
                
            }
            return false;
        }
    private void Minimize_by_counting_method(string sticked_sdnf_or_sknf)
        {
            string temp_sticked_sdnf_or_sknf = sticked_sdnf_or_sknf; // для таблицы
            List<string> lexemes = new(Make_lexemes_for_table(sticked_sdnf_or_sknf)); // для списка лексем
            List<List<int>> sticked_table = new();
            List<int> ind_lexemes_to_delete = new();
            Revive_sticked(ref temp_sticked_sdnf_or_sknf, sticked_sdnf_or_sknf);
            Reverse_polish_notation RPN_1 = new(temp_sticked_sdnf_or_sknf);
            Fill_table(ref sticked_table, RPN_1.Output_str);
            
            for (int i = 0; i < lexemes.Count; i++)
            {
                List<List<int>> temp_table = new();
                string temp = sticked_sdnf_or_sknf;
                temp = temp.Replace(lexemes[i], "");// на исходнике проще выполнять удаление лексемы
                if (temp.Length == 0)
                    break;
                Revive_sticked(ref temp, sticked_sdnf_or_sknf); 
                Reverse_polish_notation RPN_2 = new(temp);
                Fill_table(ref temp_table,RPN_2.Output_str);
                if(!Check_truth_tables_for_equality(sticked_table,temp_table)) 
                    ind_lexemes_to_delete.Add(i);
                
            }
            if (ind_lexemes_to_delete.Count != 0)
            {
                for (int i = 0; i < ind_lexemes_to_delete.Count; i++)
                    lexemes.Remove(lexemes[ind_lexemes_to_delete[i]]);
            }
            if (sticked_sdnf_or_sknf == sticked_sdnf)
            {
   
                for (int i = 0; i < lexemes.Count; i++)
                {
                    min_sdnf_by_counting += "|" + lexemes[i];
                }
                min_sdnf_by_counting = min_sdnf_by_counting.Remove(0, 1);
            }
            else
            {
                for (int i = 0; i < lexemes.Count; i++)
                {
                    min_sknf_by_counting += "&" + lexemes[i];
                }
                min_sknf_by_counting = min_sknf_by_counting.Remove(0, 1);
            }

        }
        public int Index_form
        {
            get
            {
                return index_form;
            }
        }
        public string Min_sdnf_by_counting
        {
            get
            {
                return min_sdnf_by_counting;
            }
        }
        public string Min_sknf_by_counting
        {
            get
            {
                return min_sknf_by_counting;
            }
        }
        public string Min_sdnf_by_table
        {
            get
            {
                return min_sdnf_by_table;
            }
        }
        public string Min_sknf_by_table
        {
            get
            {
                return min_sknf_by_table;
            }
        }
        public string Sticked_sdnf
        {
            get
            {
                return sticked_sdnf;
            }
        }
        public string Sticked_sknf
        {
            get
            {
                return sticked_sknf;
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
