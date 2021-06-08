using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class SzMetrics
    {
        public TRTTYPE treatment;
        public double burdenSEM;
        public double szBurden;
        public int szFreedom;
        public int numAnimals;

        public SzMetrics(TRTTYPE trt)
        {
            treatment = trt;
        }
    }
}
