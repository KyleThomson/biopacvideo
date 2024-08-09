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

        /// <summary>
        /// Gets the last command from the arduino
        /// </summary>
        /// <returns>The last command from the arduino as a byte</returns>
        public byte GetTopCommand()
        {
            CommandSize--;
            byte v = Commands.Dequeue();
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

        /// <summary>
        /// Waits for the command sent to the arduino to be executed
        /// </summary>
        /// <returns>A boolean saying wheather its the last command or not</returns>
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

        /// <summary>
        /// Adds a pellete delivery command to the arduino command stack
        /// </summary>
        /// <param name="Feeder">Feeder to deliver pelletes from</param>
        /// <param name="Pellets">Number of pelletes to deliver</param>
        public void AddCommand(int Feeder, int Pellets)
        {
            Commands.Enqueue((byte)Feeder); 
            Commands.Enqueue((byte)Pellets);
            string Txt; 
            if (AlternateAddress)
            {
                int n= AddressTable[Feeder]+= 1; 
                Txt = "Feeder-" + n.ToString() + " Pellets-" + Pellets.ToString();
                //this shows the feeder based on what was put into the feeder addresses 
            }
            else {
                //translate it from programmer terms into layman terms vis a vis the feeder numbers
                int n = Feeder+=1; 
                Txt = "Feeder-" + n.ToString() + " Pellets-" + Pellets.ToString();
            } 
            CommandText.Push(Txt);
            CommandSize = Commands.Count;
            Activated = true;
        }

        /// <summary>
        /// Executes the command on the arduino
        /// </summary>
        public void ExecuteAction()
        {           
            Commands.Enqueue((byte)31);
            CommandSize = Commands.Count;
            CommandReady = true;            
        }

        /// <summary>
        /// Asks the arduino to acknowledge that it recieved the pellet and the feeder
        /// </summary>
        public void ArduinoAckowledge()
        {
            //The software asks the arduino to acknowledge that it recieved the pellet and the feeder 
            Commands.Enqueue((byte)30); //gotta figure out the numbering system here
            CommandSize = Commands.Count;
            CommandReady = true;
            //ArduinoAck = false; 
        }

        /// <summary>
        /// Executes a feeding
        /// </summary>
        /// <param name="MealNum">Which meal should be executed</param>
        public void GoMeal(int MealNum) // something is going wrong in here 
        {
            Log(MealNum.ToString());
            int MealSize;     // Variable to store the calculated meal size (number of pellets)
            int Feeder;       // Variable to store the index of the feeder being used
            string Medi;      // Variable to indicate whether the meal is medicated or unmedicated
            int ActualFeeder; // Variable to store the actual feeder address used (can be translated from AddressTable)

            DateTime Start = DateTime.Now;   // Capture the current time for logging or timing purposes
            Log(Start.ToString());

            // Loop over all 16 rats (assuming Rats array has 16 elements)
            for (int RC = 0; RC < 16; RC++)
            {
                Log("Current Loop Iteration: " + RC.ToString());
                Log("Weight: " + Rats[RC].Weight.ToString());
                // Check if the rat has a non-zero weight (i.e., if the rat is valid and needs feeding)
                if (Rats[RC].Weight > 0)
                {
                    Log("Rat Was above 0 Grams");
                    Feeder = RC * 2; // Calculate the feeder index based on the rat's position (each rat has 2 feeders)
                    Log("Feeder: " + Feeder.ToString());
                    // Calculate the number of pellets based on the rat's weight and PelletsPerGram
                    MealSize = (int)Math.Round(Rats[RC].Weight * PelletsPerGram);
                    Log("Meal Size: " +  MealSize.ToString());
                    // Determine if the meal is medicated based on the rat's meal schedule
                    if (Rats[RC].Meals[MealNum])
                    {
                        Log("Rat should recieve medicated: " + Rats[RC].Meals[MealNum].ToString());
                        Feeder = Feeder + 1; // Use the alternate feeder (medicated)
                        Medi = "Medicated";  // Set the meal type as medicated
                        Log("Feeder to use: " + Feeder.ToString());
                        Log("Medi????: " + Medi.ToString());
                    }
                    else
                    {
                        Medi = "Unmedicated"; // Set the meal type as unmedicated
                        Log("Rat should not recieve medicated: " + Rats[RC].Meals[MealNum].ToString());
                        Log("Feeder to use: " + Feeder.ToString());
                        Log("Medi????: " + Medi.ToString());
                    }
                    // Determine the actual feeder to use based on whether AlternateAddress is true
                    if (AlternateAddress)
                    {
                        Log("Alternate addresses are enabled");
                        ActualFeeder = AddressTable[Feeder]; // Translate the feeder index using AddressTable
                        Log("Will actually use feeder: " + ActualFeeder.ToString());
                    }
                    else
                    {
                        Log("Alternate addresses are disabled");
                        ActualFeeder = Feeder; // Use the default feeder index
                        Log("Will actually use feeder: " + ActualFeeder.ToString());
                    }
                    // Add commands to deliver pellets while there are more than 23 pellets to deliver
                    while (MealSize > 23)
                    {
                        AddCommand(ActualFeeder, 23); // Add a command to deliver 23 pellets
                        Log("Feeder: " + Feeder + "  Pellets: 30 " + Medi); // Log the delivery of 23 pellets
                        MealSize -= 23; // Subtract 23 from MealSize to account for the pellets just queued
                    }
                    // Add the final command to deliver the remaining pellets (less than or equal to 23)
                    AddCommand(ActualFeeder, MealSize);
                    Log("Feeder to be used: " + ActualFeeder.ToString());
                    Log("Meal Size: " +  MealSize.ToString());
                    Log("Feeder: " + Feeder.ToString() + "  Pellets: " + MealSize.ToString() + " " + Medi); // Log the final delivery

                    // If this is the last meal of the week, generate the next week's meals for the rat
                    if (MealNum + 1 == DailyMealCount * 7)
                    {
                        Log("Generating meals");
                        GenMeals(RC, false); // Generate new meal schedule for the rat
                    }
                }
            }
            ExecuteAction(); // Execute all the commands that have been queued           
        }
        #endregion
    }
}
