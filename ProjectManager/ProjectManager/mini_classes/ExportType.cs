using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class ExportType
    {
        public bool Sz;
        public bool Pellet;
        public bool Med;
        public bool wt;
        public bool SzTime;
        public bool Meal;
        public bool DetailList;
        public bool Notes;
        public bool SeverityIndx;
        public bool BloodDraw;
        public bool BloodDrawList;
        public bool Injections;
        public bool InjectionsList;
        public bool binSz;

        public ExportType()
        {
            Sz = false;
            Pellet = false;
            Med = false;
            wt = false;
            SzTime = false;
            Meal = false;
            DetailList = false;
            Notes = false;
            SeverityIndx = false;
            BloodDraw = false;
            BloodDrawList = false;
            Injections = false;
            InjectionsList = false;
            binSz = false;
        }

    }
}
