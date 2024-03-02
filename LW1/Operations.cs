using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using BinaryNumbers;
namespace Operations
{
    public class Operation
    {
        private string result = "";
        
        private string Mechanical_Addition(string binary1,string binary2)
        {
            string result = "";
            bool buf = false;
            int length = 0;
            if(binary1.Length == binary2.Length) { length = binary1.Length; }
            if (binary1.Length > binary2.Length)
            {
                length = binary1.Length;
                binary2 = binary2.Remove(0, 1);
                binary2 = binary2.Insert(0,"0");
                binary1 = binary1.Remove(0,1);
                binary1 = binary1.Insert(0, "0");
                for (int i = (binary1.Length - binary2.Length); i > 0; i--)
                {
                    binary2 = binary2.Insert(0, "0"); }
                }
            else if (binary1.Length < binary2.Length)
            {
                length = binary2.Length;
                binary2 = binary2.Remove(0,1);
                binary2 = binary2.Insert(0, "0");
                binary1 = binary1.Remove(0,1);
                binary1 = binary1.Insert(0, "0");
                for (int i = (binary2.Length - binary1.Length); i > 0; i--) binary1 = binary1.Insert(0, "0");
            }
            for (int i = length - 1 ; i > 0; i--)
            {
                if (binary1[i] == '1' && binary2[i] == '1' && buf == false)
                {
                    result = result.Insert(0, "0");
                    buf = true;
                    continue;
                }
                else if ((binary1[i] == '0' && binary2[i] == '1' ||
                    binary1[i] == '1' && binary2[i] == '0')
                    && buf == false)
                {
                    result = result.Insert(0, "1");
                    continue;
                }
                else if ((binary1[i] == '0' && binary2[i] == '1' ||
                    binary1[i] == '1' && binary2[i] == '0')
                    && buf)
                {
                    result = result.Insert(0, "0");
                    buf = true;
                    continue;
                }
                else if (binary1[i] == '1' && binary2[i] == '1' && buf)
                {
                    result = result.Insert(0, "1");
                    buf = true;
                    continue;
                }
                else if (binary1[i] == '0' && binary2[i] == '0' && buf)
                {
                    result = result.Insert(0, "1");
                    buf = false;
                    continue;
                }
                else if (binary1[i] == '0' && binary2[i] == '0' && buf == false)
                {
                    result = result.Insert(0, "0");
                    continue;
                }
            }
            return result;
        }
        public string Addition(Binary a, Binary b)
        {
            //bool buf = false;
            if (a.Number >= 0 && b.Number >= 0)
            {
                return Mechanical_Addition(a.Straight_binary, b.Straight_binary).Insert(0, "0");
            }
            if (a.Number > 0 && b.Number < 0 && Math.Abs(b.Number) > a.Number)
            {
                string result = Mechanical_Addition(a.Straight_binary, b.Additional_binary).Insert(0, "1");
                return a.Make_Additional(a.Make_Reverse(result));
            }
            else if (b.Number > 0 && a.Number < 0 && Math.Abs(a.Number) > b.Number)
            {
                string result = Mechanical_Addition(b.Straight_binary, a.Additional_binary).Insert(0, "1");
                return a.Make_Additional(a.Make_Reverse(result));
            }
                if (a.Number > 0 && b.Number < 0 && Math.Abs(b.Number) < a.Number)
            {
                return Mechanical_Addition(a.Straight_binary, b.Additional_binary).Insert(0, "0");
            }
             else if (b.Number > 0 && a.Number < 0 && Math.Abs(a.Number) < b.Number)
            {
                return Mechanical_Addition(b.Straight_binary, a.Additional_binary).Insert(0, "0");
            }
            if (a.Number < 0 && b.Number < 0)
            {
                string result = Mechanical_Addition(a.Additional_binary, b.Additional_binary).Insert(0, "1");
                return a.Make_Additional(a.Make_Reverse(result));
            }
            if (a.Number == -b.Number)
            {
                string result = Mechanical_Addition(a.Straight_binary, b.Additional_binary).Insert(0, "0");
                return a.Make_Additional(a.Make_Reverse(result));
            }
            else if (b.Number == -a.Number)
            {
                string result = Mechanical_Addition(b.Straight_binary, a.Additional_binary).Insert(0, "0");
                return a.Make_Additional(a.Make_Reverse(result));
            }
            else return "";
        }
        public string Multiplication(Binary a, Binary b)
        {
            result = a.Straight_binary;
            string buf= a.Straight_binary; 
            bool is_buf = false;
            int margin = 0;
            int length = a.Straight_binary.Length;
            for (int i = length -1; i > 0; i--)
            {
                if (b.Straight_binary[i] == '0') 
                {
                    margin++;
                    continue; 
                }
                if (b.Straight_binary[i] == '1' && is_buf == false )
                {
                    for (int j = margin; j > 0; j--)
                    {
                        result = result.Insert(a.Straight_binary.Length, "0");
                    }
                    margin++;
                    is_buf = true;
                    continue;
                }
                if (b.Straight_binary[i] == '1' && is_buf)
                {
                    for (int j = margin; j > 0; j--)
                        buf = buf.Insert(buf.Length, "0");
                    margin++;
                    result = Mechanical_Addition(result, buf);
                    buf = a.Straight_binary;
                    continue;
                }
                
            }
            int counter_to_delete = result.Length - 15;
            for (int i = counter_to_delete ; i > 0; i--) 
            {
                result = result.Remove(0,1);
            }
            if(a.Number > 0 && b.Number < 0 || a.Number < 0 && b.Number > 0)
            return result.Insert(0, "1");
            else return result.Insert(0, "0");

        }

        public string Division(Binary_fract a, Binary_fract b)
        {
            return "";
        }
    }
}
