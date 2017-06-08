using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Controls
{
    public class CapturedVideoBox : VideoBox
    {
        private static int _layerImageHeight = 4;
        public CapturedVideoBox()
        {

        } 

        public override bool IsOverlayer
        {
            get
            {
                return false;
          
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Font MicroFont
        {
            get;
            set;
        }

        private VideoQuality videoQuality = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VideoQuality VideoQuality
        {
            get
            {
                return this.videoQuality;
            }
            set
            {
                this.videoQuality = value;
                this.IsNeedRender = true;
            }
        }

        public bool IsNeedRender
        {
            get;
            set;
        }

        private void RenderImage()
        {
            if (this.Size.Width <=0 || this.Size.Height <= 0)
            {
                return;
            }

            if (this.layerImage.Key == null)
            {
                this.layerImage = new KeyValuePair<Size, Bitmap>(this.Size, new Bitmap(this.Size.Width, _layerImageHeight));
                this.layerRectangle = new Rectangle(0, 0, this.Size.Width, _layerImageHeight);
                Render();
            }

            if (this.layerImage.Key.Width != this.Width || this.layerImage.Key.Height != this.Height || this.IsNeedRender)
            {
                if (this.layerImage.Value != null)
                {
                    this.layerImage.Value.Dispose();
                }
                this.layerImage = new KeyValuePair<Size, Bitmap>(this.Size, new Bitmap(this.Size.Width, _layerImageHeight));
                this.layerRectangle = new Rectangle(0, 0, this.Size.Width, _layerImageHeight);
                Render();
                this.IsNeedRender = false;
            }


        }

        private void Render()
        {
            using (Graphics g = Graphics.FromImage(this.layerImage.Value))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 240, 240, 240)), new Rectangle(this.OverlayerRectangle.Left, this.OverlayerRectangle.Top, this.OverlayerRectangle.Width, this.OverlayerRectangle.Height));

                if (this.Width <= 0 || this.VideoQuality == null)
                {
                    return;
                }

                //g.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 0, 0)), new Rectangle(this.OverlayerRectangle.Left, this.OverlayerRectangle.Top, 20, this.OverlayerRectangle.Height));
                //g.FillRectangle(new SolidBrush(Color.FromArgb(255, 253, 206, 48)), new Rectangle(20, this.OverlayerRectangle.Top, 40, this.OverlayerRectangle.Height));
                //g.FillRectangle(new SolidBrush(Color.FromArgb(255, 251, 99, 98)), new Rectangle(60, this.OverlayerRectangle.Top, 10, this.OverlayerRectangle.Height));
                //g.FillRectangle(new SolidBrush(Color.FromArgb(255, 56, 180, 75)), new Rectangle(70, this.OverlayerRectangle.Top, 40, this.OverlayerRectangle.Height));

                int width = this.Width / this.VideoQuality.BlockCount;

                int leftMemory = 0;
                foreach (var fps in this.VideoQuality.FPSCollection)
                {
                    if (fps >= 16)
                    {
                        //nice
                        g.FillRectangle(new SolidBrush(Color.FromArgb(255, 56, 180, 75)), 
                            new Rectangle(leftMemory, this.OverlayerRectangle.Top, width, this.OverlayerRectangle.Height));
                    }
                    else if(fps >= 12)
                    {
                        //good
                        g.FillRectangle(new SolidBrush(Color.FromArgb(255, 16, 174, 239)),
                            new Rectangle(leftMemory, this.OverlayerRectangle.Top, width, this.OverlayerRectangle.Height));
                    }
                    else if (fps >= 8)
                    {
                        //normal
                        g.FillRectangle(new SolidBrush(Color.FromArgb(255, 253, 206, 48)),
                            new Rectangle(leftMemory, this.OverlayerRectangle.Top, width, this.OverlayerRectangle.Height));
                    }
                    else if(fps >= 4)
                    {
                        //bad
                        g.FillRectangle(new SolidBrush(Color.FromArgb(255, 251, 99, 98)),
                            new Rectangle(leftMemory, this.OverlayerRectangle.Top, width, this.OverlayerRectangle.Height));
                    }
                    else
                    {
                        //black
                        g.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 0, 0)),
                            new Rectangle(leftMemory, this.OverlayerRectangle.Top, width, this.OverlayerRectangle.Height));
                    }
                    leftMemory += width;
                }


            }
        }
        
        private static void PaintText(string text, Font font, Graphics g, Rectangle rect)
        {
            using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
            {
                g.DrawString(text, font, new SolidBrush(Color.FromArgb(255, 255, 255, 255)), new Point(rect.Left, rect.Top));
            };
        }


        private Rectangle layerRectangle;
        private KeyValuePair<Size, Bitmap> layerImage;
        public override Image OverlayerImage
        {
            get
            {
                this.RenderImage();
                return this.layerImage.Value;
            }
        }

        public override Rectangle OverlayerRectangle
        {
            get
            {
                return this.layerRectangle;
            }
        }
    }

    public class VideoQuality
    {
        public int BlockCount
        {
            get;
            set;
        }

        public double[] FPSCollection
        {
            get;
            set;
        }
    }
}
