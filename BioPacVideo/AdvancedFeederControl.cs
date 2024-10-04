using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioPacVideo
{
    public partial class AdvancedFeederControl : Form
    {
        FeederTemplate Feeder;
        int CurrentIndex = -1;
        int Count;
        List<byte> sentCommands = new List<byte>();
        public AdvancedFeederControl()
        {
            InitializeComponent();
            Feeder = FeederTemplate.Instance;
            Feeder.MessageSent += OnFeederMessageRecieved;
            Count = Feeder.FeederStateWindowOutputCount;             
            this.AcceptButton = buttonSend;
        }

        private void OnFeederMessageRecieved(FeederMessage message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => OnFeederMessageRecieved(message)));
            } else
            {
                textBoxHistory.Text += $"{message.Type} - {message.Message}{Environment.NewLine}";
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (Byte.TryParse(CommandValue.Text, out byte result))
            {
                sentCommands.Insert(0, result);
                textBoxHistory.Text += result + Environment.NewLine;
                if (result < 0)
                {
                    return;
                }
                else if (result < 31)
                {
                    Feeder.SendDirectCommand(result);
                    textBoxHistory.Text += CommandValue.Text + Environment.NewLine;
                }
                else //Handle Biopac internal feeder cmmand
                {
                    switch (result)
                    {
                        case 32: //Print Command Queue
                            if (Feeder.GetCommandSize() > 0)
                            {
                                textBoxHistory.Text += Environment.NewLine + "Current Command queue: " + Environment.NewLine;
                                foreach (byte command in Feeder.getCommandQueu())
                                {
                                    textBoxHistory.Text += command + Environment.NewLine;
                                }
                                textBoxHistory.Text += Environment.NewLine;
                            } else
                            {
                                textBoxHistory.Text += "No Commands In Queue" + Environment.NewLine;
                            }
                            break;
                        default:
                            textBoxHistory.Text += "INVALID COMMAND" + Environment.NewLine;
                            break;
                    }
                }
                CommandValue.Text = "";
            }
        }

        private void CommandValue_KeyPressed(object sender, KeyEventArgs e)
        {
            if (sentCommands.Count() > 0)
            {
                if (e.KeyCode == Keys.Up && CurrentIndex == -1)
                {
                    CurrentIndex = 0;
                    applyHistory(true);
                }
                else if (e.KeyCode == Keys.Up && CurrentIndex < sentCommands.Count() - 1)
                {
                    applyHistory(true);
                }
                else if (e.KeyCode == Keys.Down && CurrentIndex > 0)
                {
                    applyHistory(false);
                }
            } else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                CommandValue.SelectionStart = CommandValue.Text.Length;  // This doesn't work, I declare this Someone elses problem
            }
        }

        private void applyHistory(bool add)
        {
            if (CurrentIndex == -1)
            {
                CurrentIndex = sentCommands.Count() - 1;
            }
            else if (add == true)
            {
                CurrentIndex += 1;
            }
            else if (add == false)
            {
                CurrentIndex -= 1;
            }

            CommandValue.Text = sentCommands[CurrentIndex].ToString();
            CommandValue.SelectionStart = CommandValue.Text.Length; // This doesn't work, I declare this Someone elses problem
        }

        private void comboBoxCommonCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCommonCommands.SelectedItem.ToString() != "")
            {
                CommandValue.Text = comboBoxCommonCommands.SelectedItem.ToString().Substring(0, 2);
            }
        }

        private void CommandValue_TextChanged(object sender, EventArgs e)
        {
            if (CommandValue.Text.Length <= 1)
            {
                comboBoxCommonCommands.SelectedItem = "";
            }
        }
    }
}
