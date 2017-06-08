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
    internal class MenubarItemExDesigner : ControlDesignerEx
    {
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
        }

    }

    [Designer(typeof(MenubarItemExDesigner))]
    public class MenubarItemEx: Fink.Windows.Forms.ButtonExBase
    {
        public MenubarItemEx()
            : base()
        {
            
        }

        #region Porperty

        private System.Drawing.Size _iconSize = new Size(16, 16);
        [DefaultValue(typeof(Size), "16, 16")]
        public System.Drawing.Size IconSize
        {
            get { return _iconSize; }
            set
            {
                _iconSize = value;
                this.Invalidate();
            }
        }

        #endregion

        #region override

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle rect = e.ClipRectangle;

            Point iconLocation = new Point((int)Math.Abs((rect.Width - IconSize.Width) / 2)
                                           , (int)Math.Abs((rect.Height - IconSize.Height) / 2));

            if (DesignMode)
            {
                e.Graphics.FillRectangle(Brushes.Gray, rect);
                using(Pen pen = new Pen(Brushes.White))
                {
                    e.Graphics.DrawRectangle(pen, rect);
                }
                e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(iconLocation.X, iconLocation.Y, IconSize.Width, IconSize.Height));
            }
            else
            {
                if (this.Image != null)
                {
                    e.Graphics.DrawImage(this.Image, new Rectangle(iconLocation.X, iconLocation.Y, IconSize.Width, IconSize.Height), new Rectangle(0, 0, this.Image.Size.Width, this.Image.Size.Height), GraphicsUnit.Pixel);
                }
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (this.Image!=null)
            {
                this.Image.Dispose();
            }
        }

        #endregion
    }
}
