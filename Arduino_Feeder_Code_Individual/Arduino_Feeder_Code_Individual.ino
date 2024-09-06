//For Testing Faster Fails: 
#define STEPFAILATTEMPTS 150 //Number of steps before changing direction
#define DIRFAILATTEMPTS 10 //Number of attempts to change direction

#define DataRecInt 3
#define PelletIR 2

//Address lines
#define AddA 11
#define AddB 10

//Main Controls
#define STEP 12
#define DIR 13

#define MOTORCONTROL 0  //Power for the motors
#define FAIL 0
#define EXECUTING 1
#define SUCCESS 2
#define READY 3

#define BO1 9
#define BO2 8
#define BO3 7
#define BO4 6
#define BO5 5
#define BO6 4

//EEG Inputs
#define EEG0 14
#define EEG1 15
#define EEG2 16
#define EEG3 17
#define EEG4 18
#define EEGOut0 19
#define EEGOut1 1

volatile int count = 0;
volatile int AddressTemp = 24;
volatile int innercount = 0;
volatile int attempts = 0;
volatile int failattempts = 0;
volatile int FailListCount = 0;
volatile int SpecialCom = 0;
volatile int Com = 0;     //Command

//State 0 - Fail
//State 1 = Executing
//State 2 = Success
//State 3 = Ready
volatile int State;
volatile boolean resetcount = false;
volatile boolean Execute = false;  //Whether or not to execute a command
volatile boolean Feeder = true;
volatile boolean Fail = false;
volatile boolean Ack = false;
volatile  boolean PelletCount = false;

volatile boolean dir = false;   //Direction
volatile  boolean GetC = false;  //Get Command

volatile int PList[128];  //Pellets per Feeder List
volatile int FList[128];
volatile  int Commands = 0;
//Breakout boards - Pin Association

int val = 0;



void setup() 
{
  //setup all pins
  pinMode(MOTORCONTROL, OUTPUT);            //Initialize Motor Power Pin
  digitalWrite(MOTORCONTROL, LOW);          //Turn the motors off ASAP
  attachInterrupt(1, GetCommand, RISING);  //Turn on command recieve interrupt  
  SpecialCom = 0;
  //Initialize Pins
  pinMode(LED_BUILTIN, OUTPUT);  //Control the ONboard LED for Debugging
  pinMode(DIR, OUTPUT);
  pinMode(STEP, OUTPUT);
  pinMode(AddA, OUTPUT);
  pinMode(AddB, OUTPUT);
  pinMode(BO1, OUTPUT);
  pinMode(BO2, OUTPUT);
  pinMode(BO3, OUTPUT);
  pinMode(BO4, OUTPUT);
  pinMode(BO5, OUTPUT);
  pinMode(BO6, OUTPUT);
  pinMode(EEG0, INPUT);
  pinMode(EEG1, INPUT);
  pinMode(EEG2, INPUT);
  pinMode(EEG3, INPUT);
  pinMode(EEG4, INPUT);
  pinMode(EEGOut0, OUTPUT);
  pinMode(EEGOut1, OUTPUT);

  //Start everything Low.
  digitalWrite(DIR, LOW);
  digitalWrite(STEP, LOW);
  digitalWrite(AddA, LOW);
  digitalWrite(AddB, LOW);
  digitalWrite(BO1, LOW);
  digitalWrite(BO2, LOW);
  digitalWrite(BO3, LOW);
  digitalWrite(BO4, LOW);
  digitalWrite(BO5, LOW);
  digitalWrite(BO6, LOW);
}

void SetAddress(int Addy)
 {
  digitalWrite(BO1, LOW);
  digitalWrite(BO2, LOW);
  digitalWrite(BO3, LOW);
  digitalWrite(BO4, LOW);
  digitalWrite(BO5, LOW);
  digitalWrite(BO6, LOW);

  int BO = Addy / 4;  //Divide by 4, low 2 bytes feeder, high bits breakout boards.
  int Ad = Addy % 4;  //Get feeder count (% is remainder
  switch (BO) {
    case 0:
      digitalWrite(BO1, HIGH);
      break;
    case 1:
      digitalWrite(BO2, HIGH);
      break;
    case 2:
      digitalWrite(BO3, HIGH);
      break;
    case 3:
      digitalWrite(BO4, HIGH);
      break;
    case 4:
      digitalWrite(BO5, HIGH);
      break;
    case 5:
      digitalWrite(BO6, HIGH);
      break;
  }
  if ((Ad & 1) == 1) {
    digitalWrite(AddA, HIGH);
  } else {
    digitalWrite(AddA, LOW);
  }
  if ((Ad & 2) == 2) {
    digitalWrite(AddB, HIGH);
  } else {
    digitalWrite(AddB, LOW);
  }
}

