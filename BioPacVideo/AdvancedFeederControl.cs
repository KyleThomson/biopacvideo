using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public AdvancedFeederControl()
        {
            InitializeComponent();
            Feeder = FeederTemplate.Instance;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(CommandValue.Text, out int result))
            {
                if ((result < 0) || (result > 31))
                    return;
                Feeder.SendDirectCommand((byte)result);
            }
        }
    }
}
