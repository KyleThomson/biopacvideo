using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class ExtraMath
    {
        public static int Factorial(int x)
        {
            // initialize result
            int result = 1;

            // Loop until we get to 1
            for (int i = x - 1; i > 0; i-- )
            {
                result *= i; 
            }
            return result;
        }
        public static double FisherExact(int a, int b, int c, int d, int N)
        {
            double pvalue;
            pvalue = Factorial(a + b) * Factorial(c + d) * Factorial(a + c) * Factorial(b + d) / (Factorial(a) * Factorial(b) * Factorial(c) * Factorial(d) * Factorial(N));

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
