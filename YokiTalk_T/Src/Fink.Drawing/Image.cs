using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fink.Drawing
{
    public class Image
    {
        public static Bitmap KiResizeImage(System.Drawing.Image bmp, int newW, int newH, InterpolationMode mode)
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
