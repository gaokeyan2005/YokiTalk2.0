using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yoki.View
{
    public class UserInfo
    {
        public Image HeaderImage
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Info
        {
            get;
            set;
        }
    }

    public class UserHeader: System.Windows.Forms.Control
    {

        public UserHeader()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);

        }

        private UserInfo userInfo = null;
        public UserInfo UserInfo
        {
            get
            {
                return this.userInfo;
            }
            set
            {
                this.userInfo = value;
                this.Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            //base.OnMouseClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //base.OnMouseDown(e);
        }

        public Size imageSize = new Size(48, 48);
        public UserHeader(System.Drawing.Bitmap bitmap, string nickName): this()
        {

        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            int minEdge = Math.Min(this.ClientRectangle.Width, this.ClientRectangle.Height);
            Rectangle shadowRect = new Rectangle(new Point(0, 0), new Size(minEdge, minEdge));


            //base.OnPaint(e);
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(shadowRect);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = Color.FromArgb(24, 0, 0, 0);
                    brush.SurroundColors = new Color[] { Color.FromArgb(24, 0, 0, 0) };
                    brush.FocusScales = new PointF(.2f, .45f);

                    //e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    e.Graphics.FillPath(brush, path);
                    //e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
                }
            }


            Rectangle whiteRect = shadowRect;
            whiteRect.Inflate(-1, -1);

            using(Brush brush = new SolidBrush(Color.White))
            {
                e.Graphics.FillEllipse(brush, whiteRect);
	        }

            GraphicsState gs = e.Graphics.Save();


            Rectangle imageRect = whiteRect;
            imageRect.Inflate(-2, -2);

            if (this.UserInfo != null && this.UserInfo.HeaderImage != null)
            {
                Bitmap bitmap = KiResizeImage(new Bitmap(this.UserInfo.HeaderImage), imageRect.Width, imageRect.Height, InterpolationMode.HighQualityBicubic);

                using (Image image = Image.FromHbitmap(bitmap.GetHbitmap()))
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddEllipse(imageRect);
                        using (Region r = new Region(path))
                        {
                            e.Graphics.Clip = r;
                            e.Graphics.DrawImage(image, imageRect.Location);
                        }
                    }
                }
            }

            

            e.Graphics.Restore(gs);

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            //e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            using (Pen pen = new Pen(Brushes.White, 1))
            {

                e.Graphics.DrawEllipse(pen, imageRect);
            }

            if (this.UserInfo != null && !string.IsNullOrEmpty(this.UserInfo.Name))
            {
                SizeF fontSize = e.Graphics.MeasureString(this.userInfo.Name, this.Font);
                TextRenderer.DrawText(
                    e.Graphics,
                    this.userInfo.Name,
                    this.Font,
                    new Rectangle(new Point(shadowRect.Right + 5, (this.ClientRectangle.Height - (int)fontSize.Height) / 2), new Size(this.ClientRectangle.Width - shadowRect.Right - 5, (int)fontSize.Height)),
                    Color.FromArgb(255, 96, 96, 96),
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.Left |
                    TextFormatFlags.SingleLine |
                    TextFormatFlags.WordEllipsis);

            }
        }

        public static Bitmap KiResizeImage(Bitmap bmp, int newW, int newH, InterpolationMode mode)
        {
            double rateW = (double)bmp.Width / newW;
            double rateH = (double)bmp.Height / newH;

            double rate = Math.Min(rateW, rateH);




            try
            {
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量
                g.InterpolationMode = mode;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }
    }
}
