using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace truth_table
{
    public class Operations
    {
       public int Negation(int a)
        {
            if (a == 0) return 1;
            return 0;
        }
        public int Conjunction(int a, int b) 
        {
            if ((a == 1) && (b == 1)) return 1;
            return 0;
        }
        public int Disjunction(int a, int b) 
        {
            if ((a == 0 && b == 0)) return 0;
            return 1;
        }
        public int Implication(int a, int b)
        {
            if (a == 1 && b == 0) return 0;
            return 1;
        }
        public int Equivalence(int a, int b) 
        {
            if (a == b) return 1;
            return 0;
        }
        
    }
}
