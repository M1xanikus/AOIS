using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BinaryNumbers
{
    public class Binary_IEEE
    {
        public Binary_IEEE(float number)
        {
            this.number = number;
        }   
        private float number;
        private string float_binary;
        private bool has_a_sign = false;
        public string make_binary()
        {
            if (number.ToString()[0] == '-') { has_a_sign = true; }
            int exp = 0, full_length;
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
            full_length = full_binary.Length;
            exp = 127 + full_binary.Length - 1 ;
            for(int i = full_length -1;i>0; i--)
            {
              //  i--;
            }
            return "";

        }
    }
}