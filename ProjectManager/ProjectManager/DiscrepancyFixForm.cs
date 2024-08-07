using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;


namespace ProjectManager
{
    
    public partial class DiscrepancyFixForm : Form
    {
        public string RetTest1;
        public bool listCleared = false;
        public List<DiscrepancyItem> discList;
        public List<AnimalType> animals;
        public DATR DAT;

        public bool paused = true;
        public Graphics g;
        public Graphics gg;
        int seconds = 0;
        float pos = 0;
        Pen RedPen;
        bool endOfClip = false;
        public string vidDir;
        public MediaPlayer player;
        public LibVLC vlc;
        public SeizureType currentSZ;
        
        public bool vidExists;
        
        


        public DiscrepancyFixForm(List<DiscrepancyItem> l, List<AnimalType> a, string vD, string CDatName)
        {
            InitializeComponent();
            discList = l;
            animals = a;
            vidDir = vD;

            DAT = new DATR(CDatName);

            g = GalGBox.CreateGraphics();
            gg = this.CreateGraphics();

            DAT.initDisplay(GalGBox.Width - 2, GalGBox.Height - 2);

            foreach(DiscrepancyItem di in discList)
            {
                ListViewItem tempLVI = new ListViewItem();
                tempLVI.Text = di.an.ID;
                tempLVI.SubItems.Add(di.sz.d.ToShortDateString().ToString());
                tempLVI.SubItems.Add(di.sz.Severity.ToString());
                tempLVI.SubItems.Add(di.notes.ToString());
                

                ConflictingList.Items.Add(tempLVI);

            }

           
        }




        private void closeForm(object sender, EventArgs e)
        {
            this.RetTest1 = "It Works!";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DiscrepancyFixForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.RetTest1 = "It Works!";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void myVLC_Click(object sender, EventArgs e)
        {

        }

        private void ConflictingList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ConflictingList.SelectedIndices.Count < 1)
            {
                return;
            }

            currentSZ = discList[ConflictingList.SelectedIndices[0]].sz;


            if (currentSZ.VidString == null)
            {
                DialogResult errorBox = MessageBox.Show("NO VIDEO DATA FOUND \n \n \t To view video, you must use Seizure Playback to view this seizure", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                vidExists = false;
            }
            else
            {
                vidExists = true;
            }

            UpdateDisplay();
        }




        //Section to draw the seizure in the GalGBox

        public void UpdateDisplay()
        {


            if (currentSZ == null) return;

            if (DAT.Loaded)
            {

                DAT.SetDispLength(currentSZ.length);
                DAT.cleargraph();

                DAT.DrawOneSZ(currentSZ, 0, 0, showBuffer.Checked);

                g.Clear(Color.White);
                g.DrawImage(DAT.offscreen, 0, 0);
                gg.DrawImage(DAT.offscreen, button1.Location);

            }


        }



        private void telem_CheckedChanged(object sender, EventArgs e)
        {
            if (telem.Checked)
            {
                DAT.Telemetry = true;
            } else
            {
                DAT.Telemetry = false;
            }

            UpdateDisplay();
        }

        private void ZoomBar_Scroll(object sender, EventArgs e)
        {
            DAT.Zoom = (float)ZoomBar.Value / 10f;
            UpdateDisplay();
        }

        private void showBuffer_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
    }
}
