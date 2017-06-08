using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.IM
{
    class DIB
    {

        // BITMAPINFO with embedded BITMAPINFOHEADER to simplify the code
        struct BITMAPINFO
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;

            public int bmiColors;
        };

        //struct RGBQUAD
        //{
        //    public byte rgbBlue;
        //    public byte rgbGreen;
        //    public byte rgbRed;
        //    public byte rgbReserved;
        //};

        public const int COLORONCOLOR = 3;
        public const int HALFTONE = 4;
        public const int DIB_RGB_COLORS = 0;
        public const int SRCCOPY = 0x00CC0020;

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", SetLastError = true)]
        public static extern int StretchDIBits(
            IntPtr hdc,                      // handle to DC
            int XDest,                    // x-coord of destination upper-left corner
            int YDest,                    // y-coord of destination upper-left corner
            int nDestWidth,               // width of destination rectangle
            int nDestHeight,              // height of destination rectangle
            int XSrc,                     // x-coord of source upper-left corner
            int YSrc,                     // y-coord of source upper-left corner
            int nSrcWidth,                // width of source rectangle
            int nSrcHeight,               // height of source rectangle
            IntPtr lpBits,                // bitmap bits
            IntPtr lpBitsInfo,            // bitmap data
            int iUsage,                   // usage options
            int dwRop);                   // raster operation code

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", SetLastError = true)]
        public static extern int StretchDIBits(
            IntPtr hdc,                      // handle to DC
            int XDest,                    // x-coord of destination upper-left corner
            int YDest,                    // y-coord of destination upper-left corner
            int nDestWidth,               // width of destination rectangle
            int nDestHeight,              // height of destination rectangle
            int XSrc,                     // x-coord of source upper-left corner
            int YSrc,                     // y-coord of source upper-left corner
            int nSrcWidth,                // width of source rectangle
            int nSrcHeight,               // height of source rectangle
            byte[] lpBits,                // bitmap bits
            IntPtr lpBitsInfo,            // bitmap data
            int iUsage,                   // usage options
            int dwRop);                   // raster operation code

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", SetLastError = true)]
        public static extern int SetStretchBltMode(IntPtr hdc, int iStretchMode);

        [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
        public static extern int SelectClipRgn(IntPtr hDC, IntPtr hRgn);


        // allocate BITMAPINFO and color palette in unmanaged memory
        private BITMAPINFO CreateBitmapInfo(int imageWidth, int imageHeight)
        {
            BITMAPINFO info = new BITMAPINFO();

            info.biSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(BITMAPINFO)) - System.Runtime.InteropServices.Marshal.SizeOf(typeof(Int32));  // sizeof BITMAPINFOHEADER
            info.biWidth = imageWidth;
            info.biHeight = imageHeight;
            info.biPlanes = 1;
            info.biBitCount = 32;
            info.biCompression = 0;     // BI_RGB
            info.biSizeImage = imageWidth * imageHeight * 4;
            info.biXPelsPerMeter = 0;
            info.biYPelsPerMeter = 0;
            info.biClrUsed = 0;
            info.biClrImportant = 0;
            info.bmiColors = 0;         // to prevent warning


            return info;
        }
    }
}
