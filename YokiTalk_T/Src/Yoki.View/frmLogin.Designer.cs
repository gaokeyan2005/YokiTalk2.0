namespace Yoki.View
{
    partial class frmLogin
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
            this.btnLogin = new Fink.Windows.Forms.ButtonEx();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPwd = new System.Windows.Forms.Label();
            this.lblUName = new System.Windows.Forms.Label();
            this.txtPwd = new Fink.Windows.Forms.ITextBoxEx();
            this.txtUName = new Fink.Windows.Forms.ITextBoxEx();
            this.panel2 = new System.Windows.Forms.Panel();
            this.linkCantLogin = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.IsWaring = false;
            this.btnLogin.Location = new System.Drawing.Point(152, 3);
            this.btnLogin.MinimumSize = new System.Drawing.Size(10, 8);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Radius = 16;
            this.btnLogin.RoundStyle = Fink.Drawing.RoundStyle.All;
            this.btnLogin.Size = new System.Drawing.Size(96, 36);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.TextPading = new System.Drawing.Size(22, 6);
            this.btnLogin.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::Yoki.View.Properties.Resources.yokiLogoWithText;
            this.pictureBox1.Location = new System.Drawing.Point(53, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 120);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblPwd);
            this.panel1.Controls.Add(this.lblUName);
            this.panel1.Controls.Add(this.txtPwd);
            this.panel1.Controls.Add(this.txtUName);
            this.panel1.Location = new System.Drawing.Point(78, 175);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 100);
            this.panel1.TabIndex = 6;
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.BackColor = System.Drawing.Color.White;
            this.lblPwd.Location = new System.Drawing.Point(16, 63);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(66, 15);
            this.lblPwd.TabIndex = 8;
            this.lblPwd.Text = "Password:";
            // 
            // lblUName
            // 
            this.lblUName.AutoSize = true;
            this.lblUName.BackColor = System.Drawing.Color.White;
            this.lblUName.Location = new System.Drawing.Point(8, 21);
            this.lblUName.Name = "lblUName";
            this.lblUName.Size = new System.Drawing.Size(74, 15);
            this.lblUName.TabIndex = 7;
            this.lblUName.Text = "User Name:";
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.White;
            this.txtPwd.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPwd.Location = new System.Drawing.Point(89, 57);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.ReadOnly = false;
            this.txtPwd.Size = new System.Drawing.Size(157, 29);
            this.txtPwd.TabIndex = 6;
            this.txtPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtUName
            // 
            this.txtUName.BackColor = System.Drawing.Color.White;
            this.txtUName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtUName.Location = new System.Drawing.Point(89, 15);
            this.txtUName.Name = "txtUName";
            this.txtUName.ReadOnly = false;
            this.txtUName.Size = new System.Drawing.Size(157, 29);
            this.txtUName.TabIndex = 5;
            this.txtUName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.linkCantLogin);
            this.panel2.Controls.Add(this.btnLogin);
            this.panel2.Location = new System.Drawing.Point(78, 281);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(249, 43);
            this.panel2.TabIndex = 9;
            // 
            // linkCantLogin
            // 
            this.linkCantLogin.AutoSize = true;
            this.linkCantLogin.BackColor = System.Drawing.Color.White;
            this.linkCantLogin.DisabledLinkColor = System.Drawing.Color.Green;
            this.linkCantLogin.LinkColor = System.Drawing.Color.White;
            this.linkCantLogin.LinkVisited = true;
            this.linkCantLogin.Location = new System.Drawing.Point(10, 13);
            this.linkCantLogin.Name = "linkCantLogin";
            this.linkCantLogin.Size = new System.Drawing.Size(72, 15);
            this.linkCantLogin.TabIndex = 9;
            this.linkCantLogin.TabStop = true;
            this.linkCantLogin.Tag = "";
            this.linkCantLogin.Text = "Can\'t login?";
            this.linkCantLogin.VisitedLinkColor = System.Drawing.Color.Green;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CanResize = false;
            this.CaptionHeight = 32;
            this.ClientSize = new System.Drawing.Size(407, 353);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.MaximizeBox = false;
            this.Name = "frmLogin";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Fink.Windows.Forms.ButtonEx btnLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.Label lblUName;
        private Fink.Windows.Forms.ITextBoxEx txtPwd;
        private Fink.Windows.Forms.ITextBoxEx txtUName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel linkCantLogin;
    }
}