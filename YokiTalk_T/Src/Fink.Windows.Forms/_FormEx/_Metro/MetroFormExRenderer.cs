using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Fink.Drawing;

namespace Fink.Windows.Forms
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    class MetroFormExRenderer : FormExRenderer
    {
        private FormExColorTable colorTable;

        public MetroFormExRenderer(FormExColorTable colortable)
            : base()
        {
            this.colorTable = colortable;
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
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddRectangle(rect);
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

            bool closeBox = form.ControlBox && form.CloseBox;
            bool minimizeBox = form.ControlBox && form.MinimizeBox;
            bool maximizeBox = form.ControlBox && form.MaximizeBox;

            Size textSize = TextRenderer.MeasureText(form.Text, form.CaptionFont);
#pragma warning disable CS0219 // The variable 'textWidthDec' is assigned but its value is never used
            int textWidthDec = 0;
#pragma warning restore CS0219 // The variable 'textWidthDec' is assigned but its value is never used
            //if (closeBox)
            //{
            //    textWidthDec += form.CloseBoxSize.Width + form.ControlBoxOffset.X;
            //}

            //if (maximizeBox)
            //{
            //    textWidthDec += form.MaximizeBoxSize.Width + form.ControlBoxSpace;
            //}

            //if (minimizeBox)
            //{
            //    textWidthDec += form.MinimizeBoxSize.Width + form.ControlBoxSpace;
            //}

            textRect = new Rectangle(
                (rect.Width - textSize.Width) / 2 + form.TitleOffset,
                (rect.Height - textSize.Height) / 2,
                textSize.Width,
                textSize.Height);

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
            Rectangle rect = RectangleEx.Inflate(e.ClipRectangle, new Size(-1, -1));
            FormEx form = e.FormEx;
            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                Color formColor = Color.Empty;
                switch (form.ThemeType)
	            {
		            case ThemeType.Light:
                        formColor = ColorTable.BackColor;
                     break;
                    case ThemeType.Dark:
                        formColor = ColorTable.DarkThemeBackColor;
                     break;
                    default:
                        formColor = ColorTable.BackColor;
                     break;
	            }
                using (Brush brush = new SolidBrush(formColor))
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
            
        }

        protected override void OnRenderFormExControlBox(
            FormExControlBoxRenderEventArgs e)
        {
            FormEx form = e.Form;
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;
            ControlBoxState state = e.ControlBoxtate;
            bool active = e.Active;

            bool minimizeBox = form.ControlBox && form.MinimizeBox;
            bool maximizeBox = form.ControlBox && form.MaximizeBox;

            switch (e.ControlBoxStyle)
            {
                case ControlBoxStyle.Close:
                    RenderFormExCloseBoxInternal(
                    g,
                    rect,
                    state,
                    active,
                    minimizeBox,
                    maximizeBox);
                    break;
                case ControlBoxStyle.Maximize:
                    RenderFormExMaximizeBoxInternal(
                        g,
                        rect,
                        state,
                        active,
                        minimizeBox,
                        form.WindowState == FormWindowState.Maximized);
                    break;
                case ControlBoxStyle.Minimize:
                    RenderFormExMinimizeBoxInternal(
                       g,
                       rect,
                       state,
                       active);
                    break;
            }
        }

        #region Draw Methods

        private void DrawCaptionBackground(
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

        private void DrawIcon(
            Graphics g, Rectangle iconRect, Icon icon)
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawIcon(
                icon,
                iconRect);
        }

        private void DrawCaptionText(
            Graphics g, Rectangle textRect, string text, Font font)
        {
            TextRenderer.DrawText(
                g,
                text,
                font,
                new Rectangle(new Point(textRect.X, textRect.Y + 1), textRect.Size),
                Color.FromArgb(96, 255, 255, 255),
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.Left |
                TextFormatFlags.SingleLine |
                TextFormatFlags.WordEllipsis);

            TextRenderer.DrawText(
                g,
                text,
                font,
                textRect,
                ColorTable.CaptionForeground,
                TextFormatFlags.VerticalCenter |
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

            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                using (SolidBrush brush = new SolidBrush(baseColor))
                {
                    using (GraphicsPath path =
                            RectangleEx.CreatePath(new Rectangle(new Point(rect.X, rect.Y), new Size(rect.Width, rect.Height)), 0, RoundStyle.None))
                    {
                        g.PixelOffsetMode = PixelOffsetMode.Half;
                        g.FillPath(brush, path);
                        g.PixelOffsetMode = PixelOffsetMode.Default;
                    }
                }

                if (state == ControlBoxState.Pressed)
                {
                    #region inner shadow
                    using (SolidBrush brush = new SolidBrush(this.ColorTable.Shadow))
                    {
                        using (GraphicsPath path =
                                RectangleEx.CreatePath(new Rectangle(new Point(rect.X, rect.Y), new Size(rect.Width, 1)), 0, RoundStyle.None))
                        {
                            g.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillPath(brush, path);
                            g.PixelOffsetMode = PixelOffsetMode.Default;
                        }
                    }

                    using (SolidBrush brush = new SolidBrush(this.ColorTable.Shadow))
                    {
                        using (GraphicsPath path =
                                RectangleEx.CreatePath(new Rectangle(new Point(rect.X, rect.Y), new Size(1, rect.Height)), 0, RoundStyle.None))
                        {
                            g.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillPath(brush, path);
                            g.PixelOffsetMode = PixelOffsetMode.Default;
                        }
                    }
                    #endregion

                    #region highLight
                    using (SolidBrush brush = new SolidBrush(this.ColorTable.HighLight))
                    {
                        using (GraphicsPath path =
                                RectangleEx.CreatePath(new Rectangle(new Point(rect.X, rect.Y + rect.Height), new Size(rect.Width, 1)), 0, RoundStyle.None))
                        {
                            g.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillPath(brush, path);
                            g.PixelOffsetMode = PixelOffsetMode.Default;
                        }
                    }
                    #endregion
                }

                

                using (GraphicsPath path = CreateMinimizeFlagPath(rect))
                {
                    using (Brush brush = new SolidBrush(
                            state == ControlBoxState.Pressed ? ColorTable.ControlBoxIconPressed :
                            (state == ControlBoxState.Hover ? ColorTable.ControlBoxIconHover : ColorTable.ControlBoxIconActive)
                        ))
                    {
                        g.FillPath(brush, path);
                    }
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

            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                using (SolidBrush brush = new SolidBrush(baseColor))
                {
                    using (GraphicsPath path =
                            RectangleEx.CreatePath(new Rectangle(new Point(rect.X, rect.Y), new Size(rect.Width, rect.Height)), 0, RoundStyle.None))
                    {
                        g.PixelOffsetMode = PixelOffsetMode.Half;
                        g.FillPath(brush, path);
                        g.PixelOffsetMode = PixelOffsetMode.Default;
                    }
                }

                if (state == ControlBoxState.Pressed)
                {
                    #region inner shadow
                    using (SolidBrush brush = new SolidBrush(this.ColorTable.Shadow))
                    {
                        using (GraphicsPath path =
                                RectangleEx.CreatePath(new Rectangle(new Point(rect.X, rect.Y), new Size(rect.Width, 1)), 0, RoundStyle.None))
                        {
                            g.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillPath(brush, path);
                            g.PixelOffsetMode = PixelOffsetMode.Default;
                        }
                    }

                    using (SolidBrush brush = new SolidBrush(this.ColorTable.Shadow))
                    {
                        using (GraphicsPath path =
                                RectangleEx.CreatePath(new Rectangle(new Point(rect.X, rect.Y), new Size(1, rect.Height)), 0, RoundStyle.None))
                        {
                            g.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillPath(brush, path);
                            g.PixelOffsetMode = PixelOffsetMode.Default;
                        }
                    }
                    #endregion

                    #region highLight
                    using (SolidBrush brush = new SolidBrush(this.ColorTable.HighLight))
                    {
                        using (GraphicsPath path =
                                RectangleEx.CreatePath(new Rectangle(new Point(rect.X, rect.Bottom), new Size(rect.Width, 1)), 0, RoundStyle.None))
                        {
                            g.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillPath(brush, path);
                            g.PixelOffsetMode = PixelOffsetMode.Default;
                        }
                    }
                    #endregion
                }

                using (GraphicsPath path = CreateMaximizeFlafPath(rect, maximize))
                {
                    using (Brush brush = new SolidBrush(
                            state == ControlBoxState.Pressed ? ColorTable.ControlBoxIconPressed :
                            (state == ControlBoxState.Hover ? ColorTable.ControlBoxIconHover : ColorTable.ControlBoxIconActive)
                        ))

                    {
                        g.FillPath(brush, path);
                    }
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
            Color baseColor = ColorTable.ControlCloseBoxActive;

            if (state == ControlBoxState.Pressed)
            {
                baseColor = ColorTable.ControlCloseBoxPressed;
            }
            else if (state == ControlBoxState.Hover)
            {
                baseColor = ColorTable.ControlCloseBoxHover;
            }
            else
            {
                baseColor = active ?
                    ColorTable.ControlCloseBoxActive :
                    ColorTable.ControlCloseBoxDeactive;
            }

            using (AntiAliasGraphics antiGraphics = new AntiAliasGraphics(g))
            {
                using (SolidBrush brush = new SolidBrush(baseColor))
                {
                    using (GraphicsPath path =
                            RectangleEx.CreatePath(new Rectangle(new Point(rect.X, rect.Y), new Size(rect.Width, rect.Height)), 0, RoundStyle.None))
                    {
                        g.PixelOffsetMode = PixelOffsetMode.Half;
                        g.FillPath(brush, path);
                        g.PixelOffsetMode = PixelOffsetMode.Default;
                    }
                }
                if (state != ControlBoxState.Normal)
                {
                    #region inner shadow
                    using (SolidBrush brush = new SolidBrush(this.ColorTable.Shadow))
                    {
                        using (GraphicsPath path =
                                RectangleEx.CreatePath(new Rectangle(new Point(rect.X, rect.Y), new Size(rect.Width, 1)), 0, RoundStyle.None))
                        {
                            g.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillPath(brush, path);
                            g.PixelOffsetMode = PixelOffsetMode.Default;
                        }
                    }

                    using (SolidBrush brush = new SolidBrush(this.ColorTable.Shadow))
                    {
                        using (GraphicsPath path =
                                RectangleEx.CreatePath(new Rectangle(new Point(rect.X, rect.Y), new Size(1, rect.Height)), 0, RoundStyle.None))
                        {
                            g.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillPath(brush, path);
                            g.PixelOffsetMode = PixelOffsetMode.Default;
                        }
                    }
                    #endregion

                    #region highLight
                    using (SolidBrush brush = new SolidBrush(this.ColorTable.HighLight))
                    {
                        using (GraphicsPath path =
                                RectangleEx.CreatePath(new Rectangle(new Point(rect.X, rect.Bottom), new Size(rect.Width, 1)), 0, RoundStyle.None))
                        {
                            g.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillPath(brush, path);
                            g.PixelOffsetMode = PixelOffsetMode.Default;
                        }
                    }
                    #endregion
                }

                using (GraphicsPath path = CreateCloseFlagPath(rect))
                {
                    using (Brush brush = new SolidBrush(
                            state == ControlBoxState.Pressed ? ColorTable.ControlCloseBoxIconPressed :
                            (state == ControlBoxState.Hover ? ColorTable.ControlCloseBoxIconHover : ColorTable.ControlCloseBoxIconActive)
                        ))
                    {
                        g.FillPath(brush, path);
                    }
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
