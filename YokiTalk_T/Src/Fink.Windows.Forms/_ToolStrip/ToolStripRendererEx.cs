using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using Fink.Drawing;
using System.ComponentModel;

namespace Fink.Windows.Forms
{
    public class ToolStripRendererEx
        : ToolStripRenderer
    {
        private static readonly int OffsetMargin = 24;

        private RoundStyle DropdownRoundStyle = RoundStyle.All; //RoundStyle.RightTop | RoundStyle.LeftBottom | RoundStyle.RightBottom;

        //private RoundStyle MenuRoundStyle = RoundStyle.LeftTop | RoundStyle.RightTop;


        private int _radius = 5;

        [DefaultValue(5)]
        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        private string _menuLogoString = "更多精彩尽在 www.csharpwin.com";
        private ToolStripExColorTable _colorTable;

        public ToolStripRendererEx()
            : base()
        {
            
        }

        public ToolStripRendererEx(
            ToolStripExColorTable colorTable)
            : base()
        {
            _colorTable = colorTable;
        }

        public string MenuLogoString
        {
            get { return _menuLogoString; }
            set { _menuLogoString = value; }
        }

        protected ToolStripExColorTable ColorTable
        {
            get
            {
                if (_colorTable == null)
                {
                    _colorTable = new ToolStripExColorTable();
                }
                return _colorTable;
            }
        }
        public static void CreateRegion(
            Control control,
            Rectangle bounds,
            int radius,
            RoundStyle roundStyle)
        {
            using (GraphicsPath path = RectangleEx.CreatePath(bounds, radius, roundStyle))
            {
                using (GraphicsPath pathTemp =new GraphicsPath())
                {
                    pathTemp.AddLines(path.PathPoints);
                    pathTemp.AddLine(path.PathPoints[path.PathPoints.Length - 1], path.PathPoints[0]);
                    Region region = new Region(pathTemp);
                    pathTemp.Widen(Pens.White);
                    region.Union(pathTemp);
                    if (control.Region != null)
                    {
                        control.Region.Dispose();
                    }
                    control.Region = region;
                }
            }
        }
        protected override void OnRenderToolStripBackground(
            ToolStripRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            Graphics g = e.Graphics;
            Rectangle bounds = e.AffectedBounds;


            if (toolStrip is ToolStripDropDown)
            {
                bounds.Width --;
                bounds.Height--;
                CreateRegion(toolStrip, bounds, this.Radius, DropdownRoundStyle);
                using (SolidBrush brush = new SolidBrush(this.ColorTable.BackColor))
                {
                    bounds.Width++;
                    bounds.Height++;
                    using (GraphicsPath path = RectangleEx.CreatePath(bounds, this.Radius, DropdownRoundStyle))
                    {
                        using (GraphicsPath pathTemp = new GraphicsPath())
                        {
                            pathTemp.AddLines(path.PathPoints);

                            g.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillPath(brush, pathTemp);
                            g.PixelOffsetMode = PixelOffsetMode.Default;
                        }
                    }
                }
            }
            else if (toolStrip is MenuStrip)
            {
                return;
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }

        protected override void OnRenderImageMargin(
            ToolStripRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            Graphics g = e.Graphics;
            Rectangle bounds = e.AffectedBounds;

            if (toolStrip is ToolStripDropDown)
            {
                bool bDrawLogo = NeedDrawLogo(toolStrip);
                bool bRightToLeft = toolStrip.RightToLeft == RightToLeft.Yes;

                Rectangle imageBackRect = bounds;
                imageBackRect.Width = OffsetMargin;

                if (bDrawLogo)
                {
                    Rectangle logoRect = bounds;
                    logoRect.Width = OffsetMargin;
                    if (bRightToLeft)
                    {
                        logoRect.X -= 2;
                        imageBackRect.X = logoRect.X - OffsetMargin;
                    }
                    else
                    {
                        logoRect.X += 2;
                        imageBackRect.X = logoRect.Right;
                    }
                    logoRect.Y += 1;
                    logoRect.Height -= 2;

                    using (LinearGradientBrush brush = new LinearGradientBrush(
                        logoRect,
                        Color.Red, //ColorTable.BackHover,
                        ColorTable.BackNormal,
                        90f))
                    {
                        Blend blend = new Blend();
                        blend.Positions = new float[] { 0f, .2f, 1f };
                        blend.Factors = new float[] { 0f, 0.1f, .9f };
                        brush.Blend = blend;
                        logoRect.Y += 1;
                        logoRect.Height -= 2;
                        using (GraphicsPath path =
                            RectangleEx.CreatePath(logoRect, 8, RoundStyle.All))
                        {
                            g.FillPath(brush, path);
                        }
                    }

                    StringFormat sf = new StringFormat(StringFormatFlags.NoWrap);
                    Font font = new Font(
                        toolStrip.Font.FontFamily, 11, FontStyle.Bold);
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.EllipsisCharacter;

                    g.TranslateTransform(logoRect.X, logoRect.Bottom);
                    g.RotateTransform(270f);

                    if (!string.IsNullOrEmpty(MenuLogoString))
                    {
                        Rectangle newRect = new Rectangle(
                            0, 0, logoRect.Height, logoRect.Width);

                        using (Brush brush = new SolidBrush(ColorTable.Foreground))
                        {
                            g.DrawString(
                                    MenuLogoString,
                                    font,
                                    brush,
                                    newRect,
                                    sf);
                        }
                    }

                    g.ResetTransform();
                }
                else
                {
                    if (bRightToLeft)
                    {
                        imageBackRect.X -= 3;
                    }
                    else
                    {
                        imageBackRect.X += 3;
                    }
                }

                imageBackRect.Y += 2;
                imageBackRect.Height -= 4;
                using (SolidBrush brush = new SolidBrush(ColorTable.DropDownImageBack))
                {
                    g.FillRectangle(brush, imageBackRect);
                }

                Point ponitStart;
                Point pointEnd;
                if (bRightToLeft)
                {
                    ponitStart = new Point(imageBackRect.X, imageBackRect.Y);
                    pointEnd = new Point(imageBackRect.X, imageBackRect.Bottom);
                }
                else
                {
                    ponitStart = new Point(imageBackRect.Right - 1, imageBackRect.Y);
                    pointEnd = new Point(imageBackRect.Right - 1, imageBackRect.Bottom);
                }

                using (Pen pen = new Pen(ColorTable.DropDownImageSeparator))
                {
                    g.DrawLine(pen, ponitStart, pointEnd);
                }
            }
            else
            {
                base.OnRenderImageMargin(e);
            }
        }

        protected override void OnRenderToolStripBorder(
            ToolStripRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            Graphics g = e.Graphics;
            Rectangle bounds = e.AffectedBounds;

            if (toolStrip is ToolStripDropDown)
            {
                //bounds.X--;
                bounds.Width--;
                bounds.Height--;
                using (GraphicsPath path =
                    RectangleEx.CreatePath(bounds, this.Radius, DropdownRoundStyle))
                {
                    using (Pen pen = new Pen(this.ColorTable.Border))
                    {
                        //g.DrawPath(pen, path);
                        g.DrawLines(pen, path.PathPoints);
                        if (path.PathPoints.Length > 0)
                        {
                            g.DrawLine(pen, path.PathPoints[path.PathPoints.Length - 1], path.PathPoints[0]);
                        }
                    }
                }
                bounds.Inflate(-1, -1);
                using (GraphicsPath innerPath = RectangleEx.CreatePath(bounds, this.Radius - 1, DropdownRoundStyle))
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(bounds, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                    {
                        ColorBlend blend = new ColorBlend();
                        blend.Colors = new Color[] { this.ColorTable.HighLight, this.ColorTable.HighLight, Color.Transparent, Color.Transparent };
                        blend.Positions = new float[] { 0f, .1f, .2f, 1f };
                        brush.InterpolationColors = blend;
                        using (Pen pen = new Pen(brush))
                        {
                            g.DrawLines(pen, innerPath.PathPoints);
                            //if (innerPath.PathPoints.Length > 0)
                            //{
                            //    g.DrawLine(pen, innerPath.PathPoints[innerPath.PathPoints.Length - 1], innerPath.PathPoints[0]);
                            //}
                        }
                    }
                        
                }
            }
            else if (toolStrip is StatusStrip)
            {
                using (Pen pen = new Pen(ColorTable.Border))
                {
                    e.Graphics.DrawRectangle(
                        pen, 0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
                }
            }
            else if (toolStrip is MenuStrip)
            {
                base.OnRenderToolStripBorder(e);
            }
            else
            {
                using (Pen pen = new Pen(ColorTable.Border))
                {
                    g.DrawRectangle(
                        pen, 0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
                }
            }
        }

        protected override void OnRenderMenuItemBackground(
           ToolStripItemRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            //System.Windows.Forms.ToolStripMenuItem menuItem = e.Item as System.Windows.Forms.ToolStripMenuItem;
            //if (menuItem != null)
            //{
            //    menuItem.DropDown.DropShadowEnabled = false;
            //}

            ToolStripItem item = e.Item;


            if (!item.Enabled)
            {
                return;
            }

            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);

            if (toolStrip is MenuStrip)
            {
                //Rectangle memuRect = RectangleEx.Clone(rect);
                //memuRect.Width--;
                //if (item.Pressed)
                //{
                //    using (GraphicsPath path = RectangleEx.CreatePath(memuRect, this.Radius, MenuRoundStyle))
                //    {
                //        g.PixelOffsetMode = PixelOffsetMode.Half;
                //        g.FillPath(new SolidBrush(this.ColorTable.BackColor), path);
                //        g.PixelOffsetMode = PixelOffsetMode.Default;

                //        g.DrawLines(new Pen(this.ColorTable.Border), path.PathPoints);
                //    }
                //    memuRect.Inflate(-1, -1);
                //    memuRect.Height++;
                //    using (GraphicsPath path = RectangleEx.CreatePath(memuRect, this.Radius, MenuRoundStyle))
                //    {
                //        using (LinearGradientBrush brush = new LinearGradientBrush(memuRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                //        {
                //            ColorBlend blend = new ColorBlend();
                //            blend.Colors = new Color[] { this.ColorTable.HighLight, this.ColorTable.HighLight, Color.Transparent, Color.Transparent };
                //            blend.Positions = new float[] { 0f, .1f, .2f, 1f };
                //            brush.InterpolationColors = blend;
                //            using (Pen pen = new Pen(brush))
                //            {
                //                g.DrawLines(pen, path.PathPoints);
                //            }
                //        }
                //    }
                //}
                    
                //else if (item.Selected)
                //{

                //}
                //else
                //{
                //    base.OnRenderMenuItemBackground(e);
                //}
            }
            else if (toolStrip is ToolStripDropDown)
            {
                //bool bDrawLogo = NeedDrawLogo(toolStrip);

                if (item.Selected)
                {
                    rect.Inflate(-2, -1);
                    g.Clip = new Region(rect);
                    if (item.Owner.Items[0] == item)
                    {
                        using (GraphicsPath path = RectangleEx.CreatePath(rect, this.Radius,
                            DropdownRoundStyle & ~RoundStyle.LeftBottom & ~RoundStyle.RightBottom))
                        {
                            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                            {
                                brush.InterpolationColors = this.ColorTable.BackHover;
                                g.PixelOffsetMode = PixelOffsetMode.Half;
                                g.FillPath(brush, path);
                                g.PixelOffsetMode = PixelOffsetMode.Default;
                            }
                        }

                        using (GraphicsPath path = RectangleEx.CreatePath(rect, this.Radius,
                            DropdownRoundStyle & ~RoundStyle.LeftBottom & ~RoundStyle.RightBottom))
                        {
                            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                            {
                                ColorBlend blend = new ColorBlend();
                                blend.Colors = new Color[] { Color.FromArgb(40, 0, 0, 0), Color.FromArgb(40, 0, 0, 0), Color.Transparent, Color.Transparent };
                                blend.Positions = new float[] { 0f, .1f, .2f, 1f };
                                brush.InterpolationColors = blend;
                                using (Pen pen = new Pen(brush))
                                {
                                    g.DrawLines(pen, path.PathPoints);
                                }
                            }
                        }
                    }
                    else if (item.Owner.Items[item.Owner.Items.Count - 1] == item)
                    {
                        using (GraphicsPath path = RectangleEx.CreatePath(rect, this.Radius - 5 <= 0 ? 1: this.Radius - 5,
                            DropdownRoundStyle & ~RoundStyle.RightTop))
                        {
                            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                            {
                                brush.InterpolationColors = this.ColorTable.BackHover;
                                g.PixelOffsetMode = PixelOffsetMode.Half;
                                g.FillPath(brush, path);
                                g.PixelOffsetMode = PixelOffsetMode.Default;
                            }
                        }
                    }
                    else
                    {
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                        {
                            brush.InterpolationColors = this.ColorTable.BackHover;
                            g.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillRectangle(brush, rect);
                            g.PixelOffsetMode = PixelOffsetMode.Default;
                        }
                    }

                }
                else
                {
                    base.OnRenderMenuItemBackground(e);
                }
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderItemImage(
            ToolStripItemImageRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            Graphics g = e.Graphics;

            if (toolStrip is ToolStripDropDown &&
               e.Item is ToolStripMenuItem)
            {
                bool bDrawLogo = false; // NeedDrawLogo(toolStrip);
                int offsetMargin = bDrawLogo ? OffsetMargin : 0;

                ToolStripMenuItem item = (ToolStripMenuItem)e.Item;
                if (item.Checked)
                {
                    return;
                }
                Rectangle rect = e.ImageRectangle;
                if (e.Item.RightToLeft == RightToLeft.Yes)
                {
                    rect.X -= offsetMargin + 2;
                }
                else
                {
                    rect.X += offsetMargin + 2;
                }

                    ToolStripItemImageRenderEventArgs ne =
                        new ToolStripItemImageRenderEventArgs(
                        g, e.Item, e.Image, rect);
                    base.OnRenderItemImage(ne);
            }
            else
            {
                base.OnRenderItemImage(e);
            }
        }

        protected override void OnRenderItemText(
            ToolStripItemTextRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;

            e.TextColor = ColorTable.Foreground;

            if (toolStrip is ToolStripDropDown &&
                e.Item is ToolStripMenuItem)
            {
                bool bDrawLogo = false; // NeedDrawLogo(toolStrip);

                int offsetMargin = bDrawLogo ? 18 : 0;

                Rectangle rect = e.TextRectangle;
                if (e.Item.RightToLeft == RightToLeft.Yes)
                {
                    rect.X -= offsetMargin;
                }
                else
                {
                    rect.X += offsetMargin;
                }
                e.TextRectangle = rect;
            }

            base.OnRenderItemText(e);
        }

        protected override void OnRenderItemCheck(
            ToolStripItemImageRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            Graphics g = e.Graphics;

            if (toolStrip is ToolStripDropDown &&
               e.Item is ToolStripMenuItem)
            {
                bool bDrawLogo = NeedDrawLogo(toolStrip);
                int offsetMargin = bDrawLogo ? OffsetMargin : 0;
                Rectangle rect = e.ImageRectangle;

                if (e.Item.RightToLeft == RightToLeft.Yes)
                {
                    rect.X -= offsetMargin + 2;
                }
                else
                {
                    rect.X += offsetMargin + 2;
                }

                rect.Width = 13;
                rect.Y += 1;
                rect.Height -= 3;

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddRectangle(rect);
                    using (PathGradientBrush brush = new PathGradientBrush(path))
                    {
                        brush.CenterColor = Color.White;
                        brush.SurroundColors = new Color[] { ControlPaint.Light(ColorTable.BackNormal) };
                        Blend blend = new Blend();
                        blend.Positions = new float[] { 0f, 0.3f, 1f };
                        blend.Factors = new float[] { 0f, 0.5f, 1f };
                        brush.Blend = blend;
                        g.FillRectangle(brush, rect);
                    }
                }

                using (Pen pen = new Pen(ControlPaint.Light(ColorTable.BackNormal)))
                {
                    g.DrawRectangle(pen, rect);
                }

                ControlPaintEx.DrawCheckedFlag(g, rect, ColorTable.Foreground);

            }
            else
            {
                base.OnRenderItemCheck(e);
            }
        }

        protected override void OnRenderArrow(
            ToolStripArrowRenderEventArgs e)
        {
            if (e.Item.Enabled)
            {
                e.ArrowColor = ColorTable.Foreground;
            }

            ToolStrip toolStrip = e.Item.Owner;

            if (toolStrip is ToolStripDropDown &&
                e.Item is ToolStripMenuItem)
            {
                bool bDrawLogo = NeedDrawLogo(toolStrip);

                int offsetMargin = bDrawLogo ? 3 : 0;

                Rectangle rect = e.ArrowRectangle;
                if (e.Item.RightToLeft == RightToLeft.Yes)
                {
                    rect.X -= offsetMargin;
                }
                else
                {
                    rect.X += offsetMargin;
                }

                e.ArrowRectangle = rect;
            }

            base.OnRenderArrow(e);
        }

        protected override void OnRenderSeparator(
            ToolStripSeparatorRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            Rectangle rect = e.Item.ContentRectangle;
            Graphics g = e.Graphics;

            if (toolStrip is ToolStripDropDown)
            {
                bool bDrawLogo = NeedDrawLogo(toolStrip);

                int offsetMargin = bDrawLogo ?
                    OffsetMargin * 2 : OffsetMargin;

                if (e.Item.RightToLeft != RightToLeft.Yes)
                {
                    rect.X += offsetMargin + 2;
                }
                rect.Width -= offsetMargin + 4;
            }

            RenderSeparatorLine(
               g,
               rect,
               ColorTable.DropDownImageSeparator,
               ColorTable.BackNormal,
               SystemColors.ControlLightLight,
               e.Vertical);
        }

        internal void RenderSeparatorLine(
            Graphics g,
            Rectangle rect,
            Color baseColor,
            Color backColor,
            Color shadowColor,
            bool vertical)
        {
            float angle;
            if (vertical)
            {
                angle = 90F;
            }
            else
            {
                angle = 180F;
            }
            using (LinearGradientBrush brush = new LinearGradientBrush(
                    rect,
                    baseColor,
                    backColor,
                    angle))
            {
                Blend blend = new Blend();
                blend.Positions = new float[] { 0f, .2f, .5f, .8f, 1f };
                blend.Factors = new float[] { 1f, .3f, 0f, .3f, 1f };
                brush.Blend = blend;
                using (Pen pen = new Pen(brush))
                {
                    if (vertical)
                    {
                        g.DrawLine(pen, rect.X, rect.Y, rect.X, rect.Bottom);
                    }
                    else
                    {
                        g.DrawLine(pen, rect.X, rect.Y, rect.Right, rect.Y);
                    }

                    brush.LinearColors = new Color[] {
                        shadowColor, ColorTable.BackNormal };
                    pen.Brush = brush;
                    if (vertical)
                    {
                        g.DrawLine(pen, rect.X + 1, rect.Y, rect.X + 1, rect.Bottom);
                    }
                    else
                    {
                        g.DrawLine(pen, rect.X, rect.Y + 1, rect.Right, rect.Y + 1);
                    }
                }
            }
        }

        internal bool NeedDrawLogo(ToolStrip toolStrip)
        {
            ToolStripDropDown dropDown = toolStrip as ToolStripDropDown;
            bool bDrawLogo =
                (dropDown.OwnerItem != null &&
                dropDown.OwnerItem.Owner is MenuStrip) ||
                (toolStrip is ContextMenuStrip);
            return bDrawLogo;
        }
    }
}
