using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace BioPacVideo
{
    public partial class InjectionManager : Form
    {
        FeederTemplate Feeder;
        private ArrayList IDBoxes;
        private ArrayList Injection1Buttons;
        private ArrayList Injection2Buttons;
        private ArrayList Inj1VolumeBoxes;
        private ArrayList Inj2VolumeBoxes; 
        private bool Startup;
        private bool SetInjection; 
        public InjectionManager()
        {
            Startup = true; 
            InitializeComponent();
            Feeder = FeederTemplate.Instance;
            IDBoxes = new ArrayList();
            Injection1Buttons = new ArrayList();
            Injection2Buttons = new ArrayList();
            Inj1VolumeBoxes = new ArrayList();
            Inj2VolumeBoxes = new ArrayList();
            CreateArrays();
            TextBox IDB;
            Injection1ADD.Text = Feeder.ADDC1;
            Injection2ADD.Text = Feeder.ADDC2;
            Dose1.Text = Feeder.Dose1;
            Dose2.Text = Feeder.Dose2;
            if (Feeder.Route1 == 4)
            {
                SCI1.Checked = true;
            }
            else if (Feeder.Route1 == 3)
            {
                IVI1.Checked = true;
            }
            else if (Feeder.Route1 == 2)
            {
                POI1.Checked = true;
            }
            else IPI1.Checked = true;
            if (Feeder.Route2 == 4)
            {
                SCI2.Checked = true;
            }
            else if (Feeder.Route2 == 3)
            {
                IVI2.Checked = true;
            }
            else if (Feeder.Route2 == 2)
            {
                POI2.Checked = true;
            }
            else IPI2.Checked = true;

            if (Feeder.Solve1 == 4)
            {
                OTHI1.Checked = true;
            }
            else if (Feeder.Solve1 == 3)
            {
                PEGI1.Checked = true;
            }
            else if (Feeder.Solve1 == 2)
            {
                SALI1.Checked = true;
            }
            else MCI1.Checked = true;


            if (Feeder.Solve2 == 4)
            {
                OTHI2.Checked = true;
            }
            else if (Feeder.Solve2 == 3)
            {
                PEGI2.Checked = true;
            }
            else if (Feeder.Solve2 == 2)
            {
                SALI2.Checked = true;
            }
            else MCI2.Checked = true; 

            for (int i = 0; i < 16; i++)
            {
                IDB = IDBoxes[i] as TextBox;
                IDB.Text = Feeder.Rats[i].ID.ToString();
                if (Feeder.Rats[i].DoseID == 0)
                {
                    Button BTemp = Injection1Buttons[i] as Button;
                    BTemp.Enabled = false; 
                    IDB.BackColor = Color.Gray; 

                }
                else if (Feeder.Rats[i].DoseID == 1)
                {
                    IDB.BackColor = Injection1ADD.BackColor;
                }
                else
                {
                    IDB.BackColor = Injection2ADD.BackColor;
                }
            }
            TextBox IVB; //temporary textbox
            for (int i=0; i < 16; i++)
            {
                IVB = Inj1VolumeBoxes[i] as TextBox;
                IVB.Text = Feeder.Rats[i].Weight.ToString();
                IVB.TextChanged += new System.EventHandler(this.TextChangedIVB);

            }
               for (int i=0; i < 16; i++)
            {
                IVB = Inj2VolumeBoxes[i] as TextBox;
                IVB.Text = string.Format("{0:0.00}", Math.Round(Feeder.Rats[i].Weight / 5)*0.02);
            }
            Button INJ; 
            for (int i = 0; i < 16; i++)
            {
                INJ = Injection1Buttons[i] as Button;
                INJ.Click += new System.EventHandler(this.Injection1);
            }
            for (int i = 0; i < 16; i++)
            {
                INJ = Injection2Buttons[i] as Button;
                INJ.Click += new System.EventHandler(this.Injection2);
                INJ.Enabled = false; 
            }
            SetInjection = false; 

            Startup = false;
            TextChangedIVB(null, null);
        }
        private void TextChangedIVB(object sender, EventArgs e)
        {
            TextBox IVB;
            TextBox IV2; 
            Double Weight; 
            Double Vol, VolTotA, VolTotB;
            VolTotA = 0;
            VolTotB = 0; 
            if (!Startup)
            {
                for (int i = 0; i < 16; i++)
                {
                    IVB = Inj1VolumeBoxes[i] as TextBox;
                    IV2 = Inj2VolumeBoxes[i] as TextBox;
                    if (Double.TryParse(IVB.Text, out Weight))
                    {
                        Feeder.Rats[i].Weight = Weight;
                        Vol = Math.Round(Feeder.Rats[i].Weight / 5) * 0.02;
                        IV2.Text = string.Format("{0:0.00}", Vol);
                        if (Feeder.Rats[i].DoseID == 1)
                        {
                            VolTotA += Vol;
                        }
                        else if (Feeder.Rats[i].DoseID == 2)
                        {
                            VolTotB += Vol;
                        }
                    }
                    else
                    {
                        IVB.Text = "";
                        IV2.Text = "0.00";
                    }
               }
                MixBoxA.Text =  string.Format("{0:0.00}", VolTotA*1.1);
                MixBoxB.Text = string.Format("{0:0.00}", VolTotB * 1.1);
            }
        }
        private void Injection1(object sender, EventArgs e)   
        {
            string S, Route, Solvent, Command;
            TextBox TempBox;
            TextBox IVTemp;
            Button TempButton; 
            S = sender.ToString();            
            S = S.Substring(S.IndexOf("Text: ") + 5, S.Length - (S.IndexOf("Text: ") + 5));
            int a;
            int.TryParse(S, out a);
            TempBox = IDBoxes[a-1] as TextBox;
            TempButton = Injection1Buttons[a - 1] as Button;
            TempButton.Enabled = false;
            TempBox.BackColor = Color.DarkGray; 
            Route = "";
            Solvent = "";
            Command = "";
            IVTemp = Inj2VolumeBoxes[a-1] as TextBox;
            if (Feeder.Rats[a - 1].DoseID == 1)
            {
                if (IPI1.Checked) Route = "IP";
                if (POI1.Checked) Route = "PO";
                if (IVI1.Checked) Route = "IV";
                if (SCI1.Checked) Route = "SC";
                if (MCI1.Checked) Solvent = "MC";
                if (SALI1.Checked) Solvent = "SAL";
                if (PEGI1.Checked) Solvent = "PEG";
                if (OTHI1.Checked) Solvent = "OTH";
                Command = "Inj1," + a.ToString() + "," + Feeder.Rats[a - 1].ID + "," + Injection1ADD.Text + "," + Dose1.Text + "," + Route + "," + Solvent + "," + IVTemp.Text;
            }
            else if (Feeder.Rats[a - 1].DoseID == 2)
            {
                if (IPI2.Checked) Route = "IP";
                if (POI2.Checked) Route = "PO";
                if (IVI2.Checked) Route = "IV";
                if (SCI2.Checked) Route = "SC";
                if (MCI2.Checked) Solvent = "MC";
                if (SALI2.Checked) Solvent = "SAL";
                if (PEGI2.Checked) Solvent = "PEG";
                if (OTHI2.Checked) Solvent = "OTH";                
                Command = "Inj2," + a.ToString() + "," + Feeder.Rats[a-1].ID + "," + Injection2ADD.Text + "," + Dose2.Text + "," + Route + "," + Solvent + "," + IVTemp.Text;
            }                
            Feeder.Log(Command);
            LastEntry.Text = Command;
        }
        private void Injection2(object sender, EventArgs e)
        {
            string S;
            //string Route, Solvent, Command;
            TextBox TempBox; 
            S = sender.ToString();
            S = S.Substring(S.IndexOf("Text: ") + 5, S.Length - (S.IndexOf("Text: ") + 5));
            int a;
            int.TryParse(S, out a);
            TempBox = IDBoxes[a-1] as TextBox;
             if (Feeder.Rats[a - 1].DoseID == 0)
            {
                Button TempButton = Injection1Buttons[a - 1] as Button;
                TempButton.Enabled = true; 
                TempBox.BackColor = Injection1ADD.BackColor;
                Feeder.Rats[a - 1].DoseID = 1;
            }
            else if (Feeder.Rats[a - 1].DoseID == 1)
            {
                TempBox.BackColor = Injection2ADD.BackColor;
                Feeder.Rats[a - 1].DoseID = 2;
            }
            else 
            {
                Button TempButton = Injection1Buttons[a - 1] as Button;
                TempButton.Enabled = false; 
                TempBox.BackColor = Color.Gray;
                Feeder.Rats[a - 1].DoseID = 0;
            }

            /*
            Route = "";
            Solvent = "";
            if (IPI2.Checked) Route = "IP";
            if (POI2.Checked) Route = "PO";
            if (IVI2.Checked) Route = "IV";
            if (SCI2.Checked) Route = "SC";
            if (MCI2.Checked) Solvent = "MC";
            if (SALI2.Checked) Solvent = "SAL";
            if (PEGI2.Checked) Solvent = "PEG";
            if (OTHI2.Checked) Solvent = "OTH";
            Command = "Inj2," + a.ToString() + "," + Feeder.Rats[a].ID + "," + Injection2ADD.Text + "," + Dose2.Text + "," + Route + "," + Solvent;
            Feeder.Log(Command);
            LastEntry.Text = Command;*/
        }
        private void CreateArrays()
        {
            IDBoxes.Add(IDBox1);
            IDBoxes.Add(IDBox2);
            IDBoxes.Add(IDBox3);
            IDBoxes.Add(IDBox4);
            IDBoxes.Add(IDBox5);
            IDBoxes.Add(IDBox6);
            IDBoxes.Add(IDBox7);
            IDBoxes.Add(IDBox8);
            IDBoxes.Add(IDBox9);
            IDBoxes.Add(IDBox10);
            IDBoxes.Add(IDBox11);
            IDBoxes.Add(IDBox12);
            IDBoxes.Add(IDBox13);
            IDBoxes.Add(IDBox14);
            IDBoxes.Add(IDBox15);
            IDBoxes.Add(IDBox16);

            Inj1VolumeBoxes.Add(Inj1VBox1);
            Inj1VolumeBoxes.Add(Inj1VBox2);
            Inj1VolumeBoxes.Add(Inj1VBox3);
            Inj1VolumeBoxes.Add(Inj1VBox4);
            Inj1VolumeBoxes.Add(Inj1VBox5);
            Inj1VolumeBoxes.Add(Inj1VBox6);
            Inj1VolumeBoxes.Add(Inj1VBox7);
            Inj1VolumeBoxes.Add(Inj1VBox8);
            Inj1VolumeBoxes.Add(Inj1VBox9);
            Inj1VolumeBoxes.Add(Inj1VBox10);
            Inj1VolumeBoxes.Add(Inj1VBox11);
            Inj1VolumeBoxes.Add(Inj1VBox12);
            Inj1VolumeBoxes.Add(Inj1VBox13);
            Inj1VolumeBoxes.Add(Inj1VBox14);
            Inj1VolumeBoxes.Add(Inj1VBox15);
            Inj1VolumeBoxes.Add(Inj1VBox16);

            Inj2VolumeBoxes.Add(Inj2VBox1);
            Inj2VolumeBoxes.Add(Inj2VBox2);
            Inj2VolumeBoxes.Add(Inj2VBox3);
            Inj2VolumeBoxes.Add(Inj2VBox4);
            Inj2VolumeBoxes.Add(Inj2VBox5);
            Inj2VolumeBoxes.Add(Inj2VBox6);
            Inj2VolumeBoxes.Add(Inj2VBox7);
            Inj2VolumeBoxes.Add(Inj2VBox8);
            Inj2VolumeBoxes.Add(Inj2VBox9);
            Inj2VolumeBoxes.Add(Inj2VBox10);
            Inj2VolumeBoxes.Add(Inj2VBox11);
            Inj2VolumeBoxes.Add(Inj2VBox12);
            Inj2VolumeBoxes.Add(Inj2VBox13);
            Inj2VolumeBoxes.Add(Inj2VBox14);
            Inj2VolumeBoxes.Add(Inj2VBox15);
            Inj2VolumeBoxes.Add(Inj2VBox16);

            Injection1Buttons.Add(Inj1Button1);
            Injection1Buttons.Add(Inj1Button2);
            Injection1Buttons.Add(Inj1Button3);
            Injection1Buttons.Add(Inj1Button4);
            Injection1Buttons.Add(Inj1Button5);
            Injection1Buttons.Add(Inj1Button6);
            Injection1Buttons.Add(Inj1Button7);
            Injection1Buttons.Add(Inj1Button8);
            Injection1Buttons.Add(Inj1Button9);
            Injection1Buttons.Add(Inj1Button10);
            Injection1Buttons.Add(Inj1Button11);
            Injection1Buttons.Add(Inj1Button12);
            Injection1Buttons.Add(Inj1Button13);
            Injection1Buttons.Add(Inj1Button14);
            Injection1Buttons.Add(Inj1Button15);
            Injection1Buttons.Add(Inj1Button16);

            Injection2Buttons.Add(Inj2Button1);
            Injection2Buttons.Add(Inj2Button2);
            Injection2Buttons.Add(Inj2Button3);
            Injection2Buttons.Add(Inj2Button4);
            Injection2Buttons.Add(Inj2Button5);
            Injection2Buttons.Add(Inj2Button6);
            Injection2Buttons.Add(Inj2Button7);
            Injection2Buttons.Add(Inj2Button8);
            Injection2Buttons.Add(Inj2Button9);
            Injection2Buttons.Add(Inj2Button10);
            Injection2Buttons.Add(Inj2Button11);
            Injection2Buttons.Add(Inj2Button12);
            Injection2Buttons.Add(Inj2Button13);
            Injection2Buttons.Add(Inj2Button14);
            Injection2Buttons.Add(Inj2Button15);
            Injection2Buttons.Add(Inj2Button16);

        }

        private void Close_Click(object sender, EventArgs e)
        {
            Feeder.ADDC1 = Injection1ADD.Text;
            Feeder.Dose1 = Dose1.Text;
            Feeder.ADDC2 = Injection2ADD.Text;
            Feeder.Dose2 = Dose2.Text; 
            if (IPI1.Checked) Feeder.Route1 = 1;
            if (POI1.Checked) Feeder.Route1 = 2;
            if (IVI1.Checked) Feeder.Route1 = 3;
            if (SCI1.Checked) Feeder.Route1 = 4;
            if (MCI1.Checked) Feeder.Solve1 = 1;
            if (SALI1.Checked) Feeder.Solve1 = 2;
            if (PEGI1.Checked) Feeder.Solve1 = 3;
            if (OTHI1.Checked) Feeder.Solve1 = 4;

            if (IPI2.Checked) Feeder.Route2 = 1;
            if (POI2.Checked) Feeder.Route2 = 2;
            if (IVI2.Checked) Feeder.Route2 = 3;
            if (SCI2.Checked) Feeder.Route2 = 4;

            if (MCI2.Checked) Feeder.Solve2 = 1;
            if (SALI2.Checked) Feeder.Solve2 = 2;
            if (PEGI2.Checked) Feeder.Solve2 = 3;
            if (OTHI2.Checked) Feeder.Solve2 = 4;
            this.Close();
        }

        private void InjectionManager_Load(object sender, EventArgs e)
        {

        }

        private void SetInjectionButton_Click(object sender, EventArgs e)
        {
            Button INJ;
            if (SetInjection)
            {
                SetInjectionButton.BackColor = SystemColors.Control; 
                for (int i = 0; i < 16; i++)
                {
                    INJ = Injection2Buttons[i] as Button;
                    INJ.Enabled = false;
                }
                for (int i = 0; i < 16; i++)
                {
                    INJ = Injection1Buttons[i] as Button;
                    INJ.Enabled = true;
                }
            }
            else
            {
                SetInjectionButton.BackColor = Color.Red;
                for (int i = 0; i < 16; i++)
                {
                    INJ = Injection2Buttons[i] as Button;
                    INJ.Enabled = true;
                }
                for (int i = 0; i < 16; i++)
                {
                    INJ = Injection1Buttons[i] as Button;
                    INJ.Enabled = false;
                }
            }
            SetInjection = !SetInjection;
        }

       
          

        
        

        
       
    }
}
