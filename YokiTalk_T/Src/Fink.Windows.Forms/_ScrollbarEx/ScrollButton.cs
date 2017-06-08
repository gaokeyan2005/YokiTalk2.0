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
    internal enum ScrollButtonType
    {
        Up,
        Down
    }

    internal class ScrollButton: System.Windows.Forms.Panel
    {
        
        System.Resources.ResourceManager resManager;
        public ScrollButton()
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
        }

        public ScrollButton(ScrollButtonType btnType)
            : this()
        {
            this.ButtonType = btnType;
        }

        [DefaultValue(typeof(ScrollButtonType), "Up")]
        public ScrollButtonType ButtonType
        {
            get;
            set;
        }

        #region override

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = e.ClipRectangle;

            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            if (DesignMode)
            {
                e.Graphics.FillRectangle(Brushes.Gray, rect);
                using (Pen pen = new Pen(Brushes.White))
                {
                    e.Graphics.DrawRectangle(pen, rect);
                }
            }
            else
            {
                using (System.Drawing.Image img = resManager.GetObject("Scrollbar") as System.Drawing.Image)
                {
                    switch (this.ButtonType)
                    {
                        case ScrollButtonType.Up:
                                e.Graphics.DrawImage(img, e.ClipRectangle, new Rectangle(new Point(0, 0), e.ClipRectangle.Size), GraphicsUnit.Pixel);
                            break;
                        case ScrollButtonType.Down:
                            e.Graphics.DrawImage(img, e.ClipRectangle, new Rectangle(new Point(22, 0), e.ClipRectangle.Size), GraphicsUnit.Pixel);
                            break;
                        default:
                            break;
                    }
                }
            }

            e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
        }
        #endregion
    }
}
