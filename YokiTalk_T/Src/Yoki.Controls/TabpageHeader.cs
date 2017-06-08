using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yoki.Controls
{
    public class TabpageHeader: Fink.Windows.Forms.PanelEx
    {
        private string text = string.Empty;
        public override string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }

        public TabpageHeader():base()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 192, 194, 204)), new Rectangle(0, this.ClientRectangle.Bottom - 1, this.ClientRectangle.Width, 1));

            Size textSize = System.Windows.Forms.TextRenderer.MeasureText(e.Graphics, this.Text, this.Font, Size.Empty, System.Windows.Forms.TextFormatFlags.NoPadding);

            Rectangle textRect = new Rectangle(this.Left + 20, (this.Height - textSize.Height) / 2, textSize.Width, textSize.Height);
            PaintText(this.Text, this.Font, e.Graphics, textRect);

            Image topicImage = ResourceHelper.NoPendingTopic;
            Point topicLocation = new Point(this.Width - 4 - topicImage.Width, (this.Height - topicImage.Height) / 2);

            //e.Graphics.DrawImage(topicImage, new Rectangle(topicLocation, topicImage.Size));
        }


        private static void PaintText(string text, Font font, Graphics g, Rectangle rect)
        {
            using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
            {
                g.DrawString(text, font, new SolidBrush(Color.FromArgb(255, 61, 61, 61)), new Point(rect.Left, rect.Top));
            };
        }
    }
}
