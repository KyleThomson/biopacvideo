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
    }
}
