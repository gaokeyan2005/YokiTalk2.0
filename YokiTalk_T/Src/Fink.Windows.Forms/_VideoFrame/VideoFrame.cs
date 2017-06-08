using Fink.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    public class VideoFrame : System.Windows.Forms.Panel
    {
        public VideoFrame()
        {
            //this.BackColor = Color.FromArgb(10, 255, 255, 255);
            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
            //this.BackColor = Color.FromArgb(100, 100, 100, 100);

        }



        private int conerRaduis = 0;
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //Rectangle outerShadowRect = e.ClipRectangle;
            //using (GraphicsPath path = RectangleEx.CreatePath(outerShadowRect, conerRaduis, RoundStyle.All))
            //{
            //    using (Brush brush = new SolidBrush(Color.FromArgb(255, 233, 233, 233)))
            //    {
            //        g.PixelOffsetMode = PixelOffsetMode.Half;
            //        g.FillPath(brush, path);
            //        g.PixelOffsetMode = PixelOffsetMode.Default;

            //    }
            //}

            Rectangle mainShadowRect = e.ClipRectangle;
            mainShadowRect.Inflate(new Size(-1, -1));
            using (GraphicsPath path = RectangleEx.CreatePath(mainShadowRect, conerRaduis, RoundStyle.All))
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(255, 221, 221, 221)))
                {
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;

                }
            }



            Rectangle mainRect = mainShadowRect;
            mainRect.Inflate(new Size(-1, -1));
            using (GraphicsPath path = RectangleEx.CreatePath(mainRect, conerRaduis, RoundStyle.All))
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(255, 255, 255, 255)))
                {
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;

                }
            }


            Rectangle contentRect = mainRect;
            contentRect.Inflate(new Size(-4, -4));
            using (GraphicsPath path = RectangleEx.CreatePath(contentRect, conerRaduis, RoundStyle.All))
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(255, 255, 0, 0)))
                {
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;

                }
            }

        }
    }
}
