using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioPacVideo
{
    public class FeederTemplate
    {
        public TimeSpan Breakfast; //Breakfast time, in  seconds
        public TimeSpan Lunch; //Lunch time, in seconds
        public TimeSpan Dinner; //Dinner Time, in seconds
        public double PelletsPerGram;
        public bool Enabled;       
        public FeederTemplate()
        {                         
        }
        


    }
}
