namespace IM.View.UserControl
{
    partial class ucVideoChat
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucVideoChat));
            this.pnlVideoBase = new System.Windows.Forms.Panel();
            this.switchControls = new Fink.Windows.Forms.SwitchCapsuleEx();
            this.imgsDevices = new System.Windows.Forms.ImageList(this.components);
            this.hostStuInfo = new System.Windows.Forms.Integration.ElementHost();
            this.ucStuInfo = new IM.View.ucStuInfo();
            this.pbLocal = new IM.View.PictureBox();
            this.pbRemote = new IM.View.PictureBox();
            this.pnlVideoBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLocal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemote)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlVideoBase
            // 
            this.pnlVideoBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlVideoBase.Controls.Add(this.pbLocal);
            this.pnlVideoBase.Controls.Add(this.pbRemote);
            this.pnlVideoBase.Location = new System.Drawing.Point(5, 5);
            this.pnlVideoBase.Name = "pnlVideoBase";
            this.pnlVideoBase.Size = new System.Drawing.Size(290, 290);
            this.pnlVideoBase.TabIndex = 4;
            // 
            // switchControls
            // 
            this.switchControls.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.switchControls.AutoSize = true;
            this.switchControls.ImageList = this.imgsDevices;
            this.switchControls.Location = new System.Drawing.Point(100, 305);
            this.switchControls.MinimumSize = new System.Drawing.Size(40, 36);
            this.switchControls.Name = "switchControls";
            this.switchControls.Size = new System.Drawing.Size(100, 36);
            this.switchControls.TabIndex = 3;
            this.switchControls.Text = "switchControls";
            // 
            // imgsDevices
            // 
            this.imgsDevices.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgsDevices.ImageStream")));
            this.imgsDevices.TransparentColor = System.Drawing.Color.Transparent;
            this.imgsDevices.Images.SetKeyName(0, "camera_on.png");
            this.imgsDevices.Images.SetKeyName(1, "camera_off.png");
            this.imgsDevices.Images.SetKeyName(2, "mic_on.png");
            this.imgsDevices.Images.SetKeyName(3, "mic_off.png");
            this.imgsDevices.Images.SetKeyName(4, "speaker_on.png");
            this.imgsDevices.Images.SetKeyName(5, "speaker_off.png");
            // 
            // hostStuInfo
            // 
            this.hostStuInfo.BackColor = System.Drawing.Color.White;
            this.hostStuInfo.Location = new System.Drawing.Point(38, 384);
            this.hostStuInfo.Name = "hostStuInfo";
            this.hostStuInfo.Size = new System.Drawing.Size(200, 153);
            this.hostStuInfo.TabIndex = 5;
            this.hostStuInfo.Text = "elementHost1";
            this.hostStuInfo.Child = this.ucStuInfo;
            // 
            // pbLocal
            // 
            this.pbLocal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pbLocal.CacheData = null;
            this.pbLocal.Location = new System.Drawing.Point(0, 300);
            this.pbLocal.Name = "pbLocal";
            this.pbLocal.Size = new System.Drawing.Size(0, 0);
            this.pbLocal.TabIndex = 1;
            this.pbLocal.TabStop = false;
            // 
            // pbRemote
            // 
            this.pbRemote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pbRemote.CacheData = null;
            this.pbRemote.Location = new System.Drawing.Point(0, 0);
            this.pbRemote.Name = "pbRemote";
            this.pbRemote.Size = new System.Drawing.Size(300, 300);
            this.pbRemote.TabIndex = 0;
            this.pbRemote.TabStop = false;
            // 
            // ucVideoChat
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.hostStuInfo);
            this.Controls.Add(this.pnlVideoBase);
            this.Controls.Add(this.switchControls);
            this.Name = "ucVideoChat";
            this.Size = new System.Drawing.Size(300, 660);
            this.pnlVideoBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLocal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemote)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Fink.Windows.Forms.SwitchCapsuleEx switchControls;
        private System.Windows.Forms.Panel pnlVideoBase;
        private PictureBox pbLocal;
        private PictureBox pbRemote;
        private System.Windows.Forms.ImageList imgsDevices;
        private System.Windows.Forms.Integration.ElementHost hostStuInfo;
        private ucStuInfo ucStuInfo;
    }
}
