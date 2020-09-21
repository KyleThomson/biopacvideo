using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
namespace SeizureHeatmap
{
    class RecordAnimals
    {
        public string animalID;
        // same size
        public List<double> seizureTimes;
        public List<int> seizureStages;
        // same size
        public List<double> trtTimes;
        public List<string> trtMethods;
        //
        public List<string> seizureNotes;
        // same size lists
        public List<int> days;
        public List<int> srsPerDay;
        public Array allDaySrs;
        public int maxTrtDay;
    }
}
