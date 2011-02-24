// VideoWrapper.cpp : Defines the initialization routines for the DLL.
//

#include "stdafx.h"
#include "VideoWrapper.h"
#include <vfw.h>
#include <time.h>
#include <queue>




#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define ENC_BUF_SIZE 1024*1024*3

using namespace std;
typedef queue<LPVOID> LPQUEUE;
#define MAXMUXS 4
#define MAXDEVS 2
#define ID_NEW_FRAME 37810
#define ID_MUX0_NEW_FRAME 37800
#define ID_MUX1_NEW_FRAME 37801
#define ID_MUX2_NEW_FRAME 37802
#define ID_MUX3_NEW_FRAME 37803

const INT64 MBYTE = 1024*1024;   // 1MB

HANDLE hAVIFile[MAXDEVS*MAXMUXS];
BOOL bStartTime[MAXDEVS*MAXMUXS];
BOOL bSave[MAXDEVS];
BOOL bStartSave[MAXDEVS*MAXMUXS];
BOOL bKeyFrame[MAXDEVS*MAXMUXS];
BOOL bEncInited[MAXDEVS*MAXMUXS];
time_t start_time[MAXDEVS*MAXMUXS];
time_t end_time[MAXDEVS*MAXMUXS];
int nWidth[MAXDEVS];
int nHeight[MAXDEVS];
int nVideoInput[MAXDEVS];
int nEncFrameRate[MAXDEVS*MAXMUXS];
int nEncQuant[MAXDEVS*MAXMUXS];
int nEncKeyInterval[MAXDEVS*MAXMUXS];
int nFrameCount[MAXDEVS*MAXMUXS];
int nFileIndex[MAXDEVS*MAXMUXS];
INT64 nAccumulatedSize[MAXDEVS*MAXMUXS];

int Global_Frate;
typedef int (WINAPI* ptr_func1)(void **pp);
ptr_func1 AdvDVP_CreateSDKInstence  = NULL;
ptr_func1 AdvDVP_CreateEncSDKInstence  = NULL;
HINSTANCE hDll;
HINSTANCE hEncDll;
IDVP7010BDLL *pDVPSDK = NULL;
IDVP7010BEncDLL *pDVPEncSDK = NULL;


int initSDK()
{
	int ErrorVal;			
	hDll = LoadLibrary(TEXT("D:\\WINDOWS\\system32\\DVP7010B.dll"));
	if (hDll)
	{
		AdvDVP_CreateSDKInstence = (ptr_func1)GetProcAddress(hDll, (LPCSTR)"AdvDVP_CreateSDKInstance");
	}
	else return 2;

	if (AdvDVP_CreateSDKInstence)
	{
		ErrorVal = AdvDVP_CreateSDKInstence((void **)&pDVPSDK);
		if (ErrorVal != SUCCEEDED)
			return ErrorVal;
	}
	else return 3;	
	hEncDll = LoadLibrary(TEXT("D:\\WINDOWS\\system32\\DVP7010BEnc.dll"));
	if (hEncDll)
	{
		AdvDVP_CreateEncSDKInstence = (ptr_func1)GetProcAddress(hEncDll, (LPCSTR)"AdvDVP_CreateEncSDKInstence");
	} else return 2;
	if (AdvDVP_CreateEncSDKInstence)
	{
		ErrorVal = AdvDVP_CreateEncSDKInstence((void **)&pDVPEncSDK);
		if (ErrorVal != SUCCEEDED)
			return ErrorVal;
	}
	else
		return 3;
	return 1;	
}
int StartSDK(void)
{
	int res = pDVPSDK->AdvDVP_InitSDK();
	if (res == SUCCEEDED)
	{
		res = pDVPSDK->AdvDVP_GetNoOfDevices(&nDevCount);
		if (res != SUCCEEDED)
			return res;	
	}
	return 1;
}

int GetDeviceCount()
{
	return nDevCount;
}

