//Values to trigger fails on feeders
#define STEPFAILATTEMPTS 200  //Motor takes 200 steps for 1 rotation
#define DIRFAILATTEMPTS 4     //Change Direction 4 times before Giving up

#define DELAYTIME 1000
#define DEBOUNCETIME 50
#define STEPTIME 40
#define MOTORPOWERUPTIME 500
#define LOOPDELAYTIME 50

//Command Values
#define RUNALL 24
#define FEEDER 25
#define PELLET 26
#define TESTSTATES 27
#define ACKNOWLEDGE 28
#define EXECUTE 29
#define BLINKLED 30
#define NULL 31

//Feeder Null Address Value
#define NullAddress 24

//IR Pin
#define PelletIR 2

//Command Pulse Pin
#define CommandPulse 3

//Address lines
#define AddA 11
#define AddB 10

//Motor Controls
#define STEP 12
#define DIR 13
#define MOTORCONTROL 0  //Power for the motors

//Feeder States
#define FAIL 0
#define EXECUTING 1
#define SUCCESS 2
#define READY 3

//Break out board pins
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

volatile int StepAttempts = 0;
volatile int DirectionAttempts = 0;
volatile int Command = -1;
volatile int State;
volatile boolean IRDebounce = false;  //Used to debounce the IR Sensors
volatile boolean NextCommandIsFeederNumber = false;
volatile boolean Ack = false;
volatile boolean dir = false;  //Direction
volatile boolean ignoreFirstDrop = true;
volatile boolean execute = false;
volatile int Pellets = -1;       //Pellets per Feeder List
volatile int FeederNumber = -1;  //Feeder to deliver pellets from
volatile int IRValue = 0;

void setup() {
  //setup all pins
  pinMode(MOTORCONTROL, OUTPUT);    //Initialize Motor Power Pin
  digitalWrite(MOTORCONTROL, LOW);  //Turn the motors off ASAP


  //Initialize Pins
  pinMode(CommandPulse, INPUT_PULLUP);
  pinMode(PelletIR, OUTPUT);
  digitalWrite(PelletIR, HIGH);
  pinMode(PelletIR, INPUT_PULLUP);
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
  pinMode(EEG0, INPUT_PULLUP);
  pinMode(EEG1, INPUT_PULLUP);
  pinMode(EEG2, INPUT_PULLUP);
  pinMode(EEG3, INPUT_PULLUP);
  pinMode(EEG4, INPUT_PULLUP);
  pinMode(EEGOut0, OUTPUT);
  pinMode(EEGOut1, OUTPUT);
  digitalWrite(EEGOut0,LOW);

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

  //Attach interupts
  attachInterrupt(digitalPinToInterrupt(CommandPulse), GetCommand, FALLING);  //Turn on command recieve interrupt
  SetState(FAIL);
  delay(500);
  SetState(READY);
}

