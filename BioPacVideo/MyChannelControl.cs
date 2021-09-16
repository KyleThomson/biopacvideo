using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BioPacVideo
{
    public partial class MyChannelControl : UserControl
    {
        public uint m_nChannelNumber = 0;

        public MyChannelControl()
        {
            //InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            /*if (m.Msg == 0x0204) // WM_RBUTTONDOWN
            {
                Form1 myForm = (Form1)this.Parent;

                myForm.OnRButtonDown_ChannelControl(m_nChannelNumber);
            }

            if (m.Msg == 0x0201) // WM_LBUTTONDOWN
            {
                Form1 myForm = (Form1)this.Parent;

                myForm.OnLButtonDown_ChannelControl(m_nChannelNumber);
            }            
            */
            base.WndProc(ref m);
        }
    }
}
