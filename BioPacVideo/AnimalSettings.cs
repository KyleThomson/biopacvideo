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
        public AnimalSettings(FeederTemplate passFeeder)
        {
            InitializeComponent();
            Feeder = passFeeder;
            x = 0;
            y = 0;
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
                    location = new Point((150 * x) + 33, (216 * y) + 38);
                    groupList[k].Location = location;
                    groupList[k].Show();
                    x++;
                }
            }
            else
            {

            }

            submitButton.Location = new Point((150*(Feeder.Cages_X-1)+33), (216*(Feeder.Cages_Y)+22));
            instructLabel.Location = new Point(33, 8); 
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
            for (int i = 0; i<totalCages; i++)
            {
                if(weightBox[i].Text == "" || weightBox[i].Text == "0")
                {
                    medCheck[i].Enabled = false;
                    unmedCheck[i].Enabled = false;
                    percentBox[i].Enabled = false; 
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
        private void weightBox_FocusLeave(object sender, EventArgs e, TextBox tb)
        {
            if (focusBusy) return;
            focusBusy = true; 
            foreach (TextBox t in animalID)
            {
                
                if(t.Equals(tb))
                {

                }
                else
                {
                    if(t.Text == tb.Text)
                    {
                        MessageBox.Show("Two animals cannot have the same ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tb.Text = ""; 
                    }
                }
            }
            focusBusy = false; 
        }

        private void checkBox_Change (object sender, EventArgs e)
        {
            for (int i = 0; i<totalCages; i++)
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

        private void percentBox_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine(" "); 
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
                Console.WriteLine(Feeder.Rats[i].Medication.ToString()); 

            }
        }



        /*private void groupBox_MouseHover(object sender, EventArgs e, GroupBox gb)
        {
            foreach (GroupBox g in groupList)
            {
                if (g.Equals(gb))
                {
                    int index = groupList.IndexOf(g);
                    if (weightBox[index].Text == "" || weightBox[index].Text == "0")
                    {
                        tooltip1 = new ToolTip();
                        tooltip1.SetToolTip(groupList[index], "please input weight before \nspecifying medication");
                        Console.WriteLine("Activating tooltip event");
                    }
                }
            }
        }*/
        
    }
}
