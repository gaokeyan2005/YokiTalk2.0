namespace Yoki.View
{
    partial class frmATest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmATest));
            this.videoControlPanel1 = new Yoki.Controls.VideoControlPanel();
            this.videoPanel1 = new Yoki.Controls.VideoPanel();
            this.textBoxEx1 = new Fink.Windows.Forms.TextBoxEx();
            this.iTextBoxEx1 = new Fink.Windows.Forms.ITextBoxEx();
            this.textBoxEx2 = new Fink.Windows.Forms.TextBoxEx();
            this.buttonEx1 = new Fink.Windows.Forms.ButtonEx();
            this.ucChangeName1 = new Yoki.View.UserControl.ucChangeName();
            this.ucLeftMenu1 = new Yoki.View.UserControl.ucLeftMenu();
            this.ucVideoControl4 = new Yoki.View.UserControl.ucVideoControl();
            this.ucVideoControl3 = new Yoki.View.UserControl.ucVideoControl();
            this.ucVideoControl2 = new Yoki.View.UserControl.ucVideoControl();
            this.ucVideoControl1 = new Yoki.View.UserControl.ucVideoControl();
            this.SuspendLayout();
            // 
            // videoControlPanel1
            // 
            this.videoControlPanel1.HitTestVisibility = false;
            this.videoControlPanel1.IsTimeWaring = false;
            this.videoControlPanel1.Location = new System.Drawing.Point(487, 167);
            this.videoControlPanel1.Name = "videoControlPanel1";
            this.videoControlPanel1.Size = new System.Drawing.Size(269, 114);
            this.videoControlPanel1.TabIndex = 6;
            // 
            // videoPanel1
            // 
            this.videoPanel1.Location = new System.Drawing.Point(39, 47);
            this.videoPanel1.Name = "videoPanel1";
            this.videoPanel1.Size = new System.Drawing.Size(205, 387);
            this.videoPanel1.TabIndex = 5;
            // 
            // textBoxEx1
            // 
            this.textBoxEx1.AutoCompleteCustomSource.AddRange(new string[] {
            "ss",
            "aa"});
            this.textBoxEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxEx1.HotBackColor = System.Drawing.Color.White;
            this.textBoxEx1.HotBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(180)))), ((int)(((byte)(75)))));
            this.textBoxEx1.Location = new System.Drawing.Point(450, 380);
            this.textBoxEx1.LostBackColor = System.Drawing.SystemColors.Window;
            this.textBoxEx1.LostBorderColor = System.Drawing.Color.Transparent;
            this.textBoxEx1.Name = "textBoxEx1";
            this.textBoxEx1.Size = new System.Drawing.Size(144, 21);
            this.textBoxEx1.TabIndex = 0;
            // 
            // iTextBoxEx1
            // 
            this.iTextBoxEx1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iTextBoxEx1.Location = new System.Drawing.Point(587, 287);
            this.iTextBoxEx1.Multiline = true;
            this.iTextBoxEx1.Name = "iTextBoxEx1";
            this.iTextBoxEx1.ReadOnly = false;
            this.iTextBoxEx1.Size = new System.Drawing.Size(171, 36);
            this.iTextBoxEx1.TabIndex = 14;
            this.iTextBoxEx1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxEx2
            // 
            this.textBoxEx2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxEx2.HotBackColor = System.Drawing.Color.White;
            this.textBoxEx2.HotBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(180)))), ((int)(((byte)(75)))));
            this.textBoxEx2.Location = new System.Drawing.Point(413, 297);
            this.textBoxEx2.LostBackColor = System.Drawing.SystemColors.Window;
            this.textBoxEx2.LostBorderColor = System.Drawing.Color.Transparent;
            this.textBoxEx2.Name = "textBoxEx2";
            this.textBoxEx2.Size = new System.Drawing.Size(100, 21);
            this.textBoxEx2.TabIndex = 0;
            // 
            // buttonEx1
            // 
            this.buttonEx1.BackColor = System.Drawing.Color.Transparent;
            this.buttonEx1.Image = global::Yoki.View.Properties.Resources.closeWaring;
            this.buttonEx1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEx1.IsWaring = false;
            this.buttonEx1.Location = new System.Drawing.Point(470, 324);
            this.buttonEx1.Name = "buttonEx1";
            this.buttonEx1.Radius = 16;
            this.buttonEx1.RoundStyle = Fink.Drawing.RoundStyle.All;
            this.buttonEx1.Size = new System.Drawing.Size(111, 37);
            this.buttonEx1.TabIndex = 12;
            this.buttonEx1.Text = "↻ 刷新";
            this.buttonEx1.TextPading = new System.Drawing.Size(16, 7);
            this.buttonEx1.UseVisualStyleBackColor = false;
            // 
            // ucChangeName1
            // 
            this.ucChangeName1.BackColor = System.Drawing.Color.Transparent;
            this.ucChangeName1.Location = new System.Drawing.Point(413, 427);
            this.ucChangeName1.MaximumSize = new System.Drawing.Size(362, 38);
            this.ucChangeName1.Name = "ucChangeName1";
            this.ucChangeName1.NewText = "";
            this.ucChangeName1.Size = new System.Drawing.Size(362, 38);
            this.ucChangeName1.TabIndex = 15;
            this.ucChangeName1.OkClicked += new Yoki.View.UserControl.ucChangeName.ucChangeNameOkClick(this.ucChangeName1_OkClicked);
            // 
            // ucLeftMenu1
            // 
            this.ucLeftMenu1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ucLeftMenu1.BlMenu1 = false;
            this.ucLeftMenu1.BlMenu2 = false;
            this.ucLeftMenu1.BlMenu3 = false;
            this.ucLeftMenu1.Location = new System.Drawing.Point(260, 20);
            this.ucLeftMenu1.Name = "ucLeftMenu1";
            this.ucLeftMenu1.Size = new System.Drawing.Size(120, 445);
            this.ucLeftMenu1.TabIndex = 11;
            this.ucLeftMenu1.LeftMenu1Clicked += new Yoki.View.UserControl.ucLeftMenu.ucLeftMenu1Click(this.ucLeftMenu1_LeftMenu1Clicked);
            this.ucLeftMenu1.LeftMenu2Clicked += new Yoki.View.UserControl.ucLeftMenu.ucLeftMenu2Click(this.ucLeftMenu1_LeftMenu2Clicked);
            // 
            // ucVideoControl4
            // 
            this.ucVideoControl4.BackColor = System.Drawing.Color.Transparent;
            this.ucVideoControl4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucVideoControl4.BackgroundImage")));
            this.ucVideoControl4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ucVideoControl4.BoardColor = System.Drawing.Color.Transparent;
            this.ucVideoControl4.Location = new System.Drawing.Point(432, 62);
            this.ucVideoControl4.MaximumSize = new System.Drawing.Size(102, 39);
            this.ucVideoControl4.MinimumSize = new System.Drawing.Size(102, 39);
            this.ucVideoControl4.Name = "ucVideoControl4";
            this.ucVideoControl4.Size = new System.Drawing.Size(102, 39);
            this.ucVideoControl4.TabIndex = 10;
            this.ucVideoControl4.ValueCamera = true;
            this.ucVideoControl4.ValueMic = true;
            this.ucVideoControl4.ValueSpeaker = true;
            // 
            // ucVideoControl3
            // 
            this.ucVideoControl3.BackColor = System.Drawing.Color.Transparent;
            this.ucVideoControl3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucVideoControl3.BackgroundImage")));
            this.ucVideoControl3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ucVideoControl3.BoardColor = System.Drawing.Color.Transparent;
            this.ucVideoControl3.Location = new System.Drawing.Point(432, 122);
            this.ucVideoControl3.MaximumSize = new System.Drawing.Size(102, 39);
            this.ucVideoControl3.MinimumSize = new System.Drawing.Size(102, 39);
            this.ucVideoControl3.Name = "ucVideoControl3";
            this.ucVideoControl3.Size = new System.Drawing.Size(102, 39);
            this.ucVideoControl3.TabIndex = 9;
            this.ucVideoControl3.ValueCamera = true;
            this.ucVideoControl3.ValueMic = true;
            this.ucVideoControl3.ValueSpeaker = true;
            // 
            // ucVideoControl2
            // 
            this.ucVideoControl2.BackColor = System.Drawing.Color.Transparent;
            this.ucVideoControl2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucVideoControl2.BackgroundImage")));
            this.ucVideoControl2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ucVideoControl2.BoardColor = System.Drawing.Color.Transparent;
            this.ucVideoControl2.Location = new System.Drawing.Point(633, 106);
            this.ucVideoControl2.MaximumSize = new System.Drawing.Size(102, 39);
            this.ucVideoControl2.MinimumSize = new System.Drawing.Size(102, 39);
            this.ucVideoControl2.Name = "ucVideoControl2";
            this.ucVideoControl2.Size = new System.Drawing.Size(102, 39);
            this.ucVideoControl2.TabIndex = 8;
            this.ucVideoControl2.ValueCamera = true;
            this.ucVideoControl2.ValueMic = true;
            this.ucVideoControl2.ValueSpeaker = true;
            // 
            // ucVideoControl1
            // 
            this.ucVideoControl1.BackColor = System.Drawing.Color.Transparent;
            this.ucVideoControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucVideoControl1.BackgroundImage")));
            this.ucVideoControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ucVideoControl1.BoardColor = System.Drawing.Color.Red;
            this.ucVideoControl1.Location = new System.Drawing.Point(633, 62);
            this.ucVideoControl1.MaximumSize = new System.Drawing.Size(102, 39);
            this.ucVideoControl1.MinimumSize = new System.Drawing.Size(102, 39);
            this.ucVideoControl1.Name = "ucVideoControl1";
            this.ucVideoControl1.Size = new System.Drawing.Size(102, 39);
            this.ucVideoControl1.TabIndex = 7;
            this.ucVideoControl1.ValueCamera = true;
            this.ucVideoControl1.ValueMic = true;
            this.ucVideoControl1.ValueSpeaker = true;
            // 
            // frmATest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 505);
            this.Controls.Add(this.ucChangeName1);
            this.Controls.Add(this.textBoxEx2);
            this.Controls.Add(this.iTextBoxEx1);
            this.Controls.Add(this.textBoxEx1);
            this.Controls.Add(this.buttonEx1);
            this.Controls.Add(this.ucLeftMenu1);
            this.Controls.Add(this.ucVideoControl4);
            this.Controls.Add(this.ucVideoControl3);
            this.Controls.Add(this.ucVideoControl2);
            this.Controls.Add(this.ucVideoControl1);
            this.Controls.Add(this.videoControlPanel1);
            this.Controls.Add(this.videoPanel1);
            this.Name = "frmATest";
            this.Text = "frmATest";
            this.Load += new System.EventHandler(this.frmATest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.VideoPanel videoPanel1;
        private Controls.VideoControlPanel videoControlPanel1;
        private UserControl.ucVideoControl ucVideoControl1;
        private UserControl.ucVideoControl ucVideoControl2;
        private UserControl.ucVideoControl ucVideoControl3;
        private UserControl.ucVideoControl ucVideoControl4;
        private UserControl.ucLeftMenu ucLeftMenu1;
        private Fink.Windows.Forms.ButtonEx buttonEx1;
        private Fink.Windows.Forms.TextBoxEx textBoxEx1;
        private Fink.Windows.Forms.ITextBoxEx iTextBoxEx1;
        private Fink.Windows.Forms.TextBoxEx textBoxEx2;
        private UserControl.ucChangeName ucChangeName1;
    }
}