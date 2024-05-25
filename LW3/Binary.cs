using System.Xml.Linq;

namespace BinaryNumbers
{
    public class Binary
    {
   
        public Binary(string straight_binary)
        { 
            this.straight_binary = straight_binary;
            Make_Decimal();

        }
        private int number;
        private string straight_binary = "";

        
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
     

    }

}