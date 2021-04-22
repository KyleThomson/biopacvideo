using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class LabelType
    {
        public string Name;
        public int IDNum;

        public LabelType(string a, string b)
        {
            int.TryParse(a, out IDNum);
            Name = b;
        }
        public string LabelMatch(int IDtest, string returnstr)
        {
            if (IDtest == IDNum)
                return Name;
            else
                return returnstr;
        }
    }
}
