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
    public partial class RecordSelect : Form
    {
        #region Lifecylce
        public RecordSelect(bool[] RecordAC, bool[] RecordingDevice, int RecordingDeviceAll)
        {
            InitializeComponent();
            ChannelAcq1.Checked = RecordAC[0];
            ChannelAcq2.Checked = RecordAC[1];
            ChannelAcq3.Checked = RecordAC[2];
            ChannelAcq4.Checked = RecordAC[3];
            ChannelAcq5.Checked = RecordAC[4];
            ChannelAcq6.Checked = RecordAC[5];
            ChannelAcq7.Checked = RecordAC[6];
            ChannelAcq8.Checked = RecordAC[7];
            ChannelAcq9.Checked = RecordAC[8];
            ChannelAcq10.Checked = RecordAC[9];
            ChannelAcq11.Checked = RecordAC[10];
            ChannelAcq12.Checked = RecordAC[11];
            ChannelAcq13.Checked = RecordAC[12];
            ChannelAcq14.Checked = RecordAC[13];
            ChannelAcq15.Checked = RecordAC[14];
            ChannelAcq16.Checked = RecordAC[15];
<<<<<<< HEAD
            Telemetry.Checked = RecordingDevice[0];
            recordingDeviceBox.Items.Add("Rat Telemetry");
            recordingDeviceBox.Items.Add("Mouse Telemetry");
            recordingDeviceBox.Items.Add("Tethered"); 
            recordingDeviceBox.SelectedIndex = RecordingDeviceAll; 
            /*TelemetryCh2.Checked = RecordingDevice[1];
            TelemetryCh3.Checked = RecordingDevice[2];
            TelemetryCh4.Checked = RecordingDevice[3];
            TelemetryCh5.Checked = RecordingDevice[4];
            TelemetryCh6.Checked = RecordingDevice[5];
            TelemetryCh7.Checked = RecordingDevice[6];
            TelemetryCh8.Checked = RecordingDevice[7];
            TelemetryCh9.Checked = RecordingDevice[8];
            TelemetryCh10.Checked = RecordingDevice[9];
            TelemetryCh11.Checked = RecordingDevice[10];
            TelemetryCh12.Checked = RecordingDevice[11];
            TelemetryCh13.Checked = RecordingDevice[12];
            TelemetryCh14.Checked = RecordingDevice[13];
            TelemetryCh15.Checked = RecordingDevice[14];
            TelemetryCh16.Checked = RecordingDevice[15];*/
=======
            TelemetryCheck.Checked = RecordingDevice[0];

>>>>>>> master
        }
        #endregion

        #region Checkbox Lists
        /// <summary>
        /// Checks wheater a channel is enabled or disabled
        /// </summary>
        /// <returns>Returns a Boolean array of all recording channels in order up to 16 and weather they are enabled or not</returns>
        public bool[] AC()
        {
            bool[] allchan = new bool[] {ChannelAcq1.Checked, ChannelAcq2.Checked, ChannelAcq3.Checked,ChannelAcq4.Checked,
                ChannelAcq5.Checked,ChannelAcq6.Checked,ChannelAcq7.Checked,ChannelAcq8.Checked,
            ChannelAcq9.Checked,ChannelAcq10.Checked,ChannelAcq11.Checked,ChannelAcq12.Checked,
            ChannelAcq13.Checked,ChannelAcq14.Checked,ChannelAcq15.Checked,ChannelAcq16.Checked};

            return allchan;
        }

        /// <summary>
        /// Gets the status of Telemetry Checked
        /// </summary>
        /// <returns>A Boolean array of weather Telemetry is in use on the given cameras in order for all 16 channels</returns>
        public bool[] RC()
        {
<<<<<<< HEAD
            bool[] allchan = new bool[16]; 
            for (int i=0; i<16; i++)
            {
                allchan[i] = Telemetry.Checked; 
                
                /*TelemetryCh2.Checked, TelemetryCh3.Checked, TelemetryCh4.Checked, TelemetryCh5.Checked, 
                TelemetryCh6.Checked, TelemetryCh7.Checked, TelemetryCh8.Checked, TelemetryCh9.Checked, TelemetryCh10.Checked, TelemetryCh11.Checked, 
                TelemetryCh12.Checked, TelemetryCh13.Checked, TelemetryCh14.Checked, TelemetryCh15.Checked, TelemetryCh16.Checked};*/
            }

=======
            bool[] allchan = new bool[] {TelemetryCheck.Checked};
>>>>>>> master
            return allchan;
        }

        /// <summary>
        /// Gets the current recording device
        /// </summary>
        /// <returns>The current recording device as an integer</returns>
        public int RD()
        {
            int RecDevice = new int();
            RecDevice = recordingDeviceBox.SelectedIndex;

            return RecDevice; 
        }
        #endregion

        #region Input Handlers
        private void ID_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
<<<<<<< HEAD
        #endregion
=======
>>>>>>> master
    }
}
