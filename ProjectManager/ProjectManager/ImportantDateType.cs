using System;

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
