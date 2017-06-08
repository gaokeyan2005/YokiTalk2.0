namespace Yoki.View.UserControl
{
    partial class ucVideoControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.picBoxCamera = new System.Windows.Forms.PictureBox();
            this.picBoxMic = new System.Windows.Forms.PictureBox();
            this.picBoxSpeaker = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCamera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSpeaker)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxCamera
            // 
            this.picBoxCamera.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBoxCamera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBoxCamera.Location = new System.Drawing.Point(9, 7);
            this.picBoxCamera.Name = "picBoxCamera";
            this.picBoxCamera.Size = new System.Drawing.Size(25, 24);
            this.picBoxCamera.TabIndex = 0;
            this.picBoxCamera.TabStop = false;
            this.picBoxCamera.Click += new System.EventHandler(this.picBoxCamera_Click);
            // 
            // picBoxMic
            // 
            this.picBoxMic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBoxMic.Location = new System.Drawing.Point(37, 7);
            this.picBoxMic.Name = "picBoxMic";
            this.picBoxMic.Size = new System.Drawing.Size(25, 24);
            this.picBoxMic.TabIndex = 1;
            this.picBoxMic.TabStop = false;
            this.picBoxMic.Click += new System.EventHandler(this.picBoxMic_Click);
            // 
            // picBoxSpeaker
            // 
            this.picBoxSpeaker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBoxSpeaker.Location = new System.Drawing.Point(66, 7);
            this.picBoxSpeaker.Name = "picBoxSpeaker";
            this.picBoxSpeaker.Size = new System.Drawing.Size(25, 24);
            this.picBoxSpeaker.TabIndex = 2;
            this.picBoxSpeaker.TabStop = false;
            this.picBoxSpeaker.Click += new System.EventHandler(this.picBoxSpeaker_Click);
            // 
            // ucVideoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Yoki.View.Properties.Resources.videocontrol;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.picBoxSpeaker);
            this.Controls.Add(this.picBoxMic);
            this.Controls.Add(this.picBoxCamera);
            this.DoubleBuffered = true;
            this.Name = "ucVideoControl";
            this.Size = new System.Drawing.Size(101, 38);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCamera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSpeaker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxCamera;
        private System.Windows.Forms.PictureBox picBoxMic;
        private System.Windows.Forms.PictureBox picBoxSpeaker;
    }
}
