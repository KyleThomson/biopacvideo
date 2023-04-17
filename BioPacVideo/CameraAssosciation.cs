using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace BioPacVideo
{
    public partial class CameraAssosciation : Form
    {
        Graphics g;
        private VideoWrapper Video;
        public List<ComboBox> comboList;
        int[] comboTracker;
        bool trigger = false; 
        
        public CameraAssosciation()
        {
            InitializeComponent();
            
            Video = VideoWrapper.Instance;
            g = this.CreateGraphics(); 
            // Populate all combo boxes with all channel values 
            for (int i = 0; i < 16; i++)
                comboBox1.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox2.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox3.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox4.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox5.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox6.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox7.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox8.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox9.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox10.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox11.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox12.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox13.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox14.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox15.Items.Add("Channel " + (i + 1).ToString());
            for (int i = 0; i < 16; i++)
                comboBox16.Items.Add("Channel " + (i + 1).ToString());
            // Set selected indexes for each combo box to whatever channel is assosciated with each camera
            comboBox1.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 0); 
            comboBox2.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 1);
            comboBox3.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 2);
            comboBox4.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 3);
            comboBox5.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 4);
            comboBox6.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 5);
            comboBox7.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 6);
            comboBox8.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 7);
            comboBox9.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 8);
            comboBox10.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 9);
            comboBox11.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 10);
            comboBox12.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 11);
            comboBox13.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 12);
            comboBox14.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 13);
            comboBox15.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 14);
            comboBox16.SelectedIndex = Array.IndexOf(Video.CameraAssociation, 15);

            comboTracker = new int[16]; 

            for(int i = 0; i<16; i++)
            {
               comboTracker[i] = Array.IndexOf(Video.CameraAssociation, i); // For tracking the values of the combo boxes before they're changed
            }
             

            // making the array of panel addresses for the video
            Video.panelhandlesnew = new Int32[16];
            Video.panelhandlesnew[0] = panel1.Handle.ToInt32();
            Video.panelhandlesnew[1] = panel2.Handle.ToInt32();
            Video.panelhandlesnew[2] = panel3.Handle.ToInt32();
            Video.panelhandlesnew[3] = panel4.Handle.ToInt32();
            Video.panelhandlesnew[4] = panel5.Handle.ToInt32();
            Video.panelhandlesnew[5] = panel6.Handle.ToInt32();
            Video.panelhandlesnew[6] = panel7.Handle.ToInt32();
            Video.panelhandlesnew[7] = panel8.Handle.ToInt32();
            Video.panelhandlesnew[8] = panel9.Handle.ToInt32();
            Video.panelhandlesnew[9] = panel10.Handle.ToInt32();
            Video.panelhandlesnew[10] = panel11.Handle.ToInt32();
            Video.panelhandlesnew[11] = panel12.Handle.ToInt32();
            Video.panelhandlesnew[12] = panel13.Handle.ToInt32();
            Video.panelhandlesnew[13] = panel14.Handle.ToInt32();
            Video.panelhandlesnew[14] = panel15.Handle.ToInt32();
            Video.panelhandlesnew[15] = panel16.Handle.ToInt32();

            // Call the function that should show the videos
            Video.NewCloneVideo(true); 



            // delegate the events that will call the combo boxes to make changes to each other when the indexes change (TestEvent) 
            comboBox1.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox1); };
            comboBox2.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox2); };
            comboBox3.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox3); };
            comboBox4.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox4); };
            comboBox5.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox5); };
            comboBox6.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox6); };
            comboBox7.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox7); };
            comboBox8.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox8); };
            comboBox9.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox9); };
            comboBox10.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox10); };
            comboBox11.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox11); };
            comboBox12.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox12); };
            comboBox13.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox13); };
            comboBox14.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox14); };
            comboBox15.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox15); };
            comboBox16.SelectedIndexChanged += delegate (object sender, System.EventArgs e) { TestEvent(sender, e, comboBox16); };

            comboList = new List<ComboBox>() { comboBox1, comboBox2, comboBox3, comboBox4, comboBox5, comboBox6, comboBox7, comboBox8, comboBox9, comboBox10, comboBox11, comboBox12, comboBox13, comboBox14, comboBox15, comboBox16 };
            

            // Populate the text box to show the assosciations of Channels to Cameraas 
            // This may be confusing to the end user as it is flipped and now you are seeing channels in order and cameras out of order 
            for (int i = 0; i < 16; i++)
                textBox1.Text += "Channel " + (i + 1).ToString() + " = Camera " + (Video.CameraAssociation[i]+1).ToString() + Environment.NewLine;
        }

        private void setButton_Click(object sender, EventArgs e)
        {
                //change camera assosciation to match what has been selected in the combo boxes
                //This will update to the ini file when the window is closed. 
            Video.CameraAssociation[comboBox1.SelectedIndex] = 0;
            Video.CameraAssociation[comboBox2.SelectedIndex] = 1;
            Video.CameraAssociation[comboBox3.SelectedIndex] = 2;
            Video.CameraAssociation[comboBox4.SelectedIndex] = 3;
            Video.CameraAssociation[comboBox5.SelectedIndex] = 4;
            Video.CameraAssociation[comboBox6.SelectedIndex] = 5;
            Video.CameraAssociation[comboBox7.SelectedIndex] = 6;
            Video.CameraAssociation[comboBox8.SelectedIndex] = 7;
            Video.CameraAssociation[comboBox9.SelectedIndex] = 8;
            Video.CameraAssociation[comboBox10.SelectedIndex] = 9;
            Video.CameraAssociation[comboBox11.SelectedIndex] = 10; 
            Video.CameraAssociation[comboBox12.SelectedIndex] = 11;
            Video.CameraAssociation[comboBox13.SelectedIndex] = 12;
            Video.CameraAssociation[comboBox14.SelectedIndex] = 13;
            Video.CameraAssociation[comboBox15.SelectedIndex] = 14;
            Video.CameraAssociation[comboBox16.SelectedIndex] = 15;

            // Clear the text boxes and repopulate with the new channel to camera assosciations 
            textBox1.Clear(); 
                for (int i = 0; i < 16; i++)
                    textBox1.Text += "Channel " + (i + 1).ToString() + " = Camera " + (Video.CameraAssociation[i] + 1).ToString() + Environment.NewLine;
        }

        public void TestEvent(object sender, EventArgs e, ComboBox cb)
        { // This event triggers when one of the combo boxes is changed to a new value
            //It checks to see what other combo box has that value that isn't itself
            //And it changes that combo box to have the value that it previously had by calling upon an array, comboTracker
            //i.e. if combo box 3 changes its value from 3 to 4, then combo box 4 will change its value from 4 to 3, etc. 
            if (trigger)
            {
                return;
            }
            trigger = true; 
            foreach (ComboBox c in comboList)
            {


                if (c.Equals(cb))
                {
                    //if the combo box it's looking at is itself, do nothing 
                } else
                {

                    if (c.SelectedItem.ToString() == cb.SelectedItem.ToString())
                    {
                        
                        //if the checked combo box has the same value as the triggering combo box, change the checked combo box
                        int temp = comboList.IndexOf(cb);
                        int temp2 = comboList.IndexOf(c);
                        c.SelectedIndex = comboTracker[temp];
                        int valuec = comboTracker[temp2];
                        int valuecb = comboTracker[temp];
                        comboTracker[temp] = valuec;
                        comboTracker[temp2] = valuecb;
                        // update combo tracker with the new values 
                        Console.WriteLine("Event Triggered");
                        break; 
                    }
                }

            }
            trigger = false; 
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            this.Close(); //Close the program. Which will update the ini file with the new camera assosciations 
        }
    }
}
