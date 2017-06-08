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
    public class TooltipFromEx: TooltipBase
    {
        public TooltipFromEx(Form parent) : base(parent)
        {

        }
        

        private Bitmap bitmap = null;
        public Bitmap Bitmap
        {
            get
            {
                return this.bitmap;
            }
            set
            {
                this.bitmap = value;
            }
        }

        private byte bitmapOpacity = 255;
        public byte BitmapOpacity
        {
            get
            {
                return this.bitmapOpacity;
            }
            set
            {
                this.bitmapOpacity = value;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (t != null)
            {
                t.Dispose();
            }
            if (bitmap != null)
            {
                Bitmap.Dispose();
            }
            base.OnClosing(e);
        }

        System.Windows.Forms.Timer t = null;
        public override void ShowTooltip()
        {
            DateTime dtStart = DateTime.Now;
            if (t != null)
            {
                t.Dispose();
                t = null;
            }
            t = new Timer();
            t.Tick += (o, e) =>
            {
                if ((DateTime.Now - dtStart).TotalSeconds > 3)
                {
                    t.Dispose();
                    this.BeginInvoke((MethodInvoker)delegate {
                        this.Close();
                    });
                }
            };
            t.Interval = 1000;
            t.Start();
            
            this.Show();
        }

        public override void UpdateBitmap()
        {
            this.SetBitmap(this.Bitmap, this.BitmapOpacity);
        }
    }
}
