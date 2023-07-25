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
        public bool Activated; 
        public int State;
        //public int RecievingState; 
        public string StateText;
        public string ADDC1;
        public string ADDC2;
        public string Dose1;
        public string Dose2;
        public int Route1;
        public int Route2;
        public int Solve1;
        public int Solve2;
        public bool AlternateAddress;
        public int[] AddressTable;
        private Queue<byte> Commands;
        private Stack<string> CommandText;
        public int CommandSize = 0;  //Number of commands left to run. 
        public bool CommandReady; //Set once all commands queued.
        public int gap = 0;
        private Random Randomizer;
        public RatTemplate[] Rats;
        public StreamWriter log;
        //public bool ArduinoAck = true;
        private string LogFileName;
        public int Cages_X;
        public int Cages_Y;
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
            Random random = new Random(Randomizer.Next(0,Int32.MaxValue));
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
            Activated = false;
            CommandReady = false;
            ErrorState = false;
            State = 3;
            StateText = "READY";
            AddressTable = new int[32];
            Rats = RatTemplate.NewInitArray(16);
            LogFileName = "";
            Randomizer = new Random(); 
        }
        public string GetLastCommandText()
        {
            if (CommandText.Count > 0)
                return CommandText.Pop();
            else
                return "No Text in Stack";
        }
        public bool CommandWaitEx()
        {
            if (CommandText.Count>0)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }
        public byte GetTopCommand()
        {
            CommandSize--;
            byte v = Commands.Dequeue();          
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
            Activated = true;
        }
        public void ExecuteAction()
        {           
            Commands.Enqueue((byte)31);
            CommandSize = Commands.Count;
            CommandReady = true;
            
        }
        public void ArduinoAckowledge()
        {
            //The software asks the arduino to acknowledge that it recieved the pellet and the feeder 
            Commands.Enqueue((byte)30); //gotta figure out the numbering system here
            CommandSize = Commands.Count;
            CommandReady = true;
            //ArduinoAck = false; 
        }
        public void GoMeal(int MealNum)
        {
            int MealSize;
            int Feeder;
            string Medi;
            int ActualFeeder; //Need to keep track of the feeder we're sending to

            //int a, b, tmp;            
            DateTime Start = DateTime.Now;                        
            for (int RC = 0; RC < 16; RC++)
            {
                if (Rats[RC].Weight > 0)
                {
                    Feeder = RC * 2;
                    
                    MealSize = (int)Math.Round(Rats[RC].Weight * PelletsPerGram);
                    if (Rats[RC].Meals[MealNum])
                    {
                        Feeder = Feeder + 1;
                        Medi = "Medicated";
                    }
                    else
                    {                        
                        Medi = "Unmedicated";
                    }
                    if (AlternateAddress)
                    {
                        ActualFeeder = AddressTable[Feeder]; //Translate new address from the table
                    }
                    else
                    {
                        ActualFeeder = Feeder; //Otherwise use default feeder
                    }
                    while (MealSize > 30)
                    {
                        AddCommand(ActualFeeder, 30);
                        Log("Feeder: " + Feeder + "  Pellets: 30 " + Medi);
                        MealSize -= 30;
                    }
                    AddCommand(ActualFeeder, MealSize);
                    Log("Feeder: " + Feeder.ToString() + "  Pellets: " + MealSize.ToString() + " " + Medi);
                    if (MealNum + 1 == DailyMealCount * 7)
                    {
                        GenMeals(RC, false);
                    }
                }

            }
            ExecuteAction();            
        }

    }
}
