using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioPacVideo
{
    class VideoTemplate
    {
        public static string[] Res_text = new string[] { "INPUTERROR", "DEVICENUMERROR", "NOSAMPLE", "NODEVICES", "PARAMERROR", "SDKINITFAILED", 
            "FAILED", "SUCCCEEDED","DLL FAILED TO LOAD","FAILED TO CALL"};
        public AdvantechCodes.tagRes Res;
        static readonly VideoTemplate instance = new VideoTemplate(); //Create Constant instance.        
        public int Device_Count;
        public int XRes;
        public int YRes;
        public int KeyFrames;
        public int Quant;
        public bool SDK_Running; 
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
            SDK_Running = false;
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
            Res =  (AdvantechCodes.tagRes) VideoWrapper.initSDK();
            Res = (AdvantechCodes.tagRes) VideoWrapper.StartSDK();
            Device_Count = VideoWrapper.GetDeviceCount();
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
    }
}
