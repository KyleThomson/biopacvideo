using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class MealType
    {
        public DateTime d;
        public string type;
        public int pelletcount;
        public MealType(string a, string b, string c)
        {
            DateTime.TryParse(a, out d);
            type = b;
            int.TryParse(c, out pelletcount);
        }
        public bool Compare(MealType C)
        {
            if ((DateTime.Compare(d, C.d) == 0) && (string.Compare(type, C.type) == 0) && (pelletcount == C.pelletcount))
                return true;
            else
                return false;
        }
    }
}
