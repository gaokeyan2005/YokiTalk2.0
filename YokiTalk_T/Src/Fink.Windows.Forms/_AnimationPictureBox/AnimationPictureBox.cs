using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    public class AnimationPictureBox: System.Windows.Forms.Control
    {
        public AnimationPictureBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
        }
        


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(Brushes.Red, new Rectangle(10,10,50,50));
        }

        //protected override void OnBackColorChanged(EventArgs e)
        //{
        //    if (this.Parent != null)
        //    {
        //        Parent.Invalidate(this.Bounds, true);
        //    }
        //    base.OnBackColorChanged(e);
        //}

        //protected override void OnParentBackColorChanged(EventArgs e)
        //{
        //    this.Invalidate(); 
        //    base.OnParentBackColorChanged(e);
        //}
    }
}
