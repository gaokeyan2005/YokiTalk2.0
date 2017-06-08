using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    public class ScrollEventArgs
    {
        public int Value
        {
            get;
            set;
        }


    }

    public delegate void ScrollValueChangedHandle(object sender, ScrollEventArgs e);

    internal class ScrollBarEx : System.Windows.Forms.Panel
    {

        private ScrollButton _upBtn;
        private ScrollButton _downBtn;
        private ScrollArea _scrollArea;
        public event ScrollValueChangedHandle ScrollValueChanged;

        public ScrollBarEx()
            : base()
        {
            base.SetStyle(
                   ControlStyles.UserPaint |
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.ResizeRedraw |
                   ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();

            Init();
            this.SizeChanged += ScrollBarEx_SizeChanged;
        }


        [DefaultValue(typeof(int), "1")]
        public int MaxValue
        {
            get { return this._scrollArea.MaxValue; }
            set
            {
                this._scrollArea.MaxValue = value;
            }
        }

        [DefaultValue(typeof(int), "1")]
        public int ViewportValue
        {
            get { return this._scrollArea.ViewportValue; }
            set
            {
                this._scrollArea.ViewportValue = value;
            }
        }

        [DefaultValue(typeof(int), "0")]
        public int Value
        {
            get { return this._scrollArea.Value; }
            set
            {
                this._scrollArea.Value = value;
            }
        }


        void ScrollBarEx_SizeChanged(object sender, EventArgs e)
        {
            this._upBtn.Size = new System.Drawing.Size(22, 22);
            this._upBtn.Location = new System.Drawing.Point(0, 0);


            this._downBtn.Size = new System.Drawing.Size(22, 22);
            this._downBtn.Location = new System.Drawing.Point(0, this.ClientRectangle.Height - 22);

            this._scrollArea.Size = new System.Drawing.Size(22, this.ClientRectangle.Height - 22 * 2);
            this._scrollArea.Location = new System.Drawing.Point(0, 22);
            this._scrollArea.ScrollValueChanged += _scrollArea_ScrollValueChanged;
        }

        void _scrollArea_ScrollValueChanged(object sender, ScrollEventArgs e)
        {
            if (this.ScrollValueChanged != null)
            {
                this.ScrollValueChanged(this, new ScrollEventArgs() { Value = e.Value });
            }
        }

        private void Init()
        {
            this._upBtn = new ScrollButton(ScrollButtonType.Up);
            this.Controls.Add(this._upBtn);
            this._downBtn = new ScrollButton(ScrollButtonType.Down);
            this.Controls.Add(this._downBtn);
            this._scrollArea = new ScrollArea();
            this.Controls.Add(this._scrollArea);
        }


    }
}
