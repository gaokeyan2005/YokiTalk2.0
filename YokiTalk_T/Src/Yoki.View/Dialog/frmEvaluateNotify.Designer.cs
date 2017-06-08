namespace Yoki.View.Dialog
{
    partial class frmEvaluateNotify
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
            this.ucEvaluate1 = new Yoki.View.UserControl.ucEvaluate();
            this.scrollviewEx1 = new Fink.Windows.Forms.ScrollviewEx();
            this.SuspendLayout();
            // 
            // ucEvaluate1
            // 
            this.ucEvaluate1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ucEvaluate1.Location = new System.Drawing.Point(0, 0);
            this.ucEvaluate1.Name = "ucEvaluate1";
            this.ucEvaluate1.Size = new System.Drawing.Size(575, 500);
            this.ucEvaluate1.TabIndex = 0;
            // 
            // scrollviewEx1
            // 
            this.scrollviewEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollviewEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.scrollviewEx1.Child = this.ucEvaluate1;
            this.scrollviewEx1.Location = new System.Drawing.Point(1, 120);
            this.scrollviewEx1.Name = "scrollviewEx1";
            this.scrollviewEx1.Size = new System.Drawing.Size(598, 432);
            this.scrollviewEx1.TabIndex = 3;
            this.scrollviewEx1.Text = "scrollviewEx1";
            // 
            // frmEvaluateNotify
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.CaptionFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.ControlBox = false;
            this.Controls.Add(this.scrollviewEx1);
            this.IsSigleButtonMode = true;
            this.Name = "frmEvaluateNotify";
            this.ThemeType = Fink.Windows.Forms.ThemeType.Dark;
            this.Controls.SetChildIndex(this.scrollviewEx1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.ucEvaluate ucEvaluate1;
        private Fink.Windows.Forms.ScrollviewEx scrollviewEx1;
    }
}