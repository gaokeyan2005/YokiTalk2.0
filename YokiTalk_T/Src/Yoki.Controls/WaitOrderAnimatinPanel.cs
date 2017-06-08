using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yoki.Controls
{
    public class WaitOrderAnimatinPanel : System.Windows.Forms.PictureBox
    {
        public WaitOrderAnimatinPanel() : base()
        {
            base.SetStyle(
                   ControlStyles.UserPaint |
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.ResizeRedraw |
                   ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();

            this.TooltipImage = ResourceHelper.SwitchONMsg;
            this.PauseImage = ResourceHelper.OrderPuase;

            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o) =>
            {
                using (this.AnimatedGif = ResourceHelper.OrderIsCommingGIF)
                {
                    DoAnimation();
                }
            }));
        }
        
        public bool IsWaiting
        {
            get;
            set;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (!this.IsWaiting)
            {
                base.OnPaint(pe);
                PaintPause(pe.Graphics);
            }
            else
            {
                if (this.AnimatedGif != null)
                {
                    lock (this.AnimatedGif)
                    {
                        using (Bitmap bitmap = new Bitmap(this.Width, this.Height))
                        {
                            using (Graphics bitmapG = Graphics.FromImage(bitmap))
                            {
                                PaintGIF(bitmapG);
                                PaintTooltip(bitmapG);
                            }
                            pe.Graphics.DrawImage(bitmap, this.ClientRectangle);
                        }
                    }
                }

                PaintTooltip(pe.Graphics);
            }
        }
        

        private void PaintGIF(Graphics g)
        {
            Rectangle gifRect = Locations[0];
            g.DrawImage(this.AnimatedGif, gifRect);
        }

        private void PaintTooltip(Graphics g)
        {
            Rectangle tooltipRect = Locations[1];
            g.DrawImage(this.TooltipImage, tooltipRect);
        }

        private void PaintPause(Graphics g)
        {
            Rectangle puaseRect = Locations[2];
            g.DrawImage(this.PauseImage, puaseRect);
        }

        private Rectangle clientRectMemory = Rectangle.Empty;
        private Rectangle[] locations = null;
        private Rectangle[] Locations
        {
            get
            {
                if (this.locations == null || this.ClientRectangle != clientRectMemory)
                {
                    Rectangle[] rects = new Rectangle[3];

                    //gif area
                    if (this.AnimatedGif != null)
                    {
                        rects[0] = new Rectangle(
                       (this.Width - this.AnimatedGif.Width) / 2,
                       (this.Height - this.AnimatedGif.Height) / 2,
                       this.animatedGif.Width,
                       this.animatedGif.Height
                       );
                    }

                    //tooltip area
                    rects[1] = new Rectangle(
                      this.Width - this.TooltipImage.Width - 5,
                      2,
                      this.TooltipImage.Width,
                      this.TooltipImage.Height
                      );

                    //pause area
                    rects[2] = new Rectangle(
                      (this.Width - this.PauseImage.Width) / 2,
                      (this.Height - this.PauseImage.Height) / 2,
                      this.PauseImage.Width,
                      this.PauseImage.Height
                      );

                    this.clientRectMemory = this.ClientRectangle;
                    this.locations = rects;
                }

                return this.locations;
            }
        }

        #region Animation


        private Image animatedGif = null;
        private Image AnimatedGif
        {
            get
            {
                return this.animatedGif;
            }
            set
            {
                if (this.animatedGif != value)
                {
                    this.animatedGif = value;
                    // Get the frame count for the Gif...
                    int PropertyTagFrameDelay = 0x5100;
                    this.PropItem = this.AnimatedGif.GetPropertyItem(PropertyTagFrameDelay);
                    this.FrameDimension = new System.Drawing.Imaging.FrameDimension(this.AnimatedGif.FrameDimensionsList[0]);
                    this.FrameCount = this.AnimatedGif.GetFrameCount(System.Drawing.Imaging.FrameDimension.Time);

                    byte[] bytes = this.PropItem.Value;
                    this.GIFDelays = new int[this.FrameCount + 1];
                    int i = 0;
                    for (i = 0; i <= this.FrameCount - 1; i++)
                    {
                        this.GIFDelays[i] = BitConverter.ToInt32(bytes, i * 4);
                    }
                }
            }
        }

        private Image TooltipImage
        {
            get;
            set;
        }
        private Image PauseImage
        {
            get;
            set;
        }

        private int FrameCount
        {
            get;
            set;
        }

        private System.Drawing.Imaging.FrameDimension FrameDimension
        {
            get;
            set;
        }

        private System.Drawing.Imaging.PropertyItem PropItem
        {
            get;
            set;
        }

        private int[] GIFDelays
        {
            get;
            set;
        }




        Size sMemory = new Size();
        private void DoAnimation()
        {
            
            
            // A Gif image's frame delays are contained in a byte array
            // in the image's PropertyTagFrameDelay Property Item's
            // value property.
            // Retrieve the byte array...
            // Create an array of integers to contain the delays,
            // in hundredths of a second, between each frame in the Gif image.
            
            // Play the Gif one time...
            while (true)
            {
                if (!this.IsWaiting)
                {
                    System.Threading.Thread.Sleep(100);
                    continue;
                }
                else
                {
                    for (int i = 0; i <= this.FrameCount - 1; i++)
                    {
                        lock (this.AnimatedGif)
                        {
                            this.AnimatedGif.SelectActiveFrame(this.FrameDimension, i);
                        }
                        if (this.Width > 0 && this.Height > 0)
                        {

                            if (sMemory != this.Size)
                            {
                                sMemory = this.Size;
                            }

                            if (this.IsHandleCreated)
                            {
                                this.BeginInvoke((MethodInvoker)delegate
                                {
                                    this.Invalidate();
                                });
                            }
                        }

                        System.Threading.Thread.Sleep(this.GIFDelays[i] * 8);
                    }
                }
            }
        }

        #endregion

    }
}
