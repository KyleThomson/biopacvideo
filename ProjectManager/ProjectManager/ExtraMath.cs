using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Accord.Statistics.Testing;

namespace ProjectManager
{
    public class ExtraMath
    {
        public static BigInteger Factorial(int x)
        {
            // initialize result
            // BigInteger makes this not return an absurd result since huge factorials are involved here
            BigInteger result = 1;

            // Loop until we get to 1
            // 0! = 1, create condition by stopping loop at i == 1, no point in hard coding a rule that only multiplies by 1
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
            return result;
        }
        public static double FisherExact(int a, int b, int c, int d)
        {
            // hypothesis test for 2x2 contingency table
            int N = a + b + c + d;

            // calculate numerator for fisher exact
            BigInteger numerator = Factorial(a + b) * Factorial(c + d) * Factorial(a + c) * Factorial(b + d);
            // calculate denominator
            BigInteger denominator = Factorial(a) * Factorial(b) * Factorial(c) * Factorial(d) * Factorial(N);

            double pvalue;
            pvalue = (double)numerator / (double)denominator;

            return pvalue;
        }
        public static double MannWhitneyWilcoxon(double[] sample1, double[] sample2)
        {
            // hypothesis test
            double pvalue;
            MannWhitneyWilcoxonTest mwwTest = new MannWhitneyWilcoxonTest(sample1, sample2, 
                TwoSampleHypothesis.FirstValueIsSmallerThanSecond, exact: false);
            pvalue = mwwTest.PValue;

            return pvalue;
        }

        public static double Variance(List<double> x)
        {
            double variance = 0;
            if (x.Count == 0) return variance; 
            double n = x.Count();
            double mean = x.Average();

            for (int i = 0; i < x.Count; i++)
            {
                variance += Math.Pow(x[i] - mean, 2) / (n - 1);
            }

            return variance;
        }

        public static double StdDev(List<double> x)
        {
            // standard deviation
            double variance = Variance(x);
            double sigma = Math.Sqrt(variance);

            return sigma;
        }
        public static double Sem(List<double> x)
        {
            // Computes standard error of the mean
            var n = x.Count;
            var sigma = StdDev(x);
            var sem = sigma / Math.Sqrt(n);
            return Math.Round(sem, 1);
        }
    }
}
