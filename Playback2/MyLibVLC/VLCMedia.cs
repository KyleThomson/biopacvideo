using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace SeizurePlayback
{

    public class VlcMediaPlayer : IDisposable
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
                paused = true;
        }

        public void Stop()
        {
            LibVlc.libvlc_media_player_stop(Handle);

            playing = false;
            paused = false;
        }
        public void NextFrame()
        {
            LibVlc.libvlc_media_player_next_frame(Handle);
        }
        public void Speed(float Rate)
        {
            int ret = LibVlc.libvlc_media_player_set_rate(Handle, Rate);
            if (ret == -1)
                throw new VlcException();
        }
        public void seek(long time)
        {
            
                LibVlc.libvlc_media_player_set_time(Handle, time);
            
        }
        public long getpos()
        {
            return LibVlc.libvlc_media_player_get_time(Handle);
        }        
            
        public void EncodeSeizure(int StartTime, int length, string infile, string outfile)
        {
           

        }
    }
}
