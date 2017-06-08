namespace IM.View.UserControl
{
    partial class ucStatusBar
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
            this.toggleStatus = new Fink.Windows.Forms.ToggleButtonEx();
            this.btnHangup = new Fink.Windows.Forms.ButtonEx();
            this.dropDownMenu1 = new Fink.Windows.Forms.DropDownMenu();
            this.EvaluatetoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.changePasswordToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.logoutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.userHeader = new IM.View.UserHeader();
            this.lblDuration = new System.Windows.Forms.Label();
            this.picClassBeginStatus = new System.Windows.Forms.PictureBox();
            this.dropDownMenu1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClassBeginStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // toggleStatus
            // 
            this.toggleStatus.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toggleStatus.BackColor = System.Drawing.Color.Transparent;
            this.toggleStatus.Checked = true;
            this.toggleStatus.Location = new System.Drawing.Point(711, 11);
            this.toggleStatus.Margin = new System.Windows.Forms.Padding(0);
            this.toggleStatus.MinimumSize = new System.Drawing.Size(66, 30);
            this.toggleStatus.Name = "toggleStatus";
            this.toggleStatus.Radius = 16;
            this.toggleStatus.RoundStyle = Fink.Drawing.RoundStyle.All;
            this.toggleStatus.Size = new System.Drawing.Size(66, 30);
            this.toggleStatus.TabIndex = 2;
            this.toggleStatus.TextPading = new System.Drawing.Size(10, 5);
            this.toggleStatus.UseVisualStyleBackColor = false;
            // 
            // btnHangup
            // 
            this.btnHangup.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnHangup.BackColor = System.Drawing.Color.Transparent;
            this.btnHangup.IsWaring = true;
            this.btnHangup.Location = new System.Drawing.Point(701, 9);
            this.btnHangup.Margin = new System.Windows.Forms.Padding(0);
            this.btnHangup.MinimumSize = new System.Drawing.Size(86, 32);
            this.btnHangup.Name = "btnHangup";
            this.btnHangup.Radius = 16;
            this.btnHangup.RoundStyle = Fink.Drawing.RoundStyle.All;
            this.btnHangup.Size = new System.Drawing.Size(86, 32);
            this.btnHangup.TabIndex = 1;
            this.btnHangup.Text = "End Class";
            this.btnHangup.TextPading = new System.Drawing.Size(9, 5);
            this.btnHangup.UseVisualStyleBackColor = false;
            this.btnHangup.Visible = false;
            // 
            // dropDownMenu1
            // 
            this.dropDownMenu1.BackColor = System.Drawing.Color.White;
            this.dropDownMenu1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dropDownMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EvaluatetoolStripButton,
            this.changePasswordToolStripButton,
            this.logoutToolStripButton});
            this.dropDownMenu1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.dropDownMenu1.Name = "dropDownMenu1";
            this.dropDownMenu1.Padding = new System.Windows.Forms.Padding(5);
            this.dropDownMenu1.SearchPattern = null;
            this.dropDownMenu1.Size = new System.Drawing.Size(138, 103);
            // 
            // EvaluatetoolStripButton
            // 
            this.EvaluatetoolStripButton.AutoSize = false;
            this.EvaluatetoolStripButton.Name = "EvaluatetoolStripButton";
            this.EvaluatetoolStripButton.Padding = new System.Windows.Forms.Padding(5);
            this.EvaluatetoolStripButton.Size = new System.Drawing.Size(128, 28);
            this.EvaluatetoolStripButton.Text = "My evaluation";
            this.EvaluatetoolStripButton.Click += new System.EventHandler(this.EvaluatetoolStripButton_Click);
            // 
            // changePasswordToolStripButton
            // 
            this.changePasswordToolStripButton.AutoSize = false;
            this.changePasswordToolStripButton.Name = "changePasswordToolStripButton";
            this.changePasswordToolStripButton.Padding = new System.Windows.Forms.Padding(5);
            this.changePasswordToolStripButton.Size = new System.Drawing.Size(128, 28);
            this.changePasswordToolStripButton.Text = "Change Password";
            this.changePasswordToolStripButton.Click += new System.EventHandler(this.changePasswordToolStripButton_Click);
            // 
            // logoutToolStripButton
            // 
            this.logoutToolStripButton.AutoSize = false;
            this.logoutToolStripButton.Name = "logoutToolStripButton";
            this.logoutToolStripButton.Padding = new System.Windows.Forms.Padding(5);
            this.logoutToolStripButton.Size = new System.Drawing.Size(128, 28);
            this.logoutToolStripButton.Text = "Logout";
            this.logoutToolStripButton.Click += new System.EventHandler(this.logoutToolStripButton_Click);
            // 
            // userHeader
            // 
            this.userHeader.Location = new System.Drawing.Point(12, 1);
            this.userHeader.Margin = new System.Windows.Forms.Padding(0);
            this.userHeader.Name = "userHeader";
            this.userHeader.Size = new System.Drawing.Size(230, 48);
            this.userHeader.TabIndex = 0;
            this.userHeader.Text = "userHeader1";
            this.userHeader.UserInfo = null;
            // 
            // lblDuration
            // 
            this.lblDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblDuration.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDuration.Location = new System.Drawing.Point(350, 1);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(100, 48);
            this.lblDuration.TabIndex = 3;
            this.lblDuration.Text = "label1";
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picClassBeginStatus
            // 
            this.picClassBeginStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picClassBeginStatus.Location = new System.Drawing.Point(661, 19);
            this.picClassBeginStatus.Name = "picClassBeginStatus";
            this.picClassBeginStatus.Size = new System.Drawing.Size(16, 16);
            this.picClassBeginStatus.TabIndex = 4;
            this.picClassBeginStatus.TabStop = false;
            // 
            // ucStatusBar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.picClassBeginStatus);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.toggleStatus);
            this.Controls.Add(this.btnHangup);
            this.Controls.Add(this.userHeader);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucStatusBar";
            this.Size = new System.Drawing.Size(800, 50);
            this.dropDownMenu1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picClassBeginStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserHeader userHeader;
        private Fink.Windows.Forms.ButtonEx btnHangup;
        private Fink.Windows.Forms.ToggleButtonEx toggleStatus;
        private Fink.Windows.Forms.DropDownMenu dropDownMenu1;
        private System.Windows.Forms.ToolStripButton changePasswordToolStripButton;
        private System.Windows.Forms.ToolStripButton logoutToolStripButton;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.ToolStripButton EvaluatetoolStripButton;
        private System.Windows.Forms.PictureBox picClassBeginStatus;
    }
}
