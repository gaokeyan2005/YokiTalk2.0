using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoki.Controls
{
    public class VideoControlPanel: Fink.Windows.Forms.PanelEx
    {
        public Fink.Windows.Forms.SwitchCapsuleCheckedhandle SwitchCheckedChanged;

        private static int _textAreaWidth = 32;
        private static Size _swicthAreaSize = new Size(100, 36);

        public static int DefaultHeight = 44;


        private Fink.Windows.Forms.SwitchCapsuleEx switchCapsuleEx;

        public VideoControlPanel() : base()
        {
            base.SetStyle(
                   ControlStyles.UserPaint |
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.ResizeRedraw |
                   ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();

            InitializeComponent();
        }
        public VideoControlPanel(System.Windows.Forms.ImageList imageList): this()
        {
            this.ImageList = imageList;
        }
        
        [DefaultValue(null)]
        public ImageList ImageList
        {
            get
            {
                return this.switchCapsuleEx.ImageList;
            }
            set
            {
                this.switchCapsuleEx.ImageList = value;
            }
        }

        
        [DefaultValue("false")]
        public bool IsTimeWaring
        {
            get;
            set;
        }

        private string text = "VideoControlPanel";
        [DefaultValue("VideoControlPanel")]
        public override string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.Invalidate(new Rectangle(0, 0, this.Width / 2, this.Height));
            }
        }

        private void InitializeComponent()
        {
            this.switchCapsuleEx = new Fink.Windows.Forms.SwitchCapsuleEx();

            // 
            // switchCapsuleEx1
            // 
            this.switchCapsuleEx.AutoSize = true;
            this.switchCapsuleEx.BackColor = System.Drawing.Color.White;
            this.switchCapsuleEx.ImageList = this.ImageList;
            this.switchCapsuleEx.Location = new System.Drawing.Point(0, 0);
            this.switchCapsuleEx.MinimumSize = new System.Drawing.Size(40, 36);
            this.switchCapsuleEx.Name = "switchCapsuleEx1";
            this.switchCapsuleEx.Size = new System.Drawing.Size(100, 36);
            this.switchCapsuleEx.TabIndex = 0;
            this.switchCapsuleEx.Text = string.Empty;
            this.switchCapsuleEx.SwitchCapsuleCheckedChanged += (o, e) =>
            {
                if (this.SwitchCheckedChanged != null)
                {
                    this.SwitchCheckedChanged(o, e);
                }
            };

            this.Controls.Add(this.switchCapsuleEx);
            Rearray();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Rearray();
        }
        

        private void Rearray()
        {
            if (this.Width <=0 || this.Height <= 0)
            {
                return;
            }
            this.SuspendLayout();
            this.switchCapsuleEx.Size = _swicthAreaSize;
            int switchLeft = Math.Max((this.Width - this.switchCapsuleEx.Width - 1), _textAreaWidth);
            int switchTop = (this.Height - this.switchCapsuleEx.Height) / 2;

            this.switchCapsuleEx.Location = new Point(switchLeft, switchTop);
            
            this.ResumeLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            Size textSize = System.Windows.Forms.TextRenderer.MeasureText(e.Graphics, this.Text, this.Font, Size.Empty, System.Windows.Forms.TextFormatFlags.NoPadding);

            Rectangle textRect = new Rectangle(this.Left  + 5, (this.Height - textSize.Height) / 2, textSize.Width, textSize.Height);
            PaintText(this.Text, this.Font, e.Graphics, textRect, this.IsTimeWaring);
        }


        private static void PaintText(string text, Font font, Graphics g, Rectangle rect, bool isWaring =false)
        {
            using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
            {
                g.DrawString(text, font, new SolidBrush(isWaring? Color.FromArgb(255, 251, 99, 98) : Color.FromArgb(255, 102, 102, 102)), new Point(rect.Left, rect.Top));
            };
        }
    }
}
