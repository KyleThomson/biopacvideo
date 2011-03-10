using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;

namespace BioPacVideo
{
    class VideoTemplate
    {
        public static string[] Res_text = new string[] { "INPUTERROR", "DEVICENUMERROR", "NOSAMPLE", "NODEVICES", "PARAMERROR", "SDKINITFAILED", 
            "FAILED", "SUCCCEEDED","DLL FAILED TO LOAD","FAILED TO CALL","NO VIDEO PRESENT","CALLBACK RUN"};
        public AdvantechCodes.tagRes Res;
        static readonly VideoTemplate instance = new VideoTemplate(); //Create Constant instance.        
        public int Device_Count;
        public delegate int Funky(int lParam, int nID, int nDevNum, int nMuxChan, int nBufSize, IntPtr pBuf);
        public int XRes;
        public int YRes;
        public int KeyFrames;
        public String FileName;
        public int FileStart;
        public int Quant;        
        public int[] Contrast;
        public int[] Brightness;
        public int[] Hue;
        public bool CapSDKStatus;
        public bool EncSDKStatus;
        public int[] Saturation; 
        public VideoTemplate()
        {
            Contrast = new int[16];
            Brightness = new int[16];
            Hue = new int[16];
            Saturation = new int[16];
            CapSDKStatus = false;
            EncSDKStatus = false;

        }
        public static VideoTemplate Instance
        {
            get
            {
                return instance;
            }
        }
        public void initVideo()
        {
            Funky X = new Funky(VideoWrapper.NewFrameCallback);
            IntPtr C = Marshal.GetFunctionPointerForDelegate(X);
            Res =  (AdvantechCodes.tagRes) VideoWrapper.initSDK();
            Res = (AdvantechCodes.tagRes) VideoWrapper.StartSDK();
            Device_Count = VideoWrapper.GetDeviceCount();           
            VideoWrapper.SetNTSC();
            VideoWrapper.SetVideoRes(XRes, YRes);
            int SRate = 30;
            VideoWrapper.SetFrameRate(SRate);   
            Res = (AdvantechCodes.tagRes)VideoWrapper.StartCapture(C);
            MessageBox.Show(C.ToString() + "   " + VideoWrapper.GetEncRes().ToString()+ "   " + X.ToString());
  

        }
             
        public Bitmap GetSnap()
        {
            //AdvantechCodes.BITMAPINFOHEADER Header;
            Bitmap BMP = new Bitmap("NoSignal.Bmp");
            IntPtr pDF = new IntPtr();    
            pDF = VideoWrapper.GetSnapShot(0);
            if (pDF != null)
            {
                 BMP = new Bitmap(XRes, YRes, XRes*3, PixelFormat.Format24bppRgb, pDF);
                
            }
            return BMP;
            

        }
        public string GetResText()
        {
            return Res_text[(int)Res + 6];
        }
        public void SetSensorControls(int Chan)
        {
            VideoWrapper.SetContrast(Chan, Contrast[Chan]);
            VideoWrapper.SetBrightness(Chan, Brightness[Chan]);
            VideoWrapper.SetHue(Chan, Hue[Chan]);
            VideoWrapper.SetSaturation(Chan, Saturation[Chan]);
        }
        public void SetFileName()
        {
            StringBuilder X = new StringBuilder(256);
            X.Append(FileName);
            VideoWrapper.SetFName(X, FileStart);
        }
        public void StartRecording()
        {            
            //VideoWrapper.StartEncoding();            
        }

        public string EncoderStatus()
        {
            switch (VideoWrapper.GetEncoderStatus())
            {             
                case 1:
                    return "ENCODER STOPPED";                    
                case 2:
                    return "ENCODER RUNNING";                    
                case -1:
                    return "UNINITIALIZED";                   
                default:
                    return "UNKNOWN ERROR";                    
            }
        }
        public string CaptureStatus()
        {
            switch (VideoWrapper.GetCaptureStatus())
            {
                case 2:
                    return "CAPTURE RUNNING";
                case 1:
                    return "CAPTURE STOPPED";
                case -1:
                    return "CAPTURE FAIL";
                default:
                    return "UNKNOWN";
            }
        }
        public string EncoderResult()
        {
            switch (VideoWrapper.GetEncRes())
            {
                case 5:
                    return "CALLBACK RUN";
                case 1:
                    return "ENCODER SUCCEEDED";
                case 0:
                    return "ENCODER FAILED";
                case -1:
                    return "SDK INIT FAILED";
                case -2:
                    return "ENCODER INIT FAILED";
                case -3:
                    return "ENCODER PARAMETER ERROR";
                case -4:
                    return "ENOCDER NUM ERROR";
                case -5:
                    return "ENCODER BUFFER FULL";
                case -6:
                    return "ENCODE OF I-FRAME FAILED";
                case -7:
                    return "ENCODE OF P-FRAME FAILED";
                default:
                    return "UNKNOWN STATUS";
            }
        }


        public void LoadSettings()
        {            
            //VideoWrapper.SetVideoQuant(Quant);
            //VideoWrapper.SetKeyInterval(KeyFrames);            
                 
        }

        public void StopRecording()
        {
          //  VideoWrapper.CloseRecording();
        }
    }
}
