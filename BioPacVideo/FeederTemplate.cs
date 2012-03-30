using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BioPacVideo
{
    public class FeederTemplate
    {
        static readonly FeederTemplate instance = new FeederTemplate();
        public TimeSpan Meal1; //Breakfast time, in  seconds
        public TimeSpan Meal2; //Lunch time, in seconds
        public TimeSpan Meal3; //Dinner Time, in seconds
        public TimeSpan Meal4;
        public TimeSpan Meal5;
        public TimeSpan Meal6;
        public double PelletsPerGram;        
        public bool Enabled;
        public int State;
        public string StateText;
        private Queue<byte> Commands;
        public int CommandSize = 0;  //Number of commands left to run. 
        public bool CommandReady; //Set once all commands queued.
        public int gap = 0;
        public RatTemplate[] Rats;
        public StreamWriter log;
        private string LogFileName;

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
            CommandReady = false; 
            State = 3;
            StateText = "READY";
            Rats = RatTemplate.NewInitArray(16);
            LogFileName = "";
        }
        public byte GetTopCommand()
        {
            CommandSize--;
            byte v = Commands.Dequeue();
            if (v == 255) { CommandReady = false; }
            return v;
        }
        public void SetLogName(string FName)
        {
            LogFileName = FName;
        }
        private void Log(string Command)
        {
            if (LogFileName == "") { return; }
            if (!File.Exists(LogFileName))
            {
                log = new StreamWriter(LogFileName);
            }
            else
            {
                log = File.AppendText(LogFileName);
            }

            // Write to the file:
            log.WriteLine(DateTime.Now.ToString() + "  " + Command);
            log.Close();
        }
        public void AddCommand(int Feeder, int Pellets)
        {
            Commands.Enqueue((byte)Feeder);
            Commands.Enqueue((byte)Pellets);
            CommandSize = Commands.Count;
        }
        public void Execute()
        {
            Commands.Enqueue((byte)255);
            CommandSize = Commands.Count;
            CommandReady = true;
        }
        public void GoMeal()
        {
            int MealSize;
            int Feeder;
            string Medi; 
            //int a, b, tmp;
            Random random = new Random();
            DateTime Start = DateTime.Now;            
            /*int[] RatVec = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            for (int j = 0; j < 1000; j++)
            {
                a = random.Next(0, 15);
                b = random.Next(0, 15);
                tmp = RatVec[a];
                RatVec[a] = RatVec[b];
                RatVec[b] = tmp;
            }*/
            for (int RC = 0; RC < 16; RC++)
            {
                if (Rats[RC].Weight > 0)
                {
                    MealSize = (int)Math.Ceiling(Rats[RC].Weight*PelletsPerGram);
                    Feeder = RC * 2;
                    if (random.Next(1, 100) > Rats[RC].Medication)
                    {                     
                        Medi = "Unmedicated";
                    }
                    else
                    {
                        Feeder = Feeder + 1;
                        Medi = "Medicated";
                    }
                    
                    while (MealSize > 30)
                    {
                        AddCommand(Feeder, 30);
                        Log("Feeder: " + Feeder + "  Pellets: 30 " + Medi);
                        MealSize -= 30;
                    }
                    AddCommand(Feeder, MealSize);
                    Log("Feeder: " + Feeder.ToString() + "  Pellets: " + MealSize.ToString() + " " + Medi);                    
                }

            }
            Execute();            
        }

    }
}
