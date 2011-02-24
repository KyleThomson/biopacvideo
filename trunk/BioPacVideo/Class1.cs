using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

string[] MPRET = {
	"NULL", "SUCCESS", "DRIVERERROR", "DLLBUSY","INVALIDPARAM", "MP_NOT_CONNECTED","MPREADY","PRETRIGGER",
"TRIGGER","BUSY","NOACTIVECHANNELS","COMERROR", "INVTYPE", "NO_NETWORK_CONNECT", "OVERWROTESAMPLES","MEMERROR"
"SOCKETERROR","UNDERFLOW","PRESETERROR","XMLERROR" };


namespace BioPacVideo
{
    class MPTemplate
{
public
	BOOL isrecording;
	BOOL isconnected;
	ofstream *File;
	CString Filename;
	int MPReturn;
	BOOL RecordAC[16];	
	int SampleRate;
	int TotChan();		
	short GetActiveChan(int ChanNum);
	int SampleCount;	
	void Collect();
	double *buff;
	void CreateNewFile();
	void CloseFile();
	void WriteData(int numsamples);	
protected:
	DWORD WINAPI CollectThread(LPVOID);
};

