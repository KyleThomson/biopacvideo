using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace BioPacVideo
{
    class AdvantechCodes
    {        
        public enum tagRes //Last Capture Result from the video card, with a few added
        {
            CALLBACK_RUN = 5,
            NO_VIDEO_PRESENT = 4,
            CALL_FAIL = 3,
            DLL_FAIL = 2,
            SUCCEEDED = 1,
            FAILED = 0,
            SDKINITFAILED = -1,
            PARAMERROR = -2,
            NODEVICES = -3,
            NOSAMPLE = -4,
            DEVICENUMERROR = -5,
            INPUTERROR = -6,
        }
        public enum CapState //Current Status of Capture
        {
            STOPPED = 1,
            RUNNING = 2,
            UNINITIALIZED = -1,
            UNKNOWNSTATE = -2
        }

        public enum VideoSize 
        {
            FULLPAL = 0, // (PAL: 768x576)
            SIZED1, // (NTSC: 720x480, PAL: 720x576)
            SIZEVGA, // (640x480)
            SIZEQVGA, // (320x240)
            SIZESUBQVGA // (160x120)
        }
    }
    class VideoWrapper
    {
        //Imports from DLL, see DLL for definition. Also, CDECL is the needed calling convention so callbacks work. 
        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int initCaptureSDK();

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartCaptureSDK();

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int initEncoderSDK();

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartEncoderSDK();     

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetDeviceCount();

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetVideoQuant(int Quant);

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetContrast(int Chan, int Contrast);

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetBrightness(int Chan, int Contrast);

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetHue(int Chan, int Contrast);

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetSaturation(int Chan, int Contrast);
 
        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetKeyInterval(int KeyInt);

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetCaptureRes(int XRes, int YRes);

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetEncoderRes();


        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetFrameRate(int Frate);

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetFrameRate();

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CloseRecording();

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartCapture();

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetCurrentBuffer(); //Sends the pointer from DLL to a buffer of data

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetNTSC(); //Sets all devices to NTSC

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SelectChannel(int Chan); //Selects Channel for output
        
        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetSnapShot(int Chan);

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetFName([MarshalAs(UnmanagedType.LPStr)]string FName, int FStart);

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEncoderStatus();

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetCaptureStatus();

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEncRes();

        [DllImport(@".\VideoWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartEncoding();       
      
    }
}
