using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Fink.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Fink.Windows.Forms
{
    public abstract class ControlEx: System.Windows.Forms.Control
    {
        private RoundStyle _roundStyle;
        private int _radius;

        public ControlEx()
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
