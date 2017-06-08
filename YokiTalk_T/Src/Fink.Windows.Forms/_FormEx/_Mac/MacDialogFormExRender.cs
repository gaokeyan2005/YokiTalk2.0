using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Fink.Drawing;

namespace Fink.Windows.Forms
{
    class MacDialogFormExRender : MacFormExRenderer
    {
        public MacDialogFormExRender(FormExColorTable colortable)
            : base(colortable)
        {

        }

        protected override void OnRenderFormExCaption(
            FormExCaptionRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;
            FormEx form = e.FormEx;
            Rectangle iconRect = form.IconRect;
            Rectangle textRect = Rectangle.Empty;

            bool closeBox = form.ControlBox;
            bool minimizeBox = form.ControlBox && form.MinimizeBox;
            bool maximizeBox = form.ControlBox && form.MaximizeBox;

            SizeF textSize = g.MeasureString(form.Text, form.CaptionFont);

            textRect = new Rectangle(
                Convert.ToInt32(Math.Max((form.Width - textSize.Width) / 2, 0)),
                form.ControlBoxOffset.Y + (int)Math.Abs((16 - form.Font.Size) / 2),
                rect.Width - iconRect.Right - 6,
                rect.Height - form.BorderWidth);

            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                DrawCaptionBackground(
                    g,
                    rect,
                    e.Active);

                if (form.ShowIcon && form.Icon != null)
                {
                    DrawIcon(g, iconRect, form.Icon);
                }

                if (!string.IsNullOrEmpty(form.Text))
                {
                    DrawCaptionText(
                        g,
                        textRect,
                        form.Text,
                        form.CaptionFont);
                }
            }
        }
    }
}
