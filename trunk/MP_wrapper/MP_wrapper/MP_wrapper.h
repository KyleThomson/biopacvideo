// MP_wrapper.h : main header file for the MP_wrapper DLL
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


extern "C" __declspec(dllexport) int ConnectBioPac(void);
extern "C" __declspec(dllexport) int DisconnectBioPac(void);
extern "C" __declspec(dllexport) int InitRecording(void);
extern "C" __declspec(dllexport) void RecordingThread(void);
extern "C" __declspec(dllexport) void Set_SamplingRate(int Rate);
extern "C" __declspec(dllexport) void SetRecordingChannels(short Chans);
extern "C" __declspec(dllexport) void SetFileName(LPTSTR Fname);
extern "C" __declspec(dllexport) int EndRecording();

extern "C" __declspec(dllexport) int LastDaemonError(void);
extern "C" __declspec(dllexport) int RecordingStatus(void);
//extern "C" __declspec(dllexport) 



// CMP_wrapperApp
// See MP_wrapper.cpp for the implementation of this class
//

class CMP_wrapperApp : public CWinApp
{
public:
	CMP_wrapperApp();

// Overrides
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};


