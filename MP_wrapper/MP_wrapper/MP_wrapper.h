// MP_wrapper.h : main header file for the MP_wrapper DLL
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


extern "C" __declspec(dllexport) int InitRecording();
extern "C" __declspec(dllexport) void RecordingThread();
extern "C" __declspec(dllexport) int SetSamplingRate();
extern "C" __declspec(dllexport) int SetRecordingChannels(short Chans);
extern "C" __declspec(dllexport) int 




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


