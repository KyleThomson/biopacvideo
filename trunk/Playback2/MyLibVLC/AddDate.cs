using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class AddDate : Form
    {
        public Project pjt; 
        public AddDate(Project J)
        {
            InitializeComponent();
            pjt = J;
            string[] Ans = pjt.Get_Animals();
            foreach (string A in Ans)
            {
                AnimalBox.Items.Add(A);
            }
            AnimalBox.SelectedIndex = 0; 
        }
        private void DateBox1_TextChanged(object sender, EventArgs e)
        {
            DateTime dt;
            if (DateTime.TryParse(DateBox1.Text, out dt))
            {
                DateBox1.Text = dt.ToShortDateString();
            }
        }        
        private void RefreshBox()
        {
            DateList.Items.Clear();            
            string [] Datestuff = pjt.GetImportantDates(AnimalBox.Items[AnimalBox.SelectedIndex].ToString());
            foreach(string D in Datestuff)
            {               
                DateList.Items.Add(D);
            }
        }
        private void Add_Click(object sender, EventArgs e)
        {
            //Add the date to the animal's list
            pjt.AddImportantDate(AnimalBox.Items[AnimalBox.SelectedIndex].ToString(), DateBox1.Text, LabelBox1.Text);            
            RefreshBox();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            if (DateList.SelectedIndex > -1)
            {                
                //Removes date based on location in the box.
                pjt.RemoveImportantDate(AnimalBox.Items[AnimalBox.SelectedIndex].ToString(), DateList.SelectedIndex);                
            }
            RefreshBox();
        }

        private void AnimalBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshBox();
        }

        private void Finish_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        
    }
}
