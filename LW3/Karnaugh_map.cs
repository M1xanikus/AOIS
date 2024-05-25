using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryNumbers;
namespace truth_table
{
    public class KarnaughMap
    {

        private List<char> variables = new List<char>();
        private int vars_count;
        private List<List<string>> table =new();
        private string ind_form = "";
        private List<string> terms = new();
        private List<string> minimizedTerms = new();
        public string min_sdnf = "";
        public string min_sknf = "";

        public KarnaughMap(string expression, string ind_form)
        {
           
            this.ind_form = ind_form;
            DetermineVariables(expression);
            vars_count = variables.Count;
            Filltable();
            FindPrimeImplicants();
            Minimize();
            
            
        }

        static List<string> GenerateGrayCode(int n)
        {
            List<string> result = new List<string> { "0", "1" };
            for (int i = 1; i < n; i++)
            {
                for (int j = result.Count - 1; j >= 0; j--)
                {
                    result.Add(result[j]);
                }
                for (int j = 0; j < result.Count / 2; j++)
                {
                    result[j] = "0" + result[j];
                }
                for (int j = result.Count / 2; j < result.Count; j++)
                {
                    result[j] = "1" + result[j];
                }
            }
            return result;
        }

        private void DetermineVariables(string expr)
        {
            foreach (var ch in expr)
            {

                if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z')) //added 0 1
                {
                    if (!variables.Contains(ch))
                    {
                        variables.Add(ch);
                    }

                }
            }
        }
        private int Get_index(int i, int j)
        {
            string temp = table[i][0] + table[0][j];
            Binary a = new(temp.Insert(0,"0"));
            return a.Number;
          
        }
        private void Filltable()
        {
            if (vars_count % 2 == 0)
            {
                List<string> buf = new();
                for (int i = 0; i < vars_count+1; i++) buf.Add("");

                List<string> temp = GenerateGrayCode(vars_count / 2);
                for (int i = 0; i < vars_count+ 1; i++)
                {
                    List<string> temp_2 = new(buf);
                    table.Add(temp_2);
                }
                for(int i = 1, j = 0; i <  table.Count;j++, i++)
                {
                    table[0][i] = temp[j];
                    table[i][0] = temp[j];
                }
                for (int i = 1; i < table.Count; i++)
                {
                    for (int j = 1; j < table[i].Count; j++)
                    {
                        table[i][j] = ind_form[Get_index(i,j)].ToString();
                    }
                }
                table[0][0] = " ";
            }
            else
            {
                List<string> buf = new();
                for (int i = 0; i < 5; i++) buf.Add("");

                List<string> temp_for_2 = GenerateGrayCode(2);
                List<string> temp_for_1 = GenerateGrayCode(1);
                for (int i = 0; i < 3; i++)
                {
                    List<string> temp_2 = new(buf);
                    table.Add(temp_2);
                }
                for (int i = 1, j = 0; i < table[0].Count; j++, i++)
                {
                    table[0][i] = temp_for_2[j];
                }
                table[1][0] = temp_for_1[0];
                table[2][0] = temp_for_1[1];
                for (int i = 1; i < table.Count; i++)
                {
                    for (int j = 1; j < table[i].Count; j++)
                    {
                        table[i][j] = ind_form[Get_index(i, j)].ToString();
                    }
                }
                table[0][0] = " ";
            }
        }

        public void PrintKarnaughTable()
        {
            Console.WriteLine("Карта Карно:");
            for(int i = 0; i < table[0].Count; ++i) { Console.Write(table[0][i].ToString()+ " "); }
            Console.WriteLine();
            for (int i = 1; i < table.Count; i++)
            {
                for (int j = 0; j < table[i].Count; j++)
                {
                    Console.Write(table[i][j] + "  ");
                }
                Console.WriteLine();
            }
        }

