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
        public AdvancedFeederControl()
        {
            InitializeComponent();
            Feeder = FeederTemplate.Instance;
            Count = Feeder.FeederStateWindowOutputCount;             
            this.AcceptButton = buttonSend;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (int.TryParse(CommandValue.Text, out int result))
            {
                if (result < 0)
                {
                    return;
                }
                else if (result < 31)
                {

                    Feeder.SendDirectCommand((byte)result);
                    textBoxHistory.Text = CommandValue.Text + Environment.NewLine + textBoxHistory.Text;
                    CommandValue.Text = "";
                }
                else //Handle Biopac internal feeder cmmand
                {
                    switch (result)
                    {
                        case 32:

                            break;
                        case 33:

                            break; 
                        default:
                            Console.WriteLine("INVALID COMMAND");
                            break;
                    }
                }
            }
        }

        private void CommandValue_KeyPressed(object sender, KeyEventArgs e)
        {
            if (textBoxHistory.Lines.Length > 0)
            {
                if (e.KeyCode == Keys.Up && CurrentIndex < textBoxHistory.Lines.Count() - 2) //Count adds 1 to array and Text box always has a newline.
                {
                    CurrentIndex += 1;
                    fillFromHistory();
                }
                else if (e.KeyCode == Keys.Down && CurrentIndex > -1)
                {
                    CurrentIndex -= 1;
                    fillFromHistory();
                }
            }
        }

        private void fillFromHistory()
        {
            if (CurrentIndex == -1)
            {
                CommandValue.Text = "";
            } else if (CurrentIndex < textBoxHistory.Lines.Length)
            {
                CommandValue.Text = textBoxHistory.Lines[CurrentIndex];
                CommandValue.SelectionStart = CommandValue.Text.Length;
            }            
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
