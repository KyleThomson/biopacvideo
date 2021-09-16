using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeizurePlayback
{
    public partial class AddGroup : Form
    {
        public Project pjt; 
        int[] IDs;
        int[] counts;
        string[] Names;
        public AddGroup(Project J)
        {            
            InitializeComponent();
            pjt = J;
            foreach (AnimalType A in pjt.Animals)
            {
                AnimalList.Items.Add(A.ID);                
            }
            UpdateGroupList();
            AnimalList.SelectedIndex = 0;         
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void UpdateGroupList()
        {
            GroupsList.Items.Clear();
            GroupsList.Items.Add("Not Assigned");
            GroupNameBox.Items.Clear();            
            IDs = new int[pjt.Groups.Count];
            counts = new int[pjt.Groups.Count];
            Names = new string[pjt.Groups.Count];
            int c = 0; 
            foreach (GroupType G in pjt.Groups)
            {
                IDs[c] = G.IDNum;
                counts[c] = G.count;
                Names[c] = G.Name;
                GroupsList.Items.Add(G.Name);
                GroupNameBox.Items.Add(G.Name);
                c++; 
            }
            AnimalList.SelectedIndex = 0; 

        }
        private void CreateGroup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(GroupNameBox.Text))
            {
                return;
            }
            int IDN = 0; 
            foreach (GroupType G in pjt.Groups)
            { 
                if (string.Compare(GroupNameBox.Text, G.Name) == 0) 
                {
                    MessageBox.Show("Group name already exists");
                    return;
                }
              if (IDN < G.IDNum)
                  IDN = G.IDNum;
            }
            GroupType Gt = new GroupType((IDN + 1).ToString(), GroupNameBox.Text, "0");
            pjt.Groups.Add(Gt);
            UpdateGroupList();
        }

        private void AnimalList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pjt.Animals[AnimalList.SelectedIndex].Group.IDNum == 0)
                GroupsList.SelectedIndex = 0; 
            else
                GroupsList.SelectedIndex = Array.IndexOf(IDs, pjt.Animals[AnimalList.SelectedIndex].Group.IDNum)+1;
        }

        private void Enroll_Click(object sender, EventArgs e)
        {
            if (GroupsList.SelectedIndex == 0)
            {
                pjt.Animals[AnimalList.SelectedIndex].Group.IDNum = 0;
                pjt.Animals[AnimalList.SelectedIndex].Group.Name = "";
            }
            else
            {
                pjt.Animals[AnimalList.SelectedIndex].Group.IDNum = IDs[GroupsList.SelectedIndex-1];
                pjt.Animals[AnimalList.SelectedIndex].Group.Name = Names[GroupsList.SelectedIndex - 1];                
            }

        }

        private void GroupNameBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
