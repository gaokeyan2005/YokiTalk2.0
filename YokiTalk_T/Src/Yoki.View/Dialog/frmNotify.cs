using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yoki.View.Dialog
{
    public partial class frmNotify : Fink.Windows.Forms.DialogFormEx
    {
        public frmNotify(Form parent, string text, Image icon = null)
            : base(parent)
        {
            InitializeComponent();
            this.ShowText = text;
            if (icon != null)
            {
                this.Icon = icon;
            }
        }

        public string ShowText
        {
            get;
            set;
        }

        public new Image Icon
        {
            get;
            set;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int iconRight = PaintIcon(e.Graphics);
            int textRight =  PaintText(e.Graphics, iconRight);

            this.Width = textRight + 48;
        }

        private int PaintIcon(Graphics g)
        {
            int iconRight = 0;
            if (this.Icon != null)
            {
                Size iconSize = this.Icon.Size;
                Rectangle iconRect = new Rectangle(24, (this.Height - 48 - iconSize.Height) / 2, iconSize.Width, iconSize.Height);

                using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
                {
                    int left = iconRect.Left;
                    g.DrawImage(this.Icon,
                        new Rectangle(
                        new Point(left, iconRect.Top),
                        iconSize
                        )
                        );
                    iconRight = left + this.Icon.Width;
                };
            }
            return iconRight;
        }

        private int PaintText(Graphics g, int iconRight)
        {
            int textRight = 0;
            Font nameFont = FontUtil.DefaultBoldFont;
            Size nameSize = TextRenderer.MeasureText(g, this.ShowText, this.Font, Size.Empty, TextFormatFlags.NoPadding);

            Rectangle textRect = new Rectangle(iconRight + 20, (this.Height - 48 - nameSize.Height) / 2, nameSize.Width, nameSize.Height);


            using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
            {
                g.DrawString(this.ShowText, this.Font, new SolidBrush(Color.FromArgb(255, 255, 255, 255)), textRect.Location);
            };
            textRight = textRect.Right;
            return textRight;
        }


    }
}
