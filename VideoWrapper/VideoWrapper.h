// VideoWrapper.h : main header file for the VideoWrapper DLL
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CVideoWrapperApp
// See VideoWrapper.cpp for the implementation of this class
//
extern "C" _declspec(dllexport) int initSDK(void);
extern "C" _declspec(dllexport) int StartSDK(void);
extern "C" _declspec(dllexport) int GetDeviceCount();
extern "C" _declspec(dllexport) int GetSampleRate(int *Frate);
extern "C" _declspec(dllexport) int SetSampleRate(int Frate);
extern "C" _declspec(dllexport) int StartCapture();
extern "C" _declspec(dllexport) void CloseRecording();
extern "C" _declspec(dllexport) void SetVideoQuant(int Quant);
extern "C" _declspec(dllexport) void SetVideoRes(int XRes, int YRes);
extern "C" _declspec(dllexport) void SetKeyInterval(int KeyInt);
extern "C" _declspec(dllexport) int SetContrast(int Chan, long Contrast);
extern "C" _declspec(dllexport) int SetBrightness(int Chan, long Brightness);
extern "C" _declspec(dllexport) int SetHue(int Chan, long Hue);
extern "C" _declspec(dllexport) int SetSaturation(int Chan, long Saturation);


int nDevCount;
int SelChan = 0;
int Switching = 4; 

int NewFrameCallback(int lParam, int nID, int nDevNum, int nMuxChan, int nBufSize, BYTE* pBuf);

typedef enum
{
	ENC_STOPPED			= 1,
	ENC_RUNNING			= 2,
	ENC_UNINITIALIZED	= -1,
} EncoderState;

typedef enum
{
	ENC_SUCCEEDED		= 1,
	ENC_FAILED			= 0,
	ENC_SDKINITFAILED	= -1,
	ENC_ENCINITFAILED	= -2,
	ENC_PARAMERROR		= -3,
	ENC_ENCNUMERROR		= -4,
	ENC_BUFFERFULL		= -5
} EncRes;




