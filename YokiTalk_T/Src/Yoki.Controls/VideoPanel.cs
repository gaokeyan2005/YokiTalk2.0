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
    public class VideoPanel: System.Windows.Forms.Panel
    {

        private ReceivedVideoBox receivedVideoBox = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ReceivedVideoBox ReceivedVideoBox
        {
            get
            {
                return this.receivedVideoBox;
            }
        }

        private CapturedVideoBox capturedVideoBox = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CapturedVideoBox CapturedVideoBox
        {
            get
            {
                return this.capturedVideoBox;
            }
        }
        private VideoControlPanel videoControlPanel;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VideoControlPanel VideoControlPanel
        {
            get
            {
                return this.videoControlPanel;
            }
        }

        public VideoPanel():base()
        {
            InitializeComponent();
        }


        private Size capturedVideoSize = new Size(640, 480);
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size CapturedVideoSize
        {
            get
            {
                return this.capturedVideoSize;
            }
            set
            {
                if (this.capturedVideoSize != value)
                {
                    this.capturedVideoSize = value;
                    if (this.IsHandleCreated)
                    {
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            this.Rearray();
                        });
                    }
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTimeWaring
        {
            get
            {
                return this.videoControlPanel.IsTimeWaring;
            }
            set
            {
                if (this.videoControlPanel.IsTimeWaring != value)
                {
                    this.videoControlPanel.IsTimeWaring = value;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TimeTag
        {
            get
            {
                return this.videoControlPanel.Text;
            }
            set
            {
                if (this.videoControlPanel.Text != value)
                {
                    this.videoControlPanel.Text = value;
                }
            }
        }


        [DefaultValue(null)]
        public System.Windows.Forms.ImageList ImageList
        {
            get
            {
                return this.videoControlPanel.ImageList;
            }
            set
            {
                this.videoControlPanel.ImageList = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VideoBoxStatus Status
        {
            get
            {
                return this.CapturedVideoBox.Status & this.ReceivedVideoBox.Status;
            }
            set
            {
                this.CapturedVideoBox.Status = value;
                this.ReceivedVideoBox.Status = value;
                if (value == VideoBoxStatus.NoVideo)
                {
                    this.TimeTag = string.Empty;
                }
            }
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {
                base.Font = value;
                this.receivedVideoBox.Font = this.Font;
            }
        }

        private Font microFont;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Font MicroFont
        {
            get
            {
                return this.microFont;
            }
            set
            {
                this.microFont = value;
                this.receivedVideoBox.MicroFont = this.microFont;
            }
        }

        private void InitializeComponent()
        {
            this.capturedVideoBox = new Yoki.Controls.CapturedVideoBox();
            this.receivedVideoBox = new Yoki.Controls.ReceivedVideoBox();
            this.videoControlPanel = new Yoki.Controls.VideoControlPanel();

            this.SuspendLayout();

            this.Controls.Clear();
            // 
            // capturedVideoBox
            // 
            this.capturedVideoBox.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom);
            this.capturedVideoBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.capturedVideoBox.Location = new System.Drawing.Point(0, 0);
            this.capturedVideoBox.Name = "capturedVideoBox";
            this.capturedVideoBox.Size = new System.Drawing.Size(0, 0);
            this.capturedVideoBox.TabIndex = 1;
            this.capturedVideoBox.TabStop = false;
            // 
            // videoControlPanel1
            // 
            this.videoControlPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom);
            this.videoControlPanel.BackColor = System.Drawing.Color.White;
            //this.videoControlPanel.ImageList = this.;
            this.videoControlPanel.Location = new System.Drawing.Point(0, 0);
            this.videoControlPanel.Name = "videoControlPanel";
            this.videoControlPanel.Size = new System.Drawing.Size(0, 0);
            this.videoControlPanel.TabIndex = 2;
            this.videoControlPanel.Text = "";
            // 
            // receivedVideoBox
            // 
            this.receivedVideoBox.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom);
            this.receivedVideoBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.receivedVideoBox.Location = new System.Drawing.Point(0, 0);
            this.receivedVideoBox.Name = "receivedVideoBox";
            this.receivedVideoBox.Size = new System.Drawing.Size(0, 0);
            this.receivedVideoBox.TabIndex = 3;
            this.receivedVideoBox.TabStop = false;

            this.Controls.Add(this.capturedVideoBox);
            this.Controls.Add(this.videoControlPanel);
            this.Controls.Add(this.receivedVideoBox);

            this.ResumeLayout();
            this.Rearray();

        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Rearray();
        }


        private void Rearray()
        {
            if (this.Width <= 0 || this.Height <= 0)
            {
                return;
            }
            this.SuspendLayout();

            double capturedRate = (double)this.CapturedVideoSize.Width / this.CapturedVideoSize.Height;

            this.capturedVideoBox.Size = new Size(this.Width, Convert.ToInt32(this.Width / capturedRate));
            this.capturedVideoBox.Location = new Point(0, this.Height - this.capturedVideoBox.Height);

            this.videoControlPanel.Size = new Size(this.Width, VideoControlPanel.DefaultHeight);
            this.videoControlPanel.Location = new Point(0, this.Height - this.capturedVideoBox.Height - this.videoControlPanel.Height);

            this.receivedVideoBox.Size = new Size(this.Width, this.Height - this.capturedVideoBox.Height - this.videoControlPanel.Height);
            this.receivedVideoBox.Location = new Point(0, 0);

            this.ResumeLayout();
        }

    }
}
