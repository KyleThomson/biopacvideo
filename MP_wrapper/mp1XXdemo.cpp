/*
 Copyright 2004-2006 BIOPAC Systems, Inc.

 This software is provided 'as-is', without any express or implied warranty.
 In no event will BIOPAC Systems, Inc. or BIOPAC Systems, Inc. employees be
 held liable for any damages arising from the use of this software.

 Permission is granted to anyone to use this software for any purpose,
 including commercial applications, and to alter it and redistribute it
 freely, subject to the following restrictions:

 1. The origin of this software must not be misrepresented; you must not
 claim that you wrote the original software. If you use this software in a
 product, an acknowledgment (see the following) in the product documentation
 is required.

 Portions Copyright 2004-2006 BIOPAC Systems, Inc.

 2. Altered source versions must be plainly marked as such, and must not be
 misrepresented as being the original software.

 3. This notice may not be removed or altered from any source distribution.
 */

/* mp1XXXdemo.cpp : This program demonstrates the capabilities and programming logic of
					the BIOPAC Hardware API

   NOTE: Link to mpdev.lib during compile
 */

#include <iostream>
#include <stdio.h>
#include <windows.h>
//must be change to specify the path of mpdev.h
#include "mpdev.h"

using namespace std;

void getBufferDemo();
void startAcqDaemonDemo();
void ioDemo();

int main(int argc, char** argv)
{
	MPRETURNCODE retval;

	//configure the API and connect to the MP Device
	//Note: Currently set to automatically discover the MP150.
	//Change the third parameter the serial number of the MP150
	//if it is not the BHAPI is not connecting to the correct MP150
	retval = connectMPDev(MP150, MPUDP, "auto");

	if(retval != MPSUCCESS)
	{
		cout << "Program failed to connect to MP Device" << endl;
		cout << "connectMPDev returned with " << retval << " as a return code." << endl;

		cout << "Disconnecting..." << endl;

		disconnectMPDev();

		cout << "Exit" << endl;

		return 0;
	}


	//execute Get Buffer Demo
	cout << "Executing Get Buffer Demo..." << endl;
	getBufferDemo();

	//execute Start Acquistion Daemon Demo
	cout << "Executing Start Acquistion Daemon Demo..." << endl;
	startAcqDaemonDemo();

	//execute Start Acquistion Daemon Demo
	cout << "Executing Input Ouput Demo..." << endl;
	ioDemo();

	cout << "Disconnecting..." << endl;

	disconnectMPDev();

	cout << "Exit" << endl;

	return 1;
}


