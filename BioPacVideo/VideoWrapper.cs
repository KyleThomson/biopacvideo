using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using QCAP.NET.X64;

namespace BioPacVideo
{
    public class VideoWrapper
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string message);
        public Int32[] PanelHandles;
        static readonly VideoWrapper instance = new VideoWrapper();
        public uint maxdevices = 16;
        public uint i = 0;
        MPTemplate MP = MPTemplate.Instance; 
        public bool m_bIsMaximizedForm = false;

        public bool[] m_bIsMaximizedChannelWindow = new bool [16];
        
        public bool[] m_bNoSignal = new bool[16];

        public string[] m_strFormatChangedOutput = new string[16];

        public bool[] m_bShareRecordCH = new bool[16];

        public bool m_bShowClone = false;

        public bool m_bIsShareRecord = false;

        public uint m_nVideoWidth = 1920;

        public uint m_nVideoHeight = 1080;

        public double m_dVideoFrameRate = 60.0;

        public uint m_bVideoIsInterleaved = 0;

        public uint m_nAudioChannels = 2;

        public uint m_nAudioBitsPerSample = 16;

        public uint m_nAudioSampleFrequency = 48000;
        

        public int[] CameraAssociation;
        public bool Enabled;
        public int XRes;
        public int YRes;
        public int Res;
        public int Quality;
        public int Bitrate;
        public int KeyFrames;
        public int[] Brightness;
        public int[] Contrast;
        public int[] Hue;
        public int[] Saturation;
        public bool EncoderStarted;
        public string Filename;
        public int LengthWise;
        public MyChannelControl[] m_pChannelControl_LIVE = new MyChannelControl[16];

        public VideoWrapper()
        {
            CameraAssociation = new int[16];
            Brightness = new int[32];
            Hue = new int[32];
            Saturation = new int[32];
            Contrast = new int[32];
        }
        public static VideoWrapper Instance
        {
            get
            {
                return instance;
            }
        }

        // FOURCC MARCO
        //
        uint MAKEFOURCC(uint ch0, uint ch1, uint ch2, uint ch3)
        {
            return ((uint)(byte)(ch0) | ((uint)(byte)(ch1) << 8) | ((uint)(byte)(ch2) << 16) | ((uint)(byte)(ch3) << 24));
        }

        // CALLBACK FUNCTION
        //        
        public static EXPORTS.PF_FORMAT_CHANGED_CALLBACK m_pFormatChangedCB;

        public static EXPORTS.PF_NO_SIGNAL_DETECTED_CALLBACK m_pNoSignalDetectedCB;

        public static EXPORTS.PF_SIGNAL_REMOVED_CALLBACK m_pSignalRemovedCB;

        public static EXPORTS.PF_VIDEO_HARDWARE_ENCODER_CALLBACK m_pVideoHardwareEncoderCallback;

        public static EXPORTS.PF_VIDEO_PREVIEW_CALLBACK m_pPreviewVideoCB;

        public static EXPORTS.PF_AUDIO_PREVIEW_CALLBACK m_pPreviewAudioCB;

        //public MySetupControl m_cSetupControl = new MySetupControl();

        // LIVE PREVIEW CHANNEL WINDOW
        //
        //public MyChannelControl[] m_pChannelControl_LIVE = new MyChannelControl[4];

        string m_strChipName = "MZ0380 PCI";

        // DEVICE PROPERTY
        //
        public ulong[] m_hCapDev = new ulong[16];                         // STREAM CAPTURE DEVICE

        public ulong[] m_hCloneCapDev = new ulong[16];                // CLONE STREAM CAPTURE DEVICE
        public ulong m_hCloneCapDev_temp = new ulong(); 
        //  FORMAT CHANGED CALLBACK FUNCTION
        //
        EXPORTS.ReturnOfCallbackEnum on_process_format_changed(ulong pDevice, uint nVideoInput, uint nAudioInput, uint nVideoWidth, uint nVideoHeight, uint bVideoIsInterleaved, double dVideoFrameRate, uint nAudioChannels, uint nAudioBitsPerSample, uint nAudioSampleFrequency, ulong pUserData)
        {
            ulong nCH = pUserData;

            // OUTPUT FORMAT CHANGED MESSAGE
            //
            string strOutput = "CH" + (nCH + 1).ToString() + " -> FORMAT CHANGED : pDevice : " + pDevice.ToString() + " , " + "nVideoInput : " + nVideoInput.ToString() + " , " +

                                        "nAudioInput : " + nAudioInput.ToString() + " , " + "nVideoWidth : " + nVideoWidth.ToString() + " , " +

                                        "nVideoHeight : " + nVideoHeight.ToString() + " , " + "bVideoIsInterleaved : " + bVideoIsInterleaved.ToString() + " , " +

                                        "dVideoFrameRate : " + dVideoFrameRate.ToString() + " , " + "nAudioChannels : " + nAudioChannels.ToString() + " , " +

                                        "nAudioBitsPerSample : " + nAudioBitsPerSample.ToString() + " , " + "nAudioSampleFrequency : " + nAudioSampleFrequency.ToString() + " , " +

                                        "pUserData : " + pUserData.ToString() + " \n";

            OutputDebugString(strOutput);

            m_nVideoWidth = nVideoWidth;

            m_nVideoHeight = nVideoHeight;

            m_dVideoFrameRate = dVideoFrameRate;

            m_bVideoIsInterleaved = bVideoIsInterleaved;

            m_nAudioChannels = nAudioChannels;

            m_nAudioBitsPerSample = nAudioBitsPerSample;

            m_nAudioSampleFrequency = nAudioSampleFrequency;

            uint nVH = 0;

            string strFrameType = " P ";

            string strVideoInput = "", strAudioInput = "";

            if (nVideoInput == 0) { strVideoInput = "COMPOSITE"; }
            if (nVideoInput == 1) { strVideoInput = "SVIDEO"; }
            if (nVideoInput == 2) { strVideoInput = "HDMI"; }

            if (nVideoInput == 3) { strVideoInput = "DVI_D"; }
            if (nVideoInput == 4) { strVideoInput = "COMPONENTS (YCBCR)"; }
            if (nVideoInput == 5) { strVideoInput = "DVI_A (RGB / VGA)"; }

            if (nVideoInput == 6) { strVideoInput = "SDI"; }
            if (nVideoInput == 7) { strVideoInput = "AUTO"; }

            if (nAudioInput == 0) { strAudioInput = "EMBEDDED_AUDIO"; }
            if (nAudioInput == 1) { strAudioInput = "LINE_IN"; }

            if (bVideoIsInterleaved == 1) { nVH = nVideoHeight / 2; } else { nVH = nVideoHeight; }

            if (bVideoIsInterleaved == 1) { strFrameType = " I "; } else { strFrameType = " P "; }

            m_strFormatChangedOutput[nCH] = @" INFO : " + nVideoWidth.ToString() + " x " + nVH.ToString() + strFrameType + " @" + dVideoFrameRate.ToString() +

                " FPS , " + nAudioChannels.ToString() + " CH x " + nAudioBitsPerSample.ToString() + " BITS x " + nAudioSampleFrequency.ToString() + " HZ , " +

                " VIDEO INPUT : " + strVideoInput + " , " + " AUDIO INPUT : " + strAudioInput + " \n";

            // NO SIGNAL
            //       
            if (nVideoWidth == 0 && nVideoHeight == 0 && dVideoFrameRate == 0.0 && nAudioChannels == 0 && nAudioBitsPerSample == 0 && nAudioSampleFrequency == 0)
            {
                m_bNoSignal[nCH] = true;
            }
            else
            {
                m_bNoSignal[nCH] = false;
            }

            return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
        }

        // PREVIEW VIDEO CALLBACK FUNCTION
        //
        EXPORTS.ReturnOfCallbackEnum on_process_preview_video_buffer(ulong pDevice, double dSampleTime, ulong pFrameBuffer, uint nFrameBufferLen, ulong pUserData)
        {
            ulong nCH = pUserData;

            //string strOutput = "CH" + (nCH + 1).ToString() +  " : on_process_preview_video_buffer => pDevice : " + pDevice.ToString() + " , dSampleTime : " + dSampleTime.ToString() + " , pFrameBuffer : " + pFrameBuffer.ToString() + " , nFrameBufferLen : " + nFrameBufferLen.ToString() + " , pUserData : " + pUserData.ToString() + " \n";

            //OutputDebugString(strOutput);

            //if (m_bIsShareRecord && m_bShareRecordCH[nCH])
            //{
                EXPORTS.QCAP_SET_VIDEO_SHARE_RECORD_UNCOMPRESSION_BUFFER(0, MAKEFOURCC('Y', 'V', '1', '2'), m_nVideoWidth, m_nVideoHeight, pFrameBuffer, nFrameBufferLen);
            //}      

            return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
        }

        // VIDEO HARDWARE ENCODER CALLBACK FUNCTION
        //
        EXPORTS.ReturnOfCallbackEnum on_process_hardware_encoder_video_buffer(ulong pDevice, uint iRecNum, double dSampleTime, ulong pStreamBuffer, uint nStreamBufferLen, uint bIsKeyFrame, ulong pUserData)
        {
            ulong nCH = pUserData;

            return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
        }

        // PREVIEW AUDIO CALLBACK FUNCTION
        //

        // NO SIGNAL DETEACTED CALLBACK FUNCTION
        //
        EXPORTS.ReturnOfCallbackEnum on_process_no_signal_detected(ulong pDevice, uint nVideoInput, uint nAudioInput, ulong pUserData)
        {
            ulong nCH = pUserData;

            Console.WriteLine("CH" + (nCH + 1).ToString() + " No Signal Detected  \n");

            m_bNoSignal[nCH] = true;

            return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
        }

        // SIGNAL REMOVED CALLBACK FUNCTION
        //
        EXPORTS.ReturnOfCallbackEnum on_process_signal_removed(ulong pDevice, uint nVideoInput, uint nAudioInput, ulong pUserData)
        {
            ulong nCH = pUserData;

            Console.WriteLine("CH" + (nCH + 1).ToString() + " Signal Removed \n");

            m_bNoSignal[nCH] = true;

            return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
        }

   
       
            

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //timerCheckSignal.Enabled = false;

            EXPORTS.QCAP_STOP_SHARE_RECORD(0);

            HwUnInitialize();
        }

        private void SetupControlClosed(object sender, FormClosedEventArgs e)
        {
           
        }
        public void UpdateCameraAssoc()
        {
            for (int i = 0; i < maxdevices; i++)
            {
                if (m_hCloneCapDev[i] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[i]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[i]); }
            }
            ShowCloneVideo(true);
          }

        
        public void StopRecording()
        {
            for (i = 0; i < 16; i++)
            {
                EXPORTS.QCAP_STOP_RECORD(m_hCapDev[CameraAssociation[i]], 0);
            }
        }
        public bool HwInitialize()
        {
            for (i = 0; i < maxdevices; i++) { m_hCapDev[i] = 0x00000000; }

            for (i = 0; i < maxdevices; i++) { m_hCloneCapDev[i] = 0x00000000; }

            for (i = 0; i < maxdevices; i++) { m_bNoSignal[i] = true; }

            for (i = 0; i < maxdevices; i++) { m_strFormatChangedOutput[i] = ""; }

          
            // CREATE CAPTURE DEVICE            
            //
            for (i = 0; i < maxdevices; i++)
            {
                string str_chip_name = m_strChipName;
                m_pChannelControl_LIVE[i] = new MyChannelControl();
                try
                {
                    EXPORTS.QCAP_CREATE(ref str_chip_name, i, (uint)m_pChannelControl_LIVE[i].Handle.ToInt32(), ref m_hCapDev[i], 1);
                }
                catch
                {
                    MessageBox.Show("Wrong number of capture devices set.\nIf this occurred randomly, one of your capture cards has gone bad.\nReduce the number of Capture Cards in Video->Settings and restart the program.");
                }

                
                EXPORTS.QCAP_SET_DEVICE_CUSTOM_PROPERTY(m_hCapDev[i], 273, 0);
            }        
            // REGISTER FORMAT CHANGED CALLBACK FUNCTION
            // 
            m_pFormatChangedCB = new EXPORTS.PF_FORMAT_CHANGED_CALLBACK(on_process_format_changed);
            for (i = 0; i < maxdevices; i++)
            {
                EXPORTS.QCAP_REGISTER_FORMAT_CHANGED_CALLBACK(m_hCapDev[i], m_pFormatChangedCB, i);
            }
            // REGISTER PREVIEW VIDEO CALLBACK FUNCTION
            // 
            m_pPreviewVideoCB = new EXPORTS.PF_VIDEO_PREVIEW_CALLBACK(on_process_preview_video_buffer);
            for (i = 0; i < maxdevices; i++)
            {
                EXPORTS.QCAP_REGISTER_VIDEO_PREVIEW_CALLBACK(m_hCapDev[i], m_pPreviewVideoCB, i);
            }
            // REGISTER VIDEO HARDWARE ENCODER CALLBACK FUNCTION
            //
            m_pVideoHardwareEncoderCallback = new EXPORTS.PF_VIDEO_HARDWARE_ENCODER_CALLBACK(on_process_hardware_encoder_video_buffer);

            for (i = 0; i < maxdevices; i++)
            {
                EXPORTS.QCAP_REGISTER_VIDEO_HARDWARE_ENCODER_CALLBACK(m_hCapDev[i], 0, m_pVideoHardwareEncoderCallback,i%4);
            }

            
            // REGISTER NO SIGNAL DETECTED CALLBACK FUNCTION
            //
            m_pNoSignalDetectedCB = new EXPORTS.PF_NO_SIGNAL_DETECTED_CALLBACK(on_process_no_signal_detected);

            for (i = 0; i < maxdevices; i++)
            {
                EXPORTS.QCAP_REGISTER_NO_SIGNAL_DETECTED_CALLBACK(m_hCapDev[i], m_pNoSignalDetectedCB, i);
            }
            
            // REGISTER SIGNAL REMOVED CALLBACK FUNCTION
            //
            m_pSignalRemovedCB = new EXPORTS.PF_SIGNAL_REMOVED_CALLBACK(on_process_signal_removed);
            for (i = 0; i < maxdevices; i++)
            {
                EXPORTS.QCAP_REGISTER_SIGNAL_REMOVED_CALLBACK(m_hCapDev[i], m_pSignalRemovedCB, i);
            }
            
            // SET INPUT
            //
            uint nInput = (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_SDI;
            for (i = 0; i < maxdevices; i++)
            {
                EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev[i], nInput);
            }
            for (i = 0; i < maxdevices; i++)
            {
                EXPORTS.QCAP_SET_VIDEO_DEINTERLACE(m_hCapDev[i], 0);
            }
            for (i = 0; i < maxdevices; i++)
            {
                EXPORTS.QCAP_SET_AUDIO_RECORD_PROPERTY(m_hCapDev[i], 0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.AudioEncoderFormatEnum.QCAP_ENCODER_FORMAT_AAC);
                EXPORTS.QCAP_SET_VIDEO_HARDWARE_ENCODER_PROPERTY(m_hCapDev[i], 0, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_VBR, (uint)Quality,  (uint)Bitrate * 1024 * 1024, 30, 4, 3, (uint)EXPORTS.DownScaleModeEnum.QCAP_DOWNSCALE_MODE_OFF, 0, 0);
            }

            for (i = 0; i < maxdevices; i++)
            {
                EXPORTS.QCAP_RUN(m_hCapDev[i]);
            }
          //  timerCheckSignal.Enabled = true;  

            return true;
        }
        public void StartRecording()
        {
            int i;
            string pszNULL = null;
            string FN;

            for (i = 0; i < 16; i++)
            {
                if ((MP.RecordAC[i]) && (CameraAssociation[i] < maxdevices))
                {
                    FN = Filename + "_" + i.ToString("D3") + ".mp4"; //JOSH  
                    //FN = Filename + "_" + CameraAssociation[i].ToString("D3") + ".mp4"; I think this makes it makes sense for FixedChan = CamerAssc[CamerAssc[ACQ.SelectedChan]];
                    EXPORTS.QCAP_SET_VIDEO_RECORD_PROPERTY(m_hCapDev[CameraAssociation[i]], 0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_HARDWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_VBR, (uint)Quality, (uint)Bitrate*1024*1024, 30, 4, 3, (uint)EXPORTS.DownScaleModeEnum.QCAP_DOWNSCALE_MODE_OFF);
                    EXPORTS.QCAP_START_RECORD(m_hCapDev[CameraAssociation[i]], 0, ref FN, (uint)EXPORTS.RecordFlagEnum.QCAP_RECORD_FLAG_FULL, 0.0, 0.0, 0.0, 256000, ref pszNULL);
                }
            }

        }
        public bool HwUnInitialize()
        {
            for (int i = 0; i < maxdevices; i++)
            {
                if (m_hCloneCapDev[i] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[i]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[i]); }
                if (m_hCapDev[i] != 0) { EXPORTS.QCAP_STOP(m_hCapDev[i]); EXPORTS.QCAP_DESTROY(m_hCapDev[i]); }
            }
            return true;
        }

        public void ShowCloneVideo(bool bShow)
        {
            int i;
            if (bShow)
            {
                m_bShowClone = true;
                for (i = 0; i < maxdevices; i++)
                {
                    if (m_hCapDev[CameraAssociation[i]] != 0)
                    {
                        EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[CameraAssociation[i]], (uint)PanelHandles[i], ref m_hCloneCapDev[CameraAssociation[i]], 1);

                        if (m_hCloneCapDev[CameraAssociation[i]] != 0)
                        {                            
                            EXPORTS.QCAP_RUN(m_hCloneCapDev[CameraAssociation[i]]);

                        }
                    }
                }
            }
                
        }
        public void TempCloneVideo(int channel, Int32 panelhandlet)
        {
            
            EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[channel], (uint)panelhandlet, ref m_hCloneCapDev_temp, 1);

            if (m_hCloneCapDev_temp != 0)
            {
                EXPORTS.QCAP_RUN(m_hCloneCapDev_temp);

            }
        }
        public void DestroyTempCloneVideo()
        {
            EXPORTS.QCAP_STOP(m_hCloneCapDev_temp);
            EXPORTS.QCAP_DESTROY(m_hCloneCapDev_temp);
        }

        public void OnLButtonDown_ChannelControl(uint nChannelNumber)
        {
            /*if (m_bIsMaximizedChannelWindow[nChannelNumber - 1])
            {
                m_bIsMaximizedChannelWindow[nChannelNumber - 1] = false;

                for (i = 0; i < 4; i++)
                {
                    // LEFT POSITION
                    //
                    if (i == 0) { m_pChannelControl_LIVE[i].Left = 0; }

                    if (i == 1) { m_pChannelControl_LIVE[i].Left = this.Width / 2; }

                    if (i == 2) { m_pChannelControl_LIVE[i].Left = 0; }

                    if (i == 3) { m_pChannelControl_LIVE[i].Left = this.Width / 2; }

                    // TOP POSITION
                    //
                    if (i == 0) { m_pChannelControl_LIVE[i].Top = 0; }

                    if (i == 1) { m_pChannelControl_LIVE[i].Top = 0; }

                    if (i == 2) { m_pChannelControl_LIVE[i].Top = this.Height / 2; }

                    if (i == 3) { m_pChannelControl_LIVE[i].Top = this.Height / 2; }

                    m_pChannelControl_LIVE[i].Size = new System.Drawing.Size(this.Width / 2, this.Height / 2);

                    m_pChannelControl_LIVE[i].Visible = true;
                }

                EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCapDev[0], 0);

                EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCapDev[1], 0);

                EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCapDev[2], 0);

                EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCapDev[3], 0);

            /*    CloneChannelPanel1.Left = 0; CloneChannelPanel1.Top = 0; CloneChannelPanel1.Width = 160; CloneChannelPanel1.Height = 120; CloneChannelPanel1.Visible = m_bShowClone;

                CloneChannelPanel2.Left = this.Width / 2; CloneChannelPanel2.Top = 0; CloneChannelPanel2.Width = 160; CloneChannelPanel2.Height = 120; CloneChannelPanel2.Visible = m_bShowClone;

                CloneChannelPanel3.Left = 0; CloneChannelPanel3.Top = this.Height / 2; CloneChannelPanel3.Width = 160; CloneChannelPanel3.Height = 120; CloneChannelPanel3.Visible = m_bShowClone;

                CloneChannelPanel4.Left = this.Width / 2; CloneChannelPanel4.Top = this.Height / 2; CloneChannelPanel4.Width = 160; CloneChannelPanel4.Height = 120; CloneChannelPanel4.Visible = m_bShowClone;
            }
            else
            {
                m_bIsMaximizedChannelWindow[nChannelNumber - 1] = true;

                for (i = 0; i < 4; i++) { m_pChannelControl_LIVE[i].Visible = false; }

                CloneChannelPanel1.Visible = false; CloneChannelPanel2.Visible = false; CloneChannelPanel3.Visible = false; CloneChannelPanel4.Visible = false;

                m_pChannelControl_LIVE[nChannelNumber - 1].Left = 0;

                m_pChannelControl_LIVE[nChannelNumber - 1].Top = 0;

                m_pChannelControl_LIVE[nChannelNumber - 1].Size = new System.Drawing.Size(this.Width, this.Height);

                m_pChannelControl_LIVE[nChannelNumber - 1].Visible = true;

                EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCapDev[nChannelNumber - 1], 100);

                if (nChannelNumber == 1) { CloneChannelPanel1.Left = 0; CloneChannelPanel1.Top = 0; CloneChannelPanel1.Width = 160; CloneChannelPanel1.Height = 120; CloneChannelPanel1.Visible = m_bShowClone; }

                if (nChannelNumber == 2) { CloneChannelPanel2.Left = this.Width / 2; CloneChannelPanel2.Top = 0; CloneChannelPanel2.Width = 160; CloneChannelPanel2.Height = 120; CloneChannelPanel2.Visible = m_bShowClone; }

                if (nChannelNumber == 3) { CloneChannelPanel3.Left = 0; CloneChannelPanel3.Top = this.Height / 2; CloneChannelPanel3.Width = 160; CloneChannelPanel3.Height = 120; CloneChannelPanel3.Visible = m_bShowClone; }

                if (nChannelNumber == 4) { CloneChannelPanel4.Left = this.Width / 2; CloneChannelPanel4.Top = this.Height / 2; CloneChannelPanel4.Width = 160; CloneChannelPanel4.Height = 120; CloneChannelPanel4.Visible = m_bShowClone; }
            }*/
        }

        public void OnRButtonDown_ChannelControl(uint nChannelNumber)
        {
            // CHANGE CHANNEL WINDOWS SIZE AND POSITION
            //
            /*
            if (!m_bIsMaximizedForm)
            {
                this.WindowState = FormWindowState.Maximized;

                m_bIsMaximizedForm = true;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;

                m_bIsMaximizedForm = false;
            }

            if (m_bIsMaximizedChannelWindow[nChannelNumber - 1])
            {
                m_pChannelControl_LIVE[nChannelNumber - 1].Left = 0;

                m_pChannelControl_LIVE[nChannelNumber - 1].Top = 0;

                m_pChannelControl_LIVE[nChannelNumber - 1].Size = new System.Drawing.Size(this.Width, this.Height);

                m_pChannelControl_LIVE[nChannelNumber - 1].Visible = true;

                if (nChannelNumber == 1) { CloneChannelPanel1.Left = this.Width - 320; CloneChannelPanel1.Top = this.Height - 240; CloneChannelPanel1.Width = 320; CloneChannelPanel1.Height = 240; CloneChannelPanel1.Visible = m_bShowClone; }

                if (nChannelNumber == 2) { CloneChannelPanel2.Left = this.Width - 320; CloneChannelPanel2.Top = this.Height - 240; CloneChannelPanel2.Width = 320; CloneChannelPanel2.Height = 240; CloneChannelPanel2.Visible = m_bShowClone; }

                if (nChannelNumber == 3) { CloneChannelPanel3.Left = this.Width - 320; CloneChannelPanel3.Top = this.Height - 240; CloneChannelPanel3.Width = 320; CloneChannelPanel3.Height = 240; CloneChannelPanel3.Visible = m_bShowClone; }

                if (nChannelNumber == 4) { CloneChannelPanel4.Left = this.Width - 320; CloneChannelPanel4.Top = this.Height - 240; CloneChannelPanel4.Width = 320; CloneChannelPanel4.Height = 240; CloneChannelPanel4.Visible = m_bShowClone; }
            }
            else
            {
                for (i = 0; i < 4; i++)
                {
                    // LEFT POSITION
                    //
                    if (i == 0) { m_pChannelControl_LIVE[i].Left = 0; }

                    if (i == 1) { m_pChannelControl_LIVE[i].Left = this.Width / 2; }

                    if (i == 2) { m_pChannelControl_LIVE[i].Left = 0; }

                    if (i == 3) { m_pChannelControl_LIVE[i].Left = this.Width / 2; }

                    // TOP POSITION
                    //
                    if (i == 0) { m_pChannelControl_LIVE[i].Top = 0; }

                    if (i == 1) { m_pChannelControl_LIVE[i].Top = 0; }

                    if (i == 2) { m_pChannelControl_LIVE[i].Top = this.Height / 2; }

                    if (i == 3) { m_pChannelControl_LIVE[i].Top = this.Height / 2; }

                    m_pChannelControl_LIVE[i].Size = new System.Drawing.Size(this.Width / 2, this.Height / 2);

                    m_pChannelControl_LIVE[i].Visible = true;
                }

                CloneChannelPanel1.Left = 0; CloneChannelPanel1.Top = 0; CloneChannelPanel1.Width = 160; CloneChannelPanel1.Height = 120; CloneChannelPanel1.Visible = m_bShowClone;

                CloneChannelPanel2.Left = this.Width / 2; CloneChannelPanel2.Top = 0; CloneChannelPanel2.Width = 160; CloneChannelPanel2.Height = 120; CloneChannelPanel2.Visible = m_bShowClone;

                CloneChannelPanel3.Left = 0; CloneChannelPanel3.Top = this.Height / 2; CloneChannelPanel3.Width = 160; CloneChannelPanel3.Height = 120; CloneChannelPanel3.Visible = m_bShowClone;

                CloneChannelPanel4.Left = this.Width / 2; CloneChannelPanel4.Top = this.Height / 2; CloneChannelPanel4.Width = 160; CloneChannelPanel4.Height = 120; CloneChannelPanel4.Visible = m_bShowClone;
            }*/
        }       

    }
}
