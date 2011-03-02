using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace BioPacVideo
{
    class AdvantechCodes
    {
        public enum tagRes
        {
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
        public enum CapState
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
        [DllImport(@".\VideoWrapper.dll")]
        public static extern int initSDK();

        [DllImport(@".\VideoWrapper.dll")]
        public static extern int StartSDK();        

        [DllImport(@".\VideoWrapper.dll")]
        public static extern int GetDeviceCount();


        [DllImport(@".\VideoWrapper.dll")]
        public static extern int SetVideoQuant(Int16 Quant);

        [DllImport(@".\VideoWrapper.dll")]
        public static extern int SetContrast(int Chan, int Contrast);

        [DllImport(@".\VideoWrapper.dll")]
        public static extern int SetBrightness(int Chan, int Contrast);

        [DllImport(@".\VideoWrapper.dll")]
        public static extern int SetHue(int Chan, int Contrast);

        [DllImport(@".\VideoWrapper.dll")]
        public static extern int SetSaturation(int Chan, int Contrast);      

   

        [DllImport(@".\VideoWrapper.dll")]
        public static extern int SetKeyInterval(int KeyInt);

        [DllImport(@".\VideoWrapper.dll")]
        public static extern int SetVideoRes(int XRes, int YRes);

        [DllImport(@".\VideoWrapper.dll")]
        public static extern int SetSampleRate(int Frate);

        [DllImport(@".\VideoWrapper.dll")]
        public static extern int CloseRecording();

        [DllImport(@".\VideoWrapper.dll")]
        public static extern int StartCapture();
        
        [DllImport(@".\VideoWrapper.dll")]
        public static extern int SetFName(StringBuilder FName, int FStart);
        //public static extern int SetFName([MarshalAs(UnmanagedType.LPStr)]string FName, int FStart);


    }
}
