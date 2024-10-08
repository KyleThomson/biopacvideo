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
        #region Properties
        FeederTemplate Feeder;
        int CurrentIndex = -1;
        List<byte> sentCommands = new List<byte>();
        #endregion

        #region Lifecyle
        public AdvancedFeederControl()
        {
            InitializeComponent();
            Feeder = FeederTemplate.Instance;
            Feeder.MessageSent += OnFeederMessageRecieved;      
            this.AcceptButton = buttonSend;
        }

        private void AdvancedFeederControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            Feeder.MessageSent -= OnFeederMessageRecieved;
        }
        #endregion

        private void OnFeederMessageRecieved(FeederMessage message)
        {
            addLine($"{message.Type} - {message.Message}");
        }

        private void addLine(string line)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => addLine(line)));
            }
            else
            {
                textBoxHistory.Text += $"{line}{Environment.NewLine}";
                textBoxHistory.SelectionStart = textBoxHistory.Text.Length;
                textBoxHistory.ScrollToCaret();
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (Byte.TryParse(CommandValue.Text, out byte result))
            {
                sentCommands.Insert(0, result);
                addLine(result.ToString());
                if (result < 0)
                {
                    return;
                }
                else if (result < 31)
                {
                    Feeder.SendDirectCommand(result);
                    addLine(CommandValue.Text);
                }
                else //Handle Biopac internal feeder cmmand
                {
                    switch (result)
                    {
                        case 32: //Print Command Queue
                            if (Feeder.GetCommandSize() > 0)
                            {
                                addLine("");
                                addLine("Current Command queue: ");
                                foreach (byte command in Feeder.getCommandQueu())
                                {
                                    addLine(command.ToString());
                                }
                                addLine("");
                            } else
                            {
                                addLine("No Commands In Queue");
                            }
                            break;
                        default:
                            addLine("INVALID COMMAND");
                            break;
                    }
                }
                CommandValue.Text = "";
                CurrentIndex = -1;
            }
        }

        private void CommandValue_KeyPressed(object sender, KeyEventArgs e)
        {
            if (sentCommands.Count() > 0 && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                if (e.KeyCode == Keys.Up && CurrentIndex == -1)
                {
                    CurrentIndex = 0;
                }
                else if (e.KeyCode == Keys.Up && CurrentIndex < sentCommands.Count() - 1)
                {
                    CurrentIndex += 1;
                }
                else if (e.KeyCode == Keys.Down && CurrentIndex > 0)
                {
                    CurrentIndex -= 1;
                } else if (e.KeyCode == Keys.Down && CurrentIndex == 0 && sentCommands.Count > 0)
                {
                    CurrentIndex = -1;
                }
                applyHistory();
            }
        }

        private void applyHistory()
        {
            if (CurrentIndex == -1)
            {
                CommandValue.Text = "";
            } else
            {
                CommandValue.Text = sentCommands[CurrentIndex].ToString();
            }                
            CommandValue.BeginInvoke(new Action(() =>
            {
                CommandValue.SelectionStart = CommandValue.Text.Length;
            }));
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