void SetAddress(int Address) {
  digitalWrite(BO1, LOW);
  digitalWrite(BO2, LOW);
  digitalWrite(BO3, LOW);
  digitalWrite(BO4, LOW);
  digitalWrite(BO5, LOW);
  digitalWrite(BO6, LOW);

  int Board = Address / 4;   //Divide by 4, low 2 bytes feeder, high bits breakout boards.
  int Feeder = Address % 4;  //Get feeder count (% is remainder)
  switch (Board) {
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
  if ((Feeder & 1) == 1) {
    digitalWrite(AddA, HIGH);
  } else {
    digitalWrite(AddA, LOW);
  }
  if ((Feeder & 2) == 2) {
    digitalWrite(AddB, HIGH);
  } else {
    digitalWrite(AddB, LOW);
  }
}

void SetState(int newState) {
  if (newState != State) {
    //State 0 - Fail
    //State 1 = Executing
    //State 2 = Success
    //State 3 = Ready
    switch (newState) {
      case FAIL:
        digitalWrite(EEGOut0, LOW);
        digitalWrite(EEGOut1, LOW);
        break;
      case EXECUTING:
        digitalWrite(EEGOut0, HIGH);
        digitalWrite(EEGOut1, LOW);
        break;
      case SUCCESS:
        digitalWrite(EEGOut0, LOW);
        digitalWrite(EEGOut1, HIGH);
        break;
      case READY:
        digitalWrite(EEGOut0, HIGH);
        digitalWrite(EEGOut1, HIGH);
        break;
    }
    State = newState;
  }
}

void RunAllFeeders() {
  Pellets = 1;
  for (int Feeder = 0; Feeder < 24; Feeder++) {
    FeederNumber = Feeder;
    executeFeeding();
  }
}

void ProcessCommand() {
  if (Command != -1) {
    if (Command < 24) {
      if (NextCommandIsFeederNumber)  //Are we reading a feeder
      {
        FeederNumber = Command;  //Store Feeder to deliver pellets from
      } else {
        Pellets = Command;  //Store Number of Pellets to be delivered
      }
    } else {
      switch (Command) {
        case RUNALL:
          RunAllFeeders();
          break;
        case ACKNOWLEDGE:
          Ack = true;
          if (State == READY) {
            SetState(SUCCESS);
            delay(DELAYTIME);
            SetState(READY);
          }
          break;
        case FEEDER:
          NextCommandIsFeederNumber = true;
          break;
        case PELLET:
          NextCommandIsFeederNumber = false;
          break;
        case TESTSTATES:
          SetState(FAIL);
          delay(DELAYTIME);
          SetState(EXECUTING);
          delay(DELAYTIME);
          SetState(SUCCESS);
          delay(DELAYTIME);
          SetState(READY);
          delay(DELAYTIME);
          break;

        case EXECUTE:
          execute = true;
          break;
        case BLINKLED:  //Reset to default values
          digitalWrite(LED_BUILTIN, HIGH);
          delay(DELAYTIME);
          digitalWrite(LED_BUILTIN, LOW);
          break;
        case NULL:
          break;
      }
    }
    Command = -1;
  }
}

void GetCommand() {
  Command = 0;
  //Convert Message from BioPac to integer
  if (!digitalRead(EEG0)) { Command = Command + 1; }
  if (!digitalRead(EEG1)) { Command = Command + 2; }
  if (!digitalRead(EEG2)) { Command = Command + 4; }
  if (!digitalRead(EEG3)) { Command = Command + 8; }
  if (!digitalRead(EEG4)) { Command = Command + 16; }
  if (Command == 28) {
    Ack = true;
  }
}

void stepCurrentMotor() {
  //One motor step, 80ms long
  digitalWrite(STEP, HIGH);
  delay(STEPTIME);
  digitalWrite(STEP, LOW);
  delay(STEPTIME);
}

void checkIRStatus() {
  IRValue = digitalRead(PelletIR);
  if (IRValue == HIGH) {
    SetState(READY);
  } else {
    SetState(FAIL);
  }
}

void IRBroken() {
  if (!ignoreFirstDrop) {
    if (!IRDebounce) {
      delay(DEBOUNCETIME);
      IRDebounce = true;
      Pellets--;         //Subtract a pellet
      StepAttempts = 0;  //Reset the number of attempts to get a pellet
      IRDebounce = false;
    }
  }
}

void executeFeeding() {
  //Init Variables for feeding
  StepAttempts = 0;
  DirectionAttempts = 0;
  bool Fail = false;
  //Attach Interupt for IR Sensors
  ignoreFirstDrop = true;
  attachInterrupt(digitalPinToInterrupt(PelletIR), IRBroken, FALLING);  //Turn on pellet IR
  delay(DEBOUNCETIME);
  //Set the Feeder we are Feeding to
  SetAddress(FeederNumber);
  //Set the Motor Step Pin Low
  digitalWrite(STEP, LOW);
  //Turn on the motors
  digitalWrite(MOTORCONTROL, HIGH);

  SetState(EXECUTING);
  //Wait for Motor Power up
  delay(MOTORPOWERUPTIME);

  ignoreFirstDrop = false;
  while (Pellets > 0) {
    stepCurrentMotor();
    StepAttempts++;
    if (StepAttempts > STEPFAILATTEMPTS) {
      dir = !dir;
      if (dir) {
        digitalWrite(DIR, HIGH);
      } else {
        digitalWrite(DIR, LOW);
      }
      StepAttempts = 0;
      DirectionAttempts++;

      if (DirectionAttempts > DIRFAILATTEMPTS)  //We've failed completely, time to move on.
      {
        Fail = true;
        Pellets = 0;
        DirectionAttempts = 0;
      }
    }
  }
  Ack = false;
  if (Fail) {
    SetState(FAIL);
    Fail = false;
  } else {
    SetState(SUCCESS);
  }

  while (Ack == false) {
    delay(LOOPDELAYTIME);
  }
  Ack = false;
  StepAttempts = 0;
  DirectionAttempts = 0;
  Fail = false;
  FeederNumber = -1;
  Pellets = -1;
  execute = false;
  SetAddress(NullAddress);                           //Set to a null address
  digitalWrite(MOTORCONTROL, LOW);                   //Turn off power to the motors
  detachInterrupt(digitalPinToInterrupt(PelletIR));  //Turn off Pellet IR sensor
  SetState(READY);
}

void loop() {
  checkIRStatus();
  ProcessCommand();
  if (execute)  //Recived both a feeder number and a pellete amount okay to execute
  {
    if (FeederNumber != -1 && Pellets != -1) {
      executeFeeding();
    } else {
      execute = false;
      SetState(FAIL);
      delay(DELAYTIME);
      SetState(READY);
    }
  }
  Ack = false;
  delay(LOOPDELAYTIME);
}