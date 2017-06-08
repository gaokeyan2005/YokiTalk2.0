using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoki.Controls
{
    public enum VideoBoxStatus
    {
        NoVideo = 0,
        Video = 1,
    }

    public abstract class VideoBox: System.Windows.Forms.PictureBox
    {
        public VideoBox(): base()
        {
        }

        private KeyValuePair<System.Drawing.Size, System.Drawing.Graphics> graphics = new KeyValuePair<System.Drawing.Size, System.Drawing.Graphics>();
        public System.Drawing.Graphics Graphics
        {
            get
            {
                if (this.graphics.Key.Width != this.Width || this.graphics.Key.Height != this.Height)
                {
                    if (this.graphics.Value != null)
                    {
                        this.graphics.Value.Dispose();
                    }
                    this.graphics = new KeyValuePair<System.Drawing.Size, System.Drawing.Graphics>(this.Size, this.CreateGraphics());
                }
                return this.graphics.Value;
            }
        }
        
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (this.Status == VideoBoxStatus.NoVideo ||
                (this.Status == VideoBoxStatus.Video && !this.IsBeginShowVideo))
            {
                if (NoVideoImage != null)
                {
                    float rw = (float)this.Width / NoVideoImage.Width;
                    float rh = (float)this.Height / NoVideoImage.Height;
                    float rate = Math.Max(rw, rh);

                    Size imageSize = Size.Empty;
                    if (rate <= 1)
                    {
                        imageSize = new Size((int)(NoVideoImage.Width * rate), (int)(NoVideoImage.Height * rate));
                    }
                    else
                    {
                        imageSize = NoVideoImage.Size;
                    }

                    Rectangle imageRect = new Rectangle(new Point((this.Width - imageSize.Width) / 2, (this.Height - imageSize.Height) / 2), imageSize);

                    pe.Graphics.DrawImage(NoVideoImage, imageRect);
                }
            }
            else if (this.Status == VideoBoxStatus.Video && this.IsBeginShowVideo)
            {
                if (this.CacheImage != null)
                {
                    float sw = (float)this.Width / this.CacheImage.Width;
                    float sh = (float)this.Height / this.CacheImage.Height;
                    var scale = Math.Min(sw, sh);
                    int newWidth = Convert.ToInt32(this.CacheImage.Width * scale);
                    int newHeight = Convert.ToInt32(this.CacheImage.Height * scale);

                    pe.Graphics.DrawImage(this.CacheImage,
                        new Rectangle(
                            new Point((this.Width - newWidth) / 2, (this.Height - newHeight) / 2),
                            new Size(newWidth, newHeight)
                            )
                    );
                }
            }
            
        }

        private VideoBoxStatus status = VideoBoxStatus.NoVideo;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VideoBoxStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                if (this.status != value || value == VideoBoxStatus.NoVideo)
                {
                    this.status = value;
                    if (this.status == VideoBoxStatus.NoVideo)
                    {
                        this.IsBeginShowVideo = false;
                    }
                    this.Invalidate();
                }

            }
        }

        private Image cacheImage = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image CacheImage
        {
            get
            {
                return this.cacheImage;
            }
            set
            {
                if (this.cacheImage != null)
                {
                    this.cacheImage.Dispose();
                }
                this.cacheImage = value;
            }
        }

        private bool isBeginShowVideo = false;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsBeginShowVideo
        {
            get
            {
                return this.isBeginShowVideo;
            }
            set
            {
                this.isBeginShowVideo = value;
            }
        }


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static Image NoVideoImage
        {
            get;
            set;
        }


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract Image OverlayerImage
        {
            get;
        }


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract Rectangle OverlayerRectangle
        {
            get;
        }


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract bool IsOverlayer
        {
            get;
        }


    }
}