void loop() 
{
  delay(1000);
  val = digitalRead(PelletIR);
  if (val == HIGH) {
    SetState(READY);
  } else {
    SetState(FAIL);
  }
  if (SpecialCom) {
    switch (SpecialCom) {
      case 24:  //Send Feeder Next
        Feeder = true;
        break;
      case 25:  //Send pellet next
        Feeder = false;
        break;
      case 26:
        Commands++;
        break;
      case 27:
        SetState(FAIL);
        delay(10000);
        SetState(EXECUTING);
        delay(10000);
        SetState(SUCCESS);
        delay(10000);
        SetState(READY);
        delay(10000);
        break;
      case 28:  //Run ALL feeders
        for (count = 24; count > 0; count--) {
          PList[count - 1] = 3;
          FList[count - 1] = count - 1;
        }
        count = 0;
        Commands = 24;
        break;
      case 29:  //Reset
        count = 0;
        AddressTemp = 24;
        innercount = 0;
        attempts = 0;
        failattempts = 0;
        FailListCount = 0;       
        resetcount = false;
        Execute = false;  //Whether or not to execute a command
        Feeder = true;
        Fail = false;
        Ack = false;
        PelletCount = false;
        Commands = 0;
        Execute = false;
        SetAddress(24);                   //Set to a null address
        digitalWrite(MOTORCONTROL, LOW);  //Turn off power to the motors      
        SetState(READY);
        break;
      case 30:
        if (State == READY) 
        {
          SetState(SUCCESS);
          delay(10000);
          SetState(READY);
        }
        Ack=false; 
        break;         
      }
      SpecialCom = 0;
    }
  if (Execute)  //Recieved final command, good to execute
  {
    
    delay(1000);                              //Probably not necessary, for safety.
    innercount = 0;
    attempts = 0;
    failattempts = 0;
    resetcount = true;
    Fail = false;
    while (Commands > 0)  //While we have commands enqued
    {
      attachInterrupt(0, PelletDrop, FALLING);  //Turn on pellet IR
      digitalWrite(LED_BUILTIN,LOW);        
      Commands--;  //Remove a command //does this break out of the while loop if Commands = 1 to start?
      //Get the current command
      AddressTemp = FList[Commands];
      count = PList[Commands];
      SetAddress(AddressTemp);
      digitalWrite(STEP, LOW);
      //Turn on the motors
      digitalWrite(MOTORCONTROL, HIGH);
      SetState(EXECUTING);
      delay(2000);
      //Reset the counters
      while (count > 0) {
        //One motor step, 80ms long
        digitalWrite(STEP, HIGH);
        delay(40);
        digitalWrite(STEP, LOW);
        delay(40);

        //increment debounce
        innercount++;

        //increment attempts count
        attempts++;

        //if we are failing to see a pellet drop within 10 seconds
        //reverse the direction of the motor
        //BEREK 150
        if (attempts > STEPFAILATTEMPTS) {
          if (dir) {
            digitalWrite(DIR, HIGH);
          } else {
            digitalWrite(DIR, LOW);
          }
          dir = !dir;
          attempts = 0;
          failattempts = failattempts + 1;
          //Berek reset to 9
          if (failattempts > DIRFAILATTEMPTS)  //We've failed completely, time to move on.
          {
            Fail = true;
            count = 0;
            failattempts = 0;
          }
        }
        if (innercount > 4)  //Must wait for 4 rotations
        {
          resetcount = true;  //Debounce for pellet drop
        }
      }  //While (count > 0)
      Ack = false;
      if (Fail) {
        SetState(FAIL);
        Fail = false;
      } else {
        SetState(SUCCESS);
      } 
      digitalWrite(LED_BUILTIN,LOW);        
      while (Ack==false) {                
        delay(2000); 
      }
      Ack = false;
      innercount = 0;
      attempts = 0;
      failattempts = 0;
      resetcount = true;
      Fail = false;
    }  //While (Commands > 0)
    Execute = false;
    SetAddress(24);  //Set to a null address
    // FAIL LOOP
    digitalWrite(MOTORCONTROL, LOW);  //Turn off power to the motors
    detachInterrupt(0);               //Turn off Pellet IR sensor
    SetState(READY);
    digitalWrite(LED_BUILTIN,LOW);        
  }                                   //if (Execute)
}

//Pellet drop IR sensor break
void PelletDrop() 
{
  if (resetcount)  //Need to see 4 steps, before a pellet will be counted
  {
    resetcount = false;  //Pellet Debounce - always first.
    innercount = 0;      //Reset the counter
    count--;             //Subtract a pellet
    attempts = 0;        //Reset the number of attempts to get a pellet
  }
}

void GetCommand() 
{
   Com=0; 
    SpecialCom = 0;  //Ensure SpecialCom is empty
    //Convert Message from BioPac to number
    if (!digitalRead(EEG0)) { Com = Com + 1; }
    if (!digitalRead(EEG1)) { Com = Com + 2; }
    if (!digitalRead(EEG2)) { Com = Com + 4; }
    if (!digitalRead(EEG3)) { Com = Com + 8; }
    if (!digitalRead(EEG4)) { Com = Com + 16; }
    if (Com < 24) 
    {
      if (Feeder)  //Are we reading a feeder
      {
        FList[Commands] = Com;  //Store feeder number
      } else {
        PList[Commands] = Com;  //Store Pellets number
      }
    } 
    else 
    {
      digitalWrite(LED_BUILTIN,HIGH); 
      switch(Com)
      {
        case 30:
          Ack = true;                        
          break;
        case 31:
          Execute = true;
          break; 
      }
      SpecialCom = Com;
    }    
}



void SetState(int newState) 
{
  //State 0 - Fail
  //State 1 = Executing
  //State 2 = Success
  //State 3 = Ready
  switch (newState) 
  {
    case FAIL:
      digitalWrite(EEGOut1, LOW);
      digitalWrite(EEGOut0, LOW);
      break;
    case EXECUTING:
      digitalWrite(EEGOut1, LOW);
      digitalWrite(EEGOut0, HIGH);
      break;
    case SUCCESS:
      digitalWrite(EEGOut1, HIGH);
      digitalWrite(EEGOut0, LOW);
      break;
    case READY:
      digitalWrite(EEGOut1, HIGH);
      digitalWrite(EEGOut0, HIGH);
      break;
  }
  State = newState;
}