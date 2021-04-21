using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class ImportantDateType
    {
        public int LabelID;
        public DateTime Date;
        public ImportantDateType(string a, string b)
        {
            int.TryParse(a, out LabelID);
            DateTime.TryParse(b, out Date);
        }

    }
}
