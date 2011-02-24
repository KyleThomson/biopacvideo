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
        public RecordSelect(bool[] RecordAC)
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
        }
        public bool[] AC()
        {
            bool[] allchan = new bool[] {ChannelAcq1.Checked, ChannelAcq2.Checked, ChannelAcq3.Checked,ChannelAcq4.Checked,
                ChannelAcq5.Checked,ChannelAcq6.Checked,ChannelAcq7.Checked,ChannelAcq8.Checked,
            ChannelAcq9.Checked,ChannelAcq10.Checked,ChannelAcq11.Checked,ChannelAcq12.Checked,
            ChannelAcq13.Checked,ChannelAcq14.Checked,ChannelAcq15.Checked,ChannelAcq16.Checked};
            return allchan;
        }
        private void ID_OK_Click(object sender, EventArgs e)
        {
            this.Close();             
        }
    }
}
