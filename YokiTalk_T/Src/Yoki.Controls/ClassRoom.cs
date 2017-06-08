using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yoki.Controls
{
    public class ClassRoom: Fink.Windows.Forms.PanelEx
    {
        private VideoPanel videoPanel = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VideoPanel VideoPanel
        {
            get
            {
                return this.videoPanel;
            }
        }


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ReceivedVideoBox ReceivedVideoBox
        {
            get
            {
                return this.videoPanel.ReceivedVideoBox;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CapturedVideoBox CapturedVideoBox
        {
            get
            {
                return this.videoPanel.CapturedVideoBox;
            }
        }



        private Fink.Windows.Forms.TabControlEx tabControl = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Fink.Windows.Forms.TabControlEx TabControl
        {
            get
            {
                return this.tabControl;
            }
        }


        private Fink.Windows.Forms.ButtonEx btnEndClass = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Fink.Windows.Forms.ButtonEx BtnEndClass
        {
            get
            {
                return this.btnEndClass;
            }
        }

        private System.Windows.Forms.Label labPage = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.Label LabPage
        {
            get
            {
                return this.labPage;
            }
        }

        private ImageViewer imageViewer = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ImageViewer ImageViewer
        {
            get
            {
                return this.imageViewer;
            }
        }


        [DefaultValue(null)]
        public System.Windows.Forms.ImageList ImageList
        {
            get
            {
                return this.videoPanel.VideoControlPanel.ImageList;
            }
            set
            {
                this.videoPanel.VideoControlPanel.ImageList = value;
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
            }
        }

        private Font boldFont;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Font BoldFont
        {
            get
            {
                return this.boldFont;
            }

            set
            {
                this.boldFont = value;
                this.videoPanel.Font = this.boldFont;
                this.tabpageHeader.Font = this.boldFont;
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
                this.videoPanel.MicroFont = this.microFont;
            }
        }


        
        public string Title
        {
            get
            {
                return this.tabpageHeader.Text;
            }
            set
            {
                this.tabpageHeader.Text = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RemoteUserInfo RemoteUserInfo
        {
            get
            {
                return this.ReceivedVideoBox.RemoteUserInfo;
            }
            set
            {
                this.ReceivedVideoBox.RemoteUserInfo = value;
            }
        }


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VideoQuality VideoQuality
        {
            get
            {
                return this.CapturedVideoBox.VideoQuality;
            }
            set
            {
                this.CapturedVideoBox.VideoQuality = value;
            }
        }

        public ClassRoom():base()
        {
            InitializeComponent();
        }

        private Fink.Windows.Forms.TabPageEx tabPageCourseware;
        private Fink.Windows.Forms.TabPageEx tabPageStuInfo;
        private TabpageHeader tabpageHeader = null;
        private void InitializeComponent()
        {
            this.tabControl = new Fink.Windows.Forms.TabControlEx();
            this.tabPageCourseware = new Fink.Windows.Forms.TabPageEx();
            this.tabPageStuInfo = new Fink.Windows.Forms.TabPageEx();
            this.btnEndClass = new Fink.Windows.Forms.ButtonEx();
            this.videoPanel = new Yoki.Controls.VideoPanel();

            this.tabpageHeader = new TabpageHeader();
            this.imageViewer = new ImageViewer();

            this.labPage = new Label();

            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCourseware);
            this.tabControl.Controls.Add(this.tabPageStuInfo);
            this.tabControl.ItemSize = new System.Drawing.Size(0, 36);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(0, 0);
            this.tabControl.TabIndex = 6;



            // 
            // tabpageHeader
            // 
            this.tabpageHeader.Dock = DockStyle.Top;
            this.tabpageHeader.HitTestVisibility = false;
            this.tabpageHeader.BackColor = Color.White;
            this.tabpageHeader.Location = new System.Drawing.Point(0, 0);
            this.tabpageHeader.Name = "tabpageHeader";
            this.tabpageHeader.Size = new System.Drawing.Size(0, 37);
            this.tabpageHeader.Font = this.Font;
            this.tabpageHeader.Text = "Where am I?";

            // 
            // imageViewer
            // 
            this.imageViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageViewer.BackColor = Color.FromArgb(255, 240, 240 ,240);
            this.imageViewer.Location = new System.Drawing.Point(0, 0);
            this.imageViewer.Name = "imageViewer";
            this.imageViewer.Size = new System.Drawing.Size(0, 0);
            this.imageViewer.TabIndex = 0;


            // 
            // tabPageCourseware
            // 
            this.tabPageCourseware.BackColor = System.Drawing.Color.Transparent;
            this.tabPageCourseware.Location = new System.Drawing.Point(1, 39);
            this.tabPageCourseware.Name = "tabPageCourseware";
            this.tabPageCourseware.Size = new System.Drawing.Size(366, 264);
            this.tabPageCourseware.TabIndex = 0;
            this.tabPageCourseware.Text = "     Courseware     ";

            this.tabPageCourseware.Controls.Add(this.imageViewer);
            this.tabPageCourseware.Controls.Add(this.tabpageHeader);
            // 
            // tabPageStuInfo
            // 
            this.tabPageStuInfo.BackColor = System.Drawing.Color.Transparent;
            this.tabPageStuInfo.Location = new System.Drawing.Point(1, 39);
            this.tabPageStuInfo.Name = "tabPageStuInfo";
            this.tabPageStuInfo.Size = new System.Drawing.Size(366, 264);
            this.tabPageStuInfo.TabIndex = 1;
            this.tabPageStuInfo.Text = "     Student info     ";


            // 
            // buttonEx1
            // 
            this.btnEndClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEndClass.BackColor = System.Drawing.Color.White;
            this.btnEndClass.IsWaring = true;
            this.btnEndClass.Location = new System.Drawing.Point(0, 0);
            this.btnEndClass.Name = "btnEndClass";
            this.btnEndClass.Radius = 16;
            this.btnEndClass.RoundStyle = Fink.Drawing.RoundStyle.All;
            this.btnEndClass.Size = new System.Drawing.Size(84, 35);
            this.btnEndClass.TabIndex = 7;
            this.btnEndClass.Text = "END";
            this.btnEndClass.TextPading = new System.Drawing.Size(16, 7);
            this.btnEndClass.UseVisualStyleBackColor = false;

            // 
            // labPage
            // 
            //this.labPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labPage.BackColor = Color.FromArgb(255, 240, 240, 240); 
            this.labPage.Location = new System.Drawing.Point(0, 0);
            this.labPage.Name = "labPage";
            this.labPage.Size = new System.Drawing.Size(60, 25);
            this.labPage.TabIndex = 9;
            this.labPage.Font = new Font("", 10, FontStyle.Regular); ;
            this.labPage.AutoSize = true;
            this.labPage.Text = "1 / 10";
            
            // 
            // videoPanel
            // 
            this.videoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.videoPanel.Location = new System.Drawing.Point(0, 0);
            this.videoPanel.Name = "videoPanel";
            this.videoPanel.Size = new System.Drawing.Size(0, 0);
            this.videoPanel.TabIndex = 5;

            this.Controls.Add(this.labPage);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnEndClass);
            this.Controls.Add(this.videoPanel);
            this.ResumeLayout(false);
            //this.labPage.BringToFront();
            Rearray();
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Rearray();
        }

        private static double TabRate = 0.7;
        private static double VideoRate = 0.3;
        private static int SpliterWidth = 12;
        private static int EndButtonAreaHeight = 38;
        private void Rearray()
        {
            if (this.Width <= 0 || this.Height <= 0)
            {
                return;
            }
            this.SuspendLayout();



            int tabWidth = Convert.ToInt32(Math.Floor((this.Width - SpliterWidth) * TabRate));
            int videoWidth = Convert.ToInt32(Math.Floor((this.Width - SpliterWidth) * VideoRate));
            

            this.tabControl.Size = new Size(tabWidth, this.Height);
            this.tabControl.Location = new Point(0, 0);
            
            this.btnEndClass.Location = new Point(this.Width -this.btnEndClass.Width, (EndButtonAreaHeight - this.btnEndClass.Height) / 2);
            
            this.videoPanel.Size = new Size(videoWidth, this.Height - EndButtonAreaHeight);
            this.videoPanel.Location = new Point(this.tabControl.Width + SpliterWidth, EndButtonAreaHeight);
            this.labPage.Location = new Point((this.Width - this.labPage.Width - this.videoPanel.Width - 20) / 2, this.Height - 35);

            this.ResumeLayout();
        }
    }
}
