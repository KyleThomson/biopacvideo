using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    // Object to wrap features of a particular group
    public class GroupedData
    {
        public string groupID;
        public List<double> szBurdens;
        public List<double> treatmentTimes;
        public GroupType group;
        public double burdenSEM;
        public double szBurden;
        public int szFreedom;
        public int numAnimals;
        public double pValue;
        public SzMetrics BASELINE;
        public GroupedData(string group)
        {
            // take group as constructor
            groupID = group;
        }
    }
}
