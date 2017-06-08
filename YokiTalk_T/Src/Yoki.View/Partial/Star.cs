using Fink.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Yoki.View.Partial
{
    public class Star
    {
        public event EventHandler OnCheckedChanged;
        public Star()
        {
            this.Location = new Point(0, 0);
            this.Size = new Size(32, 30);
            this.Fill = Brushes.Yellow;
            this.Border = Brushes.Gray;
            this.IsChecked = false;
        }

        public Size Size
        {
            get;
            set;
        }

        public Point Location
        {
            get;
            set;
        }

        private Rectangle rect = Rectangle.Empty;
        public Rectangle Rect
        {
            get
            {
                if (this.rect == Rectangle.Empty)
                {
                    this.rect = new Rectangle(this.Location, this.Size);
                }
                return this.rect;
            }
            set
            {
                this.rect = value;
                this.Size = this.rect.Size;
                this.Location = this.rect.Location;
            }
        }

        public Brush Fill
        {
            get;
            set;
        }

        public Brush Border
        {
            get;
            set;
        }

        private bool isChecked = false;
        public bool IsChecked
        {
            get
            {
                return this.isChecked;
            }
            set
            {
                if (this.isChecked != value)
                {
                    this.isChecked = value;
                    if (this.OnCheckedChanged != null)
                    {
                        this.OnCheckedChanged(this, new EventArgs());
                    }
                }
            }
        }

        public void OnPaint(Graphics g)
        {

            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                Bitmap star = IsChecked ? ResourceHelper.EvaluateStar: ResourceHelper.EvaluateStarEmpty;

                if (star != null)
                {
                    g.DrawImage(star, this.Rect.Left, this.Rect.Top, this.Rect.Width, this.Rect.Height);
                }
            }

        }

        //public Point[] GetStarEndPoint(Rectangle rect)
        //{
        //    int r = Math.Min(rect.Width, rect.Height) / 2;

        //    int[] angles = new int[] {54, 126, 198, 270, 342};

        //    Point[] reslut = new Point[angles.Length];
        //    for (int i = 0; i < angles.Length; i++)
        //    {
        //        var ang = angles[i];
        //        reslut[i].X = rect.Left + r + Convert.ToInt16(Math.Round(r * Math.Cos(Math.PI / 180.0 * ang)));
        //        reslut[i].Y = rect.Top + r + Convert.ToInt16(Math.Round(r * Math.Sin(Math.PI / 180.0 * ang)));
        //    }
        //    return reslut;
        //}

    }
}
