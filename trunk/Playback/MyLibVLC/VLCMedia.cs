using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace SeizurePlayback
{

    class VlcMediaPlayer : IDisposable
    {
        internal IntPtr Handle;
        private IntPtr drawable;
        private bool playing, paused;
        public string FName;
        public string FPath;
        public float fps = 15;
        private int SeizureCount;        

        public VlcMediaPlayer(VlcMedia media)
        {
            Handle = LibVlc.libvlc_media_player_new_from_media(media.Handle);
            if (Handle == IntPtr.Zero) throw new VlcException();
            SeizureCount = 0;
        }

        public long GetLengthMs()
        {
            return LibVlc.libvlc_media_player_get_length(Handle);
        }

        public void Dispose()
        {

            LibVlc.libvlc_media_player_release(Handle);
        }

        public IntPtr Drawable
        {
            get
            {
                return drawable;
            }
            set
            {
                LibVlc.libvlc_media_player_set_hwnd(Handle, value);
                drawable = value;
            }
        }

        public VlcMedia Media
        {
            get
            {
                IntPtr media = LibVlc.libvlc_media_player_get_media(Handle);
                if (media == IntPtr.Zero) return null;
                return new VlcMedia(media);
            }
            set
            {
                LibVlc.libvlc_media_player_set_media(Handle, value.Handle);
            }
        }

        public bool IsPlaying { get { return playing && !paused; } }

        public bool IsPaused { get { return playing && paused; } }

        public bool IsStopped { get { return !playing; } }

        public void Play()
        {
            int ret = LibVlc.libvlc_media_player_play(Handle);
            if (ret == -1)
                throw new VlcException();            
            playing = true;
            paused = false;
        }

        public void Pause()
        {
            LibVlc.libvlc_media_player_pause(Handle);

            if (playing)
                paused ^= true;
        }

        public void Stop()
        {
            LibVlc.libvlc_media_player_stop(Handle);

            playing = false;
            paused = false;
        }

        public void Speed(float Rate)
        {
            int ret = LibVlc.libvlc_media_player_set_rate(Handle, Rate);
            if (ret == -1)
                throw new VlcException();
        }
        public void seek(int time)
        {
            LibVlc.libvlc_media_player_set_time(Handle, time * 1000);
        }

        public void EncSeizureFromNow(int length)
        {
            int frame; 
            long ms;
            if (playing & !paused)
            {
                ms = LibVlc.libvlc_media_player_get_time(Handle);
                frame = (int)((ms * fps)/1000);
                EncodeSeizure(frame, length);
            }
        }
            
        private void EncodeSeizure(int frame, int length)
        {
            Process p = new Process();
            string CmdString;
            SeizureCount++;
            CmdString = " " + FPath + "\\" + FName;
            CmdString += " -o " + FPath + "\\S" + SeizureCount + "_" + FName;
            CmdString += " --seek " + frame + " --frames " + (length * fps).ToString();
            CmdString += " --keyint 25 --crf 12";
            //MessageBox.Show(CmdString);
            p.StartInfo.Arguments = CmdString;
            p.StartInfo.FileName = "C:\\x264\\x264.exe";
            p.Start();
            p.WaitForExit();

        }
    }
}
