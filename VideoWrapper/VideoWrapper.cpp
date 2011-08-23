// VideoWraper.cpp : Defines the initialization routines for the DLL.
//

#include "stdafx.h"
#include "VideoWrapper.h"
#include <vfw.h>
#include <time.h>
#include <queue>
#include <math.h>




#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define ENC_BUF_SIZE 1024*1024*3*10 

using namespace std;
typedef queue<LPVOID> LPQUEUE;

#define MAXMUXS 4
#define MAXDEVS 8
#define ID_NEW_FRAME 37810
#define ID_MUX0_NEW_FRAME 37800
#define ID_MUX1_NEW_FRAME 37801
#define ID_MUX2_NEW_FRAME 37802
 
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
bool Pres[MAXDEVS][MAXMUXS];
int Present[MAXDEVS];
bool CamIsRecording[MAXDEVS*MAXMUXS];
int CamAssoc[MAXDEVS*MAXMUXS];	
INT64 nAccumulatedSize[MAXDEVS*MAXMUXS];


typedef int (WINAPI* ptr_func1)(void **pp);
ptr_func1 AdvDVP_CreateSDKInstence  = NULL;
ptr_func1 AdvDVP_CreateEncSDKInstence  = NULL;
HINSTANCE hDll;
HINSTANCE hEncDll;
IDVP7010BDLL *pDVPSDK = NULL;
IDVP7010BEncDLL *pDVPEncSDK = NULL;
//Globals
int Global_Frate;
long lDstSize;
bool EncoderRunning;
bool NoSig = true;
char* SaveName;
BYTE* pDstBuf;
BYTE* ExtPtr;
BYTE* P2Buff = new BYTE[640*480*MAXDEVS*MAXMUXS*2];
int nDevCount;
int SelChan = 0;
int SelDevice = 0;
int Switching = 4; 
int FileStart = 0;


void StreamReadBegin(int nChNum);	//Begin Video Stream Read Callback function
void StreamReadProc(int nChNum, LPVOID pStreamBuf, long lBufSize, DWORD dwCompFlags);	//Video Stream Read Callback function
void StreamReadEnd(int nChNum);		//End Video Stream Read Callback function
//StreamRead callback function sturcture
STREAMREAD_STRUCT StreamRead = {StreamReadBegin,
								StreamReadProc,
								StreamReadEnd};


//Create instance of the Capture Library
int initCaptureSDK()
{
	for (int i=0; i<MAXDEVS*MAXMUXS; i++)
	{
		start_time[i] = 0;
		end_time[i] = 0;
		nFrameCount[i] = 0;
		bStartTime[i] = TRUE;
		bEncInited[i] = FALSE;
		bKeyFrame[i] = TRUE; 	
		hAVIFile[i] = NULL;
	}
	for (int i=0; i<MAXDEVS; i++)
	{
		for (int j=0; j<MAXMUXS; j++)
		{
			Pres[i][j] = false;
		}
	}
	int ErrorVal;			
	hDll = LoadLibrary(TEXT(".\\DVP7010B.dll"));
	if (hDll)
	{
		AdvDVP_CreateSDKInstence = (ptr_func1)GetProcAddress(hDll, (LPCSTR)"AdvDVP_CreateSDKInstance");
	}
	else return 2;

	if (AdvDVP_CreateSDKInstence)
	{
		ErrorVal = AdvDVP_CreateSDKInstence((void **)&pDVPSDK);
		if (ErrorVal != SUCCEEDED)
			return -1;
	}
	else return 3;	

	return 1; //Succeeded
}

//Initialize Capture library, download device count
int StartCaptureSDK(void)
{
	int res = pDVPSDK->AdvDVP_InitSDK();	
	if (res == SUCCEEDED)
	{
		res = pDVPSDK->AdvDVP_GetNoOfDevices(&nDevCount);
		if (res != SUCCEEDED)
			return res;	
	}	
	Sleep(100);
	return 1;
}


