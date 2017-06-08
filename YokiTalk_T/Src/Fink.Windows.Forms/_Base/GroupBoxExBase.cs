using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fink.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Fink.Windows.Forms
{
    public abstract class GroupBoxExBase: System.Windows.Forms.GroupBox
    {
        private RoundStyle _roundStyle;
        private int _radius;

        public GroupBoxExBase()
            : base()
        {
            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
        }

        #region porperty

        [DefaultValue(typeof(RoundStyle), "All")]
        public RoundStyle RoundStyle
        {
            get { return _roundStyle; }
            set
            {
                if (this._roundStyle != value)
                {
                    this._roundStyle = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(8)]
        public int Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    base.Invalidate();
                }
            }
        }

        protected Rectangle displayRectangle;

        public override Rectangle DisplayRectangle
        {
            get { return this.displayRectangle; }
        }

        public void SetDisplayRectangle(Rectangle rect)
        {
            this.displayRectangle = rect;
        }

        #endregion

        #region 
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Set graphic porperty
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
        }
        #endregion 
    }
}
