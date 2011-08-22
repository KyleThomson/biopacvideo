using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioPacVideo
{
    public class FeederTemplate
    {
        static readonly FeederTemplate instance = new FeederTemplate();
        public TimeSpan Breakfast; //Breakfast time, in  seconds
        public TimeSpan Lunch; //Lunch time, in seconds
        public TimeSpan Dinner; //Dinner Time, in seconds
        public double PelletsPerGram;        
        public bool Enabled;
        public Queue<byte> Commands;
        public int CommandSize = 0;
        public int CurCommand=0;
        public static FeederTemplate Instance
        {
            get
            {
                return instance;
            }
        }

        public FeederTemplate()
        {
            Commands = new Queue<byte>();
        }
        public void AddCommand(int Feeder, int Pellets)
        {
            Commands.Enqueue((byte)Feeder);
            Commands.Enqueue((byte)Pellets);
            Commands.Enqueue(255);      
        }


    }
}
