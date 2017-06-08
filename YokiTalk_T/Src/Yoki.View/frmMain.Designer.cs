namespace Yoki.View
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlLeft = new Fink.Windows.Forms.PanelEx();
            this.imageListVideoControl = new System.Windows.Forms.ImageList(this.components);
            this.waitingRoom = new Yoki.Controls.WaitingRoom();
            this.classRoom = new Yoki.Controls.ClassRoom();
            this.blockMenu1 = new Yoki.Controls.BlockMenu();
            this.userHeader = new Yoki.Controls.UserHeader();
            this.ucReload = new Yoki.View.UserControl.ucReload();
            this.pnlLeft.SuspendLayout();
            this.classRoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlLeft.Controls.Add(this.blockMenu1);
            this.pnlLeft.Controls.Add(this.userHeader);
            this.pnlLeft.HitTestVisibility = false;
            this.pnlLeft.Location = new System.Drawing.Point(1, 1);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(128, 559);
            this.pnlLeft.TabIndex = 1;
            // 
            // imageListVideoControl
            // 
            this.imageListVideoControl.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListVideoControl.ImageStream")));
            this.imageListVideoControl.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListVideoControl.Images.SetKeyName(0, "control_camera.png");
            this.imageListVideoControl.Images.SetKeyName(1, "control_disable_camera.png");
            this.imageListVideoControl.Images.SetKeyName(2, "control_mic.png");
            this.imageListVideoControl.Images.SetKeyName(3, "control_disable_mic.png");
            this.imageListVideoControl.Images.SetKeyName(4, "control_speaker.png");
            this.imageListVideoControl.Images.SetKeyName(5, "control_disable_speaker.png");
            // 
            // waitingRoom
            // 
            this.waitingRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.waitingRoom.BackColor = System.Drawing.Color.White;
            this.waitingRoom.HitTestVisibility = false;
            this.waitingRoom.IsWaiting = true;
            this.waitingRoom.Location = new System.Drawing.Point(140, 35);
            this.waitingRoom.Name = "waitingRoom";
            this.waitingRoom.Size = new System.Drawing.Size(832, 514);
            this.waitingRoom.TabIndex = 10;
            // 
            // classRoom
            // 
            this.classRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.classRoom.BackColor = System.Drawing.Color.White;
            this.classRoom.Controls.Add(this.ucReload);
            this.classRoom.HitTestVisibility = false;
            this.classRoom.ImageList = this.imageListVideoControl;
            this.classRoom.Location = new System.Drawing.Point(140, 35);
            this.classRoom.Name = "classRoom";
            this.classRoom.Size = new System.Drawing.Size(832, 514);
            this.classRoom.TabIndex = 8;
            this.classRoom.Title = "Where am I?";
            // 
            // blockMenu1
            // 
            this.blockMenu1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blockMenu1.Location = new System.Drawing.Point(0, 96);
            this.blockMenu1.Name = "blockMenu1";
            this.blockMenu1.Size = new System.Drawing.Size(128, 463);
            this.blockMenu1.TabIndex = 1;
            this.blockMenu1.TabStop = false;
            // 
            // userHeader
            // 
            this.userHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.userHeader.HasNotify = true;
            this.userHeader.HeaderImage = ((System.Drawing.Image)(resources.GetObject("userHeader.HeaderImage")));
            this.userHeader.HeaderTitle = "Queenie";
            this.userHeader.Location = new System.Drawing.Point(0, 0);
            this.userHeader.Name = "userHeader";
            this.userHeader.Size = new System.Drawing.Size(128, 96);
            this.userHeader.TabIndex = 0;
            this.userHeader.TabStop = false;
            // 
            // ucReload
            // 
            this.ucReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucReload.BackColor = System.Drawing.Color.Transparent;
            this.ucReload.Location = new System.Drawing.Point(485, 46);
            this.ucReload.Name = "ucReload";
            this.ucReload.Size = new System.Drawing.Size(80, 20);
            this.ucReload.TabIndex = 10;
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(76)))), ((int)(((byte)(79)))));
            this.CaptionFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.CaptionHeight = 32;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.waitingRoom);
            this.Controls.Add(this.classRoom);
            this.Controls.Add(this.pnlLeft);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YOKI TALK TEACHER CLIENT";
            this.pnlLeft.ResumeLayout(false);
            this.classRoom.ResumeLayout(false);
            this.classRoom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.UserHeader userHeader;
        private Fink.Windows.Forms.PanelEx pnlLeft;
        private Controls.BlockMenu blockMenu1;
        private System.Windows.Forms.ImageList imageListVideoControl;
        private Controls.ClassRoom classRoom;
        private Controls.WaitingRoom waitingRoom;
        private UserControl.ucReload ucReload;
    }
}

