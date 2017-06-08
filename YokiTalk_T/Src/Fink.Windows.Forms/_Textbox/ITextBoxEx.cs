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
    public class ITextBoxEx : System.Windows.Forms.Control
    {
        public delegate void RenderPathEventHandler(System.Drawing.Drawing2D.GraphicsPath path);
        private TextBoxEx textBox = new TextBoxEx();
        public ITextBoxEx()
        {
            this.SuspendLayout();
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.TabIndex = 1;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            
            this.Controls.Add(this.textBox);
            this.ResumeLayout();

            this.Click += (object sender, EventArgs e) =>
            {
                this.textBox.Focus();
            };
            this.GotFocus += (object sender, EventArgs e) =>
            {
                this.textBox.Focus();
            };

            this.textBox.GotFocus += (object sender, EventArgs e) =>
            {
                this.BorderColor = Color.FromArgb(255, 56, 180, 75);
            };
            this.textBox.LostFocus += (object sender, EventArgs e) =>
            {
                this.BorderColor = Color.FromArgb(255, 192, 194, 204);
            };
        }

        #region Property

        private Color borderColor = Color.FromArgb(255, 192, 194, 204);
        [DefaultValue(typeof(Color), "192,194,204")]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; this.Invalidate(); }
        }

        private Color textForeColor = Color.Black;
        [DefaultValue(typeof(Color), "Black")]
        public Color TextForeColor
        {
            get { return textForeColor; }
            set
            {
                textForeColor = value;
                this.textBox.ForeColor = textForeColor;
                this.Invalidate();
            }
        }

        public System.Windows.Forms.TextBox TextBox
        {
            get { return this.textBox; }
        }

        [System.ComponentModel.Browsable(true)]
        public bool ReadOnly
        {
            get { return this.textBox.ReadOnly; }
            set { 
                this.textBox.ReadOnly = value;
            }
        }
        [System.ComponentModel.Browsable(true)]
        [DefaultValue(typeof(System.Drawing.Font), "Arial")]
        public new System.Drawing.Font Font
        {
            get { return this.textBox.Font; }
            set { this.textBox.Font = value; }
        }
        [System.ComponentModel.Browsable(true)]
        public new System.Windows.Forms.Padding Margin
        {
            get { return this.textBox.Margin; }
            set { this.textBox.Margin = value; }
        }
        //[System.ComponentModel.Browsable(true)]
        //public new int TabIndex
        //{
        //    get { return this.textBox.TabIndex; }
        //    set { this.textBox.TabIndex = value; }
        //}
        [System.ComponentModel.Browsable(true)]
        [DefaultValue(typeof(System.Windows.Forms.HorizontalAlignment), "System.Windows.Forms.HorizontalAlignment.Center")]
        public System.Windows.Forms.HorizontalAlignment TextAlign
        {
            get { return this.textBox.TextAlign; }
            set { this.textBox.TextAlign = value; }
        }
        [System.ComponentModel.Browsable(true)]
        public new string Text
        {
            get { return this.textBox.Text; }
            set { this.textBox.Text = value; }
        }

        [DefaultValue(typeof(bool), "false")]
        public bool Multiline
        {
            get
            {
                return this.textBox.Multiline;
            }
            set
            {
                this.textBox.Multiline = value;
                this.Invalidate();
            }
        }
        #endregion

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            OnLayout();
        }

        private void OnLayout()
        {
            int radius = Math.Min((this.Width - 1) / 2, (this.Height - 1) / 2);
            int left = radius;
            int top = (this.Height - this.textBox.Height) / 2;
            this.textBox.Location = new Point(left, top);
            int width = this.Width - radius * 2;
            this.textBox.Width = width > 0 ? width: 0;
        }

        public new void Focus()
        {
            this.textBox.Focus();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            //
            System.Drawing.Drawing2D.GraphicsPath path = 
                this.Multiline ? 
                Fink.Drawing.RectangleEx.CreatePath(new Rectangle(0, 0, this.Width - 1, this.Height - 1), 6, Drawing.RoundStyle.All)
                :
                Fink.Drawing.RectangleEx.CreatePath(new Rectangle(0, 0, this.Width - 1, this.Height - 1), Math.Min((this.Width - 1) / 2, (this.Height - 1) / 2), Drawing.RoundStyle.All);

            g.PixelOffsetMode = PixelOffsetMode.Half;
            using (Brush b = new SolidBrush(this.BorderColor))
            {
                g.FillPath(b, path);
            }

            System.Drawing.Drawing2D.GraphicsPath borderPath2 =
                this.Multiline ?
                Fink.Drawing.RectangleEx.CreatePath(new Rectangle(1, 1, this.Width - 1 - 2, this.Height - 1 - 2), 5, Drawing.RoundStyle.All)
                : 
                Fink.Drawing.RectangleEx.CreatePath(new Rectangle(1, 1, this.Width - 1 - 2, this.Height - 1 - 2), Math.Min((this.Width - 1 - 2) / 2, (this.Height - 1 - 2) / 2), Drawing.RoundStyle.All);

            using (Brush b = new SolidBrush(Color.White))
            {
                g.FillPath(b, borderPath2);
            }
            g.PixelOffsetMode = PixelOffsetMode.Default;
        }

    }
}
