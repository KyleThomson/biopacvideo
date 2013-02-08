using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using System.Runtime.InteropServices;

namespace SeizurePlayback
{

    public delegate void NewFrameEventHandler(Bitmap frame);

    internal sealed unsafe class MemoryRenderer 
    {
        IntPtr Handle;
        NewFrameEventHandler m_callback = null;
        BitmapFormat m_format;
        
        object m_lock = new object();
        List<Delegate> m_callbacks = new List<Delegate>();

        IntPtr pLockCallback;
        IntPtr pUnlockCallback;
        IntPtr pDisplayCallback;

        GCHandle m_pixelDataPtr = default(GCHandle);
        PixelData m_pixelData;
        void* m_pBuffer = null;

        public MemoryRenderer(IntPtr hMediaPlayer)
        {
            Handle = hMediaPlayer;

            LibVlc.LockEventHandler leh = OnpLock;
            LibVlc.UnlockEventHandler ueh = OnpUnlock;
            LibVlc.DisplayEventHandler deh = OnpDisplay;

            pLockCallback = Marshal.GetFunctionPointerForDelegate(leh);
            pUnlockCallback = Marshal.GetFunctionPointerForDelegate(ueh);
            pDisplayCallback = Marshal.GetFunctionPointerForDelegate(deh);

            m_callbacks.Add(leh);
            m_callbacks.Add(deh);
            m_callbacks.Add(ueh);
            
        }
        public void Play()
        {
            LibVlc.libvlc_media_player_play(Handle);
        }
         public void Pause()
        {
            LibVlc.libvlc_media_player_pause(Handle);
         }
         public void NextFrame()
         {
             LibVlc.libvlc_media_player_next_frame(Handle);
         }
         public void seek(long time)
         {

             LibVlc.libvlc_media_player_set_time(Handle, time);

         }
        unsafe void* OnpLock(void* opaque, void** plane)
        {
            PixelData* px = (PixelData*)opaque;
            *plane = px->pPixelData;
            return null;
        }
       
        unsafe void OnpUnlock(void* opaque, void* picture, void** plane)
        {

        }

        unsafe void OnpDisplay(void* opaque, void* picture)
        {
            lock (m_lock)
            {
                PixelData* px = (PixelData*)opaque;
                MemoryHeap.CopyMemory(m_pBuffer, px->pPixelData, px->size);
                
                if (m_callback != null)
                {
                    using (Bitmap frame = GetBitmap())
                    {
                        m_callback(frame);
                    }
                }
            }
        }

        private Bitmap GetBitmap()
        {
            return new Bitmap(m_format.Width, m_format.Height, m_format.Pitch, (System.Drawing.Imaging.PixelFormat)m_format.PixelFormat, new IntPtr(m_pBuffer));
        }
        

        public void SetCallback(NewFrameEventHandler callback)
        {
            m_callback = callback;
        }

        public void SetFormat(BitmapFormat format)
        {
            m_format = format;

            LibVlc.libvlc_video_set_format(Handle, m_format.ChromaStr(), m_format.Width, m_format.Height, m_format.Pitch);
            m_pBuffer = MemoryHeap.Alloc(m_format.ImageSize);

            m_pixelData = new PixelData(m_format.ImageSize);
            m_pixelDataPtr = GCHandle.Alloc(m_pixelData, GCHandleType.Pinned);
            LibVlc.libvlc_video_set_callbacks(Handle, pLockCallback, pUnlockCallback, pDisplayCallback, m_pixelDataPtr.AddrOfPinnedObject());
        }

      

        public Bitmap CurrentFrame
        {
            get
            {
                lock (m_lock)
                {
                    return GetBitmap();
                }
            }
        }

        
        /*
        protected override void Dispose(bool disposing)
        {
            IntPtr zero = IntPtr.Zero;
            LibVlc.libvlc_video_set_format_callbacks(m_hMediaPlayer, zero, zero, zero, zero);

            m_pixelDataPtr.Free();
            m_pixelData.Dispose();

            MemoryHeap.Free(m_pBuffer);

            if (disposing)
            {                
                m_callback = null;
                m_callbacks.Clear();
            }
        }*/
    }

    internal unsafe struct PixelData : IDisposable
    {
        public byte* pPixelData;
        public int size;

        public PixelData(int size)
        {
            this.size = size;
            this.pPixelData = (byte*)MemoryHeap.Alloc(size);
        }
        

