using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Fink.Drawing;
using System.ComponentModel.Design;

namespace Fink.Windows.Forms
{
    public class PanelExDesigner : ParentControlDesignerEx
    {
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            var control = this.Control as PanelEx;
            EnableDesignMode(control, control.Name);
        }


    }

    [Designer(typeof(PanelExDesigner))]
    public class PanelEx: PanelExBase
    {
        private PanelExColorTable _colorTable;

        public PanelEx()
            : base()
        {

        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)NativeMethods.WindowMessages.WM_SIZE)
            {
                int width = NativeMethods.LOWORD((int)(m.LParam));
                int height = NativeMethods.HIWORD((int)(m.LParam));
                Rectangle rect = this.ClientRectangle;
                rect.Height = height;
            }
            base.WndProc(ref m);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);  
        }

        #region porperty

        private PanelExColorTable ColorTable
        {
            get
            {
                if (this._colorTable == null)
                {
                    this._colorTable = new PanelExColorTable();
                }
                return this._colorTable;
            }
            set
            {
                this._colorTable = value;
            }
        }
        #endregion



        #region override

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            //base.OnPaintBackground(e);

            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
        }
        #endregion





    }
}
