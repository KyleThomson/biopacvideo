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
        public void ShowDialog(int bubbleStage, int notesStage, string ID, string notes)
        {
            // Create form and form size
            Form prompt = new Form();
            // Turn off minimize/red x buttons
            prompt.ControlBox = false;
            prompt.Width = 250;
            prompt.Height = 150;
            prompt.Text = "Select correct severity.";
            
            // Create label to inform user of animal that has the conflict.
            Label animalLabel = new Label();
            animalLabel.Location = new System.Drawing.Point(0, 0);
            animalLabel.Text = ID + ": " + notes;
            animalLabel.AutoSize = true;

            // Create buttons with stages
            FlowLayoutPanel panel = new FlowLayoutPanel();
            Button bubbled = new Button() { Text = "Bubbled Stage: " + bubbleStage.ToString(), AutoSize = false };
            bubbled.Size = new System.Drawing.Size(110, 25);
            bubbled.Location = new System.Drawing.Point(0, prompt.Height);

            Button noted = new Button() { Text = "Notes Stage: " + notesStage.ToString(), AutoSize = false };
            noted.Size = new System.Drawing.Size(110, 25);
            noted.Location = new System.Drawing.Point(120, prompt.Height);

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
