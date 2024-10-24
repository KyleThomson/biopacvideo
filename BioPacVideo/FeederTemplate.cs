﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace BioPacVideo
{
    public enum MEALSTATE
    {
        NONE=0,
        WAITING=1, 
        EXECUTING=2,
        FINISHED=3
    }

    public enum FEEDERSTATE : int
    {
        FAIL=0,
        EXECUTING=1,
        SUCCESS=2,
        READY=3
    }

    public class MealType
    {
        public int Pellets;
        public int Feeder;
        public int IndicatedFeeder;
        public bool Test;
        public MealType(int F, int P, int I) { Feeder = F; Pellets = P; IndicatedFeeder = I; Test = false; }
        public MealType(int F, int P, int I, bool T) { Feeder = F; Pellets = P; IndicatedFeeder = I; Test = T; }
    }

    public class FeederTemplate
    {
        #region Properties
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
        public int LastState; 
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

        public int gap = 0;
        private Random Randomizer;
        public RatTemplate[] Rats;
        public StreamWriter log;
        //public bool ArduinoAck = true;
        private string LogFileName;
        public int Cages_X;
        public int Cages_Y;
        public MEALSTATE MealState; 
        private Queue<MealType> Meals;
        #endregion

        #region Initilizers
        public static FeederTemplate Instance
        {
            get
            {
                return instance;
            }
        }

        public FeederTemplate()
        {
            Meals = new Queue<MealType>();
            Commands = new Queue<byte>();
            CommandText = new Stack<string>();
            MealState = MEALSTATE.NONE; 
            ErrorState = false;
            State = 3;
            StateText = "READY";
            AddressTable = new int[32];
            Rats = RatTemplate.NewInitArray(16);
            LogFileName = "";
            Randomizer = new Random();
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the current day of the week as an integer
        /// </summary>
        /// <returns>The current day as an integer</returns>
        public int GetDay()
        {
            DateTime X = DateTime.Now;
            int Day = ((int)X.DayOfWeek + 6) % 7; //Shift the date so Monday is the start of a new week
            return Day;
        }

        /// <summary>
        /// Gets the last meal that occured as an Integer
        /// </summary>
        /// <returns>The last meal to occur as an Integer</returns>
        public int LastMeal()
        {
            int Meal = 0;

            TimeSpan T = (TimeSpan)DateTime.Now.TimeOfDay;
            if ((TimeSpan.Compare(Meal1, T) < 0) && (Meal1.Hours != 0))
            {
                Meal = 1;
            }
            if ((TimeSpan.Compare(Meal2, T) < 0) && (Meal2.Hours != 0))
            {
                Meal = 2;
            }
            if ((TimeSpan.Compare(Meal3, T) < 0) && (Meal3.Hours != 0))
            {
                Meal = 3;
            }
            if ((TimeSpan.Compare(Meal4, T) < 0) && (Meal4.Hours != 0))
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

        /// <summary>
        /// Gets the last message from the arduino
        /// </summary>
        /// <returns>The last message from the arduino</returns>
        public string GetLastCommandText()
        {
            if (CommandText.Count > 0)
                return CommandText.Pop();
            else
                return "No Text in Stack";
        }
        
        public void SendDirectCommand(byte Command)
        {
            Commands.Enqueue(Command);
            CommandSize++; 
        }

        /// <summary>
        /// Gets the most recent command for the arduino
        /// </summary>
        /// <returns>The most recent command to send to the arduimo as a byte</returns>
        public byte GetTopCommand()
        {
            
            CommandSize--;
            byte v = Commands.Dequeue();
            Console.WriteLine("FEEDER COMMAND: " + v.ToString()); 
            return v;
        }
        #endregion

        #region Control Functions
        /// <summary>
        /// Generates the meals per week and can be randomized to test for drug adherence
        /// </summary>
        /// <param name="Rat">Rat to Gen meals for as an Int</param>
        /// <param name="MidWeek">Bool to declare if we are past the middle of the week</param>
        public void GenMeals(int Rat, bool MidWeek)
        {
            Random random = new Random(Randomizer.Next(0, Int32.MaxValue));
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
            int MedMeals = (int)Math.Round((double)(MealsLeft * Rats[Rat].Medication) / 100);
            int R;
            for (int i = 0; i < MedMeals; i++)
            {
                R = random.Next(0, MealMatrix.Count - 1);
                Rats[Rat].Meals[MealMatrix[R]] = true;
                MealMatrix.RemoveAt(R);
            }
        }
        public void RunNextMeal()
        {
            MealType M=Meals.Dequeue();
            Console.WriteLine("FEEDING:" + M.Feeder.ToString());
            AddCommand(M);
           
        }
        public int CheckMealsCount()
        {
            return Meals.Count; 
        }
        /// <summary>
        /// Waits for the command sent to the arduino to be executed
        /// </summary>
        /// <returns>A boolean saying wheather its the last command or not</returns>
        public bool CommandWaitEx()
        {
            if (CommandText.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets the name for the feeder log
        /// </summary>
        /// <param name="FName">The file name for the feeder log</param>
        public void SetLogName(string FName)
        {
            LogFileName = FName;
        }

        /// <summary>
        /// Logs the text provided
        /// </summary>
        /// <param name="Command">The text to log to the feeder log</param>
        public void Log(string Command)
        {
            if (String.IsNullOrEmpty(LogFileName))
            {
                return;
            }
            else
            {
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
        }

        /// <summary>
        /// Adds a pellete delivery command to the arduino command stack
        /// </summary>
        /// <param name="Feeder">Feeder to deliver pelletes from</param>
        /// <param name="Pellets">Number of pelletes to deliver</param>
        public void AddCommand(MealType M)
        {
            string Txt;
            MealState = MEALSTATE.EXECUTING; 
            Commands.Enqueue((byte)24); //Feeder 
            Commands.Enqueue((byte)M.Feeder);
            Commands.Enqueue((byte)25); //Pellets
            Commands.Enqueue((byte)M.Pellets);
            Commands.Enqueue((byte)26);
            Commands.Enqueue((byte)31); //Execute
            CommandSize = Commands.Count;
            Console.WriteLine(M.Feeder.ToString() + " " + M.Pellets.ToString() + " " + CommandSize);
            //This is to disable logging during testing meals 
            if (!M.Test)
            {
                Txt = "Feeder-" + (M.IndicatedFeeder + 1).ToString() + " Pellets-" + M.Pellets.ToString();
                CommandText.Push(Txt);
            }
             
        }        
        /// <summary>
        /// Executes the command on the arduino
        /// </summary>
        public void ExecuteAction()  //This is likely depreciated 
        {
            Commands.Enqueue((byte)31); 
            CommandSize = Commands.Count;
        }

        /// <summary>
        /// Asks the arduino to acknowledge that it recieved the pellet and the feeder
        /// </summary>
        public void ArduinoAckowledge()
        {
            //The software asks the arduino to acknowledge that it recieved the pellet and the feeder 
            Commands.Enqueue((byte)30); //gotta figure out the numbering system here
            CommandSize = Commands.Count;
            
            //ArduinoAck = false; 
        }

        /// <summary>
        /// Executes a feeding
        /// </summary>
        /// <param name="MealNum">Which meal should be executed</param>
        public void GoMeal(int MealNum) // something is going wrong in here 
        {
            Log("Executing Meal #" + MealNum.ToString());
            int MealSize;     // Variable to store the calculated meal size (number of pellets)
            int Feeder;       // Variable to store the index of the feeder being used
            string Medi;      // Variable to indicate whether the meal is medicated or unmedicated
            int ActualFeeder; // Variable to store the actual feeder address used (can be translated from AddressTable)
            MealType M;

            //int a, b, tmp;
            DateTime Start = DateTime.Now;
            for (int RC = 0; RC < 16; RC++)
            {
                if (Rats[RC].Weight > 0 && Rats[RC].ID.ToLower() != "e" && Rats[RC].ID.ToLower() != "empty")
                {
                    Feeder = RC * 2; // Calculate the feeder index based on the rat's position (each rat has 2 feeders)
                    // Calculate the number of pellets based on the rat's weight and PelletsPerGram
                    MealSize = (int)Math.Round(Rats[RC].Weight * PelletsPerGram);
                    // Determine if the meal is medicated based on the rat's meal schedule
                    if (Rats[RC].Meals[MealNum])
                    {
                        Feeder = Feeder + 1; // Use the alternate feeder (medicated)
                        Medi = "Medicated";  // Set the meal type as medicated
                    }
                    else
                    {
                        Medi = "Unmedicated"; // Set the meal type as unmedicated
                    }
                    // Determine the actual feeder to use based on whether AlternateAddress is true
                    ActualFeeder = AlternateAddress ? AddressTable[Feeder] : Feeder;                
                    while (MealSize > 23)
                    {
                        M = new MealType(ActualFeeder, 23, Feeder);
                        Meals.Enqueue(M);   // Add a command to deliver 23 pellets
                        Log("Feeder: " + Feeder + "  Pellets: 23 " + Medi); // Log the delivery of 23 pellets
                        MealSize -= 23; // Subtract 23 from MealSize to account for the pellets just queued
                    }
                    // Add the final command to deliver the remaining pellets (less than or equal to 23)
                    M = new MealType(ActualFeeder, MealSize, Feeder);    
                    Meals.Enqueue(M);  
                    Log("Feeder: " + Feeder.ToString() + "  Pellets: " + MealSize.ToString() + " " + Medi); // Log the final delivery                   
                    // If this is the last meal of the week, generate the next week's meals for the rat
                    if (MealNum + 1 == DailyMealCount * 7)
                    {
                        GenMeals(RC, false); // Generate new meal schedule for the rat
                    }
                }
            }
            MealState = MEALSTATE.WAITING;
            
        }
        #endregion
    }
}
