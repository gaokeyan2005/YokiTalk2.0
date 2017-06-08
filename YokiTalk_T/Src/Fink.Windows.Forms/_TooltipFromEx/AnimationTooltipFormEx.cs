using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    public class AnimationTooltipFormEx : TooltipBase
    {
        private int currectFrame = 0;
        public AnimationTooltipFormEx(Form parent) : base(parent)
        {
            //this.TopMost = false;
            //this.TopLevel = true;
        }

        private Bitmap[] bitmaps = null;
        public Bitmap[] Bitmaps
        {
            get
            {
                return this.bitmaps;
            }
            set
            {
                this.bitmaps = value;
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
            foreach (var bitmap in Bitmaps)
            {
                bitmap.Dispose();
            }

            base.OnClosing(e);
        }

        System.Windows.Forms.Timer t = null;
        public override void ShowTooltip()
        {
            currectFrame = 0;
            if (t != null)
            {
                t.Dispose();
                t = null;
            }
            t = new Timer();
            t.Tick += (o, e) =>
            {
                UpdateBitmap();
            };
            t.Interval = 50;
            t.Start();

            this.Show();
        }

        public override void UpdateBitmap()
        {
            if (this.Bitmaps.Length <= 0)
            {
                return;
            }
            currectFrame %= this.Bitmaps.Length;
            SetBitmap(this.bitmaps[currectFrame], this.BitmapOpacity);
            currectFrame++;
        }
    }
}
