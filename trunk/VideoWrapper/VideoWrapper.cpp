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
#define MAXDEVS 4
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
char* SaveName;

int Global_Frate;
typedef int (WINAPI* ptr_func1)(void **pp);
ptr_func1 AdvDVP_CreateSDKInstence  = NULL;
ptr_func1 AdvDVP_CreateEncSDKInstence  = NULL;
HINSTANCE hDll;
HINSTANCE hEncDll;
IDVP7010BDLL *pDVPSDK = NULL;
IDVP7010BEncDLL *pDVPEncSDK = NULL;


void StreamReadBegin(int nChNum);	//Begin Video Stream Read Callback function
void StreamReadProc(int nChNum, LPVOID pStreamBuf, long lBufSize, DWORD dwCompFlags);	//Video Stream Read Callback function
void StreamReadEnd(int nChNum);		//End Video Stream Read Callback function
//StreamRead callback function sturcture
STREAMREAD_STRUCT StreamRead = {StreamReadBegin,
								StreamReadProc,
								StreamReadEnd};


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
	else return 3;
	return 1; //Succeeded
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
	res = pDVPEncSDK->AdvDVP_InitSDK();
	if (res == SUCCEEDED)
	{
		res = pDVPSDK->AdvDVP_GetNoOfDevices(&nDevCount);
		if (res != SUCCEEDED)
			return res;	
	}
	for (int i = 0; i < nDevCount*MAXMUXS; i++)
	{
		LastEncRes = pDVPEncSDK->AdvDVP_InitEncoder(i, ENC_BUF_SIZE);
		if (LastEncRes != 1)
			return LastEncRes;
	}
	return 1;
}
	
int SetVideoQuant(int Quant)
{
	for (int i=0; i<nDevCount*MAXMUXS; i++)
	{
		LastEncRes = pDVPEncSDK->AdvDVP_SetVideoQuant(i,Quant);
		if (LastEncRes != 1)
			return LastEncRes;
	}
	return 1;
}

int SetContrast(int Chan, long Contrast)
{
	int DevNum = Chan / 4;
	int Switch = Chan % 4;
	return pDVPSDK->AdvDVP_SetContrast(DevNum,Switch,Contrast);
}
int SetBrightness(int Chan, long Brightness)
{
	int DevNum = Chan / 4;
	int Switch = Chan % 4;
	return pDVPSDK->AdvDVP_SetBrightness(DevNum,Switch,Brightness);
}
int SetHue(int Chan, long Hue)
{
	int DevNum = Chan / 4;
	int Switch = Chan % 4;
	return pDVPSDK->AdvDVP_SetHue(DevNum,Switch,Hue);
}
int SetSaturation(int Chan, long Saturation)
{
	int DevNum = Chan / 4;
	int Switch = Chan % 4;
	return pDVPSDK->AdvDVP_SetSaturation(DevNum,Switch,Saturation);	
}

int GetEncoderStatus()
{
	return pDVPEncSDK->AdvDVP_GetState(0);
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
		if (pDVPSDK->AdvDVP_GetCapState(i) == STOPPED)
		{
			res = pDVPSDK->AdvDVP_SetNewFrameCallback(i, (int)NewFrameCallback);
			if (res !=  SUCCEEDED) 
			{
				return res;
			}
			//Start Video Capture 	
			res = pDVPSDK->AdvDVP_Start(i,3,NULL,NULL);		
			if (res !=  SUCCEEDED) 
			{
				return res;
			}
		}			
	}
	return res;

}
int StartEncoding()
{	
	for (int i = 0; i < nDevCount*MAXMUXS; i++)
	{
		pDVPEncSDK->AdvDVP_SetStreamReadCB(&StreamRead);		
		LastEncRes = pDVPEncSDK->AdvDVP_StartVideoEncode(i);
		if (LastEncRes !=  SUCCEEDED) 
		{
			return LastEncRes;
		}
	}
	return 1;
}



void StreamReadBegin(int nChNum)
{
	nFileIndex[nChNum] = 0;
	nAccumulatedSize[nChNum] = 0;
	bStartSave[nChNum] = FALSE;
}

void StreamReadProc(int nChNum, LPVOID pStreamBuf, long lBufSize, DWORD dwCompFlags)
{
	int nDevNum = nChNum/MAXMUXS;
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
	//Create AVI file (if closed or unopened)
	if (hAVIFile[nChNum] == NULL)
	{		
		char FileName[MAX_PATH];
		//sprintf(SaveName, "C:\\Test");
		sprintf(FileName, "%02d_%04d.avi", nChNum, FileStart);						
		hAVIFile[nChNum] = pDVPEncSDK->AdvDVP_CreateAVIFile(FileName, nWidth[nDevNum], nHeight[nDevNum], 7);		
	}		
	//First frame of the video file must be key frame.
	if (dwCompFlags == AVIIF_KEYFRAME)
	{
		bStartSave[nChNum] = TRUE;
	}

	//Write AVI file
	if (hAVIFile[nChNum] && bStartSave[nChNum])
	{
		LastEncRes = pDVPEncSDK->AdvDVP_WriteAVIFile(hAVIFile[nChNum], pStreamBuf, lBufSize, dwCompFlags);
	}

}

int GetEncRes()
{
	return LastEncRes;
}

void StreamReadEnd(int nChNum)
{
	//Close AVI file
	if (hAVIFile[nChNum])
	{
		LastEncRes = pDVPEncSDK->AdvDVP_CloseAVIFile(hAVIFile[nChNum]);
		hAVIFile[nChNum] = NULL;
	}
}


