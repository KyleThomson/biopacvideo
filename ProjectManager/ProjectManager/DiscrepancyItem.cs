using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager
{

    public class DiscrepancyItem
    {
        public AnimalType an;
        public SeizureType sz;
        public int notes;
        public int anIndex;
        public int szIndex;
       

        public DiscrepancyItem(SeizureType s, int n, AnimalType a, int aI, int sI)
        {
            sz = s;
            notes = n;
            an = a;
            anIndex = aI;
            szIndex = sI;
        }



    }
}
