using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Fink.Drawing;
using System.ComponentModel.Design;

namespace Fink.Windows.Forms
{
    public enum GroupHeadExStyle
    {
        Cyan,
        Deepblue
    }

    public class GroupPanelExDesigner : ParentControlDesignerEx
    {
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            var control = this.Control as GroupPanelEx;
            EnableDesignMode(control, control.Name);
        }


    }

    [Designer(typeof(GroupPanelExDesigner))]
    public class GroupPanelEx: Fink.Windows.Forms.GroupBoxExBase
    {
        private GroupPanelExColorTable _colorTable;
        private Padding _graphicMargin = new Padding(5, 0, 5, 0);
        private Padding _shadowThinkness = new Padding(2, 1, 2, 4);
        private int _headerHeight = 28;
        private Padding _headerTextMargin = new Padding(10, 8, 10, 0);

        public GroupPanelEx()
            : base()
        {
            this.displayRectangle = base.DisplayRectangle;

            this.Radius = 4;
            this.RoundStyle = Drawing.RoundStyle.All;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)NativeMethods.WindowMessages.WM_SIZE)
            {
                int width = NativeMethods.LOWORD((int)(m.LParam));
                int height = NativeMethods.HIWORD((int)(m.LParam));
                Rectangle rect = this.ClientRectangle;
                rect.Width = width;
                rect.Height = height;
                rect = this.GetContentRect(rect);
                this.SetDisplayRectangle(rect);
            }
            base.WndProc(ref m);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (this.AutoSize)
            {
                base.SetBoundsCore(x, y, width, height + this.HeaderHeight, specified); 
            }
            else
            {
                base.SetBoundsCore(x, y, width, height, specified);
            }
                
        }

        #region porperty

        private GroupPanelExColorTable ColorTable
        {
            get
            {
                if (this._colorTable == null)
                {
                    this._colorTable = new PureCyanGroupPanelExColorTable();
                }
                return this._colorTable;
            }
            set
            {
                this._colorTable = value;
            }
        }

        public Padding GraphicMargin
        {
            get { return _graphicMargin; }
            set { _graphicMargin = value; }
        }

        private Padding ShadowThinkness
        {
            get
            {
                return this._shadowThinkness;
            }
            set
            {
                this._shadowThinkness = value;
            }
        }

        [DefaultValue(28)]
        public int HeaderHeight
        {
            get { return _headerHeight; }
            set { _headerHeight = value; }
        }

        [DefaultValue(typeof(Padding), "10, 8, 10, 0")]
        public Padding HeaderTextMargin
        {
            get { return _headerTextMargin; }
            set { _headerTextMargin = value; }
        }

        private GroupHeadExStyle _style;
        public GroupHeadExStyle Style
        {
            get
            {
                return this._style;
            }
            set
            {
                if (this._style != value)
                {
                    this._style = value;
                    StyleChange(this._style);
                }

            }
        }

        #endregion

        private void StyleChange(GroupHeadExStyle style)
        {
            switch (style)
            {
                case GroupHeadExStyle.Cyan:
                    this.ColorTable = new PureCyanGroupPanelExColorTable();
                    break;
                case GroupHeadExStyle.Deepblue:
                    this.ColorTable = new PureDeepblueGroupPanelExColorTable();
                    break;
                default:
                    this.ColorTable = new PureCyanGroupPanelExColorTable();
                    break;
            }
            this.Invalidate(this.GetHeaderRect(this.ClientRectangle));
        }


        #region override

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnPaint(e);
            base.OnPaintBackground(e);

            Rectangle headerRect = this.GetHeaderRect(this.ClientRectangle);
            DrawHeader(e.Graphics, headerRect);

            Rectangle coreBaseRect = this.GetCoreBaseRect(this.ClientRectangle);
            DrawShadow(e.Graphics, coreBaseRect);

            Rectangle coreRect = this.GetCoreRect(this.ClientRectangle);
            DrawBackground(e.Graphics, coreRect);
        }

        private Rectangle GetHeaderRect(Rectangle rect)
        {
            Rectangle headerRect = GetGraphicRect(rect);
            headerRect.Height = this.HeaderHeight;
            return headerRect;
        }

        private Rectangle GetCoreBaseRect(Rectangle rect)
        {
            Rectangle graphicRect = GetGraphicRect(rect);
            Rectangle headerRect = graphicRect;
            headerRect.Height = this.HeaderHeight;

            Rectangle coreBaseRect = graphicRect;
            coreBaseRect.Y += this.HeaderHeight;
            coreBaseRect.Height -= this.HeaderHeight;

            return coreBaseRect;
        }

        private Rectangle GetCoreRect(Rectangle rect)
        {
            Rectangle coreRect = GetCoreBaseRect(rect);
            coreRect.X += this.ShadowThinkness.Left;
            coreRect.Y += this.ShadowThinkness.Top;
            coreRect.Width -= this.ShadowThinkness.Left + this.ShadowThinkness.Right;
            coreRect.Height -= this.ShadowThinkness.Top + this.ShadowThinkness.Bottom;

            return coreRect;
        }

        private Rectangle GetContentRect(Rectangle rect)
        {
            Rectangle coreRect = GetCoreBaseRect(rect);
            coreRect.X += this.ShadowThinkness.Left + this.Margin.Left;
            coreRect.Y += this.ShadowThinkness.Top + this.Margin.Top;
            coreRect.Width -= this.ShadowThinkness.Left + this.ShadowThinkness.Right + this.Margin.Left + this.Margin.Right;
            coreRect.Height -= this.ShadowThinkness.Top + this.ShadowThinkness.Bottom + this.Margin.Top + this.Margin.Bottom;

            return coreRect;
        }

        private Rectangle GetGraphicRect(Rectangle rect)
        {
            Rectangle r = rect;
            r.X += this.GraphicMargin.Left;
            r.Y += this.GraphicMargin.Top;
            r.Width -= this.GraphicMargin.Left + this.GraphicMargin.Right;
            r.Height -= this.GraphicMargin.Top + this.GraphicMargin.Bottom;

            return r;
        }

        private void DrawHeader(Graphics g, Rectangle rect)
        {
            rect.Height += 5;
            rect.X = 16;

            Size textSize = TextRenderer.MeasureText(this.Text, this.Font);
            rect.Width = HeaderTextMargin.Left + textSize.Width + HeaderTextMargin.Right;

            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.FillRectangle(new SolidBrush(this.ColorTable.HeaderBackColor), rect);
            g.PixelOffsetMode = PixelOffsetMode.Default;

            Rectangle borderRect = rect;
            borderRect.Width--;
            borderRect.Height--;
            g.DrawRectangle(new Pen(this.ColorTable.HeaderBorder), borderRect);

            //inner Glow
            using (Pen pen = new Pen(Color.FromArgb(250, this.ColorTable.HeaderHighLight)))
            {
                Rectangle innerGlowRect = borderRect;
                innerGlowRect.Inflate(-1, -1);
                g.DrawRectangle(pen, innerGlowRect);
            }

            using (Pen pen = new Pen(Color.FromArgb(50, this.ColorTable.HeaderHighLight)))
            {
                Rectangle innerGlowRect = borderRect;
                innerGlowRect.Inflate(-2, -2);
                g.DrawRectangle(pen, innerGlowRect);
            }

            //darw tittle

            using (Brush brush = new SolidBrush(Color.FromArgb(20, this.ColorTable.Shadow)))
            {
                g.DrawString(this.Text, this.Font, brush, new Point(rect.Left + HeaderTextMargin.Left, rect.Top + HeaderTextMargin.Top - 2));
            }

            using (Brush brush = new SolidBrush(Color.FromArgb(40, this.ColorTable.Shadow)))
            {
                g.DrawString(this.Text, this.Font, brush, new Point(rect.Left + HeaderTextMargin.Left, rect.Top + HeaderTextMargin.Top - 1));
            }

            using (Brush brush = new SolidBrush(Color.FromArgb(255, this.ColorTable.Foreground)))
            {
                g.DrawString(this.Text, this.Font, brush, new Point(rect.Left + HeaderTextMargin.Left, rect.Top + HeaderTextMargin.Top));
            }

        }
        #endregion

        private void DrawShadow(Graphics g, Rectangle rect)
        {
            using (GraphicsPath path = RectangleEx.CreatePath(rect, this.Radius + 1, this.RoundStyle))
            {
                Rectangle brushRect = rect;
                brushRect.Width++;
                brushRect.Height++;
                using (LinearGradientBrush brush = new LinearGradientBrush(
                brushRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {

                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[] { Color.FromArgb(15, this.ColorTable.Shadow),
                                            Color.FromArgb(5, this.ColorTable.Shadow) };
                    blend.Positions = new float[] { 0f, 1f };
                    brush.InterpolationColors = blend;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }
            }

            rect.Inflate(-1, -1);
            using (GraphicsPath path = RectangleEx.CreatePath(rect, this.Radius, this.RoundStyle))
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(10, this.ColorTable.Shadow)))
                {
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }
            }

            rect.Inflate(-1, -1);
            using (GraphicsPath path = RectangleEx.CreatePath(rect, this.Radius + 2, this.RoundStyle))
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(20, this.ColorTable.Shadow)))
                {
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }
            }
            rect.Height--;
            using (GraphicsPath path = RectangleEx.CreatePath(rect, this.Radius + 1, this.RoundStyle))
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(20, this.ColorTable.Shadow)))
                {
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }
            }

            rect.Inflate(1, 0);
            using (GraphicsPath path = RectangleEx.CreatePath(rect, this.Radius, this.RoundStyle))
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(20, this.ColorTable.Shadow)))
                {
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }
            }



        }

        private void DrawBackground(Graphics g, Rectangle rect)
        {
            using (GraphicsPath path = RectangleEx.CreatePath(rect, this.Radius, this.RoundStyle))
            {
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.FillPath(new SolidBrush(this.ColorTable.BackColor), path);
                g.PixelOffsetMode = PixelOffsetMode.Default;
            }

            Rectangle borderRect = rect;
            borderRect.Width--;
            borderRect.Height--;

            using (GraphicsPath path = RectangleEx.CreatePath(borderRect, this.Radius, this.RoundStyle))
            {
                g.DrawPath(new Pen(this.ColorTable.Border), path);
            }

            //inner Glow


            Rectangle innerGlowRect = borderRect;
            innerGlowRect.Inflate(-1, -1);
            using (GraphicsPath path = RectangleEx.CreatePath(innerGlowRect, this.Radius, this.RoundStyle))
            {
                using (Pen pen = new Pen(Color.FromArgb(40, this.ColorTable.HighLight)))
                {
                    g.DrawPath(pen, path);
                }
            }

            Rectangle innerInnerGlowRect = borderRect;
            innerInnerGlowRect.Inflate(-2, -2);
            using (GraphicsPath path = RectangleEx.CreatePath(innerInnerGlowRect, this.Radius, this.RoundStyle))
            {
                using (Pen pen = new Pen(Color.FromArgb(20, this.ColorTable.HighLight)))
                {
                    g.DrawPath(pen, path);
                }
            }
           

        }



    }
}
