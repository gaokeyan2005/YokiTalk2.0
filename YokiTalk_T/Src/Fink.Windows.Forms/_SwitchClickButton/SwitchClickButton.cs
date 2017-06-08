using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    public delegate void MainClickHandler(object sender, MouseEventArgs e);
    public delegate void FunctionClickHandler(object sender, MouseEventArgs e);

    public class SwitchClickButton: System.Windows.Forms.Control
    {
        public event MainClickHandler OnMainClick;
        public event FunctionClickHandler OnFunctionClick;

        public SwitchClickButton()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);

            this.Click += (object sender, EventArgs e) =>
            {
                this.Focus();
            };
            this.GotFocus += (object sender, EventArgs e) =>
            {
                this.BorderColor = Color.FromArgb(255, 56, 180, 75);
            };
            this.LostFocus += (object sender, EventArgs e) =>
            {
                this.BorderColor = Color.FromArgb(255, 192, 194, 204);
            };

            this.MouseClick += (object sender, MouseEventArgs e)=>
            {
                if (this.CircleRect.Contains(e.Location))
                {
                    if (this.OnFunctionClick != null)
                    {
                        this.OnFunctionClick(sender, e);
                    }
                }
                else
                {
                    if (this.OnMainClick != null)
                    {
                        this.OnMainClick(sender, e);
                    }
                }
            };
        }


        #region Property

        private Color borderColor = Color.FromArgb(255, 192, 194, 204);
        [DefaultValue(typeof(Color), "192, 194, 204")]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; this.Invalidate(); }
        }

        [DefaultValue(4)]
        private int circleButtonMargin = 4;
        public int CircleButtonMargin
        {
            get { return circleButtonMargin; }
            set { circleButtonMargin = value; this.Invalidate(); }
        }

        public Rectangle CircleRect
        {
            get
            {
                int circleRight = this.Width - this.CircleButtonMargin;
                int circleRadius = (this.Height - (this.CircleButtonMargin * 2)) / 2;
                int circleLeft = circleRight - circleRadius * 2;
                int circleTop = this.CircleButtonMargin;
                return  new Rectangle(circleLeft, circleTop, circleRadius * 2 - 1, circleRadius * 2 - 1);
            }
        }

        #endregion

        Color HightLight = Color.FromArgb(114, 216, 125);
        Color Foreground = Color.FromArgb(255, 255, 255);
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            //
            System.Drawing.Drawing2D.GraphicsPath borderPath = Fink.Drawing.RectangleEx.CreatePath(new Rectangle(0, 0, this.Width - 1, this.Height - 1), Math.Min((this.Width - 1) / 2, (this.Height - 1) / 2), Drawing.RoundStyle.All);

            g.PixelOffsetMode = PixelOffsetMode.Half;
            using (Brush b = new SolidBrush(this.BorderColor))
            {
                g.FillPath(b, borderPath);
            }

            System.Drawing.Drawing2D.GraphicsPath borderPath2 = Fink.Drawing.RectangleEx.CreatePath(new Rectangle(1, 1, this.Width - 1 - 2, this.Height - 1 - 2), Math.Min((this.Width - 1 - 2) / 2, (this.Height - 1 - 2) / 2), Drawing.RoundStyle.All);

            using (Brush b = new SolidBrush(Color.White))
            {
                g.FillPath(b, borderPath2);
            }
            g.PixelOffsetMode = PixelOffsetMode.Default;


            //g.FillEllipse(new SolidBrush(Color.FromArgb(255, 255, 255, 255)), new Rectangle(circleLeft, circleTop, circleRadius * 2, circleRadius * 2));

            CircleRect.Inflate(2, 2);

            g.PixelOffsetMode = PixelOffsetMode.Half;
            using (LinearGradientBrush brush = new LinearGradientBrush(
                CircleRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
            {
                ColorBlend blend = new ColorBlend();

                blend.Colors = new Color[] { Color.FromArgb(64 - 8, 0, 0, 0),
                                                        Color.FromArgb(25, 0, 0, 0),
                                                        Color.FromArgb(5, 0, 0, 0) };
                blend.Positions = new float[] { 0f, .15f, 1f };
                brush.InterpolationColors = blend;
                g.FillEllipse(brush, CircleRect);
            }
            g.PixelOffsetMode = PixelOffsetMode.Default;




            using (LinearGradientBrush brush = new LinearGradientBrush(
                    CircleRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
            {
                ColorBlend blend = new ColorBlend();
                blend.Colors = new Color[] {
                    Color.FromArgb(255, 92, 198, 64),
                    Color.FromArgb(255, 56, 180, 72)
                };
                blend.Positions = new float[] { 0f, 1f };
                brush.InterpolationColors = blend;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.FillEllipse(brush, CircleRect);
                g.PixelOffsetMode = PixelOffsetMode.Default;
            }

            Rectangle coreBorderRect = CircleRect;
            coreBorderRect.Width--;
            coreBorderRect.Height--;

            using (Pen pen = new Pen(Color.FromArgb(80, 0, 0, 0)))
            {
                g.DrawEllipse(pen, coreBorderRect);
            }

            Rectangle hightLightborderRect = coreBorderRect;
            hightLightborderRect.Inflate(-1, -1);
            Rectangle hightLightBorderBrushRect = hightLightborderRect;
            hightLightBorderBrushRect.Inflate(1, 1);
            using (LinearGradientBrush brush = new LinearGradientBrush(
                hightLightBorderBrushRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
            {
                ColorBlend blend = new ColorBlend();
                blend.Colors = new Color[] { Color.FromArgb(250, HightLight), Color.FromArgb(0, HightLight), Color.FromArgb(20, HightLight) };
                blend.Positions = new float[] { 0f, .65f, 1f };
                brush.InterpolationColors = blend;
                g.DrawEllipse(new Pen(brush), hightLightborderRect);
                g.PixelOffsetMode = PixelOffsetMode.Default;
            }

           

            if (string.IsNullOrEmpty(this.Text))
            {
                string text = "Click to add.";
                SizeF fontSize = g.MeasureString(text, this.Font); //TextRenderer.MeasureText();
                PointF fontLocation = new PointF(
                    (this.ClientRectangle.Left + this.ClientRectangle.Height / 2 + this.CircleRect.Left) / 2 - fontSize.Width / 2, this.ClientRectangle.Top + (this.ClientRectangle.Height - fontSize.Height) / 2 + 2);

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(128, Color.Black)))
                {
                    g.DrawString(text, this.Font, brush, new PointF(fontLocation.X, fontLocation.Y));
                }
            }
            else
            {
                SizeF fontSize = g.MeasureString(this.Text, this.Font); //TextRenderer.MeasureText();
                PointF fontLocation = new PointF(this.ClientRectangle.Left + this.ClientRectangle.Height / 4, this.ClientRectangle.Top + (this.ClientRectangle.Height - fontSize.Height) / 2 + 2);

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, Color.Black)))
                {
                    g.DrawString(this.Text, this.Font, brush, new PointF(fontLocation.X, fontLocation.Y));
                }
            }



        }
    }
}
