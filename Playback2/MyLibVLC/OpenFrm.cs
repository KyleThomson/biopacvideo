using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class OpenFrm : Form
    {         
        public OpenFrm(string FN, string Rvwr, string N, double PC, int FileLength, DateTime LR, DateTime LO, bool warning, bool Compressed)
        {
            InitializeComponent();
            FileLabel.Text = "File Name: " + FN;
            Reviewing.Checked = true;
            Reviewer.Text = Rvwr;
            if (Compressed)
            {
                CompLabel.Text = "File has been compressed.";
            }
            else
            {
                CompLabel.Text = "File has not been compressed.";
            }
            if (warning)
            {
                WarningLabel.Text = "Warning: Video playback may break"; // lol
            }
            else
            {
                WarningLabel.Text = "";
            }
            NotesLabel.Text = "Notes: " + N;
            if (PC == 0)
            {                
                PercentLabel.Text = "";
                LastReviewLabel.Text = "File has not been reviewed.";
            }
            else if (Math.Ceiling(PC) >= 100)
            {
                PercentLabel.Text = "Review Complete";
                LastReviewLabel.Text = "Last reviewed on " + LR.ToLongDateString();
                Reviewing.Checked = false; 
            }
            else
            {
                TimeSpan t = TimeSpan.FromSeconds((int)((PC * FileLength)/100));
                string answer = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
                PercentLabel.Text = "Stopped at " + answer; 
                LastReviewLabel.Text = "Last reviewed on " + LR.ToShortDateString();
            }
            if (LO == DateTime.MinValue)
            {
                LastOpenLabel.Text = "File has never been opened.";
            }
            else
            {
                LastOpenLabel.Text = "Last opened on " + LO.ToLongDateString();
            }
        }
        public string GetReviewer()
        {
            return Reviewer.Text;
        }
        public bool GetReviewing()
        {
            return Reviewing.Checked;   
        }
       
        private void OKbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
