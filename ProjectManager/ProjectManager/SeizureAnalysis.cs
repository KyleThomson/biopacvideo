using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    class SeizureAnalysis
    {
        public int CompareSeizures(SeizureType seizure)
        {
            int bubbleSeverity = default; // default
            int noteSeverity = default; // default
            int finalStage;
            if (seizure.Severity >= 0 && seizure.Severity <= 5)
            {
                bubbleSeverity = seizure.Severity;
            }
            if (seizure.Notes.Length > 0)
            {
                noteSeverity = ParseSeizure(seizure.Notes);
            }

            // Check if bubble and note match and flag if it doesn't -- want to prompt user with messagebox
            if (bubbleSeverity != noteSeverity)
            {
                SeizureStageDialog seizureStageDialog = new SeizureStageDialog();
                SeizureStageDialog.ShowDialog(bubbleSeverity, noteSeverity);
            }// Do something

            finalStage = bubbleSeverity;

            return finalStage;
        }
        public int ParseSeizure(string note)
        {
            int severity = default;
            string storeNum = String.Join("", note.Where(char.IsDigit));
            if (storeNum.Length > 0)
            {
                if (int.Parse(storeNum) <= 5 && int.Parse(storeNum) >= 0)
                {
                    severity = int.Parse(storeNum);
                }
                else
                {
                    severity = -1;
                }
            }
            return severity;
        }
    }
}
