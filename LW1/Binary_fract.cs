using System;
namespace BinaryNumbers
{
	public class Binary_fract 
	{
		public Binary_fract(double num) 
		{
			this.number = num;
            Make_Binary();
		}
        public Binary_fract(string binary)
        {
            this.binary = binary;
            Make_Decimal();
        }

        private double number;
		private string binary = "";
        private bool has_a_sign = false;
		public string Make_Binary()
		{
            if (number.ToString()[0] == '-') { has_a_sign = true; }
            int length;
            double remainder;
            double fractional_part = this.number - Math.Truncate(this.number);
			int full_part = (int)Math.Abs(Math.Truncate(this.number));
			for (int i = 0; i<5; i++)
			{
				binary = binary.Insert(binary.Length, Math.Truncate((fractional_part * 2)).ToString()); 
				fractional_part =	fractional_part*2 - Math.Truncate((fractional_part * 2));
            }
            
            while (full_part / 2 != 0)
            {
                remainder = full_part % 2;
                full_part /= 2;
                binary = binary.Insert(0, remainder.ToString());
            }
            remainder = full_part % 2;
            binary = binary.Insert(0, remainder.ToString());
            length = binary.Length;
            for (int i = 0 ; i < 15 - length; i++) { binary = binary.Insert(0, "0"); }
            if (has_a_sign) { binary = binary.Insert(0, "1"); }
            else binary = binary.Insert(0, "0");
            return binary;
		}
        public double Make_Decimal()
        {
            double result = 0;
            double grade = 0;
            double fractional_part = 0;
            for (int i = binary.Length - 6; i > 0; i--)
            {
                if (binary[i] == '1') { result += Math.Pow(2, grade); }
                grade++;
            }
            grade = -1;
            for (int i = binary.Length - 5; i < binary.Length; i++)
            {
                if (binary[i] == '1') { fractional_part += Math.Pow(2, grade); }
                grade--;
            }
            result += fractional_part;
                if (binary[0] == '1') { result = 0 - result; }
            this.number = result;
            return result;
        }
        public double Number
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
        public string Binary
        {
            get
            {
                return binary;
            }
            set
            {
                binary = value;
            }
        }
    }
}