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
    public partial class AnimalSettings : Form
    {
        private FeederTemplate Feeder;
        List<GroupBox> groupList;
        List<TextBox> animalID;
        List<TextBox> weightBox;
        List<CheckBox> medCheck;
        List<CheckBox> unmedCheck;
        List<TextBox> percentBox;
        bool[] access; 
        Point location; 
        int totalCages;
        int x;
        int y;
        bool focusBusy;
        bool recordingOn; 
        public AnimalSettings(FeederTemplate passFeeder, bool Recording)
        {
            InitializeComponent();
            Feeder = passFeeder;
            x = 0;
            y = 0;
            recordingOn = Recording; 
            
            //List of all the group boxes, one per animal
            groupList = new List<GroupBox> { groupBox1, groupBox2, groupBox3, groupBox4, groupBox5, groupBox6, groupBox7, groupBox8,
            groupBox9, groupBox10, groupBox11, groupBox12, groupBox13, groupBox14, groupBox15, groupBox16};
            //List of all the boxes for the animal IDs
            animalID = new List<TextBox> { animalID1, animalID2, animalID3, animalID4, animalID5, animalID6, animalID7, animalID8,
            animalID9,animalID10,animalID11,animalID12,animalID13,animalID14,animalID15,animalID16};
            //List of all the boxes for the weights
            weightBox = new List<TextBox> { weightBox1, weightBox2, weightBox3, weightBox4, weightBox5, weightBox6, weightBox7, weightBox8,
            weightBox9,weightBox10,weightBox11,weightBox12,weightBox13,weightBox14,weightBox15,weightBox16,};
            //List of all the medication check boxes
            medCheck = new List<CheckBox> { medCheck1, medCheck2, medCheck3, medCheck4, medCheck5, medCheck6, medCheck7, medCheck8,
            medCheck9,medCheck10,medCheck11,medCheck12,medCheck13,medCheck14,medCheck15,medCheck16};
            //List of all the unmedicated check boxes 
            unmedCheck = new List<CheckBox> { unmedCheck1, unmedCheck2, unmedCheck3, unmedCheck4, unmedCheck5, unmedCheck6, unmedCheck7, unmedCheck8,
            unmedCheck9,unmedCheck10,unmedCheck11,unmedCheck12,unmedCheck13,unmedCheck14,unmedCheck15,unmedCheck16};
            //List of all the percentage text boxes
            percentBox = new List<TextBox> {percentBox1, percentBox2, percentBox3, percentBox4, percentBox5, percentBox6, percentBox7, percentBox8,
            percentBox9, percentBox10, percentBox11, percentBox12, percentBox13, percentBox14, percentBox15, percentBox16};


            totalCages = (Feeder.Cages_X) * (Feeder.Cages_Y);
            access = Enumerable.Repeat(false, 16).ToArray();
            cages_x_text.Text = Feeder.Cages_X.ToString();
            cages_y_text.Text = Feeder.Cages_Y.ToString();
            this.Height = (Feeder.Cages_Y * 216) + 138;
            this.Width = (Feeder.Cages_X * 150) + 88;

            if (recordingOn)
            {
                warningLabel.Text = "Animal IDs may not be changed while recording is in progress";
                foreach(TextBox t in animalID)
                {
                    t.Enabled = false; 
                }
            }

            //groupBox1.MouseHover += delegate (object sender, System.EventArgs e) { groupBox_MouseHover(sender, e, groupBox1); };
            animalID1.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID1); };
            animalID2.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID2); };
            animalID3.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID3); };
            animalID4.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID4); };
            animalID5.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID5); };
            animalID6.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID6); };
            animalID7.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID7); };
            animalID8.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID8); };
            animalID9.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID9); };
            animalID10.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID10); };
            animalID11.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID11); };
            animalID12.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID12); };
            animalID13.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID13); };
            animalID14.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID14); };
            animalID15.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID15); };
            animalID16.LostFocus += delegate (object sender, System.EventArgs e) { weightBox_FocusLeave(sender, e, animalID16); };

            animalID1.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID1); };
            animalID2.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID2); };
            animalID3.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID3); };
            animalID4.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID4); };
            animalID5.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID5); };
            animalID6.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID6); };
            animalID7.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID7); };
            animalID8.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID8); };
            animalID9.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID9); };
            animalID10.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID10); };
            animalID11.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID11); };
            animalID12.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID12); };
            animalID13.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID13); };
            animalID14.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID14); };
            animalID15.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID15); };
            animalID16.TextChanged += delegate (object sender, System.EventArgs e) { animalID_TextChanged(sender, e, animalID16); };

            for (int i = 0; i<16; i++)
            {
                
                groupList[i].Hide();
                medCheck[i].Enabled = false;
                unmedCheck[i].Enabled = false;
                percentBox[i].Enabled = false;
                
            }
            if (totalCages < 17)
            {
                for (int k = 0; k < totalCages; k++)
                {
                    animalID[k].Text = Feeder.Rats[k].ID.ToString();
                    weightBox[k].Text = Feeder.Rats[k].Weight.ToString();
                    percentBox[k].Text = Feeder.Rats[k].Medication.ToString();
                    if (animalID[k].Text == "e" || animalID[k].Text == "E" || animalID[k].Text == "")
                    {
                        medCheck[k].Enabled = false;
                        unmedCheck[k].Enabled = false;
                        percentBox[k].Enabled = false;
                        weightBox[k].Text = "0";
                        weightBox[k].Enabled = false; 
                    }
                    if (weightBox[k].Text == "" || weightBox[k].Text == "0")
                    {

                    }
                    else
                    {
                        medCheck[k].Enabled = true;
                        unmedCheck[k].Enabled = true;
                    }
                    if (Feeder.Rats[k].Medication == 100)
                    {
                        medCheck[k].Checked = true;
                        unmedCheck[k].Checked = false;
                    }
                    else if (Feeder.Rats[k].Medication == 0){
                        medCheck[k].Checked = false;
                        unmedCheck[k].Checked = true;
                    }
                    else
                    {
                        medCheck[k].Checked = true;
                        unmedCheck[k].Checked = true;
                    }
                    if (x == Feeder.Cages_X)
                    {
                        x = 0;
                        y++;
                    }
                    location = new Point((150 * x) + 33, (216 * y) + 88);
                    groupList[k].Location = location;
                    groupList[k].Show();
                    x++;
                }
            }
            else
            {

            }

            groupBox17.Location = new Point(33, 8);
            warningLabel.Location = new Point(360, 8); 
            submitButton.Location = new Point((150 * (Feeder.Cages_X - 1) + 70), (216 * (Feeder.Cages_Y) + 88));
            instructText.Location = new Point(33, (216 * (Feeder.Cages_Y) + 88));
            instructText.Text = "Please input animal ID and weight. Specify unmedicated or medicated. If both, specify the percent medicated";
            instructText.Width = ((150 * (Feeder.Cages_X - 1) + 70) - 40);
            instructText.Height = Feeder.Cages_Y * 15;
            instructText.Multiline = true;
            instructText.Enabled = false;
            //refreshButton.Location = new Point(130, 45); 
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            bool error = false; 
            for (int i=0; i<totalCages; i++)
            {
                Feeder.Rats[i].ID = animalID[i].Text;
                int temp;
                if (int.TryParse(weightBox[i].Text, out temp))
                {
                    Feeder.Rats[i].Weight = temp;
                }
                else
                {
                    Feeder.Rats[i].Weight = 0; //this might help the feeder code nonsense
                }
            }
            if (error)
            {

            }
            else
            {
                this.Dispose(true);
            }
            
        }
        private void weightBox_TextChange(object sender, EventArgs e)
        {
            for (int i = 0; i < totalCages; i++)
            {
                if (weightBox[i].Text.Contains(" "))
                {
                    weightBox[i].Text = weightBox[i].Text.Replace(" ", ""); 
                }
                if (weightBox[i].Text == "" || weightBox[i].Text == "0")
                {
                    medCheck[i].Enabled = false;
                    unmedCheck[i].Enabled = false;
                    percentBox[i].Enabled = false; 
                }
                else if (animalID[i].Text == "E" || animalID[i].Text == "e" || animalID[i].Text == "")
                {
                    //if no animal ID, or animal ID empty, do nothing because the medication stuff is already inactivated
                }
                else
                {
                    medCheck[i].Enabled = true;
                    unmedCheck[i].Enabled = true;
                    if(access[i] == false)
                    {
                        if (Feeder.Rats[i].Medication == 100)
                        {
                            medCheck[i].Checked = true;
                            unmedCheck[i].Checked = false;
                            percentBox[i].Text = "100";
                        }
                        else if (Feeder.Rats[i].Medication == 0)
                        {
                            unmedCheck[i].Checked = true;
                            medCheck[i].Checked = false;
                            percentBox[i].Text = "0";
                        }
                        else
                        {
                            medCheck[i].Checked = true;
                            unmedCheck[i].Checked = true;
                            percentBox[i].Enabled = true;
                        }
                    }
                    access[i] = true;
                }
                
            }
        }
        private void weightBox_FocusLeave(object sender, EventArgs e, TextBox tb) // this actually refers to focus leave on animal ID box, no idea why its called this 
        {
            if (focusBusy) return;
            focusBusy = true;
            int i = animalID.IndexOf(tb);
            //Console.WriteLine("i = " + i.ToString()); 
            if (tb.Text.Contains(" "))
            {
                tb.Text = tb.Text.Replace(" ", "");
            }
            if (tb.Text == "e" || tb.Text == "E" || tb.Text == "")
            {
                medCheck[i].Enabled = false;
                unmedCheck[i].Enabled = false;
                percentBox[i].Enabled = false;
                weightBox[i].Text = "0";
                weightBox[i].Enabled = false;
                focusBusy = false; 
                return;
            }
            else
            {
                weightBox[i].Enabled = true;
                if (weightBox[i].Text != "" && weightBox[i].Text != "0")
                {
                    medCheck[i].Enabled = true;
                    unmedCheck[i].Enabled = true;
                    percentBox[i].Enabled = true;
                }
            }
            foreach (TextBox t in animalID)
            {
                if (t.Equals(tb))
                {
                    
                }
                else
                {
                    if (t.Text == tb.Text)
                    {
                        MessageBox.Show("Two animals have the same ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        focusBusy = false; 
                        return;
                    }
                }
            }
            focusBusy = false; 
        }
        private void checkBox_FocusLeave(object sender, EventArgs e, CheckBox cb) // the idea was there, but its glitchy and not worth it 
        {
            int i = medCheck.IndexOf(cb);
            if (i == -1)
            {
                i = unmedCheck.IndexOf(cb); 
            }
            if (!medCheck[i].Checked && !unmedCheck[i].Checked)
            {
                unmedCheck[i].Checked = true;
                percentBox[i].Text = "0";
            }
        }

        private void checkBox_Change (object sender, EventArgs e)
        {
            for (int i = 0; i<totalCages; i++)
            {
                if (animalID[i].Text != "")
                {
                    if (medCheck[i].Checked && unmedCheck[i].Checked)
                    {
                        percentBox[i].Enabled = true;
                        percentBox[i].Text = Feeder.Rats[i].Medication.ToString();
                    }
                    else
                    {
                        percentBox[i].Enabled = false;
                        if (medCheck[i].Checked && !unmedCheck[i].Checked)
                        {
                            percentBox[i].Text = "100";
                        }
                        else if (unmedCheck[i].Checked && !medCheck[i].Checked)
                        {
                            percentBox[i].Text = "0";
                        }
                    }
                }
            }
        }

        private void percentBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < totalCages; i++)
            {

                int temp2;
                if (int.TryParse(percentBox[i].Text, out temp2))
                {
                    if (temp2 > 100)
                    {
                        percentBox[i].Text = Feeder.Rats[i].Medication.ToString();
                    }
                    Feeder.Rats[i].Medication = temp2;
                }

            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            int temp;
            int temp2;
            if (int.TryParse(cages_x_text.Text, out temp))
            {
                Feeder.Cages_X = temp;
            }
            else
            {
                MessageBox.Show("Please input cage layout", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (int.TryParse(cages_y_text.Text, out temp2))
            {
                Feeder.Cages_Y = temp2;
            }
            else
            {
                MessageBox.Show("Please input cage layout", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Feeder.Cages_X * Feeder.Cages_Y > 16)
            {
                MessageBox.Show("Program does not allow for more than 16 cages at a time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            RefreshForm(); 
            
        }

        private void RefreshForm()
        {
            x = 0; y = 0; int i = 0; 
            totalCages = (Feeder.Cages_X) * (Feeder.Cages_Y);
            this.Height = (Feeder.Cages_Y * 216) + 138;
            this.Width = (Feeder.Cages_X * 150) + 88;
            submitButton.Location = new Point((150 * (Feeder.Cages_X - 1) + 70), (216 * (Feeder.Cages_Y) + 88));
            instructText.Location = new Point(33, (216 * (Feeder.Cages_Y) + 88));
            instructText.Text = "Please input animal ID and weight. Specify unmedicated or medicated. If both, specify the percent medicated";
            instructText.Width = ((150 * (Feeder.Cages_X - 1) + 70) - 40);
            instructText.Height = Feeder.Cages_Y * 15;
            instructText.Multiline = true;
            instructText.Enabled = false;
            for (int k = 0; k < totalCages; k++)
            {
                animalID[k].Text = Feeder.Rats[k].ID.ToString();
                weightBox[k].Text = Feeder.Rats[k].Weight.ToString();
                percentBox[k].Text = Feeder.Rats[k].Medication.ToString();
                if (animalID[k].Text == "e" || animalID[k].Text == "E" || animalID[k].Text == "")
                {
                    medCheck[k].Enabled = false;
                    unmedCheck[k].Enabled = false;
                    percentBox[k].Enabled = false;
                    weightBox[k].Text = "0";
                    weightBox[k].Enabled = false;
                }
                if (weightBox[k].Text == "" || weightBox[k].Text == "0")
                {

                }
                else
                {
                    medCheck[k].Enabled = true;
                    unmedCheck[k].Enabled = true;
                }
                if (Feeder.Rats[k].Medication == 100)
                {
                    medCheck[k].Checked = true;
                    unmedCheck[k].Checked = false;
                }
                else if (Feeder.Rats[k].Medication == 0)
                {
                    medCheck[k].Checked = false;
                    unmedCheck[k].Checked = true;
                }
                else
                {
                    medCheck[k].Checked = true;
                    unmedCheck[k].Checked = true;
                }
                if (x == Feeder.Cages_X)
                {
                    x = 0;
                    y++;
                }
                location = new Point((150 * x) + 33, (216 * y) + 88);
                groupList[k].Location = location;
                groupList[k].Show();
                x++;
                i++; 
            }
            for (int p = i; p < 16; p++)
            {
                groupList[p].Hide(); 
            }
            this.Height = (Feeder.Cages_Y * 216) + 138;
            this.Width = (Feeder.Cages_X * 150) + 88;
            this.Invalidate();
            this.Refresh();
        }

        private void animalID_TextChanged(object sender, EventArgs e, TextBox tb)
        {
            int i = animalID.IndexOf(tb);
            weightBox[i].Enabled = true; 
        }
    }
}
