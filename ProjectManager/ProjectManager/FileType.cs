using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class FileType
    {
        public string[] AnimalIDs;
        public int Chans;
        public DateTime Start;
        public TimeSpan Duration;
        public String Reviewer;
        public DateTime ReviewDate;

        public FileType(string[] A, int B, DateTime C, string D)
        {
            AnimalIDs = A;
            Chans = B;
            Start = C;
            TimeSpan.TryParse(D, out Duration);
            Reviewer = "";
            ReviewDate = Start; 
        }
        public FileType(string[] A, int B, DateTime C, string D,string E, DateTime F)
        {
            AnimalIDs = A;
            Chans = B;
            Start = C;
            TimeSpan.TryParse(D, out Duration);
            Reviewer = E;
            ReviewDate = F;
        }
        public FileType()
        {
        }
        public bool Compare(FileType C)
        {
            if ((DateTime.Compare(Start, C.Start) == 0) && (string.Compare(AnimalIDs[0], C.AnimalIDs[0]) == 0))
                return true;
            else
                return false;
        }
    }
}
