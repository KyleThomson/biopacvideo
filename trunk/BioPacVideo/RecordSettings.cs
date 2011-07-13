using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BioPacVideo
{
    
    public partial class RecordSettings : Form
    {
        MPTemplate MP;   
        public static int[] SampleRateList = new int[] { 250, 500, 1000, 2000, 2500, 5000, 10000 };
        public static int[] DisplayLengthSize = new int[] { 1, 5, 10, 30, 60 };
        public static int[] VoltageSettings = new int[] { 1, 10, 50, 100, 250, 500, 1000, 2000 };
        public static int[] GainSettings = new int[] { 1000, 2000, 5000, 10000 };
        public static string[] ModeSettings = new string[] {"NORM", "ALPHA"};
        public static double[] LPFilterSettings = new double[] { .1, 1 };
        public static string[] HPFilterSettings = new string[] { "OFF", "ON" };
        public RecordSettings()
        {
            InitializeComponent();
            MP = MPTemplate.Instance; //Pull Instance from MP Template - So we only have a single instance in all code
            for (int i=0; i < SampleRateList.Length;i++)
            {
                ID_SRATE.Items.Add(string.Format("{0} Hz",SampleRateList[i]));
            }
            ID_SRATE.SelectedIndex = Array.IndexOf(SampleRateList, MP.SampleRate);
            for (int i = 0; i < GainSettings.Length; i++)
            {
                ID_GAIN.Items.Add(string.Format("{0}", GainSettings[i]));
            }
            ID_GAIN.SelectedIndex = Array.IndexOf(GainSettings, MP.Gain);
            for (int i = 0; i < DisplayLengthSize.Length; i++)
            {
                ID_DWS.Items.Add(string.Format("{0} seconds",DisplayLengthSize[i]));
            }
            ID_DWS.SelectedIndex = Array.IndexOf(DisplayLengthSize, MP.DisplayLength);
            for (int i = 0; i < VoltageSettings.Length; i++)
            {
                if (VoltageSettings[i] < 1000)
                    IDC_MINMAXV.Items.Add(string.Format("-{0} / {0} mV", VoltageSettings[i]));
                else
                    IDC_MINMAXV.Items.Add(string.Format("-{0} / {0} V",VoltageSettings[i]/1000));
            }
            IDC_MINMAXV.SelectedIndex = Array.IndexOf(VoltageSettings, MP.Voltage);

        }
         

        private void ID_OK_Click(object sender, EventArgs e)
        {
            MP.SampleRate = SampleRateList[ID_SRATE.SelectedIndex];
            MP.DisplayLength = DisplayLengthSize[ID_DWS.SelectedIndex];
            MP.Voltage = VoltageSettings[IDC_MINMAXV.SelectedIndex];
            MP.Gain = GainSettings[ID_GAIN.SelectedIndex];
            this.Close();
        }

        private void ID_GAIN_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        

    }
}
