using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BinaryNumbers
{
    public class Binary_IEEE
    {
        public Binary_IEEE(float number)
        {
            this.number = number;
            Make_Binary();
        }
        public Binary_IEEE(string binary)
        {
            this.binary_float = binary;
            Make_Decimal();
        }
        
        private float number;
        private int exp = 0;
        private string mantissa = "";
        private string binary_exp = "";
        private string binary_float = "";
        private bool has_a_sign = false;
        private void Count_exp(string full_part, string fract_part)
        {
            if (full_part.Length == 0)
            {
                for (int i = 0; i < fract_part.Length; i++)
                {
                    exp--;
                    if (fract_part[i] == '1') return;
                    else if (fract_part[i] == '0') continue;
                }
            }
            exp = full_part.Length - 1;
        }
        private void Make_binary_exp()
        {
            int temp_number = exp + 127;
            int remainder, length;
            while (temp_number / 2 != 0)
            {
                remainder = temp_number % 2;
                temp_number /= 2;
                binary_exp = binary_exp.Insert(0, remainder.ToString());
            }
            remainder = temp_number % 2;
            binary_exp = binary_exp.Insert(0, remainder.ToString());
            length = binary_exp.Length;
            for (int i = 0; i < 8 - length; i++)
               binary_exp = binary_exp.Insert(0, "0");
                
        }
        private void Make_Decimal_exp()
        {
            double result = 0;
            double grade = 0;
            this.binary_exp = binary_float.Substring(1, binary_float.Length - 24);
            for (int i = binary_exp.Length - 1; i >= 0; i--)
            {
                if (binary_exp[i] == '1') { result += Math.Pow(2, grade); }
                grade++;
            }
           
            this.exp = (int)result - 127;
        }
        public double Make_Decimal()
        {
            string full_part = "";
            string fract_part = "";
            Make_Decimal_exp();
            mantissa = binary_float.Substring(9);
            if (exp < 0)
            {
                fract_part = mantissa.Insert(0, "1");
                for (int i = 0; i < Math.Abs(exp)-1; i++)
                {
                    fract_part = fract_part.Insert(0, "0");
                }
                double grade = -1;
                double fractional_part = 0;
                for (int i = 0; i < fract_part.Length; i++)
                {
                    if (fract_part[i] == '1') { fractional_part += Math.Pow(2, grade); }
                    grade--;
                }
                this.number = (float)fractional_part;
                return fractional_part;
            }
            else
            {
                double result = 0;
                full_part = mantissa.Insert(0, "1");
                full_part = full_part.Substring(0, exp + 1);
                fract_part = mantissa.Insert(0, "1");
                fract_part = fract_part.Substring(exp + 1);
                double grade = 0;
                double fractional_part = 0;
                for (int i = full_part.Length -1 ; i >= 0; i--)
                {
                    if (full_part[i] == '1') { result += Math.Pow(2, grade); }
                    grade++;
                }
                grade = -1;
                for (int i = 0; i < fract_part.Length; i++)
                {
                    if (fract_part[i] == '1') { fractional_part += Math.Pow(2, grade); }
                    grade--;
                }
                result += fractional_part;
                this.number = (float)result;
                return result;

            }
        }
        public string Make_Binary()
        {
            if (number.ToString()[0] == '-') { has_a_sign = true; }
            int full_length;
            string full_binary = "", fract_binary = "";
            double remainder;
            double fractional_part = this.number - Math.Truncate(this.number);
			int full_part = (int)Math.Abs(Math.Truncate(this.number));
            
            
                while (full_part / 2 != 0)
                {
                    remainder = full_part % 2;
                    full_part /= 2;
                    full_binary = full_binary.Insert(0, remainder.ToString());
                }
                remainder = full_part % 2;
                full_binary = full_binary.Insert(0, remainder.ToString());
            if (full_binary[0] == '0' && full_binary.Length == 1 ) full_binary = "";
            
            full_length = full_binary.Length;
            for (int i = 0; i < 23; i++)
            {
                fract_binary = fract_binary.Insert(fract_binary.Length, Math.Truncate((fractional_part * 2)).ToString());
                fractional_part = fractional_part * 2 - Math.Truncate((fractional_part * 2));
            }
            Count_exp(full_binary, fract_binary);
            Make_binary_exp();
            if (has_a_sign) binary_float = binary_float.Insert(0, "1"); 
            else binary_float = binary_float.Insert(0, "0");
            binary_float += binary_exp;
            if (exp < 0)
            {
                int fract_length;
                fract_binary = fract_binary.Substring(Math.Abs(exp));
                fract_length = fract_binary.Length;
                for (int i = 0 ;i < 23 - fract_length ;i++) 
                {
                    fract_binary = fract_binary.Insert(fract_binary.Length, "0");
                }
                mantissa = fract_binary;
                binary_float += fract_binary;
                return binary_float;
            }
            else
            {
                full_binary = full_binary.Remove(0, 1);
                mantissa = full_binary + fract_binary.Substring(0, 23 - full_binary.Length);
                binary_float += full_binary + fract_binary.Substring(0,23-full_binary.Length);
                return binary_float;
            }

             
        }
        public float Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }
        public int Exp
        {
            get
            {
                return exp;
            }
        }
        public string Binary
        {
            get
            {
                return binary_float;
            }
            set
            {
                binary_float = value;
            }
        }
        public string Mantissa
        {
            get
            {
                return mantissa;
            }
        }
        public string Binary_exp
        {
            get
            {
                return binary_exp;
            }
            set
            {
                binary_exp = value;
            }
        }

    }
}