        public void Dispose()
        {
            MemoryHeap.Free(this.pPixelData);
        }
        

        public static bool operator ==(PixelData pd1, PixelData pd2)
        {
            return (pd1.size == pd2.size && pd1.pPixelData == pd2.pPixelData);
        }

        public static bool operator !=(PixelData pd1, PixelData pd2)
        {
            return !(pd1 == pd2);
        }

        public override int GetHashCode()
        {
            return size.GetHashCode() ^ pPixelData->GetHashCode();
        }

        public override bool Equals(object obj)
        {
            PixelData pd = (PixelData)obj;
            if (pd == null)
            {
                return false;
            }

            return this == pd;
        }
    }

    internal unsafe class MemoryHeap
    {
        static int ph = GetProcessHeap();

        private MemoryHeap() { }

        /// <summary>
        /// Allocates a memory block of the given size. The allocated memory is
        /// automatically initialized to zero.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static void* Alloc(int size)
        {
            void* result = HeapAlloc(ph, HEAP_ZERO_MEMORY, size);
            if (result == null)
            {
                throw new OutOfMemoryException();
            }

            return result;
        }

        /// <summary>
        /// Frees a memory block.
        /// </summary>
        /// <param name="block"></param>
        public static void Free(void* block)
        {
            if (!HeapFree(ph, 0, block))
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Re-allocates a memory block. If the reallocation request is for a
        /// larger size, the additional region of memory is automatically
        /// initialized to zero.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static void* ReAlloc(void* block, int size)
        {
            void* result = HeapReAlloc(ph, HEAP_ZERO_MEMORY, block, size);
            if (result == null)
            {
                throw new OutOfMemoryException();
            }

            return result;
        }

        /// <summary>
        /// Returns the size of a memory block.
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public static int SizeOf(void* block)
        {
            int result = HeapSize(ph, 0, block);
            if (result == -1)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        // Heap API flags
        const int HEAP_ZERO_MEMORY = 0x00000008;

        // Heap API functions
        [DllImport("kernel32")]
        static extern int GetProcessHeap();

        [DllImport("kernel32")]
        static extern void* HeapAlloc(int hHeap, int flags, int size);

        [DllImport("kernel32")]
        static extern bool HeapFree(int hHeap, int flags, void* block);

        [DllImport("kernel32")]
        static extern void* HeapReAlloc(int hHeap, int flags, void* block, int size);

        [DllImport("kernel32")]
        static extern int HeapSize(int hHeap, int flags, void* block);

        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = true)]
        public static unsafe extern void CopyMemory(void* dest, void* src, int size);
    }
    [Serializable]
    public class BitmapFormat
    {
        /// <summary>
        /// Initializes new instance of BitmapFormat class
        /// </summary>
        /// <param name="width">The width of the bitmap in pixels</param>
        /// <param name="height">The height of the bitmap in pixels</param>
        /// <param name="chroma">Chroma type of the bitmap</param>
        public BitmapFormat(int width, int height, ChromaType chroma)
        {
            Width = width;
            Height = height;
            ChromaType = chroma;
            Planes = 1;
            PlaneSizes = new int[3];

            Init();

            Chroma = ChromaType.ToString();
            if (IsRGB)
            {
                Pitch = Width * BitsPerPixel / 8;
                PlaneSizes[0] = ImageSize = Pitch * Height;
                Pitches = new int[1] { Pitch };
                Lines = new int[1] { Height };
            }            

        }
        public byte[] ChromaStr()
        {
            if (string.IsNullOrEmpty(ChromaType.ToString()))
            {
                return null;
            }

            return Encoding.UTF8.GetBytes(ChromaType.ToString());
        }
        private void Init()
        {
            switch (ChromaType)
            {
                case ChromaType.RV15:
                    PixelFormat = PixelFormat.Format16bppRgb555;
                    BitsPerPixel = 16;
                    break;

                case ChromaType.RV16:
                    PixelFormat = PixelFormat.Format16bppRgb565;
                    BitsPerPixel = 16;
                    break;

                case ChromaType.RV24:
                    PixelFormat = PixelFormat.Format24bppRgb;
                    BitsPerPixel = 24;
                    break;

                case ChromaType.RV32:
                    PixelFormat = PixelFormat.Format32bppRgb;
                    BitsPerPixel = 32;
                    break;

                case ChromaType.RGBA:
                    PixelFormat = PixelFormat.Format32bppArgb;
                    BitsPerPixel = 32;
                    break;

                case ChromaType.NV12:
                    BitsPerPixel = 12;
                    Planes = 2;
                    PlaneSizes[0] = Width * Height;
                    PlaneSizes[1] = Width * Height / 2;
                    Pitches = new int[2] { Width, Width };
                    Lines = new int[2] { Height, Height / 2 };
                    ImageSize = PlaneSizes[0] + PlaneSizes[1];
                    break;

                case ChromaType.I420:
                case ChromaType.YV12:
                case ChromaType.J420:
                    BitsPerPixel = 12;
                    Planes = 3;
                    PlaneSizes[0] = Width * Height;
                    PlaneSizes[1] = PlaneSizes[2] = Width * Height / 4;
                    Pitches = new int[3] { Width, Width / 2, Width / 2 };
                    Lines = new int[3] { Height, Height / 2, Height / 2 };
                    ImageSize = PlaneSizes[0] + PlaneSizes[1] + PlaneSizes[2];
                    break;

                case ChromaType.YUY2:
                case ChromaType.UYVY:
                    BitsPerPixel = 16;
                    PlaneSizes[0] = Width * Height * 2;
                    Pitches = new int[1] { Width * 2 };
                    Lines = new int[1] { Height };
                    ImageSize = PlaneSizes[0];
                    break;

                default:
                    throw new ArgumentException("Unsupported chroma type " + ChromaType);
            }
        }

