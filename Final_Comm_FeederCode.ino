  int count = 0;
  int AddressTemp = 24;
  int innercount = 0;
  int attempts = 0; 
  int failattempts = 0; 
  int FailListCount = 0; 
  int State = 3;
  int innerloop, outerloop;
  boolean resetcount = false;
  boolean Execute = false; //Whether or not to execute a command
  boolean Feeder = true; 
  boolean Fail = false; 
  boolean Ack = false; 
  boolean PelletCount = false; 
  int PList[128]; //Pellets per Feeder List
  int FList[128];
  boolean dir = false; //Direction
  boolean GetC = false; //Get Command
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
  
  #define MOTORCONTROL 0 //Power for the motors 
  
  void setup()
  {
    //setup all pins
    pinMode(MOTORCONTROL,OUTPUT);  //Initialize Motor Power Pin
    digitalWrite(MOTORCONTROL,LOW); //Turn the motors off ASAP
    attachInterrupt(1, GetCommand, FALLING); //Turn on command recieve interrupt
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
    digitalWrite(AddA,LOW);
    digitalWrite(AddB, LOW);
    digitalWrite(BO1, LOW);
    digitalWrite(BO2, LOW);
    digitalWrite(BO3, LOW);
    digitalWrite(BO4, LOW);
    digitalWrite(BO5, LOW);
    digitalWrite(BO6, LOW);    
  //  digitalWrite(EEG0,HIGH);
   // digitalWrite(EEG1,HIGH);
   // digitalWrite(EEG2,HIGH);
   // digitalWrite(EEG3,HIGH);
   // digitalWrite(EEG4,HIGH);
   // digitalWrite(OUT0, HIGH);
   // digitalWrite(OUT1, HIGH);
   /* Feeder Test Code: Uncomment to test all feeders on boot
   for (outerloop=0; outerloop<24; outerloop++)
   {
     SetAddress(outerloop);
     for (innerloop=0; innerloop<40; innerloop++)
     {
       //One motor step, 80ms long
          digitalWrite(STEP, HIGH);   
          delay(40);              
          digitalWrite(STEP, LOW);    
          delay(40);
     }
   }*/
  }
  
  
  void SetAddress(int Addy)
  {
    digitalWrite(BO1, LOW);
    digitalWrite(BO2, LOW);
    digitalWrite(BO3, LOW);
    digitalWrite(BO4, LOW);
    digitalWrite(BO5, LOW);
    digitalWrite(BO6, LOW);  

    int BO = Addy / 4; //Divide by 4, low 2 bytes feeder, high bits breakout boards.
    int Ad = Addy%4; //Get feeder count (% is remainder
    switch(BO) {
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
    if ((Ad&1) == 1) 
    {
      digitalWrite(AddA, HIGH);
    }
    else
    {
       digitalWrite(AddA, LOW);
    }
    if ((Ad&2) == 2) 
    {
      digitalWrite(AddB, HIGH);
    }
    else
    {
       digitalWrite(AddB, LOW);
    }  
      
  
  
  
  }
     
  
  void loop()
  {
   delay(1000);
   val = digitalRead(PelletIR);
   if (val==HIGH)
   {
      State = 3; 
      digitalWrite(EEGOut1,HIGH);
      digitalWrite(EEGOut0,HIGH);
    }   
    else 
    {
      State = 0;
      digitalWrite(EEGOut0,LOW);
      digitalWrite(EEGOut1,LOW);
    }
   if (Execute) //Recieved final command, good to execute
   {
      attachInterrupt(0, PelletDrop, FALLING); //Turn on pellet IR
      delay(1000); //Probably not necessary, for safety.   
      while (Commands > 0) //While we have commands enqued
      {
        Commands--; //Remove a command
        //Get the current command   
        AddressTemp = FList[Commands];
        count = PList[Commands];
        SetAddress(AddressTemp); 
        digitalWrite(STEP, LOW); 
       //Turn on the motors         
        digitalWrite(MOTORCONTROL, HIGH);
        
        //Reset the counters
        innercount = 0;
        attempts = 0; 
        failattempts = 0;
        resetcount = true;
        Fail = false;
        
       digitalWrite(EEGOut1, LOW); 
       digitalWrite(EEGOut0, HIGH);
        while (count > 0)
        {
       
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
          if (attempts > 200)
          {       
            if (dir)
            {
              digitalWrite(DIR,HIGH);
            }
            else
            { 
              digitalWrite(DIR,LOW);
            }
            dir = !dir;
            attempts = 0;
            failattempts = failattempts+1; 

            if (failattempts > 9) //We've failed completely, time to move on. 
            { 
              Fail = true;
              count = 0; 
              failattempts = 0;
            }
            
          }

              if (innercount > 4) //Must wait for 4 rotations
              {
                resetcount = true;    //Debounce for pellet drop
              }    
        } //While (count > 0)
        delay(1000); //Take a quick break between feeder
        if (Fail)
        {
          State = 0;
          digitalWrite(EEGOut1,LOW);
          digitalWrite(EEGOut0,LOW);
          Fail = false;
        }
        else
        {
          State = 2;
          digitalWrite(EEGOut1,HIGH);
          digitalWrite(EEGOut0,LOW);
        }               
        delay(5000);

    } //While (Commands > 0)
    Execute = false;
    SetAddress(24); //Set to a null address
    // FAIL LOOP

     digitalWrite(MOTORCONTROL, LOW); //Turn off power to the motors
     detachInterrupt(0); //Turn off Pellet IR sensor
    // digitalWrite(OUT0, HIGH);
    // digitalWrite(OUT1, HIGH);
   } //if (Execute)
  }
   
  void PelletDrop() //Pellet drop IR sensor break
  {
    
    if (resetcount)  //Need to see 4 steps, before a pellet will be counted
    {
       resetcount = false; //Pellet Debounce - always first. 
       innercount = 0; //Reset the counter
       count--; //Subtract a pellet
       attempts = 0; //Reset the number of attempts to get a pellet
    }
  }
  
    void GetCommand()
  { 
    if (!GetC) //Debounce check
    {
      GetC = true; //Debounce set high - don't want to repeat the command
     // digitalWrite(OUT0, HIGH);
     // digitalWrite(OUT1, LOW);
      int Com = 0; //Clear last command
      //Convert Message from BioPac to number
      if (!digitalRead(EEG0)) {Com = Com+1;}
      if (!digitalRead(EEG1)) {Com = Com+2;}
      if (!digitalRead(EEG2)) {Com = Com+4;}
      if (!digitalRead(EEG3)) {Com = Com+8;}
      if (!digitalRead(EEG4)) {Com = Com+16;}   
      if  (Com == 31)
      {
          Execute=true;
      } //Check if an execute command 
      //if Com is 30, that's our ack!!!! 
      else  if (Feeder) //Are we reading a feeder 
      {
          FList[Commands] = Com; //Store feeder number
          Feeder = !Feeder; //Next is a pellet command
      }
        else 
      {
           PList[Commands] = Com; //Store Pellets number
           Feeder = !Feeder; //Next value is a feeder
           Commands++; //Add a Feeder command
      }
      GetC = false; //Debounce close
    } 
  } 
