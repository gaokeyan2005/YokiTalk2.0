using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using Fink.Drawing;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Fink.Windows.Forms
{
    internal class RadioButtonExDesigner : ControlDesignerEx
    {
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            var rdo = this.Control as ToggleButtonEx;
            rdo.MinimumSize = new System.Drawing.Size(66, 28);
            rdo.Size = new System.Drawing.Size(66, 28);

            rdo.CustomPorpertyChanged += new CustomPorpertyChangedDelegate(rdo_CustomPorpertyChanged);
        }

        void rdo_CustomPorpertyChanged(object sender, EventArgs e)
        {
            var btn = sender as ToggleButtonEx;
            btn.Invalidate();
        }

        //void btn_TextChanged(object sender, EventArgs e)
        //{
        //    var btn = sender as ButtonEx;
        //    ResizeBtn(btn);
        //}

        //private void ResizeBtn(ButtonEx btn)
        //{
        //    Size fontSize = TextRenderer.MeasureText(btn.Text, btn.Font);
        //    btn.Size = new Size(fontSize.Width + btn.BoderPading * 2 + btn.TextPading.Width * 2,
        //        fontSize.Height + btn.BoderPading * 2 + btn.TextPading.Height * 2);
        //}
    }

    [Designer(typeof(RadioButtonExDesigner))]
    [ToolboxBitmap(typeof(RadioButton))]
    public class ToggleButtonEx : System.Windows.Forms.ButtonBase
    {
        internal event CustomPorpertyChangedDelegate CustomPorpertyChanged;

        public event CheckedChangedEventHandler CheckedChanged;

        private ToggleButtonExColorTable _colorTable;
        private ControlState _controlState;

        private RoundStyle _roundStyle;
        private int _radius;

        public ToggleButtonEx()
            : base()
        {
            this.Radius = 16;
            this.BoderPading = 3;
            this.TextPading = new Size(10, 5);
            this.RoundStyle = Drawing.RoundStyle.All;
            this.ColorTable = new PureToggleButtonExColorTable();

            this.BackColor = Color.Transparent;
            this.MinimumSize = new System.Drawing.Size(66, 28);

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
        }


        #region Porperty

        public bool _checked;
        [DefaultValue(false)]
        public bool Checked
        {
            get { return this._checked; }
            set
            {
                if (this._checked != value)
                {
                    this._checked = value;
                    if (this.CheckedChanged != null)
                    {
                        this.CheckedChanged(this, new CheckedEventArgs() { Checked = this._checked });
                    }
                    if (this.CustomPorpertyChanged!= null)
                    {
                        this.CustomPorpertyChanged(this, new EventArgs());
                    }
                    this.Invalidate();
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
                if (this._textPading != value)
                {
                    this._textPading = value;
                    this.Invalidate();
                    if (this.CustomPorpertyChanged != null)
                        this.CustomPorpertyChanged(this, null);
                }
            }
        }

        [DefaultValue(3)]
        public int BoderPading
        {
            get;
            set;
        }

        public ToggleButtonExColorTable ColorTable
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



        private LightingEffect lightingEffect;
        [DefaultValue(typeof(LightingEffect), "0")]
        public LightingEffect LightingEffect
        {
            get { return lightingEffect; }
            set
            {
                if (lightingEffect != value)
                {
                    lightingEffect = value;
                    base.Invalidate();
                }
            }
        }

        #endregion

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
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                //if (GetButtonRect().Contains(e.Location))
                //{
                //    this.Checked = !this.Checked;
                //    this.Invalidate();
                //}


                Rectangle mainRect = this.ClientRectangle;
                mainRect.Inflate(-4, -4);

                using (GraphicsPath path = RectangleEx.CreatePath(mainRect, mainRect.Height / 2, Drawing.RoundStyle.All))
                {
                    using (Region r = new Region(path))
                    {
                        if (r.IsVisible(e.Location))
                        {
                            this.Checked = !this.Checked;
                            this.Invalidate();
                        }
                    }
                }


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

        protected override void OnLocationChanged(EventArgs e)
        {
            //base.OnLocationChanged(e);
        }
        #endregion

        private Rectangle GetButtonRect()
        {
                return this.Checked ?
                new Rectangle(this.ClientRectangle.Right - this.ClientRectangle.Height + 1, this.ClientRectangle.Top, this.ClientRectangle.Height, this.ClientRectangle.Height)
                :
                new Rectangle(this.ClientRectangle.Left - 1, this.ClientRectangle.Top, this.ClientRectangle.Height, this.ClientRectangle.Height);
        }
        private ColorBlend GetBackground()
        {
            ColorBlend blend = new ColorBlend();
            blend.Colors = this.Checked ?
                new Color[] {Color.FromArgb(255, 56, 180, 75),
                                                Color.FromArgb(255, 56, 180, 75),
                                                Color.FromArgb(255, 110, 198, 64)}
                    :
                new Color[] {Color.FromArgb(255, 168, 168, 168),
                                                Color.FromArgb(255, 198, 198, 198),
                                                Color.FromArgb(255, 240, 240, 240)};

            blend.Positions = new float[] { 0f, .2f, 1f };
            return blend;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            base.OnPaintBackground(e);



            //if (this.ClientRectangle.Width < 66 || this.ClientRectangle.Height < 26)
            //{
            //    return;
            //}

            using (Fink.Drawing.HAFGraphics haf = new HAFGraphics(e.Graphics, HAFGraphicMode.AandH))
            {
                DrawBase(e.Graphics, this.ClientRectangle);

                Rectangle btnRect = GetButtonRect();

                DrawControlButton(e.Graphics, btnRect);
            }

        }

        private void DrawControlButton(Graphics g, Rectangle rect)
        {
            Rectangle btnShadowRect = rect;
            btnShadowRect.Inflate(-3, -3);
            btnShadowRect.X += 1;
            btnShadowRect.Y += 2;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(btnShadowRect);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = Color.FromArgb(255, 0, 0, 0);
                    brush.SurroundColors = new Color[] { Color.FromArgb(0, 0, 0, 0) };
                    brush.FocusScales = new PointF(.2f, .45f);

                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }
            }

            Rectangle btnRect = rect;
            btnRect.Inflate(-4, -4);

            RectangleF btnRectF = btnRect;
            btnRectF.Width -= .5f;
            btnRectF.Height -= .5f;


            using (LinearGradientBrush brush = new LinearGradientBrush(btnRectF, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
            {
                ColorBlend blend = new ColorBlend();
                blend.Colors = new Color[] {Color.FromArgb(255, 255, 255, 255),
                                            Color.FromArgb(255, 229, 235, 229)};
                blend.Positions = new float[] { 0f, 1f };

                brush.InterpolationColors = blend;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.FillEllipse(brush, btnRectF);
                g.PixelOffsetMode = PixelOffsetMode.Default;
            }

            RectangleF btnInnerRect = btnRect;
            btnInnerRect.Inflate(-3, -3);      

            using (LinearGradientBrush brush = new LinearGradientBrush(btnInnerRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
            {
                ColorBlend blend = new ColorBlend();
                blend.Colors = new Color[] {Color.FromArgb(255, 222, 229, 222),
                                            Color.FromArgb(255, 255, 255, 255),
                                            Color.FromArgb(255, 255, 255, 255)};
                blend.Positions = new float[] { 0f, .9f, 1f };

                brush.InterpolationColors = blend;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.FillEllipse(brush, btnInnerRect);
                g.PixelOffsetMode = PixelOffsetMode.Default;
            }



        }


        private void DrawBase(Graphics g, Rectangle rect)
        {
            Rectangle mainRect = rect;
            mainRect.Inflate(-1, -1);

            using (GraphicsPath path = RectangleEx.CreatePath(mainRect, mainRect.Height / 2, Drawing.RoundStyle.All))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(mainRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[] { Color.FromArgb(64, 0, 0, 0),
                                                    Color.FromArgb(25, 0, 0, 0),
                                                    Color.FromArgb(5 + 7, 0, 0, 0) };
                    blend.Positions = new float[] { 0f, .15f, 1f };
                    brush.InterpolationColors = blend;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }

                using (LinearGradientBrush brush = new LinearGradientBrush(
                 rect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();
                    switch (this.LightingEffect)
                    {
                        case LightingEffect.Hard:
                            blend.Colors = new Color[] { Color.FromArgb(0, 255, 255, 255), Color.FromArgb(160, 255, 255, 255) };
                            break;
                        case LightingEffect.Soft:
                            blend.Colors = new Color[] { Color.FromArgb(0, 255, 255, 255), Color.FromArgb(24, 255, 255, 255) };
                            break;
                        default:
                            blend.Colors = new Color[] { Color.FromArgb(0, 255, 255, 255), Color.FromArgb(160, 255, 255, 255) };
                            break;
                    }
                    blend.Positions = new float[] { 0f, 1f };
                    brush.InterpolationColors = blend;
                    g.DrawPath(new Pen(brush), path);
                }
            }

            Rectangle innerRect = mainRect;
            innerRect.Inflate(-3, -3);

            using (GraphicsPath path = RectangleEx.CreatePath(innerRect, innerRect.Height / 2, Drawing.RoundStyle.All))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(mainRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    //ColorBlend blend = new ColorBlend();
                    //blend.Colors = new Color[] {Color.FromArgb(255, 106, 218, 212),
                    //                            Color.FromArgb(255, 77, 168, 171),
                    //                            Color.FromArgb(255, 73, 162, 165)};
                    //blend.Positions = new float[] { 0f, .85f, 1f };

                    brush.InterpolationColors = GetBackground();
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }

                Rectangle innerShadowPathRect = innerRect;
                innerShadowPathRect.Height = (int)(innerShadowPathRect.Height * 1.3);
                innerShadowPathRect.Inflate(3, 0);
                innerShadowPathRect.Y -= 1;
                using (GraphicsPath innerShadowPath = RectangleEx.CreatePath(innerShadowPathRect, innerRect.Height / 2, Drawing.RoundStyle.All))
                {
                    using (PathGradientBrush brush = new PathGradientBrush(innerShadowPath))
                    {
                        //brush.CenterPoint = new PointF(mainRect.Width / 2, mainRect.Height * 2 / 3);
                        Color innerShadowOrginColor = this.Checked ? Color.FromArgb(37, 102, 110) : Color.FromArgb(102, 102, 102);
                        brush.CenterColor = Color.FromArgb(0, innerShadowOrginColor);
                        brush.SurroundColors = new Color[] { Color.FromArgb(196, innerShadowOrginColor) };
                        brush.FocusScales = new PointF(.8f, .55f);

                        g.PixelOffsetMode = PixelOffsetMode.Half;
                        g.FillPath(brush, path);
                        g.PixelOffsetMode = PixelOffsetMode.Default;
                    }
                }

                Rectangle mainBorderRect = mainRect;
                mainBorderRect.Width++;
                mainBorderRect.Height++;
                using (LinearGradientBrush brush = new LinearGradientBrush(mainBorderRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[] {Color.FromArgb(48, 0, 0, 0),
                                                Color.FromArgb(0, 0, 0, 0)};
                    blend.Positions = new float[] { 0f, 1f };

                    brush.InterpolationColors = blend;

                    g.DrawPath(new Pen(brush), path);
                }

                string text = Checked ? "ON" : "OFF";
                Size textSize = TextRenderer.MeasureText(g, text, this.Font, Size.Empty, TextFormatFlags.NoPadding);


                innerRect = rect;
                innerRect.Inflate(-4, -4);

                int left = this.Checked ? (innerRect.Width - GetButtonRect().Width) /2 : innerRect.Right - (innerRect.Width - GetButtonRect().Width) / 2 - textSize.Width;
                int top = (rect.Height - textSize.Height) / 2;

                g.DrawString(text, this.Font, new SolidBrush(Color.FromArgb(this.Checked ? 40 : 160, this.Checked ? Color.Black : Color.White)), new Point(left, top + 1));
                g.DrawString(text, this.Font, new SolidBrush(Color.FromArgb(40, this.Checked ? Color.Black : Color.White)), new Point(left, top + 2));
                g.DrawString(text, this.Font, new SolidBrush(this.Checked ? Color.White : Color.FromArgb(100, 100, 100)), new Point(left, top));
                






            }



        }
    }
}