int GetFrameRate(int *Frate)
{
	int res;
	Frate = new int;
	for (int i = 0; i < nDevCount; i++)
	{
		res = pDVPSDK->AdvDVP_GetFrameRate(i, Frate);
	}
	return res;
}

int SetFrameRate(int Frate)
{
	int res;
	Global_Frate = Frate;
	for (int i = 0; i < nDevCount; i++)
	{
		res = pDVPSDK->AdvDVP_SetFrameRate(i, Switching, Frate);
	}
	return res;
}

int StartCapture()
{
	int res;
	for (int i = 0; i < nDevCount; i++)
	{
		res = pDVPSDK->AdvDVP_Start(i,Switching,NULL,NULL);		
		if (res !=  SUCCEEDED) 
		{
			return res;
		}
	}	
	for (int i = 0; i < nDevCount*MAXMUXS; i++)
	{

	}
	
}


void StreamReadBegin(int nChNum);	//Begin Video Stream Read Callback function
void StreamReadProc(int nChNum, LPVOID pStreamBuf, long lBufSize, DWORD dwCompFlags);	//Video Stream Read Callback function
void StreamReadEnd(int nChNum);		//End Video Stream Read Callback function

//StreamRead callback function sturcture
STREAMREAD_STRUCT StreamRead = {StreamReadBegin,
								StreamReadProc,
								StreamReadEnd};


void StreamReadBegin(int nChNum)
{
	nFileIndex[nChNum] = 0;
	nAccumulatedSize[nChNum] = 0;
	bStartSave[nChNum] = FALSE;
}

void StreamReadProc(int nChNum, LPVOID pStreamBuf, long lBufSize, DWORD dwCompFlags)
{
	int nDevNum = nChNum/MAXMUXS;
	if (bSave[nDevNum])
	{
		nAccumulatedSize[nChNum] += lBufSize;
		if (nAccumulatedSize[nChNum] >= 1900*MBYTE && dwCompFlags == AVIIF_KEYFRAME)	//The size limit of the AVI file is 2GB
		{
			nAccumulatedSize[nChNum] = 0;
			nFileIndex[nChNum]++;
			//Close AVI file
			if (hAVIFile[nChNum])
			{
				pDVPEncSDK->AdvDVP_CloseAVIFile(hAVIFile[nChNum]);
				hAVIFile[nChNum] = NULL;
			}
		}

		//Create AVI file
		if (hAVIFile[nChNum] == NULL)
		{
			CFileStatus cFileStatus;
			if (CFile::GetStatus(SavePath, cFileStatus) == TRUE)
			{
				char FileName[MAX_PATH];
				sprintf(FileName, "%s\\Stream%02d_%04d.avi", SavePath, nChNum, nFileIndex[nChNum]);				
				hAVIFile[nChNum] = pDVPEncSDK->AdvDVP_CreateAVIFile(FileName, nWidth[nDevNum], nHeight[nDevNum], nEncFrameRate[nChNum]);
			}
		}

		//First frame of the video file must be key frame.
		if (dwCompFlags == AVIIF_KEYFRAME)
		{
			bStartSave[nChNum] = TRUE;
		}

		//Write AVI file
		if (hAVIFile[nChNum] && bStartSave[nChNum])
		{
			pDVPEncSDK->AdvDVP_WriteAVIFile(hAVIFile[nChNum], pStreamBuf, lBufSize, dwCompFlags);
		}
	}
	else
	{
		//Close AVI file
		if (hAVIFile[nChNum])
		{
			pDVPEncSDK->AdvDVP_CloseAVIFile(hAVIFile[nChNum]);
			hAVIFile[nChNum] = NULL;
			bStartSave[nChNum] = FALSE;
		}
	}
}

void StreamReadEnd(int nChNum)
{
	//Close AVI file
	if (hAVIFile[nChNum])
	{
		pDVPEncSDK->AdvDVP_CloseAVIFile(hAVIFile[nChNum]);
		hAVIFile[nChNum] = NULL;
	}
}