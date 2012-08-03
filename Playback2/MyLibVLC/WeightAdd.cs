using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class WeightAdd : Form
    {
        public bool OK;
        public int Count;
        public DateTime Date; 
        public int[] Pellets;
        public int[] Weights;
        public string[] ID;
        private TextBox[] AID;
        private TextBox[] Wt;
        private TextBox[] Pt;
        public WeightAdd()
        {
            OK = false;
            InitializeComponent();
            ID = new string[12];
            Count = 0;
            Weights = new int[12];
            Pellets = new int[12];
            AID = new TextBox[12];
            Wt = new TextBox[12];
            Pt = new TextBox[12];
            AID[0] = A1;
            AID[1] = A2;
            AID[2] = A3;
            AID[3] = A4;
            AID[4] = A5;
            AID[5] = A6;
            AID[6] = A7;
            AID[7] = A8;
            AID[8] = A9;
            AID[9] = A10;
            AID[10] = A11;
            AID[11] = A12;
            Wt[0] = W1;
            Wt[1] = W2;
            Wt[2] = W3;
            Wt[3] = W4;
            Wt[4] = W5;
            Wt[5] = W6;
            Wt[6] = W7;
            Wt[7] = W8;
            Wt[8] = W9;
            Wt[9] = W10;
            Wt[10] = W11;
            Wt[11] = W12;
            Pt[0] = P1;
            Pt[1] = P2;
            Pt[2] = P3;
            Pt[3] = P4;
            Pt[4] = P5;
            Pt[5] = P6;
            Pt[6] = P7;
            Pt[7] = P8;
            Pt[8] = P9;
            Pt[9] = P10;
            Pt[10] = P11;
            Pt[11] = P12;            
        }

        private void WeightAdd_Load(object sender, EventArgs e)
        {

        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(DateBox.Text, out Date))
            {
                MessageBox.Show("Invalid Date");
                return;
            }
            OK = true;
            int TempWt;
            int TempP;
            for (int i = 0; i < 12; i++)
            {
                if (AID[i].Text != "")
                {
                    if (int.TryParse(Wt[i].Text, out TempWt))
                    {
                        ID[Count] = AID[i].Text;
                        Weights[Count] = TempWt;
                        if (int.TryParse(Pt[i].Text, out TempP))
                        {
                            Pellets[Count] = TempP;
                        }
                        else
                        {
                            Pellets[Count] = 0; 
                        }
                        Count++;
                    }
                }
            }
            this.Close();
        }
    }
}
