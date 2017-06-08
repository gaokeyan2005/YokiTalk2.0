using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yoki.Comm;

namespace Yoki.Controls
{
    public class ImageViewer: System.Windows.Forms.PictureBox
    {
        public ImageViewer() : base()
        {
            base.SetStyle(
                   ControlStyles.UserPaint |
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.ResizeRedraw |
                   ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();

            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o) =>
            {
                DoAnimation();
            }));
        }
        

        private Size imageSize = new Size(800, 600);

        public Size ImageSize
        {
            get
            {
                return this.imageSize;
            }
            set
            {
                this.imageSize = value;
            }
        }

        private string[] links = null;
        public string[] Links
        {
            get
            {
                return this.links;
            }
            set
            {
                if (this.links != value)
                {
                    this.links = value;
                    this.selectedIndex = -1;
                    this.ImageViewerModel = ImageViewerModel.DoAnimation;
                    
                    FillItems(this.links);
                }
            }
        }

        public System.Collections.Generic.IList<ImageViewerItem> Items
        {
            get;
            set;
        }

        private int selectedIndex = -1;
        private int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }
            set
            {
                if (this.selectedIndex != value)
                {
                    if (value >= 0)
                    {
                        if (this.Items != null && value < this.Items.Count)
                        {
                            this.selectedIndex = value;
                            this.SelectedItem = this.Items[this.selectedIndex];
                            //this.LabPage.Text = "dd";
                        }
                    }
                }
            }
        }

        private ImageViewerItem selectedItem = null;
        private ImageViewerItem SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    this.ImageViewerModel = this.selectedItem.Isloaded ? ImageViewerModel.PaintRender : ImageViewerModel.DoAnimation;
                    this.Invalidate();
                }
            }
        }

        private void FillItems(string[] links)
        {
            Queue<ImageViewerItem> items = new Queue<ImageViewerItem>();
            for (int i = 0; links != null && i < links.Length; i++)
            {
                ImageViewerItem item = new ImageViewerItem(i, links[i], (arg) =>
                {
                    if (arg == this.SelectedItem)
                    {
                        this.ImageViewerModel = ImageViewerModel.PaintRender;
                        this.ImageSize = this.selectedItem.Image.Size;
                        this.Invalidate();
                    }

                    this.Invalidate();
                });
                items.Enqueue(item);
            }
            this.Items = items.ToArray();
            this.SelectedIndex = 0;
        }

        public void NextPage()
        {
            this.SelectedIndex++;
        }
        public void LastPage()
        {
            this.SelectedIndex--;
        }



        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (Locations[1].Contains(e.Location))
            {
                this.Invalidate(Locations[1]);
            }
            else if (Locations[2].Contains(e.Location))
            {
                this.Invalidate(Locations[2]);
            }

            else if (Locations[3].Contains(e.Location))
            {
                this.Invalidate(Locations[3]);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (Locations[1].Contains(e.Location))
            {
                this.LastPage();
            }
            else if (Locations[2].Contains(e.Location))
            {
                this.NextPage();
            }
        }

        public ImageViewerModel ImageViewerModel
        {
            get;
            set;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (this.ImageViewerModel == ImageViewerModel.PaintRender)
            {
                base.OnPaint(pe);

                if (this.SelectedItem != null)
                {
                    PaintImage(pe.Graphics, this.SelectedItem.Image);
                }

                PaintBackground(pe.Graphics);
                PaintControls(pe.Graphics);
                //PaintExpansion(pe.Graphics);
            }
            else if(this.ImageViewerModel == ImageViewerModel.DoAnimation)
            {
                lock (this.AnimatedGif)
                {
                    using (Bitmap bitmap = new Bitmap(this.ImageSize.Width, this.ImageSize.Height))
                    {
                        using (Graphics bitmapG = Graphics.FromImage(bitmap))
                        {
                            bitmapG.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 204, 102)), new Rectangle(new Point(0, 0), this.ImageSize));
                            bitmapG.DrawImage(this.AnimatedGif, (bitmap.Width - this.AnimatedGif.Width) / 2, (bitmap.Height - this.AnimatedGif.Height) / 2, this.AnimatedGif.Width, this.AnimatedGif.Height);
                        }
                        PaintImage(pe.Graphics, bitmap);
                    }
                }

                PaintBackground(pe.Graphics);
                PaintControls(pe.Graphics);
            }
        }

        private void PaintImage(Graphics g, Image img)
        {
            Rectangle imageRect = Locations[0];
            g.DrawImage(img, imageRect);
        }

        private void PaintBackground(Graphics g)
        {
            Rectangle shadowRect = Locations[0];
            shadowRect.Inflate(3, 3);
            using (Fink.Drawing.HAFGraphics HAF = new Fink.Drawing.HAFGraphics(g))
            {
                using (GraphicsPath path = Fink.Drawing.RectangleEx.CreatePath(shadowRect, 4, Fink.Drawing.RoundStyle.All))
                {
                    g.DrawPath(new Pen(new SolidBrush(Color.FromArgb(255, 235, 235, 235)), 6), path);
                }
                
                using (GraphicsPath path = Fink.Drawing.RectangleEx.CreatePath(Locations[0], 2, Fink.Drawing.RoundStyle.All))
                {
                    g.DrawPath(new Pen(new SolidBrush(Color.FromArgb(255, 255, 255, 255)), 1), path);
                }
            }
        }



        private void PaintControls(Graphics g)
        {
            g.DrawImage(ResourceHelper.ArrowLeft, Locations[1]);
            g.DrawImage(ResourceHelper.ArrowRight, Locations[2]);
        }

        private void PaintExpansion(Graphics g)
        {
            using (Fink.Drawing.HAFGraphics HAFG = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.All))
            {
                using (GraphicsPath path = Fink.Drawing.RectangleEx.CreatePath(Locations[3], 2, Fink.Drawing.RoundStyle.All))
                {
                    g.FillPath(new SolidBrush(Color.FromArgb(128, 0, 0, 0)), path);
                }
            }
            g.DrawImage(ResourceHelper.Expansion, Locations[3]);
        }


        private static int _arrowAreaWidth = 40;
        private static Size _arrowSize = new Size(32, 32);
        private static Size _expansionSize = new Size(26, 26);
        private static int _expansionmargin = 5;

        private Rectangle clientRectMemory = Rectangle.Empty;
        private Size imageSizeMemory = Size.Empty;
        private Rectangle[] locations = null;
        private Rectangle[] Locations
        {
            get
            {
                if (this.locations == null || this.ClientRectangle != clientRectMemory || this.imageSizeMemory != this.ImageSize)
                {
                    Rectangle[] rects = new Rectangle[4];

                    int imageAreaWidth = Math.Max(this.Width - _arrowAreaWidth * 2, 0);
                    int imageAreaHeight = this.Height;

                    double rw = (double)imageAreaWidth / this.imageSize.Width;
                    double rh = (double)imageAreaHeight / this.imageSize.Height;

                    double rate = Math.Min(rw, rh);

                    Size imageRenderSize = Size.Empty;
                    if (rate <= 1)
                    {
                        imageRenderSize = new Size((int)(this.imageSize.Width * rate), (int)(this.imageSize.Height * rate));
                    }
                    else
                    {
                        imageRenderSize = this.imageSize;
                    }

                    //image area
                    rects[0] = new Rectangle((this.Width - imageRenderSize.Width) / 2, (this.Height - imageRenderSize.Height) / 2, imageRenderSize.Width, imageRenderSize.Height);

                    //leftArrow
                    rects[1] = new Rectangle((_arrowAreaWidth - _arrowSize.Width) / 2, (this.Height - _arrowSize.Height) /2 , _arrowSize.Width, _arrowSize.Height);

                    //rightArrow
                    rects[2] = new Rectangle(this.Width - (_arrowAreaWidth + _arrowSize.Width) / 2, (this.Height - _arrowSize.Height) / 2, _arrowSize.Width, _arrowSize.Height);

                    //expansion
                    int expansionWidth = Math.Min(Math.Min(rects[0].Width - _expansionmargin * 2, _expansionSize.Width), Math.Min(rects[0].Height - _expansionmargin * 2, _expansionSize.Height));
                    Size expansionSize = new Size(Math.Max(expansionWidth, 0 ), Math.Max(expansionWidth, 0));
                    rects[3] = new Rectangle(rects[0].Right - _expansionmargin - expansionSize.Width, rects[0].Bottom - _expansionmargin - expansionSize.Height, expansionSize.Width, expansionSize.Height);

                    this.clientRectMemory = this.ClientRectangle;
                    this.imageSizeMemory = this.ImageSize;
                    this.locations = rects;
                }
                ServerComm.RLocations = this.locations;
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
                }
            }
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

        Size sMemory = new Size();
        private void DoAnimation()
        {
            this.AnimatedGif = ResourceHelper.LoadingGIF;
            
            // A Gif image's frame delays are contained in a byte array
            // in the image's PropertyTagFrameDelay Property Item's
            // value property.
            // Retrieve the byte array...
            byte[] bytes = this.PropItem.Value;
            // Create an array of integers to contain the delays,
            // in hundredths of a second, between each frame in the Gif image.
            int[] delays = new int[this.FrameCount + 1];
            int i = 0;
            for (i = 0; i <= this.FrameCount - 1; i++)
            {
                delays[i] = BitConverter.ToInt32(bytes, i * 4);
            }
            // Play the Gif one time...
            while (true)
            {
                if (this.ImageViewerModel == ImageViewerModel.PaintRender)
                {
                    System.Threading.Thread.Sleep(100);
                    continue;
                }
                else
                {
                    for (i = 0; i <= this.FrameCount - 1; i++)
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

                        System.Threading.Thread.Sleep(delays[i] * 15);
                    }
                }
            }
        }

        #endregion
        private System.Windows.Forms.Label labPage = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.Label LabPage
        {
            get
            {
                return this.labPage;
            }
        }
        private void InitializeComponent()
        {
            
            this.labPage = new Label();

            this.SuspendLayout();
            // labPage
            // 
            //this.labPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labPage.BackColor = Color.FromArgb(255, 240, 240, 240);
            this.labPage.Location = new System.Drawing.Point(0, 0);
            this.labPage.Name = "labPage";
            this.labPage.Size = new System.Drawing.Size(60, 25);
            this.labPage.TabIndex = 9;
            this.labPage.Font = new Font("", 10, FontStyle.Regular); 
            this.labPage.AutoSize = true;
            this.labPage.Text = "9 / 10";

            this.Controls.Add(this.labPage);
            this.ResumeLayout(false);
            //this.labPage.BringToFront();
            this.SuspendLayout();
            this.labPage.Location = new Point(1,1);
            this.ResumeLayout();

            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
    }

    public enum ImageViewerModel
    {
        DoAnimation = 0,
        PaintRender = 1,
    }
    

    public class ImageViewerItem
    {
        private Yoki.Core.DataEventHandle<ImageViewerItem> OnLoaded;

        public ImageViewerItem(int index, string link, Yoki.Core.DataEventHandle<ImageViewerItem> onLoaded)
        {
            this.Index = index;
            this.OnLoaded = onLoaded;
            this.Link = link;
        }

        private int Index
        {
            get;
            set;
        }

        private string link = string.Empty;
        string Link
        {
            get
            {
                return this.link;
            }
            set
            {
                if (this.link != value && !string.IsNullOrEmpty(value) && !this.Isloaded)
                {
                    this.link = value;
                    this.Isloaded = false;
                    
                    Download(new Uri(this.link), (data) =>
                    {
                        REINIT:
                        try
                        {
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(data))
                            {
                                this.Image = new Bitmap(ms);
                                
                            }
                            this.Isloaded = true;
                            if (this.OnLoaded != null)
                            {
                                this.OnLoaded(this);
                            }
                        }
                        catch (Exception)
                        {
                            System.Threading.Thread.Sleep(5 * 1000);
                            goto REINIT;
                        }
                    });
                }
            }
        }

        public Bitmap Image
        {
            get;
            set;
        }

        public bool Isloaded
        {
            get;
            set;
        }

        public static void Download(Uri uri, Action<byte[]> handle)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(
                delegate
                {
                    REDOWNLOAD:
                    byte[] data = null;
                    var webClient = new System.Net.WebClient();
                    try
                    {
                        data = webClient.DownloadData(uri);
                    }
                    catch (Exception)
                    {
                        System.Threading.Thread.Sleep(5 * 1000);
                        goto REDOWNLOAD;
                    }
                    handle(data);
                });
        }
    }

}
