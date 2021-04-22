using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class GroupType
    {
        public string Name;
        public int count;
        public int IDNum;
        public GroupType()
        {
            Name = "";
            count = 0;
            IDNum = 0;
        }
        public GroupType(string a, string b, string c)
        {
            Name = b;
            int.TryParse(a, out IDNum);
            int.TryParse(c, out count);
        }
    }
}
