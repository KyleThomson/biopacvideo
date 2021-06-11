using System;

namespace ProjectManager
{
    public class InjectionType
    {
        public string Route;
        public int Dose;
        public double DoseAmount;
        public int DoseNum;
        public string ADDID;
        public string solvent;
        public DateTime TimePoint;

        //Date DoseNum ADDID Dose DoseAmount Route Solvent
        public InjectionType(string a, string b, string c, string d, string e, string f, string g)
        {
            DateTime.TryParse(a, out TimePoint);
            int.TryParse(b, out DoseNum);
            ADDID = c;
            int.TryParse(d, out Dose);
            double.TryParse(e, out DoseAmount);
            Route = f;
            solvent = g;
        }
    }
}
