using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManager
{
    public class SeizureStageDialog
    {
        public object selectedStage;
        public static void ShowDialog(int bubbleStage, int notesStage)
        {
            Form prompt = new Form();
            prompt.Width = 250;
            prompt.Height = 100;
            prompt.Text = "Select correct severity.";

            FlowLayoutPanel panel = new FlowLayoutPanel();
            CheckBox szCheck = new CheckBox();

            Button bubbled = new Button() { Text = "Bubbled Stage: " + bubbleStage.ToString(), AutoSize = true };
            Button noted = new Button() { Text = "Notes Stage: " + notesStage.ToString(), AutoSize = true };


            bubbled.Click += (sender, e) => { prompt.Close(); };
            noted.Click += (sender, e) => { prompt.Close(); };

            bubbled.Tag = bubbleStage;
            noted.Tag = notesStage;

            panel.Controls.Add(bubbled);
            panel.Controls.Add(noted);
            prompt.Controls.Add(panel);
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.ShowDialog();
            
        }
        private void Stage_Click(object sender, EventArgs e)
        {
            selectedStage = ((Button)sender).Tag;
        }
    }
}
