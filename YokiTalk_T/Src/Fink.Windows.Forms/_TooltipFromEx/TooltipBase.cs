using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    public abstract class TooltipBase : System.Windows.Forms.Form
    {
        public TooltipBase(Form parent) : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            UpdateStyles();
            
            this.ParentForm = parent;
            this.ShowInTaskbar = false;
            this.TopMost = false;
            this.FormBorderStyle = FormBorderStyle.None;
            //CanPenetrate();

            this.ParentForm.Move += (object sender, EventArgs e) =>
            {
                CalcPosition();
            };
             
            this.ParentForm.Activated += (object sender, EventArgs e) =>
            {
                IntPtr hWnd = Fink.Windows.Forms.Win32.GetActiveWindow();
                if (hWnd == parent.Handle && this.TopMost == false)
                {
                    this.TopMost = true;
                    this.ParentForm.BeginInvoke((MethodInvoker)delegate
                    {
                        this.ParentForm.Activate();
                    });
                }
            };

            this.ParentForm.Deactivate += (object sender, EventArgs e) =>
            {
                IntPtr hWnd = Fink.Windows.Forms.Win32.GetActiveWindow();
                if (hWnd == parent.Handle && this.TopMost == true)
                {
                    this.TopMost = false;
                }
            };


            this.Shown += (o, e) =>
            {
                if (this.ParentForm != null)
                {
                    CalcPosition();
                    this.UpdateBitmap();
                }
            };
        }
        
        //private const int LWA_ALPHA = 0x2;
        //public void CanPenetrate()
        //{
        //    int intExTemp = Fink.Windows.Forms.Win32.GetWindowLong(this.Handle, (int)Fink.Windows.Forms.Win32.GWL_EXSTYLE);
        //    int oldGWLEx = Fink.Windows.Forms.Win32.SetWindowLong(this.Handle, (int)Fink.Windows.Forms.Win32.GWL_EXSTYLE, (int)Fink.Windows.Forms.Win32.WS_EX_TRANSPARENT | (int)Fink.Windows.Forms.Win32.WS_EX_LAYERED);
        //    Fink.Windows.Forms.NativeMethods.SetLayeredWindowAttributes(this.Handle, 0, 100, LWA_ALPHA);
        //}
        
        public int VerticalOffset
        {
            get;
            set;
        }

        private void CalcPosition()
        {
            Size ps = this.ParentForm.Size;
            Size s = this.Size;

            Point pp = this.ParentForm.PointToScreen(new Point(Convert.ToInt32(Math.Floor((ps.Width - s.Width) / 2.0)), Convert.ToInt32(Math.Floor((ps.Height - s.Height) / 2.0)) + this.VerticalOffset));
            this.Location = new Point(pp.X, pp.Y);
        }

        public new System.Windows.Forms.Form ParentForm
        {
            get;
            private set;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= (int)Fink.Windows.Forms.NativeMethods.WindowStyleEx.WS_EX_LAYERED |
                                (int)Fink.Windows.Forms.NativeMethods.WindowStyleEx.WS_EX_TRANSPARENT;
                return cp;
            }
        }
        

        public abstract void ShowTooltip();
        public abstract void UpdateBitmap();

        public void SetBitmap(Bitmap bitmap, byte bitmapOpacity)
        {
            if (bitmap == null)
	        {
                return;
	        }

            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("位图必须是32位包含alpha 通道");

            IntPtr screenDc = Win32.GetDC(IntPtr.Zero);
            IntPtr memDc = Win32.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));   // 创建GDI位图句柄，效率较低
                oldBitmap = Win32.SelectObject(memDc, hBitmap);

                Win32.Size size = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.Point pointSource = new Win32.Point(0, 0);
                Win32.Point topPos = new Win32.Point(Left, Top);
                Win32.BLENDFUNCTION blend = new Win32.BLENDFUNCTION();
                blend.BlendOp = Win32.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = bitmapOpacity;
                blend.AlphaFormat = Win32.AC_SRC_ALPHA;
                if (!this.IsDisposed)
                {
                    Win32.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, Win32.ULW_ALPHA);
                    this.Width = this.Width;
                    this.Height = this.Height;
                }
            }
            finally
            {
                Win32.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBitmap);

                    Win32.DeleteObject(hBitmap);
                }
                Win32.DeleteDC(memDc);
            }
        }
        
    }
}
