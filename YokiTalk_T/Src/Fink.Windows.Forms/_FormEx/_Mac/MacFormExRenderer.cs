using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Fink.Drawing;

namespace Fink.Windows.Forms
{
    class MacFormExRenderer : FormExRenderer
    {
        private FormExColorTable colorTable;

        System.Resources.ResourceManager resManager;
        public MacFormExRenderer(FormExColorTable colortable)
            : base()
        {
            this.colorTable = colortable;
            resManager = new System.Resources.ResourceManager("Fink.Windows.Forms.Properties.Resources", System.Reflection.Assembly.GetCallingAssembly());
        }

        public FormExColorTable ColorTable
        {
            get 
            {
                return colorTable;
            }
        }

        public override Region CreateRegion(FormEx form)
        {
            RectangleF rect = new RectangleF(Point.Empty, form.Size);
            rect.Width += .5f;
            rect.Height += .5f;
            using (GraphicsPath path = RectangleEx.CreatePath(
                rect,
                form.Radius + 1,
                form.RoundStyle))
            {
                Matrix m = new Matrix();
                m.Translate(-.75f, -.75f);
                path.Transform(m);
                return new Region(path);
            }
        }

        public override void InitFormEx(FormEx form)
        {
            form.BackColor = this.ColorTable.Border;
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


            textRect = new Rectangle(
                form.ControlBoxOffset.X + form.ControlBoxSpace + form.CloseBoxSize.Width + form.ControlBoxSpace + form.MinimizeBoxSize.Width + form.ControlBoxSpace + 42,
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

        protected override void OnRenderFormExBorder(
            FormExBorderRenderEventArgs e)
        {
            if (e.FormEx.WindowState == FormWindowState.Maximized)
            {
                return;
            }

            Graphics g = e.Graphics;
            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                DrawBorder(
                    g,
                    e.ClipRectangle,
                    e.FormEx.RoundStyle,
                    e.FormEx.Radius);
            }
        }

        protected override void OnRenderFormExInnerBorder(
            FormExBorderRenderEventArgs e)
        {
            if (e.FormEx.WindowState == FormWindowState.Maximized)
            {
                return;
            }

            Graphics g = e.Graphics;
            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                DrawInnerBorder(
                    g,
                    e.ClipRectangle,
                    e.FormEx.RoundStyle,
                    e.FormEx.Radius);
            }
        }

        protected override void OnRenderFormExBackground(
            FormExBackgroundRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;
            FormEx form = e.FormEx;
            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                using (Brush brush = new SolidBrush(ColorTable.BackColor))
                {
                    using (GraphicsPath path = RectangleEx.CreatePath(
                        rect, form.Radius - 1, form.RoundStyle))
                    {
                        g.PixelOffsetMode = PixelOffsetMode.Half;
                        g.FillPath(brush, path);
                        g.PixelOffsetMode = PixelOffsetMode.Default;
                    }

                }
            }
        }

