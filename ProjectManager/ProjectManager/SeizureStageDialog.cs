using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManager
{
    public class SeizureStageDialog
    {
        public int returnSeverity { get; set; }
        public void ShowDialog(int bubbleStage, int notesStage, string ID)
        {
            // Create form and form size
            Form prompt = new Form();
            prompt.Width = 250;
            prompt.Height = 100;
            prompt.Text = "Select correct severity.";

            // Create label to inform user of animal that has the conflict.
            Label animalLabel = new Label();
            animalLabel.Location = new System.Drawing.Point(0, 0);
            animalLabel.Text = ID;

            // Create buttons with stages
            FlowLayoutPanel panel = new FlowLayoutPanel();
            Button bubbled = new Button() { Text = "Bubbled Stage: " + bubbleStage.ToString(), AutoSize = true };
            Button noted = new Button() { Text = "Notes Stage: " + notesStage.ToString(), AutoSize = true };

            // Click event handling
            bubbled.Click += (sender, e) => { Stage_Click(sender, e); prompt.Close(); };
            noted.Click += (sender, e) => { Stage_Click(sender, e); prompt.Close(); };
            bubbled.Tag = bubbleStage;
            noted.Tag = notesStage;
            
            // Display selection form & add controls to form
            panel.Controls.Add(bubbled);
            panel.Controls.Add(noted);
            panel.Controls.Add(animalLabel);
            prompt.Controls.Add(panel);
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.ShowDialog();
        }
        private void Stage_Click(object sender, EventArgs e)
        {
            returnSeverity = (int)((Button)sender).Tag;
        }
    }
}
