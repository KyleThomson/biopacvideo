using System;

namespace ProjectManager
{
    public class SeizureType
    {
        public TimeSpan t;
        public DateTime d;
        public int length;
        public string Notes;
        public string file;
        public int Severity;
        public bool stageAgreement = false; // Do bubble and note severity agree?
        public bool keepInAnalysis = true; // if seizure is not -1 in notes
        public SeizureType(string a, string b, string c, string e, string f)
        {
            DateTime.TryParse(a, out d);
            TimeSpan.TryParse(b, out t);
            //t = t + TimeSpan.FromSeconds(d.TimeOfDay.TotalSeconds);
            int.TryParse(e, out length);
            file = f;
            Notes = c;
            Severity = -1;
        }
        public SeizureType(string a, string b, string c, string e, string f, string g)
        {
            DateTime.TryParse(a, out d);
            TimeSpan.TryParse(b, out t);
            //t = t + TimeSpan.FromSeconds(d.TimeOfDay.TotalSeconds);
            int.TryParse(e, out length);
            file = f;
            Notes = c;
            int.TryParse(g, out Severity);
        }
        public bool Compare(SeizureType C)
        {
            if ((DateTime.Compare(d, C.d) == 0) && (TimeSpan.Compare(t, C.t) == 0) && (length == C.length))
                return true;
            else
                return false;
        }
    }
}
