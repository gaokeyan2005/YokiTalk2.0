using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Fink.Drawing;
using System.Windows.Forms;
using System.Drawing;

namespace Fink.Windows.Forms
{
    public abstract class PanelExBase: System.Windows.Forms.Panel
    {
        public PanelExBase():base()
        {
            base.SetStyle(
                   ControlStyles.UserPaint |
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.ResizeRedraw |
                   ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
        }


        public bool HitTestVisibility
        {
            get;
            set;
        }

        private const int HTVISIBEL = 0;
        private const int HTINVISIBEL = -1;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)NativeMethods.WindowMessages.WM_NCHITTEST:
                    //int wparam = m.LParam.ToInt32();
                    //Point point = new Point(
                    //    NativeMethods.LOWORD(wparam),
                    //    NativeMethods.HIWORD(wparam));
                    //point = PointToClient(point);
                    //Console.WriteLine(point.ToString());
                    if (!this.HitTestVisibility)
                    {
                        m.Result = (IntPtr)HTINVISIBEL;
                        base.WndProc(ref m);
                    }
                    else
                    {
                        m.Result = (IntPtr)HTVISIBEL;
                        base.WndProc(ref m);
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }



    }
}

