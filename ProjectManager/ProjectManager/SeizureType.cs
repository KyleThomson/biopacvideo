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
        public long Offset;
        public string VidString;
        public int BufferLen;
        public SeizureType(string a, string b, string c, string e, string f)
        {
            DateTime.TryParse(a, out d);
            TimeSpan.TryParse(b, out t);
            //t = t + TimeSpan.FromSeconds(d.TimeOfDay.TotalSeconds);
            int.TryParse(e, out length);
            file = f;
            TrimFileName();
            Notes = c;
            Severity = -1;
            Offset = -1;
            BufferLen = 0;
        }
        public SeizureType(string a, string b, string c, string e, string f, string g)
        {
            DateTime.TryParse(a, out d);
            TimeSpan.TryParse(b, out t);
            //t = t + TimeSpan.FromSeconds(d.TimeOfDay.TotalSeconds);
            int.TryParse(e, out length);
            file = f;
            TrimFileName();
            Notes = c;
            Offset = -1;
            BufferLen = 0;
            int.TryParse(g, out Severity);
        }
        public SeizureType(string a, string b, string c, string e, string f, string g, long h)
        {
            DateTime.TryParse(a, out d);
            TimeSpan.TryParse(b, out t);
            //t = t + TimeSpan.FromSeconds(d.TimeOfDay.TotalSeconds);
            int.TryParse(e, out length);
            file = f;
            TrimFileName();
            Notes = c;
            Offset = h;
            BufferLen = 0;
            int.TryParse(g, out Severity);
        }
        public SeizureType(string a, string b, string c, string e, string f, string g, long h, int l)
        {
            DateTime.TryParse(a, out d);
            TimeSpan.TryParse(b, out t);
            //t = t + TimeSpan.FromSeconds(d.TimeOfDay.TotalSeconds);
            int.TryParse(e, out length);
            file = f;
            TrimFileName();
            Notes = c;
            Offset = h;
            BufferLen = l;
            int.TryParse(g, out Severity);
        }
        public bool Compare(SeizureType C)
        {
            if ((DateTime.Compare(d, C.d) == 0) && (TimeSpan.Compare(t, C.t) == 0) && (length == C.length))
                return true;
            else
                return false;
        }
        private void TrimFileName()
        {
            int index = file.IndexOf("_");
            if (index >= 0)
                file = file.Substring(0, index);
        }
    }
}
