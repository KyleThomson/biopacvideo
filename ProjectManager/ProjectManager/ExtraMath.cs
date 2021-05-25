using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ProjectManager
{
    public class ExtraMath
    {
        public static BigInteger Factorial(int x)
        {
            // initialize result
            long result = 1;

            // Loop until we get to 1
            // 0! = 1, create condition
            if (x > 0)
            {
                for (int i = x; i > 0; i--)
                {
                    result *= i;
                }
            }
            else if (x < 0)
            {
                // factorial of negative number? don't think this will happen but crazier things have happened
            }
            BigInteger answer = result;
            return answer;
        }
        public static double FisherExact(int a, int b, int c, int d, int N)
        {
            double pvalue;
            Console.WriteLine((double)(Factorial(a + b) * Factorial(c + d) * Factorial(a + c) * Factorial(b + d)) / (double)(Factorial(a) * Factorial(b) * Factorial(c) * Factorial(d) * Factorial(N)));
            pvalue = (double)(Factorial(a + b) * Factorial(c + d) * Factorial(a + c) * Factorial(b + d)) / (double)(Factorial(a) * Factorial(b) * Factorial(c) * Factorial(d) * Factorial(N));

            return pvalue;
        }
        public static double MannWhitneyWilcoxon(double[] sample1, double[] sample2)
        {
            double pvalue;
            Accord.Statistics.Testing.MannWhitneyWilcoxonTest mwwTest = new Accord.Statistics.Testing.MannWhitneyWilcoxonTest(sample1, sample2);
            pvalue = mwwTest.PValue;

            return pvalue;
        }
    }
}
