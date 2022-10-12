using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager
{
    public class EEGOrganizer
    {
        public string ID;
        public string filePath;
        public int AnimalIn;
        
        public EEGOrganizer(string id, string FP, int a)
        {
            ID = id;
            filePath = FP;
            AnimalIn = a;
            
        }

        



    }
}
