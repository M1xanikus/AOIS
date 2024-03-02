using System.Xml.Linq;

namespace BinaryNumbers
{
    public class Binary
    {
        public Binary(int number) 
        { 
            this.number = number;
            Make_Binary();
            Make_Reverse();
            Make_Additional();
        }
        public Binary(string straight_binary)
        { 
            this.Straight_binary = straight_binary;
            Make_Decimal();
            Make_Reverse();
            Make_Additional();
        }
        private int number;
        private string straight_binary = "";
        private string reverse_binary = "";
        private string additional_binary = "";
        private bool has_a_sign = false;

        public string Make_Binary()
        {   
            if (number.ToString()[0] == '-') { has_a_sign = true; }
            int temp_number = Math.Abs(this.number);
            int remainder,length;
            
            while (temp_number / 2 != 0)
            {
                remainder = temp_number % 2;
                temp_number /= 2;
                straight_binary = straight_binary.Insert(0, remainder.ToString());
            }
            remainder = temp_number % 2;
            straight_binary = straight_binary.Insert(0, remainder.ToString());
            length = straight_binary.Length;
            for (int i = 0; i < 15 - length; i++) {straight_binary = straight_binary.Insert(0, "0"); }
            if (has_a_sign) { straight_binary = straight_binary.Insert(0, "1"); }
            else straight_binary = straight_binary.Insert(0, "0");
            return straight_binary;
        }
        public string Make_Reverse() 
        {
            for (int i = straight_binary.Length-1; i > 0; i-- )
            {
                if (straight_binary[i] == '0') { reverse_binary = reverse_binary.Insert(0, "1"); }
                else reverse_binary = reverse_binary.Insert(0, "0");
            }
            if (has_a_sign) { reverse_binary = reverse_binary.Insert(0, "1"); }
            else reverse_binary = reverse_binary.Insert(0, "0");
            return reverse_binary;
        }
        public string Make_Reverse(string binary)
        {
            bool has_a_sign = false;
            if (binary[0] == '1') { has_a_sign = !has_a_sign; }
            string reverse_binary = "";
            for (int i = binary.Length - 1; i > 0; i--)
            {
                if (binary[i] == '0') { reverse_binary = reverse_binary.Insert(0, "1"); }
                else reverse_binary = reverse_binary.Insert(0, "0");
            }
            if (has_a_sign) { reverse_binary = reverse_binary.Insert(0, "1"); }
            else reverse_binary = reverse_binary.Insert(0, "0");
            return reverse_binary;
        }   
        public string Make_Additional()
        {
            int index = 0;
            for(int i = reverse_binary.Length-1;i>0; i--) 
            {
                if (reverse_binary[i] == '1') { additional_binary = additional_binary.Insert(0, "0"); continue; }
                else if(reverse_binary[i] == '0') 
                { 
                    additional_binary = additional_binary.Insert(0, "1");
                    index = --i;
                    break;
                }
            }
            for(int j = index; j >= 0; j--)
            {
                if (reverse_binary[j] == '1') { additional_binary = additional_binary.Insert(0, "1"); }
                else additional_binary = additional_binary.Insert(0, "0");
            }
            return additional_binary;
        }
        public string Make_Additional(string binary)
        {
            string additional_binary = "";
            int index = 0;
            for (int i = binary.Length - 1; i > 0; i--)
            {
                if (binary[i] == '1') { additional_binary = additional_binary.Insert(0, "0"); continue; }
                else if (binary[i] == '0')
                {
                    additional_binary = additional_binary.Insert(0, "1");
                    index = --i;
                    break;
                }
            }
            for (int j = index; j >= 0; j--)
            {
                if (binary[j] == '1') { additional_binary = additional_binary.Insert(0, "1"); }
                else additional_binary = additional_binary.Insert(0, "0");
            }
            return additional_binary;
        }
        public double Make_Decimal()
        { 
            double result = 0;
            double grade = 0;
            for (int i = straight_binary.Length-1; i>0; i--)
            { 
                if (straight_binary[i] == '1'){result += Math.Pow(2, grade);}
               grade++;
            }
            if (straight_binary[0] == '1' && grade != 0) {result = 0 - result; }
            this.number = (int)result;
                return result;
        }

        public int Number
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
        public string Straight_binary
        {
            get
            {
                return straight_binary;    
            }
            set
            {
                straight_binary = value;
            }
        }
        public string Reverse_binary
        {
            get
            {
                return reverse_binary;    
            }
            set { reverse_binary = value; }
        }
        public string Additional_binary
        {
            get
            {
                return additional_binary;
            }
            set { additional_binary = value; }
        }

    }

}