        private void FindPrimeImplicants()
        {
            var groups = new List<List<string>>();
            int size = table[0].Count;

            for (int i = 0; i <= variables.Count; i++)
            {
                groups.Add(new List<string>());
            }

            for (int i = 0; i < size; i++)
            {
               
                    string bin = Convert.ToString(i, 2).PadLeft(variables.Count, '0');
                    groups[bin.Count(c => c == '1')].Add(bin);
                
            }

            var primeImplicants = new List<string>();
            var used = new HashSet<string>();

            for (int i = 0; i < groups.Count - 1; i++)
            {
                var nextGroup = new List<string>();

                foreach (var a in groups[i])
                {
                    foreach (var b in groups[i + 1])
                    {
                        int diff = 0;
                        int diffIndex = -1;
                        for (int k = 0; k < a.Length; k++)
                        {
                            if (a[k] != b[k])
                            {
                                diff++;
                                diffIndex = k;
                            }
                        }
                        if (diff == 1)
                        {
                            string newTerm = a.Substring(0, diffIndex) + '-' + a.Substring(diffIndex + 1);
                            nextGroup.Add(newTerm);
                            used.Add(a);
                            used.Add(b);
                        }
                    }
                }
                groups[i + 1].AddRange(nextGroup);
            }

            foreach (var group in groups)
            {
                foreach (var term in group)
                {
                    if (!used.Contains(term))
                    {
                        primeImplicants.Add(term);
                    }
                }
            }
        }
        public string ConvertToExpression(string expression_1, string expression_2)
        {
            var result = new List<string>();
            min_sdnf = expression_1;
            min_sknf = expression_2;
            foreach (var term in minimizedTerms)
            {
                var expression = new List<string>();

                for (int i = 0; i < term.Length; i++)
                {
                    if (term[i] == '1')
                    {
                        expression.Add(((char)(i + 'A')).ToString());
                    }
                    else if (term[i] == '0')
                    {
                        expression.Add("!" + ((char)(i + 'A')).ToString());
                    }
                }
                
                result.Add(string.Join('|', expression));
            }

            return string.Join('&', result);
        }
    

    private void Minimize()
        {
            List<string> primeImplicants = new List<string>();
            var essentialPrimeImplicants = new List<string>();
            var coveredTerms = new HashSet<string>();
            if (terms.Count != 0)
            {
                foreach (var term in terms)
                {
                    var coveringImplicants = primeImplicants.Where(implicant => IsCovered(term, implicant)).ToList();
                    if (coveringImplicants.Count == 1)
                    {
                        var essentialPrime = coveringImplicants.FirstOrDefault();
                        if (essentialPrime != null)
                        {
                            essentialPrimeImplicants.Add(essentialPrime);
                            coveredTerms.Add(term);
                            primeImplicants.Remove(essentialPrime);
                        }
                    }
                }
            }

            foreach (var essential in essentialPrimeImplicants)
            {
                var termsCoveredByEssential = terms.Where(term => IsCovered(term, essential)).ToList();
                foreach (var coveredTerm in termsCoveredByEssential)
                {
                    coveredTerms.Add(coveredTerm);
                }
            }

            while (coveredTerms.Count < terms.Count && primeImplicants.Any())
            {
                var termCounts = new Dictionary<string, int>();

                foreach (var prime in primeImplicants)
                {
                    foreach (var term in terms)
                    {
                        if (!coveredTerms.Contains(term) && IsCovered(term, prime))
                        {
                            if (!termCounts.ContainsKey(prime))
                            {
                                termCounts[prime] = 0;
                            }
                            termCounts[prime]++;
                        }
                    }
                }

                if (termCounts.Count == 0) break; // No more prime implicants can cover any terms

                var maxCoveringPrime = termCounts.OrderByDescending(pair => pair.Value).FirstOrDefault().Key;
                if (maxCoveringPrime != null)
                {
                    essentialPrimeImplicants.Add(maxCoveringPrime);

                    foreach (var term in terms)
                    {
                        if (IsCovered(term, maxCoveringPrime))
                        {
                            coveredTerms.Add(term);
                        }
                    }

                    primeImplicants.Remove(maxCoveringPrime);
                }
            }

        }


        private bool IsCovered(string term, string implicant)
        {
            for (int i = 0; i < term.Length; i++)
            {
                if (implicant[i] != '-' && term[i] != implicant[i])
                {
                    return false;
                }
            }
            return true;
        }
    }


}
