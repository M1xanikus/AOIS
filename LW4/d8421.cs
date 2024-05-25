using System;
using Operations_bin;
using truth_table;
public class D8421
{
	public D8421(string binary)
	{
		_binary = binary;
        for (int i = 0; i < _binary.Length; i++)
        {
            if (_binary[i] ==  '0') 
                binary_orig.Add(0);
            else
                binary_orig.Add(1);
        }
            
        Fill_table();
	}
	public string _binary = "", min_sdnf_1 = "", min_sdnf_2 = "" , min_sdnf_3 = "", min_sdnf_4 = "";
    List<int> binary_orig = new();
    private List<List<int>> table_1 = new List<List<int>>();
    private List<List<int>> table_2 = new List<List<int>>();
    private List<int> Addition_one(List<int> list)
    {
        List<int> temp = list;
        bool buf = false;
        for (int i = temp.Count - 1; i >= 0; i--)
        {
            if (temp[i] == 1 && buf == false && i == temp.Count - 1)
            {
                temp[i] = 0;
                buf = true;
                continue;
            }
            if (temp[i] == 0 && buf == false && i == temp.Count - 1)
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
    private void Fill_table()
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < 4; i++) temp.Insert(0, 0);
        table_1.Insert(0, temp);
        for (int i = 1; i < 10; i++)
        {
            List<int> buf = new List<int>(table_1[i - 1]);
            table_1.Add(Addition_one(buf));
        }
        List<int> temp_2 = new List<int>(binary_orig);
        table_2.Insert(0, temp_2);
        for (int i = 1; i < 10; i++)
        {
            List<int> buf = new List<int>(table_2[i - 1]);
            table_2.Add(Addition_one(buf));
        }
    }
    public void Print_tables()
    {
        Console.Write("A B C D E F G H");
        Console.WriteLine();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Console.Write($"{table_1[i][j]} ");
            }
            for (int j = 0; j < 4; j++)
            {
                Console.Write($"{table_2[i][j]} ");
            }
            Console.Write("\n");
        }
        
    }

    public void Minimize()
    {
        List<List<int>> transfer_table = table_1.ToList();
        for (int i = 0; i < 10; i++)
            transfer_table[i].Add(table_2[i][0]);
        Truth_table first = new(transfer_table);
        min_sdnf_1 = first.Min_sdnf_by_counting;

        List<List<int>> transfer_table_2 = table_1.ToList();
        for (int i = 0; i < 10; i++)
            transfer_table_2[i][4] = table_2[i][1];
        Truth_table sec = new(transfer_table_2);
        min_sdnf_2 = sec.Min_sdnf_by_counting;

        List<List<int>> transfer_table_3 = table_1.ToList();
        for (int i = 0; i < 10; i++)
            transfer_table_3[i][4] = table_2[i][2];
        Truth_table third = new(transfer_table_3);
        min_sdnf_3 = third.Min_sdnf_by_counting;

        List<List<int>> transfer_table_4 = table_1.ToList();
        for (int i = 0; i < 10; i++)
            transfer_table_4[i][4] = table_2[i][3];
        Truth_table fourth = new(transfer_table_4);
        min_sdnf_4 = fourth.Min_sdnf_by_counting;

    }

}
