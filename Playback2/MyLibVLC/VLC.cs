using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
namespace SeizurePlayback
{
    // http://www.videolan.org/developers/vlc/doc/doxygen/html/group__libvlc.html

    static class LibVlc
    {        
        #region core
        [DllImport("libvlc", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr libvlc_new(int argc, [MarshalAs(UnmanagedType.LPArray,
          ArraySubType = UnmanagedType.LPStr)] string[] argv);

        [DllImport("libvlc")]
        public static extern void libvlc_release(IntPtr instance);
        #endregion

        #region media
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_new_location(IntPtr p_instance,
          [MarshalAs(UnmanagedType.LPStr)] string psz_mrl);

        [DllImport("libvlc")]
        public static extern void libvlc_media_release(IntPtr p_meta_desc);

        [DllImport("libvlc")]
        public static extern int libvlc_media_player_set_rate(IntPtr player, float speed);

        [DllImport("libvlc")]
        public static extern float libvlc_media_player_get_rate(IntPtr player);

        [DllImport("libvlc")]
        public static extern float libvlc_media_player_get_fps(IntPtr player);

        [DllImport("libvlc")]
        public static extern long libvlc_media_player_get_length(IntPtr player);

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_set_time(IntPtr player, long time);
        
        [DllImport("libvlc")]
        public static extern long libvlc_media_player_get_time(IntPtr player);	

        #endregion

        #region media player
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_player_new_from_media(IntPtr media);

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_release(IntPtr player);

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_set_hwnd(IntPtr player, IntPtr drawable);

        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_player_get_media(IntPtr player);

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_set_media(IntPtr player, IntPtr media);

        [DllImport("libvlc")]
        public static extern int libvlc_media_player_play(IntPtr player);

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_pause(IntPtr player);

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_stop(IntPtr player);
        #endregion

        #region exception
        [DllImport("libvlc")]
        public static extern void libvlc_clearerr();

        [DllImport("libvlc")]
        public static extern IntPtr libvlc_errmsg();
        #endregion
    }

    class VlcException : Exception
    {
        protected string _err;

        public VlcException()
            : base()
        {
            IntPtr errorPointer = LibVlc.libvlc_errmsg();
            _err = errorPointer == IntPtr.Zero ? "VLC Exception"
                : Marshal.PtrToStringAuto(errorPointer);
        }

        public override string Message { get { return _err; } }
    }

    class VlcInstance : IDisposable
    {
        internal IntPtr Handle;

        public VlcInstance(string[] args)
        {
            Handle = LibVlc.libvlc_new(0, null);
            if (Handle == IntPtr.Zero) throw new VlcException();
        }

        public void Dispose()
        {
            //LibVlc.libvlc_release(Handle);
        }
    }

    class VlcMedia : IDisposable
    {
        internal IntPtr Handle;
        
        public VlcMedia(VlcInstance instance, string url)
        {
            Handle = LibVlc.libvlc_media_new_location(instance.Handle, url);
            if (Handle == IntPtr.Zero) throw new VlcException();
        }

        internal VlcMedia(IntPtr handle)
        {
            this.Handle = handle;
        }

        public void Dispose()
        {
            LibVlc.libvlc_media_release(Handle);
        }
    }

}
