using Fink.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoki.Controls
{
    public class BlockMenuItem : IDrawable
    {
        internal static Size ItemSize = new Size(128, 96);

        public BlockMenuItem()
        {

        }

        public BlockMenuItem(string name)
        {
            this.Name = name;
        }
        public BlockMenuItem(string name, System.Drawing.Image icon)
        {
            this.Name = name;
            this.Icon = icon;
        }

        public string Name
        {
            get;
            set;
        }

        public System.Drawing.Image Icon
        {
            get;
            set;
        }

        public Font Font
        {
            get;
            set;
        }

        public Rectangle ClientRectangle
        {
            get;
            set;
        }
        
        public void OnPaint(Graphics g)
        {
            PaintIcon(g);
            PaintName(g);
        }

        private void PaintIcon(Graphics g)
        {
            Rectangle headerRect = Locations[0];
            headerRect.X = this.ClientRectangle.Left + headerRect.Left;
            headerRect.Y = this.ClientRectangle.Top + headerRect.Top;

            using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g))
            {
                using (Bitmap bitmap = Fink.Drawing.Image.KiResizeImage(this.Icon, headerRect.Width, headerRect.Height, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic))
                {
                    g.DrawImage(bitmap, headerRect);
                }
            }
        }

        private void PaintName(Graphics g)
        {
            Rectangle nameRect = Locations[1];
            nameRect.X = this.ClientRectangle.Left + nameRect.Left;
            nameRect.Y = this.ClientRectangle.Top + nameRect.Top;

            Size nameSize = TextRenderer.MeasureText(g, this.Name, this.Font, nameRect.Size, TextFormatFlags.NoPadding);

            using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
            {
                int left = nameRect.Left + (nameRect.Width - nameSize.Width) / 2;

                g.DrawString(this.Name, this.Font, new SolidBrush(Color.FromArgb(255, 77, 77, 77)), new Point(left, nameRect.Top), StringFormat.GenericTypographic);
            };
        }

        private Rectangle[] locations = null;
        private Rectangle[] Locations
        {
            get
            {
                if (this.locations == null)
                {
                    Rectangle[] rects = new Rectangle[2];

                    //icon
                    rects[0] = new Rectangle(40, 14, 48, 48);

                    //name
                    rects[1] = new Rectangle(12, 64, 104, 16);

                    this.locations = rects;
                }
                return this.locations;
            }
        }

    }
}
