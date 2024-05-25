using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using BinaryNumbers;
namespace Operations_bin
{
    public class Operation_bin
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
        public string Addition_float(Binary_IEEE a , Binary_IEEE b )
        {
            string bin_exp = a.Binary_exp;
            string mant_1 = a.Mantissa.Insert(0, "1");
            string mant_2 = b.Mantissa.Insert(0,"1");
            if (a.Exp < b.Exp)
            {
                bin_exp = b.Binary_exp;
                for (int i = 0; i< b.Exp-a.Exp;i++) 
                {
                    mant_1= mant_1.Insert(0, "0");
                    mant_1 = mant_1.Remove(mant_1.Length - 1, 1);
                }
            }
            if (b.Exp < a.Exp)
            {
                bin_exp = a.Binary_exp;
                for (int i = 0; i < a.Exp - b.Exp; i++)
                {
                    mant_2 = mant_2.Insert(0, "0");
                    mant_2 = mant_2.Remove(mant_2.Length - 1, 1);
                }
            }
            mant_1 = mant_1.Insert(0, "00"); // нолики для увеличения экспоненты 
            mant_2 = mant_2.Insert(0, "00");
            result = Mechanical_Addition(mant_1, mant_2);
            if (result[0] == '1')
            {
                bin_exp = bin_exp.Insert(0, "0");
                bin_exp = Mechanical_Addition(bin_exp, "01");
                result = result.Remove(0, 1);
            }
            else
            {
                result = result.Remove(0, 2);
                result = result.Insert(result.Length, "0");
            }
            result = "0" + bin_exp + result;
            result = result.Remove(result.Length-1, 1); ;
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
        public bool Compare(string bigger, string smaller)
        {   
            if(bigger.Length > smaller.Length)
            {
                int length_small = smaller.Length;
                for(int i = 0; i < bigger.Length- length_small; i++)
            {
                smaller = smaller.Insert(0,"0");
            }
            }
            if(bigger.Length < smaller.Length)
            {
                int length_big = bigger.Length;
                for(int i = 0; i < smaller.Length - length_big; i++)
            {
               bigger = bigger.Insert(0,"0");
            }
            }
            for(int i = 1; i < bigger.Length; i++)
            {
                if(bigger[i] == '0' && smaller[i] == '0' ){continue;}
                if(bigger[i] == '1' && smaller[i] == '1'){continue;}
                if(bigger[i] == '1' && smaller[i] == '0'){return true;}
                else if(bigger[i] == '0' && smaller[i] == '1'){return false;}

            }
            return true;
        }   
        public string Cut_off(string a, int step)
        {
            string result = "";
            bool step_active = false;
            for( int i = 1; i < a.Length;i++)
            {   
                if( step == 0)
                {
                    break;
                }
                if(a[i] == '0' && step_active)
                {
                    step--;
                    result = result.Insert(result.Length,"0");
                }
                if(a[i] == '0' && step_active == false)
                {
                   continue;
                }
                if(a[i] == '1')
                {   step_active = true;
                    step--;
                    result = result.Insert(result.Length,"1");
                }
            }
            for(int i = 0; i < 16-result.Length; i++)
            {
                result = result.Insert(0,"0");
            }
        return result;

        }
        public int Count_num_part(string a)
        {
            int count = 0;
            bool is_num_part = false;
            for( int i = 1; i < a.Length;i++)
            {   
                if(a[i] == '0' && is_num_part == false ){ continue;}
                if(a[i] == '0' && is_num_part ){ count++;}
                if(a[i] == '1')
                { 
                    count++;
                    is_num_part = true;
                }
            }
            return count;
        }
        public string Division(Binary a, Binary b)
        {  
            //if( a.Straight_binary == b.Straight_binary)   
            // building full part
            bool first_op = false, full_part_is_zero = true;
            int demolition, count_part = Count_num_part(a.Straight_binary);
            string result = "" , buf1 = a.Straight_binary;
            for( int i = 1 ; i <= count_part ; i++)
            {
                if(Compare(Cut_off(buf1,i), b.Straight_binary) && !first_op) // first operation
                {   
                    if( i != count_part )
                    {   int len_buf;
                    full_part_is_zero = false;
                        demolition = 15 - count_part  + i + 1; // number for demolition
                        buf1 = Cut_off(buf1,i);
                        len_buf = buf1.Length;
                        for(int j = 0; j< 15 - len_buf;j++)
                        {
                            buf1 = buf1.Insert(0,"0");
                        }
                        buf1 = Mechanical_Addition(buf1, b.Additional_binary).Insert(buf1.Length, a.Straight_binary[demolition].ToString()) ;
                        first_op = true;
                        result = result.Insert(result.Length,"1");
                        continue;
                    }
                    else
                    { // срез не нужен, т.к. численная часть закончилась и мы здесь заканчиваем вычисление целой части
                        buf1 = Mechanical_Addition(buf1, b.Additional_binary);
                        result = result.Insert(result.Length,"1");
                        break;
                    }
                }
                //
                 else if( !Compare(buf1,b.Straight_binary) && i == count_part && first_op)
                 {
                  int len_buf = buf1.Length;
                  if(len_buf >= 16){buf1 = buf1.Remove(0,len_buf-15);}
                    result = result.Insert(result.Length,"0");
                }
                else if( Compare(buf1,b.Straight_binary) && first_op)
                {   
                  if( i != count_part )
                  {
                  int len_buf = buf1.Length;
                  demolition = 15 - count_part  + i + 1;
                  if(len_buf >= 16){buf1 = buf1.Remove(0,len_buf-15);}
                  if(Mechanical_Addition(buf1, b.Additional_binary) == "000000000000000")
                  {
                  buf1 = Mechanical_Addition(buf1, b.Additional_binary);
                  }
                  else
                  {
                    buf1 = Mechanical_Addition(buf1, b.Additional_binary).Insert(buf1.Length,a.Straight_binary[demolition].ToString() );
                  }
                  result = result.Insert(result.Length,"1");
                  continue;
                  }
                  else
                  {
                    buf1 = Mechanical_Addition(buf1, b.Additional_binary);
                    result = result.Insert(result.Length,"1");
                    break;
                  }
                }
                else if( !Compare(buf1,b.Straight_binary) && first_op && i != count_part)// если число получилось маленькое, еще раз сносим 
                {
                    demolition = 15 - count_part  + i + 1;
                    buf1 = buf1.Insert(buf1.Length,a.Straight_binary[demolition].ToString() );
                    int len_buf = buf1.Length;
                    buf1 = buf1.Remove(0,len_buf-16);
                    result = result.Insert(result.Length,"0");
                }
               

            }
            first_op = false;
            string fract_part = "";
            for(int i = 0; i< 5; i++)
            {
                

                    if(Compare(buf1.Insert(buf1.Length,"0"), b.Straight_binary))
                    {
                    buf1 = buf1.Insert(buf1.Length,"0");
                    int len_buf = buf1.Length;
                    buf1 = buf1.Remove(0,len_buf-16);
                    buf1 = Mechanical_Addition(buf1, b.Additional_binary);
                    fract_part = fract_part.Insert(fract_part.Length,"1");
                    continue;
                    }
                    else if(!Compare(buf1.Insert(buf1.Length,"0"), b.Straight_binary))
                    {
                        buf1 = buf1.Insert(buf1.Length,"0");
                        fract_part = fract_part.Insert(fract_part.Length,"0");
                    }
            }
           if(full_part_is_zero)
           {
            for(int i = 0; i< 11; i++)
            {
                result = result.Insert(0,"0");
            }
          
            return result += fract_part;
           }
           else
           {
           int result_len = result.Length;
            for(int i = 0; i < 11 - result_len; i++)
           {
                result = result.Insert(0,"0");
           }
                
                return result += fract_part;
           }
           
        }
    }
}