int SetVideoRes(int XRes, int YRes)
{
	for (int i = 0; i < nDevCount; i++) //Set Video res per device
	{
		switch(XRes) 
		{
			case 640:
				pDVPSDK->AdvDVP_SetResolution(i,SIZEQVGA);
			break;
			case 320:
				pDVPSDK->AdvDVP_SetResolution(i,SIZEVGA);
			break;
			case 160:
				pDVPSDK->AdvDVP_SetResolution(i,SIZESUBQVGA);
			break;
		}
	}
	//Set the resolution per camera for encoding	
	for (int i=0; i<nDevCount*MAXMUXS; i++)
	{
		LastEncRes = pDVPEncSDK->AdvDVP_SetVideoResolution(i,XRes,YRes);		
		if (LastEncRes != 1)
			return LastEncRes;
	} 
	for (int i=0; i<nDevCount; i++) //Set the capture resolution for all devices. C# rocks. 
	{
		nWidth[i] = XRes;
		nHeight[i] = YRes;
	}
	return 1;
}


int SetKeyInterval(int KeyInt)
{
	for (int i=0; i<nDevCount*MAXMUXS; i++)
	{
		LastEncRes = pDVPEncSDK->AdvDVP_SetVideoKeyInterval(i,KeyInt);
		if (LastEncRes != 1)
			return LastEncRes;
	}
	return 1;
}

int NewFrameCallback(int lParam, int nID, int nDevNum, int nMuxChan, int nBufSize, BYTE* pBuf)
{
	int nChNum;
	if (nID != ID_NEW_FRAME)
	{
		nChNum = (nDevNum*MAXMUXS)+nMuxChan;
      if (*(pBuf+1) & 0x08 ) // FIFO overrun
      {
         return 1;
      }
	/*	if (*(pBuf+1) & 0x02) {
			//Videoframe notify local nosignal, we will not give new frames form this channel until the signal is recoverd.
			//We don't render this frame, because this frame is nosignal frame.		
			return 1;
		}*/ //Render this frame, for video recording 
      // Add by boxu for testing whether the graphics card support YUYV

      /*

      // Add end
		BITMAPINFOHEADER bih;
		//Render the data of each channel to the corresponding preview window
		if (SelChan == (nDevNum*4 + nMuxChan))		
		memset(&bih, 0, sizeof(bih));
		bih.biSize = sizeof(bih);
		bih.biWidth = nWidth[nDevNum];
		bih.biHeight = nHeight[nDevNum];
		bih.biPlanes = 1;
		bih.biBitCount = 16;
		bih.biCompression = MKFOURCC('Y','U','Y','2');
		bih.biSizeImage = nHeight[nDevNum] * nWidth[nDevNum] * 2;
    
		BOOL bRetVal = DrawDibDraw(DrawDib_ID[nDevNum],
					hdcStill,
					0,
					0,
					lStillWidth,
					lStillHeight,
					&bih,
					pBuf,
					0,
					0,
					nWidth[nDevNum],
					nHeight[nDevNum],
					0);
		if ( !bRetVal )
			TRACE( _T( "DrawDibDraw Failed!\n" ) );
		::ReleaseDC(hPreviewWnd[nChNum], hdcStill);*/ 
	}
	else
	{
		nChNum = (nDevNum*MAXMUXS);
	}


	//Encode the video frame
	if (pDVPEncSDK)
	{
		if (pDVPEncSDK->AdvDVP_GetState(nChNum) == ENC_RUNNING) {
			int Ret = pDVPEncSDK->AdvDVP_VideoEncode(nChNum, (LPVOID)pBuf, nBufSize, bKeyFrame[nChNum]);
			if ((EncoderState)Ret == ENC_BUFFERFULL)
			{
				LastEncRes = Ret;
			}
			else if (Ret != ENC_SUCCEEDED)
			{
				if (bKeyFrame[nChNum])
					LastEncRes = -6;
				else
					LastEncRes = -7;
			}
			else
			{
				bKeyFrame[nChNum] = FALSE;
			}
		}
	}

	//Calculate the number of frames for each channel
	if (bStartTime[nChNum]) 
	{
		start_time[nChNum] = time(NULL);
		nFrameCount[nChNum] = 0;
		bStartTime[nChNum] = FALSE;
	}
	end_time[nChNum] = time(NULL);
	nFrameCount[nChNum]++;
	return 1;
	}


int  CloseRecording(void)
{
		
		for (int i=0; i<nDevCount*MAXMUXS; i++)
		{
			if (pDVPEncSDK->AdvDVP_GetState(i) == ENC_RUNNING)
				pDVPEncSDK->AdvDVP_StopVideoEncode(i);
			//pDVPEncSDK->AdvDVP_CloseEncoder(i);
		}
		/*for (int i=0; i<nDevCount; i++)
		{
			if (pDVPSDK->AdvDVP_GetCapState(i) == RUNNING)
				pDVPSDK->AdvDVP_Stop(i);
		}*/
	return 1;
}

void FreeCaptureDevice()
{
		pDVPEncSDK->AdvDVP_CloseSDK();
		pDVPSDK->AdvDVP_CloseSDK();
		delete pDVPSDK;
		delete pDVPEncSDK;

	if (hEncDll)
		FreeLibrary(hEncDll);

	if (hDll)
		FreeLibrary(hDll);
}

void SetFName(LPTSTR FName, int FStart)
{
	SaveName = new char[256];
	//SaveName = FName;
	FileStart = FStart;
}

int SetNTSC()
{
	for (int i = 0; i < nDevCount; i++)
	{
		pDVPSDK->AdvDVP_SetVideoFormat(i, Video_NTSC_M);
	}
	return 1;
}