        protected override void OnRenderFormExBackgroundSub(
            FormExBackgroundRenderEventArgs e)
        {
            if (e.FormEx.IsDialog)
            {
                return;
            }
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;
            FormEx form = e.FormEx;
            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(255, 235, 236, 239)))
                {
                    Rectangle rightRect = new Rectangle(282, rect.Top, rect.Width - 280, rect.Height);
                    using (GraphicsPath path = RectangleEx.CreatePath(
                        rightRect, form.Radius - 1, RoundStyle.RightTop | RoundStyle.RightBottom))
                    {
                        g.PixelOffsetMode = PixelOffsetMode.Half;
                        g.FillPath(brush, path);
                        g.PixelOffsetMode = PixelOffsetMode.Default;
                    }
                }

            }
        }

        private bool isHoverOnControlBox = false;
        protected override void OnRenderFormExControlBox(
            FormExControlBoxRenderEventArgs e)
        {
            FormEx form = e.Form;
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;
            ControlBoxState state = e.ControlBoxtate;
            bool active = e.Active;

            if (form.ControlBoxManager.CloseBoxState == ControlBoxState.Hover || form.ControlBoxManager.CloseBoxState == ControlBoxState.Pressed
                || form.ControlBoxManager.MaximizeBoxState == ControlBoxState.Hover || form.ControlBoxManager.MaximizeBoxState == ControlBoxState.Pressed
                || form.ControlBoxManager.MinimizeBoxState == ControlBoxState.Hover || form.ControlBoxManager.MinimizeBoxState == ControlBoxState.Pressed)
            {
                isHoverOnControlBox = true;
            }
            else
            {
                isHoverOnControlBox = false;
            }

            bool minimizeBox = form.ControlBox && form.MinimizeBox;
            bool maximizeBox = form.ControlBox && form.MaximizeBox;

            switch (e.ControlBoxStyle)
            {
                case ControlBoxStyle.Close:
                    RenderFormExCloseBoxInternal(
                        g,
                        rect,
                        isHoverOnControlBox? ControlBoxState.Hover: state,
                        active,
                        minimizeBox,
                        maximizeBox);
                    break;
                case ControlBoxStyle.Maximize:
                    RenderFormExMaximizeBoxInternal(
                        g,
                        rect,
                        isHoverOnControlBox ? ControlBoxState.Hover : state,
                        active,
                        minimizeBox,
                        form.WindowState == FormWindowState.Maximized);
                    break;
                case ControlBoxStyle.Minimize:
                    RenderFormExMinimizeBoxInternal(
                       g,
                       rect,
                        isHoverOnControlBox ? ControlBoxState.Hover : state,
                       active);
                    break;
            }
        }

        #region Draw Methods

        protected void DrawCaptionBackground(
            Graphics g, System.Drawing.Rectangle captionRect, bool active)
        {
            Color baseColor = active ?
                ColorTable.CaptionActive : ColorTable.CaptionDeactive;

            RenderHelper.RenderBackgroundInternal(
                g,
                captionRect,
                baseColor,
                ColorTable.Border,
                ColorTable.InnerBorder,
                RoundStyle.None,
                0,
                .25f,
                false,
                false,
                LinearGradientMode.Vertical);
        }

        protected void DrawIcon(
            Graphics g, Rectangle iconRect, Icon icon)
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawIcon(
                icon,
                iconRect);
        }

        protected void DrawCaptionText(
            Graphics g, Rectangle textRect, string text, Font font)
        {
            TextRenderer.DrawText(
                g,
                text,
                font,
                new Rectangle(new Point(textRect.X, textRect.Y + 1), textRect.Size),
                Color.FromArgb(96, 0,0,0),
                TextFormatFlags.Top |
                TextFormatFlags.Left |
                TextFormatFlags.SingleLine |
                TextFormatFlags.WordEllipsis);

            TextRenderer.DrawText(
                g,
                text,
                font,
                textRect,
                ColorTable.CaptionForeground,
                TextFormatFlags.Top |
                TextFormatFlags.Left |
                TextFormatFlags.SingleLine |
                TextFormatFlags.WordEllipsis);
        }

        private void DrawBorder(
            Graphics g, Rectangle rect,RoundStyle roundStyle, int radius)
        {
            using (GraphicsPath path = RectangleEx.CreatePath(
                rect, radius, roundStyle, new SizeF(-1f, -1f)))
            {
                using (Pen pen = new Pen(ColorTable.Border))
                {
                    g.DrawPath(pen, path);
                }
            }
        }


        private void DrawInnerBorder(
            Graphics g, Rectangle rect, RoundStyle roundStyle, int radius)
        {
            Rectangle r = RectangleEx.Inflate(rect, new Size(-1, -1));
            using (GraphicsPath path = RectangleEx.CreatePath(
                r, radius - 1, roundStyle, new SizeF(-1f, -1f)))
            {
                using (Pen pen = new Pen(ColorTable.InnerBorder))
                {
                    g.DrawPath(pen, path);
                }
            }

        }

        private void RenderFormExMinimizeBoxInternal(
           Graphics g,
           Rectangle rect,
           ControlBoxState state,
           bool active)
        {
            Color baseColor = ColorTable.ControlBoxActive;

            if (state == ControlBoxState.Pressed)
            {
                baseColor = ColorTable.ControlBoxPressed;
            }
            else if (state == ControlBoxState.Hover)
            {
                baseColor = ColorTable.ControlBoxHover;
            }
            else
            {
                baseColor = active ?
                    ColorTable.ControlBoxActive :
                    ColorTable.ControlBoxDeactive;
            }

            if (!active)
            {
                using (System.Drawing.Image img = resManager.GetObject("TrafficLights_Dark") as System.Drawing.Image)
                {
                    g.DrawImage(img, rect, new Rectangle(new Point(20, 0), rect.Size), GraphicsUnit.Pixel);
                }
                return;
            }

            if (state == ControlBoxState.Pressed || state == ControlBoxState.Hover)
            {
                using (System.Drawing.Image img = resManager.GetObject("TrafficLights_LightWithIcon") as System.Drawing.Image)
                {
                    g.DrawImage(img, rect, new Rectangle(new Point(20, 0), rect.Size), GraphicsUnit.Pixel);
                }
            }
            else if (state == ControlBoxState.Normal)
            {
                using (System.Drawing.Image img = resManager.GetObject("TrafficLights_Light") as System.Drawing.Image)
                {
                    g.DrawImage(img, rect, new Rectangle(new Point(20, 0), rect.Size), GraphicsUnit.Pixel);
                }
            }
            else
            {
                using (System.Drawing.Image img = resManager.GetObject("TrafficLights_Light") as System.Drawing.Image)
                {
                    g.DrawImage(img, rect, new Rectangle(new Point(20, 0), rect.Size), GraphicsUnit.Pixel);
                }
            }

        }

        private void RenderFormExMaximizeBoxInternal(
            Graphics g,
            Rectangle rect,
            ControlBoxState state,
            bool active,
            bool minimizeBox,
            bool maximize)
        {

            if (!active)
            {
                using (System.Drawing.Image img = resManager.GetObject("TrafficLights_Dark") as System.Drawing.Image)
                {
                    g.DrawImage(img, rect, new Rectangle(new Point(40, 0), rect.Size), GraphicsUnit.Pixel);
                }
                return;
            }

            if (state == ControlBoxState.Pressed || state == ControlBoxState.Hover)
            {
                using (System.Drawing.Image img = resManager.GetObject("TrafficLights_LightWithIcon") as System.Drawing.Image)
                {
                    g.DrawImage(img, rect, new Rectangle(new Point(40, 0), rect.Size), GraphicsUnit.Pixel);
                }
            }
            else if (state == ControlBoxState.Normal)
            {
                using (System.Drawing.Image img = resManager.GetObject("TrafficLights_Light") as System.Drawing.Image)
                {
                    g.DrawImage(img, rect, new Rectangle(new Point(40, 0), rect.Size), GraphicsUnit.Pixel);
                }
            }
            else
            {
                using (System.Drawing.Image img = resManager.GetObject("TrafficLights_Light") as System.Drawing.Image)
                {
                    g.DrawImage(img, rect, new Rectangle(new Point(40, 0), rect.Size), GraphicsUnit.Pixel);
                }
            }
        }

        private void RenderFormExCloseBoxInternal(
            Graphics g,
            Rectangle rect,
            ControlBoxState state,
            bool active,
            bool minimizeBox,
            bool maximizeBox)
        {

            if (!active)
            {
                using (System.Drawing.Image img = resManager.GetObject("TrafficLights_Dark") as System.Drawing.Image)
                {
                    g.DrawImage(img, rect, new Rectangle(new Point(0, 0), rect.Size), GraphicsUnit.Pixel);
                }
                return;
            }

            if (state == ControlBoxState.Pressed || state == ControlBoxState.Hover)
            {
                using (System.Drawing.Image img = resManager.GetObject("TrafficLights_LightWithIcon") as System.Drawing.Image)
                {
                    g.DrawImage(img, rect, new Rectangle(new Point(0, 0), rect.Size), GraphicsUnit.Pixel);
                }
            }
            else if (state == ControlBoxState.Normal)
            {
                using (System.Drawing.Image img = resManager.GetObject("TrafficLights_Light") as System.Drawing.Image)
                {
                    g.DrawImage(img, rect, new Rectangle(new Point(0, 0), rect.Size), GraphicsUnit.Pixel);
                }
            }
            else
            {
                using (System.Drawing.Image img = resManager.GetObject("TrafficLights_Light") as System.Drawing.Image)
                {
                    g.DrawImage(img, rect, new Rectangle(new Point(0, 0), rect.Size), GraphicsUnit.Pixel);
                }
            }
        }

        #endregion

        private GraphicsPath CreateCloseFlagPath(Rectangle rect)
        {
            PointF centerPoint = new PointF(
                rect.X + rect.Width / 2,
                rect.Y + rect.Height / 2);

            RectangleF rectArea = new RectangleF((int)(centerPoint.X) - 3.5f, (int)(centerPoint.Y) - 3.5f, 7, 7); 

            GraphicsPath path = new GraphicsPath();
            path.FillMode = FillMode.Winding;

            path.AddLine( new PointF(rectArea.Left + 1, rectArea.Top), rectArea.Location);

            path.AddLine(rectArea.Location, new PointF(rectArea.Left, rectArea.Top + 1));

            path.AddLine(new PointF(rectArea.Left, rectArea.Top + 1), new PointF(rectArea.Left + (rectArea.Width / 2) - 1, rectArea.Top + (rectArea.Height / 2)));

            path.AddLine(new PointF(rectArea.Left + (rectArea.Width / 2) - 1, rectArea.Top + (rectArea.Height / 2)), new PointF(rectArea.Left, rectArea.Bottom - 1));

            path.AddLine( new PointF(rectArea.Left, rectArea.Bottom - 1), new PointF(rectArea.Left, rectArea.Bottom));

            path.AddLine(new PointF(rectArea.Left, rectArea.Bottom), new PointF(rectArea.Left + 1, rectArea.Bottom));

            path.AddLine(new PointF(rectArea.Left + 1, rectArea.Bottom), new PointF(rectArea.Left + (rectArea.Width / 2), rectArea.Bottom - (rectArea.Height / 2 - 1)));

            path.AddLine(new PointF(rectArea.Left + (rectArea.Width / 2), rectArea.Bottom - (rectArea.Height / 2 - 1)), new PointF(rectArea.Right - 1, rectArea.Bottom));

            path.AddLine(new PointF(rectArea.Right - 1, rectArea.Bottom), new PointF(rectArea.Right, rectArea.Bottom));

            path.AddLine(new PointF(rectArea.Right, rectArea.Bottom), new PointF(rectArea.Right, rectArea.Bottom - 1));

            path.AddLine(new PointF(rectArea.Right, rectArea.Bottom - 1), new PointF(rectArea.Right - (rectArea.Width / 2 - 1), rectArea.Top + (rectArea.Height / 2)));

            path.AddLine(new PointF(rectArea.Right - (rectArea.Width / 2 - 1), rectArea.Top + (rectArea.Height / 2)), new PointF(rectArea.Right, rectArea.Top + 1));

            path.AddLine(new PointF(rectArea.Right, rectArea.Top + 1), new PointF(rectArea.Right, rectArea.Top));

            path.AddLine(new PointF(rectArea.Right, rectArea.Top), new PointF(rectArea.Right - 1, rectArea.Top));

            path.AddLine(new PointF(rectArea.Right - 1, rectArea.Top), new PointF(rectArea.Left + (rectArea.Width / 2), rectArea.Top + (rectArea.Height / 2 - 1)));
             
            path.CloseFigure();
            return path;
        }

        private GraphicsPath CreateMinimizeFlagPath(Rectangle rect)
        {
            PointF centerPoint = new PointF(
                rect.X + rect.Width / 2.0f,
                rect.Y + rect.Height / 2.0f);
 
            GraphicsPath path = new GraphicsPath();

            path.AddRectangle(new RectangleF(
                centerPoint.X - 4.5f,
                centerPoint.Y + .5f,
                8,
                2));
            return path;
        }

        private GraphicsPath CreateMaximizeFlafPath(
            Rectangle rect, bool maximize)
        {
            PointF centerPoint = new PointF(
               rect.X + rect.Width / 2,
               rect.Y + rect.Height / 2);

            GraphicsPath path = new GraphicsPath();

            if (maximize)
            {
                path.AddRectangle(new RectangleF(
                    centerPoint.X - 6.5f,
                    centerPoint.Y - 3.5f,
                    8,
                    6));
                path.AddRectangle(new RectangleF(
                    centerPoint.X - 5.5f,
                    centerPoint.Y - 1.5f,
                    6,
                    3));


                path.AddRectangle(new RectangleF(
                    centerPoint.X - 2.5f,
                    centerPoint.Y - 3.5f,
                    8,
                    6));
                path.AddRectangle(new RectangleF(
                    centerPoint.X - 1.5f,
                    centerPoint.Y - 1.5f,
                    6,
                    3));
            }
            else
            {
                path.AddRectangle(new RectangleF(
                    centerPoint.X - 4.5f,
                    centerPoint.Y - 3.5f,
                    8,
                    6));
                path.AddRectangle(new RectangleF(
                    centerPoint.X - 3.5f,
                    centerPoint.Y - 1.5f,
                    6,
                    3));
            }

            return path;
        }
    }
}
