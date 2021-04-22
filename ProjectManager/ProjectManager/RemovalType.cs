using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class RemovalType
    {
        public DateTime dt;
        public int count;
        public string pt;
        public RemovalType(string a, string b, string c)
        {
            DateTime.TryParse(a, out dt);
            int.TryParse(b, out count);
            pt = c;
        }
    }
}
