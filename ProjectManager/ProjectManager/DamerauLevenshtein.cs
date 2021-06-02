using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    // Implementation of Damerau-Levenshtein algorithm. Well documented method for measuring distance between an input and target string by measuring transpositions,
    // insertions, substitutions, and deletions. So two strings that are sufficiently different (i.e. apple and banana) will have a higher score/distance. 
    public static class DamerauLevenshtein
    {
        public static int DamerauLevenshteinDistanceTo(this string @string, string targetString)
        {
            return DamerauLevenshteinDistance(@string, targetString);
        }
        public static int DamerauLevenshteinDistance(string string1, string string2)
        {
            if (String.IsNullOrEmpty(string1))
            {
                if (!String.IsNullOrEmpty(string2)) { return string2.Length; } // certain 100% mismatch

                return 0;
            }

            if (String.IsNullOrEmpty(string2))
            {
                if (!String.IsNullOrEmpty(string1)) { return string1.Length; } // certain 100% mismatch

                return 0;
            }
            int length1 = string1.Length;
            int length2 = string2.Length;

            // create 2D integer array to store differences from target string
            int[,] d = new int[length1 + 1, length2 + 1];

            int cost, del, ins, sub;
            for (int i = 0; i <= d.GetUpperBound(0); i++) { d[i, 0] = i; }

            for (int i = 0; i <= d.GetUpperBound(1); i++) { d[0, i] = i; }

            for (int i = 1; i <= d.GetUpperBound(0); i++)
            {
                for (int j = 1; j <= d.GetUpperBound(1); j++)
                {
                    // Iterate through both strings and find matching characters. If character matches, cost = 0
                    if (string1[i - 1] == string2[j - 1]) { cost = 0; } else { cost = 1; }

                    del = d[i - 1, j] + 1;
                    ins = d[i, j - 1] + 1;
                    sub = d[i - 1, j - 1] + cost;

                    d[i, j] = Math.Min(del, Math.Min(ins, sub));

                    if (i > 1 && j > 1 && string1[i - 1] == string2[j - 2] && string1[i - 2] == string2[j - 1])
                    { d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost); }
                }
            }

            return d[d.GetUpperBound(0), d.GetUpperBound(1)];

        }
    }
}
