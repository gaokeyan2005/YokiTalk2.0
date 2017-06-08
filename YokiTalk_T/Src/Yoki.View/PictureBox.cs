using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IM.View
{
    public class PictureBox: System.Windows.Forms.PictureBox
    {
        public PictureBox()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
        }

        public NIM.ReceiveVDataInfo Info
        {
            get;
            set;
        }


        public byte[] CacheData
        {
            get;
            set;
        }

        public void Clear()
        {
            Graphics g = this.CreateGraphics();
            g.Clear(this.BackColor);
            this.CacheData = null;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            if (this.CacheData!= null)
            {
                Rectangle rect = GetFillParams(Info.Width, Info.Height, this.Width, this.Height);

                Graphics g = e.Graphics;
                IntPtr hdc = g.GetHdc();


                int oldStretchMode = NIM.MultimediaManager.SetStretchBltMode(hdc, NIM.MultimediaManager.HALFTONE);  // high quality

                NIM.MultimediaManager.StretchDIBits(
                    hdc,
                    rect.X,
                    rect.Y,
                    rect.Width,
                    rect.Height,
                    0,
                    0,
                    Info.Width,
                    Info.Height,
                    CacheData,
                    Info.BitmapInfoPtr,
                    NIM.MultimediaManager.DIB_RGB_COLORS,
                    NIM.MultimediaManager.SRCCOPY);

                NIM.MultimediaManager.SetStretchBltMode(hdc, oldStretchMode);


                g.ReleaseHdc(hdc);
            }


            //e.Graphics.FillRectangle(System.Drawing.Brushes.Red, new System.Drawing.Rectangle(0, 0, 10, 10));
        }


        private Rectangle GetFillParams(int ow, int oh, int pw, int ph)
        {
            double rateW = (double)ow / pw;
            double rateH = (double)oh / ph;

            double rate = Math.Max(rateW, rateH);

            int width = (int)Math.Floor(ow / rate);
            int height = (int)Math.Floor(oh / rate);

            int x = (pw - width) / 2;
            int y = (ph - height) / 2;



            return new Rectangle(x, y, width, height);
        }
    }
}
