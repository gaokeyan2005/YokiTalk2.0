using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.IM.Graphic
{
    public class VideoGraphic
    {
        public static Image ShowVideoFrame(Graphics graphics, Size clientSize, VideoFrame frame, Image overLayerImage, Rectangle overLayerRectangle)
        {
            if (clientSize.Width <= 0 || clientSize.Height <= 0)
            {
                return null;
            }

            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.AssumeLinear;

            float sw = (float)clientSize.Width / frame.Width;
            float sh = (float)clientSize.Height / frame.Height;
            var scale = Math.Min(sw, sh);
            var newWidth = frame.Width * scale;
            var newHeight = frame.Height * scale;

            Bitmap frameBitmap = new Bitmap(clientSize.Width, clientSize.Height);

            using (Graphics g = Graphics.FromImage(frameBitmap))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 61, 61, 61)), new Rectangle(0, 0, clientSize.Width, clientSize.Height));


                using (var stream = frame.GetBmpStream())
                {
                    using (Bitmap img = new Bitmap(stream))
                    {
                        g.DrawImage(img, new Rectangle((int)(clientSize.Width - newWidth) / 2, (int)(clientSize.Height - newHeight) / 2, (int)newWidth, (int)newHeight),
                            new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
                    }
                }
                g.DrawImage(overLayerImage, overLayerRectangle, overLayerRectangle, GraphicsUnit.Pixel);

            }
            try
            {
                graphics.DrawImage(frameBitmap, new Point(0, 0));
            }
            catch (Exception)
            {
            }
            return frameBitmap;
        }


    }
}
