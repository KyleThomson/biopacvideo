using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Threading;

namespace BioPacVideo
{
    public partial class CameraAssc : Form
    {
        int Cam;
        IntPtr pDF;
        private VideoWrapper Video;        
        Graphics g;
        public CameraAssc()
        {
            
            InitializeComponent();
            Video = VideoWrapper.Instance;
            g = this.CreateGraphics();
            Cam = 0;
  
            for (int i = 0; i < 16; i++)
                ChanSel.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < Video.maxdevices; i++)
                CamSel.Items.Add("Camera " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                CABox.Text += "Channel " + (i + 1).ToString() + " - Camera " + (Video.CameraAssociation[i]+1).ToString() + Environment.NewLine;
            ChanSel.SelectedIndex = 0;
            CamSel.SelectedIndex = Video.CameraAssociation[0];
            

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Video.DestroyTempCloneVideo();
            Cam = CamSel.SelectedIndex;
            Video.TempCloneVideo(Cam, CamPanel.Handle.ToInt32());
        }
        

        private void SetButton_Click(object sender, EventArgs e)
        {
            int Temp1, Temp2;
            Temp1 = 0; 
            for (int i = 0; i < 16; i++)
            {
                if (Video.CameraAssociation[i] == CamSel.SelectedIndex)
                {
                    Temp1 = i;                                      
                }
            }
            Temp2 = Video.CameraAssociation[ChanSel.SelectedIndex];
            Video.CameraAssociation[ChanSel.SelectedIndex] = CamSel.SelectedIndex;
            Video.CameraAssociation[Temp1] = Temp2;
            CABox.Text = "";
            for (int i = 0; i < 16; i++)
                CABox.Text += "Channel " + (i + 1).ToString() + " - Camera " + (Video.CameraAssociation[i] + 1).ToString() + Environment.NewLine;
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {          
            this.Close();
        }

        private void ChanSel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
