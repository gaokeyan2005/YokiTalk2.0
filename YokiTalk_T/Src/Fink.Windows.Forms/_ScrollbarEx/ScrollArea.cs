using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    internal class ScrollArea : System.Windows.Forms.Panel
    {
        public event ScrollValueChangedHandle ScrollValueChanged;
        System.Resources.ResourceManager resManager;
        public ScrollArea()
        : base()
        {
            base.SetStyle(
                    ControlStyles.UserPaint |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();

            resManager = new System.Resources.ResourceManager("Fink.Windows.Forms.Properties.Resources", this.GetType().Assembly);
            this.MouseEnter += ScrollArea_MouseEnter;
            this.MouseLeave += ScrollArea_MouseLeave;
            this.MouseDown += ScrollArea_MouseDown;
            this.MouseMove +=ScrollArea_MouseMove;
            this.MouseUp += ScrollArea_MouseUp;

        }


        private bool _isPressed = false;

        public bool IsPressed
        {
            get { return _isPressed; }
            set
            {
                _isPressed = value;
                this.Invalidate();
            }
        }

        private Point _pressedStartLoct;
        private int _pressedStartValue;

        void ScrollArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                IsPressed = false;
            }
        }

        void ScrollArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsPressed)
            {
                float movedRate = ((float)e.Location.Y - _pressedStartLoct.Y) / this.ClientRectangle.Height;
                this.Value = _pressedStartValue + Convert.ToInt32(Math.Floor(this.MaxValue * movedRate));
            }
        }

        void ScrollArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                IsPressed = true;
                _pressedStartLoct = e.Location;
                _pressedStartValue = this.Value;
            }
        }

        private bool _isHovered = false;

        public bool IsHovered
        {
            get { return _isHovered; }
            set
            {
                _isHovered = value;
                this.Invalidate();
            }
        }

        void ScrollArea_MouseEnter(object sender, EventArgs e)
        {
            IsHovered = true;
        }
         void ScrollArea_MouseLeave(object sender, EventArgs e)
        {
            IsHovered = false;
        }
        private int _maxValue = 1;
        [DefaultValue(typeof(int), "1")]
        public int MaxValue
        {
            get { return this._maxValue; }
            set
            {
                this._maxValue = value;
                this.Invalidate();
            }
        }

        private int _viewportValue = 1;
        [DefaultValue(typeof(int), "1")]
        public int ViewportValue
        {
            get { return this._viewportValue; }
            set
            {
                this._viewportValue = value;
                this.Invalidate();
            }
        }

        private int _value = 0;
        [DefaultValue(typeof(int), "0")]
        public int Value
        {
            get { return this._value; }
            set
            {
                if (value < 0)
                {
                    this._value = 0;
                }
                else if (value > this.MaxValue - this.ViewportValue)
                {
                    this._value = Math.Max(0, this.MaxValue - this.ViewportValue);
                }
                else
                {
                    this._value = value;
                }

                if (this.ScrollValueChanged != null)
                {
                    this.ScrollValueChanged(this, new ScrollEventArgs() { Value = this._value });
                }
                this.Invalidate();
            }
        }

        private Rectangle GetScrollRect()
        {
            Rectangle rect = this.ClientRectangle;

            float rate = (float)this.ViewportValue / this.MaxValue;

            if (rate > 1)
            {
                rate = 1;
            }

            int scrollWidth = 6;
            int scrollHeight = Convert.ToInt32(Math.Floor(rate * rect.Height));
            int scrollTop = this.MaxValue == 0? 0:Convert.ToInt32(Math.Floor((float)this.Value / this.MaxValue * rect.Height));

            Rectangle r = new Rectangle(8, scrollTop, scrollWidth, scrollHeight);

            return r;
        }


        #region override

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = e.ClipRectangle;

            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using (Brush b = new SolidBrush(Color.FromArgb(255, 240, 240, 240)))
            {
                e.Graphics.FillRectangle(b, rect);
            }
            Rectangle highLight = new Rectangle(rect.Location, new Size(1, rect.Height));
            using (Brush b = new SolidBrush(Color.FromArgb(255, 240, 240, 240)))
            {
                e.Graphics.FillRectangle(b, highLight);
            }

            using (Brush b = new SolidBrush(this.IsHovered ? Color.FromArgb(255, 40, 40, 46) : Color.FromArgb(255, 106, 106, 122)))
            {
                using (GraphicsPath p = new GraphicsPath())
                {
                    Rectangle scrollRect = GetScrollRect();
                    p.FillMode = FillMode.Winding;
                    p.AddEllipse(new Rectangle(scrollRect.Left, scrollRect.Top, scrollRect.Width, scrollRect.Width));
                    p.AddEllipse(new Rectangle(scrollRect.Left, scrollRect.Bottom - scrollRect.Width, scrollRect.Width, scrollRect.Width));
                    p.AddRectangle(new Rectangle(scrollRect.Left, scrollRect.Top + Convert.ToInt32(Math.Floor(scrollRect.Width / 2.0)), scrollRect.Width, scrollRect.Height - scrollRect.Width));
                    e.Graphics.FillPath(b, p);
                }
            }

            e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
        }
        #endregion
    }
}
