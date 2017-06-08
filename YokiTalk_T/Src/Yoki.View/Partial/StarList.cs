using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yoki.View.Partial
{
    public class StarList : System.Windows.Forms.Panel
    {
        public event EventHandler OnPreviewChanged;
        public event EventHandler OnSelectedChanged;
        public const int DefaultCount = 5;
        public StarList()
        {
            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
            AddStars(DefaultCount);

            this.MouseMove += (o, e) =>
            {
                double rate = (double)e.X / this.ClientRectangle.Width;
                this.PreviewIndex = Math.Max(Convert.ToInt32(Math.Ceiling(rate * this.Stars.Length - 1)), 0);
            };
            this.MouseLeave += (o, e) =>
            {
                this.SelectedIndex = this.SelectedIndex;
            };

            this.MouseClick += (o, e) =>
            {
                double rate = (double)e.X / this.ClientRectangle.Width;
                this.SelectedIndex = Convert.ToInt32(Math.Ceiling(rate * this.Stars.Length - 1));
            };
        }

        private int selectedIndex = -1;
        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }
            private set
            {
                this.selectedIndex = value;
                if (this.previewIndex >= 0 || this.previewIndex < DefaultCount)
                {
                    this.SelectTo(this.selectedIndex);
                }
                if (this.OnSelectedChanged != null)
                {
                    this.OnSelectedChanged(this, new EventArgs());
                }
            }
        }

        private int previewIndex = 0;
        public int PreviewIndex
        {
            get
            {
                return this.previewIndex;
            }
            private set
            {
                this.previewIndex = value;
                if (this.previewIndex >= 0 || this.previewIndex < DefaultCount)
                {
                    this.SelectTo(this.previewIndex);
                }

                if (this.OnPreviewChanged != null)
                {
                    this.OnPreviewChanged(this, new EventArgs());
                }
            }
        }

        private void SelectTo(int nmb)
        {
            foreach (var star in this.Stars)
            {
                star.IsChecked = false;
            }
            for (int i = 0; i < Math.Min(this.Stars.Length, nmb + 1); i++)
            {
                Stars[i].IsChecked = true;
            }
        }


        private Star[] stars = null;
        public Star[] Stars
        {
            get
            {
                return this.stars;
            }
            private set
            {
                this.stars = value;
                this.Invalidate();
            }
        }

        private int starsCount = DefaultCount;
        public int StarsCount
        {
            get
            {
                return this.starsCount;
            }
            set
            {
                this.starsCount = value;
                this.AddStars(this.starsCount);
            }
        }

        private void AddStars(int count)
        {
            Queue<Yoki.View.Partial.Star> stars = new Queue<Yoki.View.Partial.Star>();
            for (int i = 0; i < count; i++)
            {
                Yoki.View.Partial.Star star = new Yoki.View.Partial.Star();
                star.Fill = Brushes.Yellow;
                star.Border = new SolidBrush(Color.FromArgb(16, 0, 0, 0));
                star.IsChecked = false;
                star.OnCheckedChanged += (o, e) =>
                {
                    this.Invalidate();
                };
                stars.Enqueue(star);
            }
            this.Stars = stars.ToArray();
        }

        private int margin = 8;
        private Rectangle[] Arrary()
        {
            int count = this.Stars.Length;
            int width = (this.ClientRectangle.Width - margin * (count - 1)) / count;

            int len = Math.Min(width, this.ClientRectangle.Height);
            Queue<Rectangle> rects = new Queue<Rectangle>();
            for (int i = 0; i < count; i++)
            {
                Point p = new Point((len + margin) * i, (this.ClientRectangle.Height - len) / 2);
                Rectangle rect = new Rectangle(p, new Size(len, len));
                rects.Enqueue(rect);
                stars[i].Rect = rect;
            }
            return rects.ToArray();
        }


        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle[] rects = Arrary();

            if (Stars == null || Stars.Length <= 0)
            {
                return;
            }
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;
            foreach (var star in Stars)
            {
                star.OnPaint(g);
            }

        }
    }
}
