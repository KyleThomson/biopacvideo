﻿using System.Collections.Generic;

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
        public double burdenPValue;
        public double freedomPValue;
        public SzMetrics BASELINE;
        public GroupedData(string group)
        {
            // take group as constructor
            groupID = group;
        }
    }
}