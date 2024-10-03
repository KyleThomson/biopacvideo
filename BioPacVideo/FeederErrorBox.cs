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
    public partial class FeederErrorBox : Form
    {
        #region Properties
        private FeederTemplate feeder;
        #endregion

        #region Lifecycle
        public FeederErrorBox(FeederTemplate feeder)
        {
            InitializeComponent();
            this.feeder = feeder;
            feeder.MessageSent += OnFeederMessageRecieved;
        }
        #endregion

        #region Input Handlers
        private void OnFeederMessageRecieved(FeederMessage message) 
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => OnFeederMessageRecieved(message)));
            }
            else
            {
                switch (message.Type)
                {
                    case MessageType.STATUS:
                        Add_Status(message.Message);
                        break;
                    case MessageType.ERROR:
                        Add_Error(message.Message);
                        break;
                }
            }
        }

        private void Dismiss_Click(object sender, EventArgs e)
        {
            ErrorList.Text = "";
            StatusList.Text = "";
            this.Hide();
        }
        #endregion

        #region UIFunctions
        /// <summary>
        /// Adds an error on a new line to the Error Textbox in this view
        /// </summary>
        /// <param name="ErrorText">Error String to add as a new line</param>
        private void Add_Error(string ErrorText)
        {
            if (this.Visible == false)
                this.Show();             
            string value = DateTime.Now.ToString("HH:mm");
            ErrorList.Text += ErrorText + value + "\r\n";            
        }

        /// <summary>
        /// Adds a status text on a new line to the status textbox in this view
        /// </summary>
        /// <param name="StatusText">Status String to add as a new line</param>
        private void Add_Status(string StatusText)
        {
            string value = DateTime.Now.ToString("HH:mm");
            StatusList.Text += StatusText + value + "\r\n";
        }
        #endregion                
    }
}
