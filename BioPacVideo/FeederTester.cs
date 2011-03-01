using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace BioPacVideo
{
    public partial class FeederTester : Form
    {
        ArrayList TestBoxes;
        MPTemplate MP;
        public FeederTester()
        {
            InitializeComponent();
            MP = MPTemplate.Instance;
            TestBoxes = new ArrayList();
            MakeList();
        }

        private void IDC_RUNTEST_Click(object sender, EventArgs e)
        {
            CheckBox TempBox;
            //Reset Test
            
            for (int i = 0; i < 8; i++)
            {
                TempBox = TestBoxes[i] as CheckBox;
                TempBox.Text = string.Format("Feeder {0} -", i + 1);
                TempBox.Checked = false;
            }
            MP.RunFeederTest();
            for (int i = 0; i < 8; i++)
            {
                TempBox = TestBoxes[i] as CheckBox;
                //MP.Test(i)
                if (MP.FeederTest[i])                    
                {
                    TempBox.Checked = true;
                    TempBox.Text = string.Format("Feeder {0} - PASS", i + 1);
                }
                else
                {
                    TempBox.Text = string.Format("Feeder {0} - FAIL", i + 1);
                }
            }            
        }
        private void MakeList()
        {
            TestBoxes.Add(IDX_FEEDER1);
            TestBoxes.Add(IDX_FEEDER2);
            TestBoxes.Add(IDX_FEEDER3);
            TestBoxes.Add(IDX_FEEDER4);
            TestBoxes.Add(IDX_FEEDER5);
            TestBoxes.Add(IDX_FEEDER6);
            TestBoxes.Add(IDX_FEEDER7);
            TestBoxes.Add(IDX_FEEDER8);
        }
    }
}