        /// <summary>
        /// Gets the size in bytes of the scan line 
        /// </summary>
        public int Pitch { get; private set; }

        /// <summary>
        /// Gets the size of the image in bytes
        /// </summary>
        public int ImageSize { get; private set; }

        /// <summary>
        /// Gets the chroma type string
        /// </summary>
        public string Chroma { get; private set; }

        /// <summary>
        /// Gets the pixel format of the bitmap. Valid only for RGB formats.
        /// </summary>
        public PixelFormat PixelFormat { get; private set; }

        /// <summary>
        /// Gets the width of the bitmap
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height of the bitmap
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets number of bits used for a pixel according to ChromaType
        /// </summary>
        public int BitsPerPixel { get; private set; }

        /// <summary>
        /// Gets value indication whether the format contains more than one pixel plane
        /// </summary>
        public bool IsPlanarFormat
        {
            get
            {
                return ChromaType == ChromaType.I420 ||
                       ChromaType == ChromaType.NV12 ||
                       ChromaType == ChromaType.YV12 ||
                       ChromaType == ChromaType.J420;
            }
        }

        /// <summary>
        /// Gets value indicating whether the format is packed RGB
        /// </summary>
        public bool IsRGB
        {
            get
            {
                return ChromaType == ChromaType.RV15 ||
                       ChromaType == ChromaType.RV16 ||
                       ChromaType == ChromaType.RV24 ||
                       ChromaType == ChromaType.RV32 ||
                       ChromaType == ChromaType.RGBA;
            }
        }

        /// <summary>
        /// Gets number of pixel planes
        /// </summary>
        public int Planes { get; private set; }

        /// <summary>
        /// Gets array of pixel plane's sizes
        /// </summary>
        public int[] PlaneSizes { get; private set; }

        /// <summary>
        /// Gets array of pitch size per pixel plane
        /// </summary>
        public int[] Pitches { get; private set; }

        /// <summary>
        /// Gets array of scan lines (height) per pixel plane
        /// </summary>
        public int[] Lines { get; private set; }

        /// <summary>
        /// Gets the pixel format of the video frame
        /// </summary>
        public ChromaType ChromaType { get; private set; }
    }
    public enum ChromaType
    {
        /// <summary>
        /// 5 bit for each RGB channel
        /// </summary>
        RV15,

        /// <summary>
        /// 5 bit Red, 6 bit Green and 5 bit Blue
        /// </summary>
        RV16,

        /// <summary>
        /// 8 bit per channel
        /// </summary>
        RV24,

        /// <summary>
        /// 8 bit per RGB channel and 8 bit unused
        /// </summary>
        RV32,

        /// <summary>
        /// 8 bit per each RGBA channel
        /// </summary>
        RGBA,

        /// <summary>
        /// 12 bits per pixel planar format with Y plane followed by V and U planes
        /// </summary>
        YV12,

        /// <summary>
        /// Same as YV12 but V and U are swapped
        /// </summary>
        I420,

