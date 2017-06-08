using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    internal class GridViewHeader: System.Windows.Forms.Panel
    {
        public GridViewHeader()
            : base()
        {
            base.SetStyle(
                   ControlStyles.UserPaint |
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.ResizeRedraw |
                   ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();

            this.BackColor = Color.White;
        }

        Color[] _colors = {Color.FromArgb(255, 56, 180, 75),
                            Color.FromArgb(255, 110, 198, 64)
                         };

        private int _designHeigh = 35;

        public int DesignHeight
        {
            get { return this._designHeigh; }
        }


        private string[] _titles;

        public string[] Titles
        {
            get { return _titles; }
            set
            {
                _titles = value;
                this.Invalidate();
            }
        }

        private float[] _widthRate;
        public float[] WidthRate
        {
            get { return this._widthRate; }
            set
            {
                this._widthRate = value;
                this.Invalidate();
            }
        }
           

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;

            if (this.Titles == null || this.Titles.Length == 0)
            {
                return;
            }

            Rectangle[] rects = GetItemsRect();
            for (int i = 0; i < rects.Length; i++)
            {
                Rectangle rect = rects[i];


                using (Brush b = new SolidBrush(System.Drawing.Color.FromArgb(255,255,255,255)))
                {
                    g.FillRectangle(b, rect);
                }

                using (Brush b = new SolidBrush(_colors[i % 2]))
                {
                    g.FillRectangle(b, new Rectangle(rect.Location, new Size(rect.Width, rect.Height - 1)));
                }

                this.Font = new System.Drawing.Font(this.Font, FontStyle.Bold);
                SizeF fontSize = g.MeasureString(this.Titles[i], this.Font);
                PointF fontLocation = new PointF(rect.Left + (rect.Width - fontSize.Width) / 2, rect.Top + (rect.Height - fontSize.Height) / 2);

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(30, Color.Black)))
                {
                    g.DrawString(this.Titles[i], this.Font, brush, new PointF(fontLocation.X, fontLocation.Y - 2));
                }
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(80, Color.Black)))
                {
                    g.DrawString(this.Titles[i], this.Font, brush, new PointF(fontLocation.X, fontLocation.Y - 1));
                }

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, Color.White)))
                {
                    g.DrawString(this.Titles[i], this.Font, brush, new PointF(fontLocation.X, fontLocation.Y));
                }

			}
            g.SmoothingMode = SmoothingMode.Default;
            g.CompositingQuality = CompositingQuality.Default;

        }

        private Rectangle[] GetItemsRect()
        {
            Queue<Rectangle> rects = new Queue<Rectangle>();

            int scrollWidth = 23;
            int startMemory = 0;
            for (int i = 0; i < Titles.Length; i++)
            {
                int start = startMemory + (i == 0 ? 0: 1);
                int avgWidth = Convert.ToInt32(Math.Floor((float)(this.ClientSize.Width - scrollWidth) / this.Titles.Length));
                int width = this.WidthRate.Length > i ? Convert.ToInt32((this.ClientSize.Width - scrollWidth) * this.WidthRate[i]) : avgWidth;

                if (i == Titles.Length - 1)
                {
                    width = (this.ClientSize.Width - scrollWidth) - startMemory;
                }
                startMemory += (i == 0 ? 0: 1) + width;
                rects.Enqueue(new Rectangle(start, 0, width, this.ClientSize.Height));
            }

            return rects.ToArray();
        }
    }
}
