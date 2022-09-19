using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManager
{
    public partial class PMEEGView : Form
    {
        int ViewMode = 0; //0 = normal, //1 = gallery, 2 = Animal View
        List<AnimalType> Animals;
        public bool paused = true;

        public PMEEGView(List<AnimalType> AL)
        {
            InitializeComponent();
            Animals = AL;
        }

        private void NNext_MouseDown(object sender, MouseEventArgs e)
        {
            
            NNext.BackgroundImage.Dispose();
            NNext.BackgroundImage = NextIList.Images[1];
        }

        private void NNext_MouseUp(object sender, MouseEventArgs e)
        {
            NNext.BackgroundImage.Dispose();
            NNext.BackgroundImage = NextIList.Images[2];
        }

        private void NNext_MouseHover(object sender, EventArgs e)
        {
            NNext.BackgroundImage.Dispose();
            NNext.BackgroundImage = NextIList.Images[2];
        }

        private void NNext_MouseLeave(object sender, EventArgs e)
        {
            NNext.BackgroundImage.Dispose();
            NNext.BackgroundImage = NextIList.Images[0];
        }

        private void NPrev_MouseDown(object sender, MouseEventArgs e)
        {
            NPrev.BackgroundImage.Dispose();
            NPrev.BackgroundImage = PrevIList.Images[1];
        }

        private void NPrev_MouseHover(object sender, EventArgs e)
        {
            NPrev.BackgroundImage.Dispose();
            NPrev.BackgroundImage = PrevIList.Images[2];
        }

        private void NPrev_MouseLeave(object sender, EventArgs e)
        {
            NPrev.BackgroundImage.Dispose();
            NPrev.BackgroundImage = PrevIList.Images[0];
        }

        private void NPrev_MouseUp(object sender, MouseEventArgs e)
        {
            NPrev.BackgroundImage.Dispose();
            NPrev.BackgroundImage = PrevIList.Images[2];
        }

        private void normalListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (sender.ToString())
            {
                case "Default":
                    ViewMode = 0;
                    DefaultView.Checked = true;
                    animalView.Checked = false;
                    galleryView.Checked = false;

                    refreshScreen();
                    
                        break;
                case "Gallery":
                    ViewMode = 1;
                    DefaultView.Checked = false;
                    animalView.Checked = false;
                    galleryView.Checked = true;
                    refreshScreen();
                    
                    break;
                case "Animal":
                    ViewMode = 2;
                    DefaultView.Checked = false;
                    animalView.Checked = true;
                    galleryView.Checked = false;
                    refreshScreen();
                    
                    break;
            }


        }

        public void refreshScreen()
        {

        }

        private void GalleryBox_Enter(object sender, EventArgs e)
        {

        }

        private void PlayPauseButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (paused)
            {
                paused = false;
                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[2];
            }
            else
            {
                paused = true;
                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[5];
            }
        }

        private void PlayPauseButton_Click(object sender, EventArgs e)
        {

        }

        private void PlayPauseButton_MouseHover(object sender, EventArgs e)
        {
            if (paused)
            {
                
                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[1];
            }
            else
            {
                
                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[4];
            }
        }

        private void PlayPauseButton_MouseLeave(object sender, EventArgs e)
        {
            if (paused)
            {

                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[0];
            }
            else
            {

                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[3];
            }
        }

        private void PlayPauseButton_MouseHover(object sender, MouseEventArgs e)
        {
            if (paused)
            {

                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[1];
            }
            else
            {

                PlayPauseButton.BackgroundImage.Dispose();
                PlayPauseButton.BackgroundImage = PausePlayList.Images[4];
            }
        }
    }
}
