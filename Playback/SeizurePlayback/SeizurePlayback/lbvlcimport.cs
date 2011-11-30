using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Runtime.InteropServices;
 
namespace SeizurePlayback
{
  // http://www.videolan.org/developers/vlc/doc/doxygen/html/group__libvlc.html
 
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  struct libvlc_exception_t
  {
    public int b_raised;
    public int i_code;
    [MarshalAs(UnmanagedType.LPStr)]
    public string psz_message;
  }
 
  static class LibVlc
  {
    #region core
    [DllImport("libvlc")]
    public static extern IntPtr libvlc_new(int argc, [MarshalAs(UnmanagedType.LPArray,
      ArraySubType = UnmanagedType.LPStr)] string[] argv, ref libvlc_exception_t ex);
 
    [DllImport("libvlc")]
    public static extern void libvlc_release(IntPtr instance);
    #endregion
 
    #region media
    [DllImport("libvlc")]
    public static extern IntPtr libvlc_media_new(IntPtr p_instance,
      [MarshalAs(UnmanagedType.LPStr)] string psz_mrl, ref libvlc_exception_t p_e);
 
    [DllImport("libvlc")]
    public static extern void libvlc_media_release(IntPtr p_meta_desc);
    #endregion
 
    #region media player
    [DllImport("libvlc")]
    public static extern IntPtr libvlc_media_player_new_from_media(IntPtr media,
      ref libvlc_exception_t ex);
 
    [DllImport("libvlc")]
    public static extern void libvlc_media_player_release(IntPtr player);
 
    [DllImport("libvlc")]
    public static extern void libvlc_media_player_set_drawable(IntPtr player, IntPtr drawable,
      ref libvlc_exception_t p_e);
 
    [DllImport("libvlc")]
    public static extern void libvlc_media_player_play(IntPtr player, ref libvlc_exception_t ex);
 
    [DllImport("libvlc")]
    public static extern void libvlc_media_player_pause(IntPtr player, ref libvlc_exception_t ex);
 
    [DllImport("libvlc")]
    public static extern void libvlc_media_player_stop(IntPtr player, ref libvlc_exception_t ex);
    #endregion
 
    #region exception
    [DllImport("libvlc")]
    public static extern void libvlc_exception_init(ref libvlc_exception_t p_exception);
 
    [DllImport("libvlc")]
    public static extern int libvlc_exception_raised(ref libvlc_exception_t p_exception);
 
    [DllImport("libvlc")]
    public static extern string libvlc_exception_get_message(ref libvlc_exception_t p_exception);
    #endregion
  }
}