// Get Buffer Demo
void getBufferDemo()
{
	MPRETURNCODE retval;

	//acquire on channel 1 and 5
	BOOL analogCH[] = {true, false, false, false,
					   true, false, false, false,
					   false, false, false, false,
					   false, false, false, false};

	cout << "Acquire data on  Analog Channel 1 and Analog Channel 5" << endl;
	cout << "Setting Acquisition Channels..." << endl;
	retval = setAcqChannels(analogCH);

	if(retval != MPSUCCESS)
	{
		cout << "Program failed to set Acquisition Channels" << endl;
		cout << "setAcqChannels returned with " << retval << " as a return code." << endl;

		return;
	}

	//set sample rate to 1 msec per sample = 1000 Hz
	cout << "Setting Sample Rate to 1000 Hz" << endl;
	retval = setSampleRate(1.0);

	if(retval != MPSUCCESS)
	{
		cout << "Program failed to set Sample Rate" << endl;
		cout << "setSampleRate returned with " << retval << " as a return code." << endl;

		return;
	}

	cout << "Starting Acquisition..." << endl;
	retval = startAcquisition();

	if(retval != MPSUCCESS)
	{
		cout << "Program failed to Start Acquisition" << endl;
		cout << "startAcquisition returned with " << retval << " as a return code." << endl;

		cout << "Stopping..." << endl;

		stopAcquisition();

		return;
	}

	cout << "Acquiring..." << endl;

	long numsamples = 100;

	cout << "Getting " << numsamples << " samples from the MP Device... " << endl;
	cout << "Using getMPBuffer " << endl;


	//need enough space for all active channels
	//In this case we need twice the number of samples
	//because there are 2 active channels
	double * data = new double[2*numsamples];

	retval = getMPBuffer(numsamples, data);

	if(retval != MPSUCCESS)
	{
		cout << "Program failed to get data" << endl;
		cout << "getMPBuffer(...) returned with " << retval << " as a return code." << endl;

		cout << "Stopping..." << endl;

		stopAcquisition();

		return;
	}

	//de-interleave the samples
	double * ch1 = new double[numsamples];
	double * ch5 = new double[numsamples];

	for(int i = 0; i < 2*numsamples; i++)
	{
		switch(i % 2)
		{
		case 0:
			ch1[i/2] = data[i];
			break;
		case 1:
			ch5[i/2] = data[i];
			break;
		default:
			break;
		}
	}

	//print data
	cout << "CH1 Data: ";
	for(int j = 0; j < numsamples; j++)
	{
		cout << ch1[j] <<' ';
	}
	cout << endl;

	cout << "CH5 Data: ";
	for(int k = 0; k < numsamples; k++)
	{
		cout << ch5[k] <<' ';
	}
	cout << endl;

	//stop
	cout << "Stopping..." << endl;

	stopAcquisition();

	//free memory
	delete[] data;
	delete[] ch1;
	delete[] ch5;
}

// Start Acquistion Daemon Demo
void startAcqDaemonDemo()
{
	MPRETURNCODE retval;

	//acquire on channel 2, 7, and 11
	BOOL analogCH[] = {false, true, false, false,
					   false, false, true, false,
					   false, false, true, false,
					   false, false, false, false};

	cout << "Acquire data on  Analog Channel 1 and Analog Channel 5" << endl;
	cout << "Setting Acquisition Channels..." << endl;
	retval = setAcqChannels(analogCH);

	if(retval != MPSUCCESS)
	{
		cout << "Program failed to set Acquisition Channels" << endl;
		cout << "setAcqChannels returned with " << retval << " as a return code." << endl;

		return;
	}

	//set sample rate to 5 msec per sample = 200 Hz
	cout << "Setting Sample Rate to 1000 Hz" << endl;
	retval = setSampleRate(1.0);

	if(retval != MPSUCCESS)
	{
		cout << "Program failed to set Sample Rate" << endl;
		cout << "setSampleRate returned with " << retval << " as a return code." << endl;

		return;
	}

	cout << "Starting Acquisition Daemon..." << endl;
	retval =  startMPAcqDaemon();

	if(retval != MPSUCCESS)
	{
		cout << "Program failed to Start Acquisition Daemon" << endl;
		cout << "startMPAcqDaemon returned with " << retval << " as a return code." << endl;

		cout << "Stopping..." << endl;

		stopAcquisition();

		return;
	}

	cout << "Daemon Started" << endl;

	cout << "Starting Acquisition..." << endl;
	retval = startAcquisition();

	if(retval != MPSUCCESS)
	{
		cout << "Program failed to Start Acquisition" << endl;
		cout << "startAcquisition returned with " << retval << " as a return code." << endl;

		cout << "Stopping..." << endl;

		stopAcquisition();

		return;
	}

	cout << "Acquiring..." << endl;

	long numsamples = 500000;

	cout << "Getting " << numsamples << " samples from the MP Device... " << endl;

	DWORD valuesRead = 0;
	DWORD numValuesToRead = 0;
	//remember that data will be interleaved
	//therefore we need to mulitply the number of samples
	//by the number of active channels to acquire the necessary
	//data points from the active channels
	DWORD remainValues = 3*numsamples;
	double * buff = new double[remainValues];
	DWORD offset = 0;

	cout << "Acquiring...\n" << endl;

	while(remainValues > 0)
	{
	   //read 1 second of data at a time
	   //frequency times the number of active channels
	   numValuesToRead = 3*200;

	   //if there are more values to be read than the remaing number of values we want read then just read the reamining needed
	   numValuesToRead = (numValuesToRead > remainValues) ? remainValues : numValuesToRead;


		if(receiveMPData(buff+offset,numValuesToRead, &valuesRead) != MPSUCCESS)
		{
			cout << "Failed to receive MP data" << endl;


			// using of getMPDaemonLAstError is a good practice
			char szbuff3[512];
			memset(szbuff3,0,512);
			sprintf(szbuff3,"Failed to Recv data from Acq Daemon. Last ERROR=%d, Read=%d", getMPDaemonLastError(), valuesRead);
			cout << szbuff3 << endl;

			stopAcquisition();

			break;
		}

	   offset += numValuesToRead;
	   remainValues -= numValuesToRead;

	   //show status
	   printf("                                                      \r");
	   printf("Remaining Values: %d\r", remainValues );
	}

	cout << endl;

	for(int j=0; j<numsamples; j++)
	{
		cout << "Sample: " << j+1 << endl;

		for(int i=0; i < 3; i++)
			switch(i)
			{
				case 0:
					cout << "CH2: " << buff[i+(3*j)];
					break;
				case 1:
					cout << " CH7: " << buff[i+(3*j)];
					break;
				case 2:
					cout << " CH11: " << buff[i+(3*j)] << endl;
					break;
				default:
					break;

			}
	}

	//stop
	cout << "Stopping..." << endl;

	stopAcquisition();

	//free Memory
	delete[] buff;
}

