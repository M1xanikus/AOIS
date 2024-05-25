using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using truth_table;
using rev_pol_not;
using BinaryNumbers;
using Operations_bin;
namespace lab5
{
    public class Digital_automaton
    {
        public Digital_automaton()
        {
            
            List<string> temp = new();
            for (int i = 0; i < 10; i++)
                temp.Insert(0, "0");
            for (int i = 0;i < 16; i++)
            {
                List<string> buf = new(temp);
                table.Add(buf);
            }
            for (int i = 0; i < 8; i++) table[i][0] = "1";
            table[0][1] = "1";
            table[0][2] = "1";
            table[0][3] = "1";
            string buf_t = "111";
            
            for (int i = 0; i < 7; i++)
            {
                buf_t = buf_t.Insert(0, "0");
                table[i][3] = buf_t[3].ToString();
                table[i][2] = buf_t[2].ToString();
                table[i][1] = buf_t[1].ToString();
                
                Operation_bin op = new Operation_bin();
                buf_t =  op.Addition(new Binary(buf_t),v);
                table[i][6] = buf_t[3].ToString();
                table[i][5] = buf_t[2].ToString();
                table[i][4] = buf_t[1].ToString();
                if (table[i][1] != table[i][4]) table[i][7] = "1";
                if (table[i][2] != table[i][5]) table[i][8] = "1";
                if (table[i][3] != table[i][6]) table[i][9] = "1";
                buf_t = buf_t.Remove(0, 1);
            }
            table[7][6] = "1";
            table[7][5] = "1";
            table[7][4] = "1";
            if (table[7][1] != table[7][4]) table[7][7] = "1";
            if (table[7][2] != table[7][5]) table[7][8] = "1";
            if (table[7][3] != table[7][6]) table[7][9] = "1";
            buf_t = "111";
            for ( int i = 8 ; i < 15; i++)
            {
                buf_t = buf_t.Insert(0, "0");
                table[i][1] = buf_t[1].ToString();
                table[i][2] = buf_t[2].ToString();
                table[i][3] = buf_t[3].ToString();
                Operation_bin op = new Operation_bin();
                buf_t = op.Addition(new Binary(buf_t), v);
                buf_t = buf_t.Remove(0, 1);

            }
        }

        private static List<List<int>> ConvertToListOfInts(List<List<string>> input)
        {
            return input.Select(sublist => sublist.Select(int.Parse).ToList()).ToList();
        }
        static List<List<int>> RemoveColumns(List<List<int>> matrix, int startIndex)
        {
            List<List<int>> result = new List<List<int>>();
            foreach (var row in matrix)
            {
                List<int> newRow = new List<int>();
                for (int i = 0; i < startIndex && i < row.Count; i++)
                {
                    newRow.Add(row[i]);
                }
                result.Add(newRow);
            }
            return result;
        }
        public void Minimize()
        {
            List<List<int>> table_int = ConvertToListOfInts(table);
            var transfer_table = RemoveColumns(table_int, 5);
            for (int i = 0; i < transfer_table.Count; i++)
                transfer_table[i][4] = table_int[i][7];
            Truth_table first = new(transfer_table);
            min_sdnf_1 = first.Min_sdnf_by_counting;

            var transfer_table_2 = RemoveColumns(table_int, 5);
            for (int i = 0; i < transfer_table_2.Count; i++)
                transfer_table_2[i][4] = table_int[i][8];
            Truth_table sec = new(transfer_table_2);
            min_sdnf_2 = sec.Min_sdnf_by_counting;

            var transfer_table_3 = RemoveColumns(table_int, 5);
            for (int i = 0; i < transfer_table_3.Count; i++)
                transfer_table_3[i][4] = table_int[i][9];
            Truth_table third = new(transfer_table_3);
            min_sdnf_3 = third.Min_sdnf_by_counting;
        }

        public void Print_Table()
        {
            Console.WriteLine("V 1 2 3 1 2 3 A B C");
            for (int i = 0; i < table.Count; i++)
            {
                for (int j = 0; j < table[i].Count; j++)
                {
                    Console.Write($"{table[i][j]} "); 
                }
                Console.WriteLine();
            }
        }
        private Binary v = new("1001");
        private List<List<string>> table = new();
        public string min_sdnf_1 = "", min_sdnf_2 = "", min_sdnf_3 = "";
    }
}
