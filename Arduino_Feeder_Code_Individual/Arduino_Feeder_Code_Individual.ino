int count = 0;
int AddressTemp = 24;
int innercount = 0;
int attempts = 0;
int failattempts = 0;
int FailListCount = 0;
//State 0 - Fail
//State 1 = Executing
//State 2 = Success
//State 3 = Ready
int State = 3;
boolean resetcount = false;
boolean Execute = false;  //Whether or not to execute a command
boolean Feeder = true;
boolean Fail = false;
boolean Ack = false;
boolean PelletCount = false;

boolean dir = false;   //Direction
boolean GetC = false;  //Get Command

int PList[128];  //Pellets per Feeder List
int FList[128];
int Commands = 0;
//Breakout boards - Pin Association
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
int val = 0;

#define DataRecInt 3
#define PelletIR 2

//Address lines
#define AddA 11
#define AddB 10

//Main Controls
#define STEP 12
#define DIR 13

#define MOTORCONTROL 0  //Power for the motors

void setup() {
  //setup all pins
  pinMode(MOTORCONTROL, OUTPUT);            //Initialize Motor Power Pin
  digitalWrite(MOTORCONTROL, LOW);          //Turn the motors off ASAP
  attachInterrupt(1, GetCommand, FALLING);  //Turn on command recieve interrupt

  //Initialize Pins
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

void SetAddress(int Addy) {
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

void loop() {
  delay(1000);
  val = digitalRead(PelletIR);
  if (val == HIGH) {
    State = 3;
    digitalWrite(EEGOut1, HIGH);
    digitalWrite(EEGOut0, HIGH);
  } else {
    State = 0;
    digitalWrite(EEGOut0, LOW);
    digitalWrite(EEGOut1, LOW);
  }
  if (Execute)  //Recieved final command, good to execute
  {
    attachInterrupt(0, PelletDrop, FALLING);  //Turn on pellet IR
    delay(1000);                              //Probably not necessary, for safety.
    innercount = 0;
      attempts = 0;
      failattempts = 0;
      resetcount = true;
      Fail = false;
    while (Commands > 0)                      //While we have commands enqued
    {
      Commands--;  //Remove a command //does this break out of the while loop if Commands = 1 to start?
      //Get the current command
      AddressTemp = FList[Commands];
      count = PList[Commands];
      SetAddress(AddressTemp);
      digitalWrite(STEP, LOW);
      //Turn on the motors
      digitalWrite(MOTORCONTROL, HIGH);
      State = 1;
      digitalWrite(EEGOut1, LOW);
      digitalWrite(EEGOut0, HIGH);
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
        if (attempts > 50) {
          if (dir) {
            digitalWrite(DIR, HIGH);
          } else {
            digitalWrite(DIR, LOW);
          }
          dir = !dir;
          attempts = 0;
          failattempts = failattempts + 1;
          //Berek reset to 9
          if (failattempts > 2)  //We've failed completely, time to move on.
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
        State = 0;
        digitalWrite(EEGOut1, LOW);
        digitalWrite(EEGOut0, LOW);
        Fail = false;
      } else {
        State = 2;
        digitalWrite(EEGOut1, HIGH);
        digitalWrite(EEGOut0, LOW);
      }
      delay(2000);
      attempts = 0;
      while (!Ack) {
        delay(1000);
        attempts++;
        if (attempts == 0) {
          State = 1;
          digitalWrite(EEGOut1, LOW);
          digitalWrite(EEGOut0, HIGH);
          delay(5000);
          State = 0;
          digitalWrite(EEGOut1, LOW);
          digitalWrite(EEGOut0, LOW);
          delay(5000);
          Commands = 0;
          Ack = true;
        }
      }
      Ack=false; 
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
  }                                   //if (Execute)
}

//Pellet drop IR sensor break
void PelletDrop() {
  if (resetcount)  //Need to see 4 steps, before a pellet will be counted
  {
    resetcount = false;  //Pellet Debounce - always first.
    innercount = 0;      //Reset the counter
    count--;             //Subtract a pellet
    attempts = 0;        //Reset the number of attempts to get a pellet
  }
}

void GetCommand() {
  if (!GetC)  //Debounce check
  {
    GetC = true;  //Debounce set high - don't want to repeat the command
    int Com = 0;  //Clear last command
    //Convert Message from BioPac to number
    if (!digitalRead(EEG0)) { Com = Com + 1; }
    if (!digitalRead(EEG1)) { Com = Com + 2; }
    if (!digitalRead(EEG2)) { Com = Com + 4; }
    if (!digitalRead(EEG3)) { Com = Com + 8; }
    if (!digitalRead(EEG4)) { Com = Com + 16; }
    if (Com<24)
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
      switch (Com)
      {
          case 24: //Send Feeder Next
            Feeder=true; 
            break; 
          case 25: //Send pellet next
            Feeder=false; 
            break; 
          case 26: 
            Commands++; 
            break; 
          case 27: 
              digitalWrite(EEGOut1, LOW);
              digitalWrite(EEGOut0, LOW);
              delay(5000000);
              digitalWrite(EEGOut1, LOW);
              digitalWrite(EEGOut0, HIGH);
              delay(5000000);
              digitalWrite(EEGOut1, HIGH);
              digitalWrite(EEGOut0, LOW);
              delay(5000000);
              digitalWrite(EEGOut1, HIGH);
              digitalWrite(EEGOut0, HIGH);
              delay(5000000);           
              break; 
          case 28: //Run ALL feeders 
            for (count=24; count>0; count--)
            {
               PList[count-1]=3; 
               FList[count-1]=count-1; 
            }
            count = 0; 
            Commands=24; 
            break; 
          case 29: //Reset
            count=0;             
            AddressTemp = 24;
            innercount = 0;
            attempts = 0;
            failattempts = 0;
            FailListCount = 0;
            State = 3;
            resetcount = false;
            Execute = false;  //Whether or not to execute a command
            Feeder = true;
            Fail = false;
            Ack = false;
            PelletCount = false;
            Commands = 0; 
               Execute = false;
            SetAddress(24);  //Set to a null address    
            digitalWrite(MOTORCONTROL, LOW);  //Turn off power to the motors
            detachInterrupt(0);               //Turn off Pellet IR sensor           
            break; 
          case 30:
            if (State==3)
            {
                digitalWrite(EEGOut1, HIGH);
                digitalWrite(EEGOut0, LOW);
                delay(10000);
                digitalWrite(EEGOut1, HIGH);
                digitalWrite(EEGOut0, HIGH);                
            }
            Ack = true; 
            break; 
          case 31:             
            Execute = true;
            break;             
          
      }
      //Check if an execute command            
  }
  GetC = false;  //Debounce close
  }
}