void ioDemo()
{
	MPRETURNCODE retval;
	double level = -10.0;

	cout << "Setting Anaglog Out 1 to " << level << endl;
	retval = setAnalogOut(level,1);

	if(retval != MPSUCCESS)
	{
		cout << "Program failed to Set Analog Out 1 to " << level << endl;
		cout << "setAnalogOut returned with " << retval << " as a return code." << endl;

		//set analog out to zero just in case
		setAnalogOut(0,1);

		return;
	}

	cout << "Analog Out 0 set" << endl;

	cout << "Sleeping for 2 seconds" << endl;

	Sleep(2000);

	cout << "Setting Anaglog Out 0 to " << level/3 << endl;
	retval = setAnalogOut(level/3,0);

	if(retval != MPSUCCESS)
	{
		cout << "Program failed to Set Analog Out 1 to " << level/3 << endl;
		cout << "setAnalogOut returned with " << retval << " as a return code." << endl;

		//set analog out to zero just in case
		setAnalogOut(0,0);

		return;
	}

	cout << "Analog Out 0 set" << endl;

	cout << "Sleeping for 2 seconds" << endl;

	Sleep(2000);

	cout << "Setting all Digital I/O line to zero" << endl;

	cout << "Setting low bits..." << endl;
	for(int j=0; j < 8; j++)
	{
		//send digital output to the MP Device once all of digital lines has been set
		if(j == 7)
			retval = setDigitalIO(j,false,true, SET_LOW_BITS);
		else
			retval = setDigitalIO(j,false,false, SET_LOW_BITS);

		if(retval != MPSUCCESS)
		{
			cout << "Program failed to set Digital I/O" << endl;
			cout << "setDigitalIO returned with " << retval << " as a return code." << endl;

			return;
		}
	}

	for(int i=8; i < 16; i++)
	{
		//send digital output to the MP Device once all of digital lines has been set
		if(i == 15)
			retval = setDigitalIO(i,false,true, SET_HIGH_BITS);
		else
			retval = setDigitalIO(i,false,false, SET_HIGH_BITS);

		if(retval != MPSUCCESS)
		{
			cout << "Program failed to set Digital I/O" << endl;
			cout << "setDigitalIO returned with " << retval << " as a return code." << endl;

			return;
		}
	}

	cout << "Reseting Analog Outputs to 0.0 volts." << endl;

	setAnalogOut(0,1);
	setAnalogOut(0,0);
}
