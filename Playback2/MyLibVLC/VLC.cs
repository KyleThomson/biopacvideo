using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
namespace SeizurePlayback
{
    
    static class LibVlc
    {        
        #region core
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_new(int argc, [MarshalAs(UnmanagedType.LPArray,
          ArraySubType = UnmanagedType.LPStr)] string[] argv);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_release(IntPtr instance);
        #endregion

        #region media
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_media_new_location(IntPtr p_instance,
          [MarshalAs(UnmanagedType.LPStr)] string psz_mrl);


        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 libvlc_media_get_duration(IntPtr p_meta_desc);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_release(IntPtr p_meta_desc);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_set_rate(IntPtr player, float speed);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern float libvlc_media_player_get_rate(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern float libvlc_media_player_get_fps(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern long libvlc_media_player_get_length(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_time(IntPtr player, long time);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern long libvlc_media_player_get_time(IntPtr player);	

        #endregion

        #region media player
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_media_player_new_from_media(IntPtr media);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_release(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_hwnd(IntPtr player, IntPtr drawable);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_media_player_get_media(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_media(IntPtr player, IntPtr media);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_play(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_is_seekable(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_pause(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_stop(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_next_frame(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_format(IntPtr player, string Chroma, int Width, int Height, int Pitch);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_format(IntPtr player, [MarshalAs(UnmanagedType.LPArray)] byte[] Chroma, int Width, int Height, int Pitch);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_callbacks(IntPtr player, IntPtr @lock, IntPtr unlock, IntPtr display, IntPtr opaque);
		
	
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void* LockEventHandler(void* opaque, void** plane);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void UnlockEventHandler(void* opaque, void* picture, void** plane);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void DisplayEventHandler(void* opaque, void* picture);


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

    public class VlcInstance : IDisposable
    {
        internal IntPtr Handle;

        public VlcInstance(string[] args)
        {
           Handle = LibVlc.libvlc_new(0, null);           
            //Handle = LibVlc.libvlc_new(1, args);
            if (Handle == IntPtr.Zero) throw new VlcException();
        }

        public void Dispose()
        {
            //LibVlc.libvlc_release(Handle);
        }
    }

    public class VlcMedia : IDisposable
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
        public Int64 GetDuration()
        {
            return (LibVlc.libvlc_media_get_duration(Handle));
        }
    }

}
