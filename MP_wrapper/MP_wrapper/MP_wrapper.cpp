// MP_wrapper.cpp : Defines the initialization routines for the DLL.
//

#include "stdafx.h"
#include "MP_wrapper.h"
#include "mpdev.h"
int retval;
BOOL C[16];
bool isRecording = false;
int SRate = 500;

void SetRecordingChannels(short Chans)
{
	
	for (int i = 0; i < 16; i++)
	{
		if (Chans&(1 << i)) 
		{
			C[i] = true;
		}
		else
		{
			C[i] = false;
		}
	}

}

int ConnectBioPac()
{
	retval = connectMPDev(MP150, MPUDP, "auto");
	return retval;
}

int DisconnectBioPac()
{
	retval = disconnectMPDev();
	return retval;
}





void Set_SamplingRate(int Rate)
{
	SRate = Rate;
	return;
}



int InitRecording(void)
{
	retval = setAcqChannels(&C[0]);
	if(retval != MPSUCCESS)
	{	
		return retval;
	}
	retval = setSampleRate(1000/SRate);
	if(retval != MPSUCCESS)
	{	
		return retval;
	}
	retval =  startMPAcqDaemon();
	if(retval != MPSUCCESS)
	{		
		stopAcquisition();		
	}
	return retval;
}


int RecordingStatus(void)
{
	return retval;
}
int LastDaemonError(void)
{
	return getMPDaemonLastError();
}
//
//TODO: If this DLL is dynamically linked against the MFC DLLs,
//		any functions exported from this DLL which call into
//		MFC must have the AFX_MANAGE_STATE macro added at the
//		very beginning of the function.
//
//		For example:
//
//		extern "C" BOOL PASCAL EXPORT ExportedFunction()
//		{
//			AFX_MANAGE_STATE(AfxGetStaticModuleState());
//			// normal function body here
//		}
//
//		It is very important that this macro appear in each
//		function, prior to any calls into MFC.  This means that
//		it must appear as the first statement within the 
//		function, even before any object variable declarations
//		as their constructors may generate calls into the MFC
//		DLL.
//
//		Please see MFC Technical Notes 33 and 58 for additional
//		details.
//

// CMP_wrapperApp

BEGIN_MESSAGE_MAP(CMP_wrapperApp, CWinApp)
END_MESSAGE_MAP()


// CMP_wrapperApp construction

CMP_wrapperApp::CMP_wrapperApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}


// The one and only CMP_wrapperApp object

CMP_wrapperApp theApp;


// CMP_wrapperApp initialization

BOOL CMP_wrapperApp::InitInstance()
{
	CWinApp::InitInstance();

	return TRUE;
}