//Create an instance of the encoder library
int initEncoderSDK(void)
{
	int ErrorVal;
	hEncDll = LoadLibrary(TEXT(".\\DVP7010BEnc.dll"));
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

//Initialize an Encoder for each channel. 
int StartEncoderSDK()
{
	int res;
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


#define LIMIT(x)  ( (x) > 0xffff ? 0xff : ( (x) <= 0xff ? 0 : ( (x) >> 8 ) ) )
//Transform the color space from YUY2 to RGB24 
void YUYVtoRGB24(int width, int height, unsigned char *src, unsigned char *dst)
{
	int line, col, linewidth;
	int y, yy;
	int u, v;
	int vr, ug, vg, ub;
	int r, g, b;
	unsigned char *py, *pu, *pv;

	linewidth = width - (width >> 1);
	py = src+((height-1)*width*2);
	pu = py + 1;
	pv = py + 3;

	y = *py;
	yy = y << 8;
	u = *pu - 128;
	ug =   88 * u;
	ub =  454 * u;
	v = *pv - 128;
	vg =  183 * v;
	vr =  359 * v;

	for (line = height-1; line >= 0; line--) {
		py = src+(line*width*2);
		pu = py + 1;
		pv = py + 3;
		for (col = 0; col < width; col++) {
			r = LIMIT(yy +      vr);
			g = LIMIT(yy - ug - vg);
			b = LIMIT(yy + ub     );

			*dst++ = b;
			*dst++ = g;
			*dst++ = r;
			
			py += 2;
			y = *py;
			yy = y << 8;
			if ( (col & 1) == 1) {
			  pu += 4; // skip yvy every second y
			  pv += 4; // skip yuy every second y
			}
			u = *pu - 128;
			ug =   88 * u;
			ub =  454 * u;
			v = *pv - 128;
			vg =  183 * v;
			vr =  359 * v;
		} // ..for col 
	} /* ..for line */
}

//Sends a pointer to the main program, which is the current snap shot in RGB24 format. 
BYTE* GetCurrentBuffer(int Cam)
{
	
		YUYVtoRGB24(nWidth[0], nHeight[0], P2Buff+(Cam*nWidth[0]*nHeight[0]*2), pDstBuf+(Cam*nWidth[0]*nHeight[0]*3));		
		return pDstBuf+(Cam*nWidth[0]*nHeight[0]*3);
	
}


BYTE* GetSnapShot(int Chan)
{
	int nDevNum = Chan/4;
	int ChanSel = Chan%4;
	int res;
	long lSize = nWidth[nDevNum]*nHeight[nDevNum]*2;
	BYTE *pBuf = new BYTE[lSize];
	BOOL WTF;
	pDVPSDK->AdvDVP_IsVideoPresent(0,&WTF);
	if (!WTF)
	{
		return NULL;
	}
	if (pDVPSDK->AdvDVP_GetCapState(0) == RUNNING) 
	{
	res = pDVPSDK->AdvDVP_GetCurFrameBuffer(0, 0, &lSize, pBuf);
	if (res != SUCCEEDED)
	{
		delete pBuf;
		pBuf = NULL;
		return NULL;
	}
	
	YUYVtoRGB24(nWidth[nDevNum], nHeight[nDevNum], pBuf, pDstBuf);
	delete pBuf;
	pBuf = NULL;		
	return pDstBuf;
	}
	else return NULL;
}


int StartCapture()
{	
	//Start up Capture device, assuming the API is initialized
	int res;				
	for (int i = 0; i < nDevCount; i++)
	{
		if (pDVPSDK->AdvDVP_GetCapState(i) == STOPPED)
		{
			res = 1;
			res = pDVPSDK->AdvDVP_SetNewFrameCallback(i, (int)NewFrameCallback);			
	 		if (res !=  SUCCEEDED) 
			{
				return res;
			}
			//Start Video Capture 
			res = pDVPSDK->AdvDVP_Start(i,Switching-1,NULL,NULL); //Device Number, Number of Channels to switch between,
			//NULLs are for handles to windows - won't use these. 
			if (res !=  SUCCEEDED) 
			{
				return res;
			}			
		}			
		
	}
	lDstSize= nWidth[0]*nHeight[0]*3;	
	//Create a final buffer for all cameras
	pDstBuf = new BYTE[lDstSize*MAXDEVS*MAXMUXS];
	return res;
}
int StartEncoding()
{		
	int ptemp;
	for (int i = 0; i < MAXDEVS; i++)
	{
		ptemp = 0;
	    for(int j = 0; j < MAXMUXS; j++)
		{
			if (Pres[i][j])
				ptemp++;
		}
		Present[i] = ptemp;
	}	
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
	nFileIndex[nChNum] = FileStart;
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
		sprintf_s(FileName, "%s_%02d_%04d.avi", SaveName, CamAssoc[nChNum], nFileIndex[nChNum]);						
		hAVIFile[nChNum] = pDVPEncSDK->AdvDVP_CreateAVIFile(FileName, nWidth[nDevNum], nHeight[nDevNum], (int)(30/Present[nDevNum]));		
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






int __cdecl NewFrameCallback(int empty, int nID, int nDevNum, int nMuxChan, int nBufSize, BYTE* pBuf)
{	
	LastEncRes =nID;
	Pres[nDevNum][nMuxChan] = true;					
	if (*(pBuf+1) & 0x02) 		
	{
		NoSig = true;		
	}
	else 
	{
		NoSig = false;
		memcpy(P2Buff+nWidth[0]*nHeight[0]*2*(nDevNum*MAXMUXS+nMuxChan),pBuf,nBufSize);		
	}    
	int nChNum;
	
    if (nID != ID_NEW_FRAME)
	{
	  nChNum = (nDevNum*MAXMUXS)+nMuxChan;
	}
	if ((pDVPEncSDK))
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

	//Calculate the number of frames for each channels
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

int StopEncoding(void)
{
	for (int i=0; i<nDevCount*MAXMUXS; i++)
	{
		if (pDVPEncSDK->AdvDVP_GetState(i) == ENC_RUNNING)
			pDVPEncSDK->AdvDVP_StopVideoEncode(i);				
	}
	return 1;
}

int  CloseRecording(void)
{
		
		for (int i=0; i<nDevCount*MAXMUXS; i++)
		{			
				pDVPEncSDK->AdvDVP_CloseEncoder(i);
		}
		for (int i=0; i<nDevCount; i++)
		{
			if (pDVPSDK->AdvDVP_GetCapState(i) == RUNNING)
				pDVPSDK->AdvDVP_Stop(i);
		}
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



/*******************************************************
*
*
*				Setting Functions 
*
*
********************************************************/

void SetFName(LPSTR FName, int FStart)
{
	SaveName = new char[256];
	memcpy(SaveName,FName,256);
	FileStart = FStart;
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

int SetNTSC()
{
	for (int i = 0; i < nDevCount; i++)
	{
		pDVPSDK->AdvDVP_SetVideoFormat(i, Video_NTSC_M); //Force all to NTSC 
	}
	return 1;
}

int GetEncoderStatus()
{
	if (pDVPEncSDK)
		return pDVPEncSDK->AdvDVP_GetState(0);
	else
		return -3;	
}

int GetCaptureStatus()
{
	return pDVPSDK->AdvDVP_GetCapState(0);
}

int GetDeviceCount()
{
	return nDevCount;
}

void SetChanAssoc(int Chan, int Camera, bool Recording)
{
	CamAssoc[Camera] = Chan;
	CamIsRecording[Camera] = Recording;
}

int GetFrameRate()
{
	int Frate;
	for (int i = 0; i < nDevCount; i++)
	{
		pDVPSDK->AdvDVP_GetFrameRate(i, &Frate);
	}
	return Frate;
}

int SetFrameRate(int Frate)
{
	int res;
	Global_Frate = Frate;
	for (int i = 0; i < nDevCount; i++)
	{		
		res = pDVPSDK->AdvDVP_SetFrameRate(i, Switching-1, Frate);	
	}
	return res;
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



int SetCaptureRes(int XRes, int YRes)
{
	int res;
	for (int i = 0; i < nDevCount; i++) //Set Video res per device
	{
		switch(XRes) 
		{
			case 640:
				res = pDVPSDK->AdvDVP_SetResolution(i,SIZEVGA);
			break;
			case 320:
				res = pDVPSDK->AdvDVP_SetResolution(i,SIZEQVGA);
			break;
			case 160:
				res = pDVPSDK->AdvDVP_SetResolution(i,SIZESUBQVGA);
			break;
			default:
				res = pDVPSDK->AdvDVP_SetResolution(i,SIZEQVGA);
			break;
		}
	}
	for (int i=0; i<nDevCount; i++) //Set the capture resolution for all devices. C# rocks. 
	{
		nWidth[i] = XRes;
		nHeight[i] = YRes;
	}
	return res;
}

int SetEncoderRes()
{
  //Set the resolution per camera for encoding	
	for (int i=0; i<nDevCount*MAXMUXS; i++)
	{
		LastEncRes = pDVPEncSDK->AdvDVP_SetVideoResolution(i,nWidth[i],nHeight[i]);		
		if (LastEncRes != 1)
			return LastEncRes;
	} 
	return LastEncRes;
}
void SelectChannel(int Chan)
{	
	SelDevice = Chan / 4;
	SelChan = Chan % 4;
}

void SetSwitching(int Switch)
{
	Switching  = Switch;
}
