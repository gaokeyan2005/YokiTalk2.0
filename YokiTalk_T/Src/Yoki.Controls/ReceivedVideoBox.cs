using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Controls
{
    public class ReceivedVideoBox : VideoBox
    {
        private static int _layerImageHeight = 24;
        public ReceivedVideoBox()
        {

        }

        public override bool IsOverlayer
        {
            get
            {
                return true;
          
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Font MicroFont
        {
            get;
            set;
        }

        private RemoteUserInfo remoteUserInfo = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RemoteUserInfo RemoteUserInfo
        {
            get
            {
                return this.remoteUserInfo;
            }
            set
            {
                if (this.remoteUserInfo != value)
                {
                    this.remoteUserInfo = value;
                    this.IsNeedRender = true;
                }
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
            }
        }

        private void Render()
        {
            using (Graphics g = Graphics.FromImage(this.layerImage.Value))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(128, 0, 0, 0)), new Rectangle(this.OverlayerRectangle.Left, this.OverlayerRectangle.Top, this.OverlayerRectangle.Width, this.OverlayerRectangle.Height - 1));
                g.FillRectangle(new SolidBrush(Color.FromArgb(160, 0, 0, 0)), new Rectangle(this.OverlayerRectangle.Left, this.OverlayerRectangle.Bottom - 1, this.OverlayerRectangle.Width, 1));

                if (this.RemoteUserInfo == null)
                {
                    return;
                }

                string name = this.RemoteUserInfo.Name;
                Size nameSize = System.Windows.Forms.TextRenderer.MeasureText(g, name, this.Font, Size.Empty, System.Windows.Forms.TextFormatFlags.NoPadding);
                Rectangle nameRect = new Rectangle((this.OverlayerRectangle.Width - nameSize.Width) / 2, (_layerImageHeight + 1 - nameSize.Height) / 2, nameSize.Width, nameSize.Height);
                PaintText(name, this.Font, g, nameRect);
                
                string age = this.RemoteUserInfo.Age + " Y.";
                Size ageSize = System.Windows.Forms.TextRenderer.MeasureText(g, age, this.MicroFont, Size.Empty, System.Windows.Forms.TextFormatFlags.NoPadding);
                Rectangle ageRect = new Rectangle(this.OverlayerRectangle.Right - 5 - 16 - 12  - ageSize.Width, (_layerImageHeight + 1 - ageSize.Height) / 2, ageSize.Width, ageSize.Height);
                PaintText(age, this.MicroFont, g, ageRect);

                Image genderIcon = null;
                switch (this.RemoteUserInfo.Gender)
                {
                    case Core.Gender.Male:
                        genderIcon = ResourceHelper.GenderBoy;
                        break;
                    case Core.Gender.Female:
                        genderIcon = ResourceHelper.GenderGirl;
                        break;
                    default:
                        genderIcon = ResourceHelper.GenderUnknown;
                        break;
                }

                Size iconSize = genderIcon.Size;
                Rectangle iconRect = new Rectangle(this.OverlayerRectangle.Right - 5 - iconSize.Width, (_layerImageHeight + 1 - iconSize.Height) / 2, iconSize.Width, iconSize.Height);
                g.DrawImage(genderIcon, iconRect);

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

    public class RemoteUserInfo
    {
        public string Name
        {
            get;
            set;
        }
        public short Age
        {
            get;
            set;
        }

        public Core.Gender Gender
        {
            get;
            set;
        }
    }
    
}
