using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP
{
    public class Associative_Processor
    {
        private int[,] table = new int[16, 16] {
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0},
            {1, 1, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {1, 1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0},
            {0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0},
            {0, 0, 1, 0, 1, 1, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0},
            {0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0},
            {0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0},
            {1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 1, 0, 1},
            {0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1},
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        private string AddBinary(string a, string b)
        {
            int counter = 0;
            StringBuilder result = new StringBuilder("00000");
            for (int i = a.Length - 1; i >= 0; i--)
            {
                if (a[i] == '1') counter++;
                if (b[i] == '1') counter++;
                if (counter % 2 != 0) result.Remove(i + 1, 1).Insert(i + 1, '1');
                counter = counter >= 2 ? 1 : 0;
            }
            if (counter == 1) result.Remove(0, 1).Insert(0, '1');
            return result.ToString();
        }


        public string Make_Binary(int value)
        {
            int temp_number = value;
            int remainder, length;
            string result = "";
            while (temp_number / 2 != 0)
            {
                remainder = temp_number % 2;
                temp_number /= 2;
                result = result.Insert(0, remainder.ToString());
            }
            remainder = temp_number % 2;
            result = result.Insert(0, remainder.ToString());
            length = result.Length;
            for (int i = 0; i < 16 - length; i++) { result = result.Insert(0, "0"); }
            return result;
        }

        

        public int Make_Decimal(string input)
        {
            double result = 0;
            double grade = 0;
            for (int i = input.Length - 1; i > 0; i--)
            {
                if (input[i] == '1') { result += Math.Pow(2, grade); }
                grade++;
            }
            int number = (int)result;
            return number;
        }

        public void Display()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    Console.Write(table[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void SortTable()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 16; i++) list.Add(Make_Decimal(GetRow(i)));
            list.Sort();
            for (int i = 0; i < 16; i++)
            {
                AddRow(i, Make_Binary(list[i]));
            }
        }

        public void Adding(string starting_string)
        {
            for (int i = 0; i < 16; i++)
            {
                int counter = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (table[j, i] == starting_string[j] - '0') counter++;
                    if (j == 2 && counter == 3)
                        AddRow(i, RebuildWord(i));
                    
                }
            }
        }
        private string RebuildWord(int index)
        {
            string v = "", a = "", b = "";
            int count = 0;
            for (int i = index; i < Math.Min(11 + index, 16); i++)
            {
                if (count < 3) v += table[i, index].ToString();
                else if (count < 7) a += table[i, index].ToString();
                else b += table[i, index].ToString();
                count++;
            }
            for (int i = 0; count < 11; i++)
            {
                if (count < 3) v += table[i, index].ToString();
                else if (count < 7) a += table[i, index].ToString();
                else b += table[i, index].ToString();
                count++;
            }
            return v + a + b + AddBinary(a, b);
        }
        public string GetRow(int starting_index)
        {
            string result = "";
            for (int i = starting_index; i < 16 + starting_index; i++)
            {
                if (i >= 16)
                {
                    result += table[i - 16, starting_index].ToString();
                }
                else
                {
                    result += table[i, starting_index].ToString();
                }
            }
            return result;
        }

        public void AddRow(int starting_index, string word_to_write)
        {
            for (int i = starting_index; i < 16 + starting_index; i++)
            {
                if (i >= 16)table[i - 16, starting_index] = word_to_write[i - starting_index] - '0';
                
                else
                    table[i, starting_index] = word_to_write[i - starting_index] - '0';
                
            }
        }

        public string GetAddressColumn(int starting_index)
        {
            string result = "";
            for (int i = 0; i < 16; i++)
            {
                if (i + starting_index >= 16)
                {
                    result += table[i + starting_index - 16, i].ToString();
                }
                else
                {
                    result += table[i + starting_index, i].ToString();
                }
            }
            return result;
        }

        public void ComputeDisparity(string firstWord, string secondWord, int index_to_write)
        {
            string result = "";
            for (int i = 0; i < 16; i++)
                result += (firstWord[i] != secondWord[i] ? '0' : '1').ToString();
            AddRow(index_to_write, result);
        }

        public void ComputeEquivalence(string firstWord, string secondWord, int recordingPlace)
        {
            string result = "";
            for (int i = 0; i < 16; i++)
            {
                result += (firstWord[i] == secondWord[i] ? '0' : '1').ToString();
            }
            AddRow(recordingPlace, result);
        }

        public void ProhibitSecond(string firstWord, string secondWord, int recordingPlace)
        {
            string result = "";
            for (int i = 0; i < 16; i++)
                result += (firstWord[i] == '0' && secondWord[i] == '1' ? '0' : '1').ToString();
            AddRow(recordingPlace, result);
        }

        public void ImplSecondToFirst(string firstWord, string secondWord, int indtorec)
        {
            string result = "";
            for (int i = 0; i < 16; i++)
                result += (!(firstWord[i] == '0' && secondWord[i] == '1') ? '0' : '1').ToString();
            AddRow(indtorec, result);
        }
    }

}