        /// <summary>
        /// 12 bits per pixel planar format with Y plane and interleaved UV plane
        /// </summary>
        NV12,

        /// <summary>
        /// 16 bits per pixel packed YUYV array
        /// </summary>
        YUY2,

        /// <summary>
        /// 16 bits per pixel packed UYVY array 
        /// </summary>
        UYVY,

        /// <summary>
        /// Same as I420, mainly used with MJPG codecs
        /// </summary>
        J420
    }
    // Summary:
    //     Specifies the format of the color data for each pixel in the image.
    public enum PixelFormat
    {
        // Summary:
        //     No pixel format is specified.
        DontCare = 0,
        //
        // Summary:
        //     The pixel format is undefined.
        Undefined = 0,
        //
        // Summary:
        //     The maximum value for this enumeration.
        Max = 15,
        //
        // Summary:
        //     The pixel data contains color-indexed values, which means the values are
        //     an index to colors in the system color table, as opposed to individual color
        //     values.
        Indexed = 65536,
        //
        // Summary:
        //     The pixel data contains GDI colors.
        Gdi = 131072,
        //
        // Summary:
        //     Specifies that the format is 16 bits per pixel; 5 bits each are used for
        //     the red, green, and blue components. The remaining bit is not used.
        Format16bppRgb555 = 135173,
        //
        // Summary:
        //     Specifies that the format is 16 bits per pixel; 5 bits are used for the red
        //     component, 6 bits are used for the green component, and 5 bits are used for
        //     the blue component.
        Format16bppRgb565 = 135174,
        //
        // Summary:
        //     Specifies that the format is 24 bits per pixel; 8 bits each are used for
        //     the red, green, and blue components.
        Format24bppRgb = 137224,
        //
        // Summary:
        //     Specifies that the format is 32 bits per pixel; 8 bits each are used for
        //     the red, green, and blue components. The remaining 8 bits are not used.
        Format32bppRgb = 139273,
        //
        // Summary:
        //     Specifies that the pixel format is 1 bit per pixel and that it uses indexed
        //     color. The color table therefore has two colors in it.
        Format1bppIndexed = 196865,
        //
        // Summary:
        //     Specifies that the format is 4 bits per pixel, indexed.
        Format4bppIndexed = 197634,
        //
        // Summary:
        //     Specifies that the format is 8 bits per pixel, indexed. The color table therefore
        //     has 256 colors in it.
        Format8bppIndexed = 198659,
        //
        // Summary:
        //     The pixel data contains alpha values that are not premultiplied.
        Alpha = 262144,
        //
        // Summary:
        //     The pixel format is 16 bits per pixel. The color information specifies 32,768
        //     shades of color, of which 5 bits are red, 5 bits are green, 5 bits are blue,
        //     and 1 bit is alpha.
        Format16bppArgb1555 = 397319,
        //
        // Summary:
        //     The pixel format contains premultiplied alpha values.
        PAlpha = 524288,
        //
        // Summary:
        //     Specifies that the format is 32 bits per pixel; 8 bits each are used for
        //     the alpha, red, green, and blue components. The red, green, and blue components
        //     are premultiplied, according to the alpha component.
        Format32bppPArgb = 925707,
        //
        // Summary:
        //     Reserved.
        Extended = 1048576,
        //
        // Summary:
        //     The pixel format is 16 bits per pixel. The color information specifies 65536
        //     shades of gray.
        Format16bppGrayScale = 1052676,
        //
        // Summary:
        //     Specifies that the format is 48 bits per pixel; 16 bits each are used for
        //     the red, green, and blue components.
        Format48bppRgb = 1060876,
        //
        // Summary:
        //     Specifies that the format is 64 bits per pixel; 16 bits each are used for
        //     the alpha, red, green, and blue components. The red, green, and blue components
        //     are premultiplied according to the alpha component.
        Format64bppPArgb = 1851406,
        //
        // Summary:
        //     The default pixel format of 32 bits per pixel. The format specifies 24-bit
        //     color depth and an 8-bit alpha channel.
        Canonical = 2097152,
        //
        // Summary:
        //     Specifies that the format is 32 bits per pixel; 8 bits each are used for
        //     the alpha, red, green, and blue components.
        Format32bppArgb = 2498570,
        //
        // Summary:
        //     Specifies that the format is 64 bits per pixel; 16 bits each are used for
        //     the alpha, red, green, and blue components.
        Format64bppArgb = 3424269,
    }
}
