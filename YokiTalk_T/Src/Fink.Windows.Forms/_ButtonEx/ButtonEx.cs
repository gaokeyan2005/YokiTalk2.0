using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using Fink.Drawing;

namespace Fink.Windows.Forms
{
    internal class ButtonExDesigner : ControlDesignerEx
    {
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            //var btn = this.Control as ButtonEx;
            //Size fontSize = TextRenderer.MeasureText(btn.Text, btn.Font);
            //btn.MinimumSize = new Size(fontSize.Width + btn.TextPading.Width * 2, fontSize.Height + btn.TextPading.Height * 2);
            //btn.TextChanged +=new EventHandler(btn_TextChanged);

            //btn.CustomPorpertyChanged += new CustomPorpertyChangedDelegate(btn_CustomPorpertyChanged);
            
        }

        //void btn_CustomPorpertyChanged(object sender, EventArgs e)
        //{
        //    var btn = sender as ButtonEx;
        //    ResizeBtn(btn);
        //}

        //void btn_TextChanged(object sender, EventArgs e)
        //{
        //    var btn = sender as ButtonEx;
        //    ResizeBtn(btn);
        //}
        

        private void ResizeBtn(ButtonEx btn)
        {
            Size fontSize = TextRenderer.MeasureText(btn.Text, btn.Font);
            btn.Size = new Size(fontSize.Width + btn.BoderPading * 2 + btn.TextPading.Width * 2,
                fontSize.Height + btn.BoderPading * 2 + btn.TextPading.Height * 2);
        }
    }
    
    [Designer(typeof(ButtonExDesigner))]
    public class ButtonEx : ButtonExBase
    {
        internal event CustomPorpertyChangedDelegate CustomPorpertyChanged;

        private ButtonExColorTable _colorTable;
        private ControlState _controlState;

        private RoundStyle _roundStyle;
        private int _radius;

        public ButtonEx()
            : base()
        {
            this.Radius = 16;
            this.BoderPading = 3;
            this.TextPading = new Size(16, 7);
            this.RoundStyle = Drawing.RoundStyle.All;
            this.ColorTable = new PureButtonExColorTable();

            this.BackColor = Color.Transparent;
        }

        public ButtonEx(bool isWaring = false)
            : this()
        {
            this.IsWaring = isWaring;
        }

        [DefaultValue(typeof(bool), "false")]
        private bool isWaring = false;
        public bool IsWaring
        {
            get { return isWaring; }
            set
            {
                if (isWaring != value)
                {
                    isWaring = value;
                    if (isWaring)
	                {
                        this.ColorTable = new WaringButtonExColorTable();
	                }
                    else{
                        this.ColorTable = new PureButtonExColorTable();
                    }
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(typeof(RoundStyle), "0")]
        public RoundStyle RoundStyle
        {
            get { return _roundStyle; }
            set
            {
                if (_roundStyle != value)
                {
                    _roundStyle = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(8)]
        public int Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    base.Invalidate();
                }
            }
        }


        [DefaultValue(3)]
        private Size _textPading;
        public Size TextPading
        {
            get { return this._textPading; }
            set
            {
                this._textPading = value;
                this.Invalidate();
                if(this.CustomPorpertyChanged != null)
                    this.CustomPorpertyChanged(this, null);
            }
        }

        [DefaultValue(3)]
        public int BoderPading
        {
            get;
            set;
        }

        public ButtonExColorTable ColorTable
        {
            get { return _colorTable; }
            protected set { _colorTable = value; }
        }

        [DefaultValue(typeof(ControlState), "0")]
        internal ControlState ControlState
        {
            get { return _controlState; }
            set
            {
                if (_controlState != value)
                {
                    _controlState = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Rectangle), "0, 0, 80, 32")]
        internal Rectangle ControlRect
        {
            get;
            set;
        }

        #region Override Method

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            ControlState = ControlState.Hover;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ControlState = ControlState.Normal;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ControlState = ControlState.Pressed;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                if (ClientRectangle.Contains(e.Location))
                {
                    ControlState = ControlState.Hover;
                }
                else
                {
                    ControlState = ControlState.Normal;
                }
            }
        }
        #endregion


        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;


            ColorBlend currectBackground = null;
            Color highLight = Color.Empty;
            Color foreground = Color.White;
            Color shadow = Color.Empty;
            if (Enabled)
            {
                switch (ControlState)
                {
                    case ControlState.Hover:
                        currectBackground = this.ColorTable.ActiveBackground;
                        highLight = this.ColorTable.HighLight;
                        foreground = this.ColorTable.Foreground;
                        shadow = this.ColorTable.Shadow;
                        break;
                    case ControlState.Pressed:
                        currectBackground = this.ColorTable.ActiveBackground;
                        highLight = this.ColorTable.ActiveHighLight;
                        foreground = this.ColorTable.ActiveForeground;
                        shadow = this.ColorTable.ActiveShadow;
                        break;
                    default:
                        currectBackground = this.ColorTable.Background;
                        highLight = this.ColorTable.HighLight;
                        foreground = this.ColorTable.Foreground;
                        shadow = this.ColorTable.Shadow;
                        break;
                }
            }
            else
            {
                currectBackground = this.ColorTable.DisableBackground;
                highLight = this.ColorTable.DisableHighLight;
                foreground = this.ColorTable.DisableForeground;
                shadow = this.ColorTable.DisableShadow;
            }

            Rectangle coreRect = this.ClientRectangle;
            coreRect.Inflate(-1, -1);

            using (GraphicsPath path = RectangleEx.CreatePath(coreRect, this.Radius + BoderPading, this.RoundStyle))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                coreRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();

                    blend.Colors = new Color[] { Color.FromArgb(64 - 8, 0, 0, 0),
                                                        Color.FromArgb(25, 0, 0, 0),
                                                        Color.FromArgb(5, 0, 0, 0) };

                    blend.Positions = new float[] { 0f, .15f, 1f };
                    brush.InterpolationColors = blend;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;

                }
            }

            
            Rectangle coreHightlightBorderRect = this.ClientRectangle;
            coreHightlightBorderRect.Width--;
            coreHightlightBorderRect.Height--;
            using (GraphicsPath path = RectangleEx.CreatePath(coreHightlightBorderRect, this.Radius + BoderPading, this.RoundStyle))
            {
                coreHightlightBorderRect.Width++;
                coreHightlightBorderRect.Height++;
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    coreHightlightBorderRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[] { Color.FromArgb(0, 255, 255, 255),
                                            Color.FromArgb(160, 255, 255, 255) };
                    blend.Positions = new float[] { 0f, 1f };
                    brush.InterpolationColors = blend;
                    g.DrawPath(new Pen(brush), path);
                }
            }

            Rectangle coreShadowRect = this.ClientRectangle;
            coreShadowRect.Inflate(-1, -1);
            coreShadowRect.Height -= this.BoderPading;
            coreShadowRect.Y += this.BoderPading;

            using (GraphicsPath path = RectangleEx.CreatePath(coreShadowRect, this.Radius, this.RoundStyle))
            {
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = Color.FromArgb(255, 0, 0, 0);
                    brush.SurroundColors = new Color[] { Color.FromArgb(0, 0, 0, 0) };
                    brush.FocusScales = new PointF(.6f, .5f);

                    //g.TranslateTransform(0f, BoderPading);
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                    //g.TranslateTransform(0f, -BoderPading);
                }
            }


            Rectangle btnRect = coreRect;
            btnRect.Inflate(-this.BoderPading, -this.BoderPading);

            using (GraphicsPath path = RectangleEx.CreatePath(btnRect, this.Radius, this.RoundStyle))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    btnRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    brush.InterpolationColors = currectBackground;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }
            }


            Rectangle coreBorderRect = btnRect;
            coreBorderRect.Width--;
            coreBorderRect.Height--;
            using (GraphicsPath path = RectangleEx.CreatePath(coreBorderRect, this.Radius, this.RoundStyle))
            {
                using(Pen pen = new Pen(Color.FromArgb(80, 0, 0, 0)))
	            {
                    g.DrawPath(pen, path);
	            }
            }

            Rectangle hightLightborderRect = coreBorderRect;
            hightLightborderRect.Inflate(-1, -1);
            using (GraphicsPath path = RectangleEx.CreatePath(hightLightborderRect, this.Radius - 1, this.RoundStyle))
            {
                Rectangle hightLightBorderBrushRect = hightLightborderRect;
                hightLightBorderBrushRect.Inflate(1, 1);
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    hightLightBorderBrushRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[] { Color.FromArgb(250, highLight), Color.FromArgb(0, highLight), Color.FromArgb(20, highLight) };
                    blend.Positions = new float[] { 0f, .65f, 1f };
                    brush.InterpolationColors = blend;
                    g.DrawPath(new Pen(brush), path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }
            }

            //SizeF fontSize = g.MeasureString(this.Text, this.Font);
            Size fontSize = TextRenderer.MeasureText(g, this.Text, this.Font, Size.Empty, TextFormatFlags.NoPadding);
            PointF fontLocation = new PointF(btnRect.Left + (btnRect.Width - fontSize.Width) / 2, btnRect.Top + (btnRect.Height - fontSize.Height) / 2);
            
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(16, shadow)))
            {
                g.DrawString(this.Text, this.Font, brush, new PointF(fontLocation.X, fontLocation.Y + 2), StringFormat.GenericTypographic);
            }

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(40, shadow)))
            {
                g.DrawString(this.Text, this.Font, brush, new PointF(fontLocation.X, fontLocation.Y + 1), StringFormat.GenericTypographic);

            }
            using (SolidBrush brush = new SolidBrush(foreground))
            {
                g.DrawString(this.Text, this.Font, brush, new PointF(fontLocation.X, fontLocation.Y), StringFormat.GenericTypographic);
            }
            
        }



    }
}