class CVideoWrapperApp : public CWinApp
{
public:
	CVideoWrapperApp();

// Overrides
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
//_declspec(dllexport) extern char *VideoResolution[] = {"FULL PAL", "D1", "VGA", "QVGA", "SUBQVGA"};

	
	


#define MAXMUXS 4
#define ID_NEW_FRAME 37810
#define ID_MUX0_NEW_FRAME 37800
#define ID_MUX1_NEW_FRAME 37801
#define ID_MUX2_NEW_FRAME 37802
#define ID_MUX3_NEW_FRAME 37803

typedef enum
{
	SUCCEEDED		= 1,
	FAILED			= 0,
	SDKINITFAILED	= -1,
	PARAMERROR		= -2,
	NODEVICES		= -3,
	NOSAMPLE		= -4,
	DEVICENUMERROR	= -5,
	INPUTERROR		= -6,
//	VERIFYHWERROR	= -7
} Res;

typedef enum tagAnalogVideoFormat 
{
    Video_None       = 0x00000000,
    Video_NTSC_M     = 0x00000001, 
    Video_NTSC_M_J   = 0x00000002,  
    Video_PAL_B      = 0x00000010,
    Video_PAL_M      = 0x00000200,
    Video_PAL_N      = 0x00000400,
    Video_SECAM_B    = 0x00001000
} AnalogVideoFormat;

typedef enum 
{
	SIZEFULLPAL=0,
	SIZED1,
	SIZEVGA,
	SIZEQVGA,
	SIZESUBQVGA
} VideoSize;

typedef enum
{
	STOPPED			= 1,
	RUNNING			= 2,
	UNINITIALIZED	= -1,
	UNKNOWNSTATE	= -2
} CapState;


class IDVP7010BDLL 
{
public:
   virtual ~IDVP7010BDLL() {}
   int AdvDVP_CreateSDKInstance(void **pp);
   int AdvDVP_CreateSDKInstence(void **pp);
	virtual int AdvDVP_InitSDK() PURE;
	virtual int AdvDVP_CloseSDK() PURE;
	virtual int AdvDVP_GetNoOfDevices(int *pNoOfDevs) PURE;
	virtual int AdvDVP_Start(int nDevNum, int SwitchingChans, HWND Main, HWND hwndPreview) PURE;
	virtual int AdvDVP_Stop(int nDevNum) PURE;
	virtual int AdvDVP_GetCapState(int nDevNum) PURE;
	virtual int AdvDVP_IsVideoPresent(int nDevNum, BOOL* VPresent) PURE;
	virtual int AdvDVP_GetCurFrameBuffer(int nDevNum, int VMux, long* bufSize, BYTE* buf) PURE; 
	virtual int AdvDVP_SetNewFrameCallback(int nDevNum, int callback) PURE;
	virtual int AdvDVP_GetVideoFormat(int nDevNum, AnalogVideoFormat* vFormat) PURE;
	virtual int AdvDVP_SetVideoFormat(int nDevNum, AnalogVideoFormat vFormat) PURE;
	virtual int AdvDVP_GetFrameRate(int nDevNum, int *nFrameRate) PURE;
	virtual int AdvDVP_SetFrameRate(int nDevNum, int SwitchingChans, int nFrameRate) PURE;
	virtual int AdvDVP_GetResolution(int nDevNum, VideoSize *Size) PURE; 
	virtual int AdvDVP_SetResolution(int nDevNum, VideoSize Size) PURE; 
	virtual int AdvDVP_GetVideoInput(int nDevNum, int* input) PURE;
	virtual int AdvDVP_SetVideoInput(int nDevNum, int input) PURE;
	virtual int AdvDVP_GetBrightness(int nDevNum, int input, long *pnValue) PURE;
	virtual int AdvDVP_SetBrightness(int nDevNum, int input, long nValue) PURE;
	virtual int AdvDVP_GetContrast(int nDevNum, int input, long *pnValue) PURE;
	virtual int AdvDVP_SetContrast(int nDevNum, int input, long nValue) PURE;
	virtual int AdvDVP_GetHue(int nDevNum, int input, long *pnValue) PURE;
	virtual int AdvDVP_SetHue(int nDevNum, int input, long nValue) PURE;
	virtual int AdvDVP_GetSaturation(int nDevNum, int input, long *pnValue) PURE;
	virtual int AdvDVP_SetSaturation(int nDevNum, int input, long nValue) PURE;
	virtual int AdvDVP_GPIOGetData(int nDevNum, int DINum, BOOL* value) PURE;
	virtual int AdvDVP_GPIOSetData(int nDevNum, int DONum, BOOL value) PURE;
   virtual void AdvDVP_SetDeinterlace ( BOOL bEnable ) PURE;
	virtual BOOL AdvDVP_GetDeinterlace () PURE;

};

typedef struct
{
	void (*PSTREAMREADBEGIN)(int nEncNum);
	void (*PSTREAMREADPROC)(int nEncNum, LPVOID pStreamBuf, long lBufSize, DWORD dwCompFlags);
	void (*PSTREAMREADEND)(int nEncNum);
}STREAMREAD_STRUCT;



class IDVP7010BEncDLL{
public:
   virtual ~IDVP7010BEncDLL() {}
   int AdvDVP_CreateEncSDKInstance(void **pp);
   int AdvDVP_CreateEncSDKInstence(void **pp);
	virtual int AdvDVP_InitSDK() PURE;
	virtual int AdvDVP_CloseSDK() PURE;
	virtual int AdvDVP_InitEncoder(int nEncNum, int nEncBufSize) PURE;
	virtual int AdvDVP_CloseEncoder(int nEncNum) PURE;
	virtual int AdvDVP_SetVideoQuant(int nEncNum, int nQuant) PURE;
	virtual int AdvDVP_GetVideoQuant(int nEncNum, int *nQuant) PURE;
	virtual int AdvDVP_SetVideoFrameRate(int nEncNum, int nFrameRate) PURE;
	virtual int AdvDVP_GetVideoFrameRate(int nEncNum, int *nFrameRate) PURE;
	virtual int AdvDVP_SetVideoResolution(int nEncNum, int nWidth, int nHeight) PURE;
	virtual int AdvDVP_GetVideoResolution(int nEncNum, int *nWidth, int *nHeight) PURE;
	virtual int AdvDVP_SetVideoKeyInterval(int nEncNum, int nKeyInterval) PURE;
	virtual int AdvDVP_GetVideoKeyInterval(int nEncNum, int *nKeyInterval) PURE;
	virtual int AdvDVP_StartVideoEncode(int nEncNum) PURE;
	virtual int AdvDVP_VideoEncode(int nEncNum, LPVOID lpInBuf, int InBufSize, BOOL bKeyFrame) PURE;
	virtual int AdvDVP_StopVideoEncode(int nEncNum) PURE;
	virtual HANDLE AdvDVP_CreateAVIFile(LPCSTR lpcsFileName, int nWidth, int nHeight, int nFrameRate) PURE;
	virtual int AdvDVP_WriteAVIFile(HANDLE hAVIFile, LPVOID lpStreamBuf, long lBufSize, DWORD dwCompFlags) PURE;
	virtual int AdvDVP_CloseAVIFile(HANDLE hAVIFile) PURE;
	virtual int AdvDVP_GetState(int nEncNum) PURE;
	virtual void AdvDVP_SetStreamReadCB(STREAMREAD_STRUCT *pStreamRead) PURE;
};
