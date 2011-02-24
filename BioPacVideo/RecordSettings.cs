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
        public static int[] SampleRateList = new int[] { 250, 500, 1000, 2000, 2500, 5000, 10000 };
        public static int[] DisplayLengthSize = new int[] { 1, 5, 10, 30, 60 };
        public static int[] VoltageSettings = new int[] { 1, 10, 50, 100, 250, 500, 1000, 2000 };
        public RecordSettings(int SR, int DL, int VL)
        {
            InitializeComponent();
            for (int i=0; i < SampleRateList.Length;i++)
            {
                ID_SRATE.Items.Add(string.Format("{0} Hz",SampleRateList[i]));
            }
            ID_SRATE.SelectedIndex = Array.IndexOf(SampleRateList, SR);
            for (int i = 0; i < DisplayLengthSize.Length; i++)
            {
                ID_DWS.Items.Add(string.Format("{0} seconds",DisplayLengthSize[i]));
            }
            ID_DWS.SelectedIndex = Array.IndexOf(DisplayLengthSize, DL);
            for (int i = 0; i < VoltageSettings.Length; i++)
            {
                if (VoltageSettings[i] < 1000)
                    IDC_MINMAXV.Items.Add(string.Format("-{0} / {0} mV", VoltageSettings[i]));
                else
                    IDC_MINMAXV.Items.Add(string.Format("-{0} / {0} V",VoltageSettings[i]/1000));
            }
            IDC_MINMAXV.SelectedIndex = Array.IndexOf(VoltageSettings, VL);

        }
        public int SampleRate()
        {
            return SampleRateList[ID_SRATE.SelectedIndex];
        }
        public int DisplayLength()
        {
            return DisplayLengthSize[ID_DWS.SelectedIndex];
        }
        public int Voltage()
        {
            return VoltageSettings[IDC_MINMAXV.SelectedIndex];
        }

        private void ID_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

    }
}
