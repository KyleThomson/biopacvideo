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
        public IntPtr pDF;
        public bool CapSDKStatus;
        public bool EncSDKStatus;
        public int XRes;
        public int YRes;        
        public String FileName;
        public int FileStart;
        public int Quant;
        public int KeyFrames;
        public int[] Contrast;
        public int[] Brightness;
        public int[] Hue;       
        public int[] Saturation; 


        public VideoTemplate()
        {
            Contrast = new int[16];
            Brightness = new int[16];
            Hue = new int[16];
            Saturation = new int[16];
            CapSDKStatus = false;
            EncSDKStatus = false;
            pDF = new IntPtr();

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
            Res =  (AdvantechCodes.tagRes) VideoWrapper.initCaptureSDK();
            Res = (AdvantechCodes.tagRes) VideoWrapper.StartCaptureSDK();
            Device_Count = VideoWrapper.GetDeviceCount();           
            VideoWrapper.SetNTSC();
            VideoWrapper.SetCaptureRes(XRes, YRes);
            int SRate = 30;
            VideoWrapper.SetFrameRate(SRate);   
            Res = (AdvantechCodes.tagRes)VideoWrapper.StartCapture();
            pDF = VideoWrapper.GetCurrentBuffer();
            //MessageBox.Show(VideoWrapper.GetEncRes().ToString());
        }
        public void initEncoder()
        {
            VideoWrapper.initEncoderSDK();
            VideoWrapper.StartEncoderSDK();
        }      
             
        public Bitmap GetSnap()
        {
            //AdvantechCodes.BITMAPINFOHEADER Header;
            Bitmap BMP = new Bitmap("NoSignal.Bmp");              
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
        public void SetFileName(string FName)
        {                        
            VideoWrapper.SetFName(FName, 0);
        }
        public void StartRecording()
        {            
            VideoWrapper.StartEncoding();            
        }
        public void SelectChannel(int Chan)
        {
            VideoWrapper.SelectChannel(Chan);
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
            VideoWrapper.SetVideoQuant(Quant);
            VideoWrapper.SetKeyInterval(KeyFrames);            
                 
        }

        public void StopRecording()
        {
          VideoWrapper.CloseRecording();
        }
    }
}
