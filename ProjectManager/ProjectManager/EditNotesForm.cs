using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectManager
{
    public partial class EditNotesForm : Form
    {

        public int rScore;
        public string AName;
        public string Notes;
        bool textChanged = false;
        PMEEGView prnt;



        public EditNotesForm(PMEEGView p, int r, string a, string n)
        {
            InitializeComponent();
            rScore = r;
            AName = a;
            Notes = n;
            prnt = p;
            NotesBox.Text = n;
            CurrentScoreLabel.Text = r.ToString();
            this.Text = a + ": Notes";

            switch(r)
            {

                case 0:
                    NonCon.Select();
                    break;
                case 1:
                    S1.Select();
                    break;
                case 2:
                    S2.Select();
                    break;
                case 3:
                    S3.Select();
                    break;
                case 4:
                    S4.Select();
                    break;
                case 5:
                    S5.Select();
                    break;

            }



        }

        #region Radio Button Control

        private void NonCon_CheckedChanged(object sender, EventArgs e)
        {
            ChangeScore(0);
        }

        private void S1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeScore(1);
        }

        private void S2_CheckedChanged(object sender, EventArgs e)
        {
            ChangeScore(2);
        }

        private void S3_CheckedChanged(object sender, EventArgs e)
        {
            ChangeScore(3);
        }

        private void S4_CheckedChanged(object sender, EventArgs e)
        {
            ChangeScore(4);
        }

        private void S5_CheckedChanged(object sender, EventArgs e)
        {
            ChangeScore(5);
        }

        public void ChangeScore(int score)
        {
            rScore = score;
            CurrentScoreLabel.Text = score.ToString();
        }

        #endregion

        private void NotesBox_TextChanged(object sender, EventArgs e)
        {
            textChanged = true;
        }

        #region Buttons

        private void SaveButton_Click(object sender, EventArgs e)
        {

            if (textChanged) Notes = NotesBox.Text;

            prnt.EditNotes(rScore, Notes);

        }


        #endregion

        private void CancelButton_Click(object sender, EventArgs e)
        {

            this.Close();

        }
    }
}
