using System;

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
        public string fileName;

        public FileType(string[] A, int B, DateTime C, string D, string file)
        {
            AnimalIDs = A;
            Chans = B;
            Start = C;
            TimeSpan.TryParse(D, out Duration);
            Reviewer = "";
            ReviewDate = Start;
            fileName = file;
        }
        public FileType(string[] A, int B, DateTime C, string D, string E, string file, DateTime F)
        {
            AnimalIDs = A;
            Chans = B;
            Start = C;
            TimeSpan.TryParse(D, out Duration);
            Reviewer = E;
            ReviewDate = F;
            fileName = file;
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
