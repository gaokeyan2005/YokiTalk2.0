using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Fink.Windows.Forms
{
    public class MenubarEx: PanelEx
    {
        public MenubarEx()
            : base()
        {
            this.HitTestVisibility = false;
        }

        #region override

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = e.ClipRectangle;
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(235, 236, 239)))
            {
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
                e.Graphics.FillRectangle(brush, rect);
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
            }

            Rectangle tophightLightRect = new Rectangle(rect.Location, new Size(rect.Width, 1));
            Rectangle bottomhightLightRect = new Rectangle(new Point(rect.Left, rect.Top + rect.Height - 2), new Size(rect.Width, 1));
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(96, 255, 255, 255)))
            {
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
                e.Graphics.FillRectangle(brush, tophightLightRect);
                e.Graphics.FillRectangle(brush, bottomhightLightRect);
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
            }

            Rectangle bottomDarkRect = new Rectangle(new Point(rect.Left, rect.Top + rect.Height - 1), new Size(rect.Width, 1));
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(204, 204, 204)))
            {
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
                e.Graphics.FillRectangle(brush, bottomDarkRect);
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
            }
            
            
        }

        #endregion
    }
}
