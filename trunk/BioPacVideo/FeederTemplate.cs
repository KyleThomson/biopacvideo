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
        public int DailyMealCount;
        public double PelletsPerGram;
        public bool ErrorState;
        public bool Enabled;
        public int State;
        public string StateText;
        private Queue<byte> Commands;
        private Stack<string> CommandText;
        public int CommandSize = 0;  //Number of commands left to run. 
        public bool CommandReady; //Set once all commands queued.
        public int gap = 0;
        public RatTemplate[] Rats;
        public StreamWriter log;
        private string LogFileName;
        public int GetDay()
        {
            DateTime X = DateTime.Now;
            int Day = ((int)X.DayOfWeek + 6) % 7; //Shift the date so Monday is the start of a new week
            return Day;
        }
        public int LastMeal()
        {
            int Meal = 0;
            
            TimeSpan T = (TimeSpan)DateTime.Now.TimeOfDay;
            if ((TimeSpan.Compare(Meal1, T) < 0)  && (Meal1.Hours != 0))
            {
                Meal = 1;
            }
            if ((TimeSpan.Compare(Meal2, T) < 0)  && (Meal2.Hours != 0))
            {
                Meal = 2;
            }
            if ((TimeSpan.Compare(Meal3, T) < 0)  && (Meal3.Hours != 0))
            {
                Meal = 3;
            }
            if ((TimeSpan.Compare(Meal4, T) < 0)  && (Meal4.Hours != 0))
            {
                Meal = 4;
            }
            if ((TimeSpan.Compare(Meal5, T) < 0) && (Meal5.Hours != 0))
            {
                Meal = 5;
            }
            if ((TimeSpan.Compare(Meal6, T) < 0) && (Meal6.Hours != 0))
            {
                Meal = 6;
            }            
            return Meal;
        }
        public void GenMeals(int Rat, bool MidWeek)
        {
            Random random = new Random();
            for (int i = 0; i < 7 * 6; i++)
            {
                Rats[Rat].Meals[i] = false;
            }
            int MealsLeft;
            int StartPoint; 
            if (MidWeek)
            {
                //Where are we in the week?     
                StartPoint = ((GetDay() * DailyMealCount) + LastMeal()); 
                MealsLeft = DailyMealCount * 7 - StartPoint;             
            }
            else
            {
                MealsLeft = DailyMealCount * 7;
                StartPoint = 0;
            }
            List<int> MealMatrix = new List<int>();
            for (int i = StartPoint; i < DailyMealCount * 7; i++)
            {
                MealMatrix.Add(i);
            }
            int MedMeals = (int)Math.Round((double)(MealsLeft * Rats[Rat].Medication)/100);                
            int R;
            for (int i = 0; i < MedMeals; i++)
            {
                R = random.Next(0,MealMatrix.Count-1);
                Rats[Rat].Meals[MealMatrix[R]] = true;
                MealMatrix.RemoveAt(R);
            }                

        }
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
            CommandText = new Stack<string>(); 
            CommandReady = false;
            ErrorState = false;
            State = 3;
            StateText = "READY";
            Rats = RatTemplate.NewInitArray(16);
            LogFileName = "";
        }
        public string GetLastCommandText()
        {
            if (CommandText.Count > 0)
                return CommandText.Pop();
            else
                return "No Text in Stack";
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
        public void Log(string Command)
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
            string Txt = "Feeder-" + Feeder.ToString() + "  Pellets-" + Pellets.ToString();
            CommandText.Push(Txt); 
            CommandSize = Commands.Count;
        }
        public void ExecuteAck()
        {
            Commands.Enqueue((byte)255);
            CommandSize = Commands.Count;
            CommandReady = true;
        }        
        public void GoMeal(int MealNum)
        {
            int MealSize;
            int Feeder;
            string Medi;            
            
            //int a, b, tmp;            
            DateTime Start = DateTime.Now;                        
            for (int RC = 0; RC < 16; RC++)
            {
                if (Rats[RC].Weight > 0)
                {
                    Feeder = RC * 2;
                    MealSize = (int)Math.Ceiling(Rats[RC].Weight * PelletsPerGram);
                    if (Rats[RC].Meals[MealNum])
                    {
                        Feeder = Feeder + 1;
                        Medi = "Medicated";
                    }
                    else
                    {                        
                        Medi = "Unmedicated";
                    }
                    
                    while (MealSize > 30)
                    {
                        AddCommand(Feeder, 30);
                        Log("Feeder: " + Feeder + "  Pellets: 30 " + Medi);
                        MealSize -= 30;
                    }
                    AddCommand(Feeder, MealSize);
                    Log("Feeder: " + Feeder.ToString() + "  Pellets: " + MealSize.ToString() + " " + Medi);
                    if (MealNum + 1 == DailyMealCount * 7)
                    {
                        GenMeals(RC, false);
                    }
                }

            }
            ExecuteAck();            
        }

    }
}
