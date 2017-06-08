namespace Yoki.View.UserControl
{
    partial class ucEvaluateCell
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
            this.pnlBase = new System.Windows.Forms.Panel();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.Label();
            this.starList = new Yoki.View.Partial.StarList();
            this.lblRate = new System.Windows.Forms.Label();
            this.pnlBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBase
            // 
            this.pnlBase.BackColor = System.Drawing.Color.White;
            this.pnlBase.Controls.Add(this.lblCategory);
            this.pnlBase.Controls.Add(this.txtContent);
            this.pnlBase.Controls.Add(this.starList);
            this.pnlBase.Location = new System.Drawing.Point(5, 0);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Size = new System.Drawing.Size(570, 100);
            this.pnlBase.TabIndex = 0;
            // 
            // lblCategory
            // 
            this.lblCategory.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCategory.Location = new System.Drawing.Point(1, 4);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(136, 23);
            this.lblCategory.TabIndex = 6;
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCategory.UseCompatibleTextRendering = true;
            // 
            // txtContent
            // 
            this.txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContent.AutoSize = true;
            this.txtContent.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtContent.Location = new System.Drawing.Point(128, 36);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(0, 15);
            this.txtContent.TabIndex = 5;
            // 
            // starList
            // 
            this.starList.Location = new System.Drawing.Point(143, 3);
            this.starList.Name = "starList";
            this.starList.Size = new System.Drawing.Size(169, 26);
            this.starList.StarsCount = 5;
            this.starList.TabIndex = 4;
            // 
            // lblRate
            // 
            this.lblRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRate.BackColor = System.Drawing.Color.White;
            this.lblRate.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblRate.Location = new System.Drawing.Point(507, 4);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(60, 23);
            this.lblRate.TabIndex = 7;
            this.lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRate.UseCompatibleTextRendering = true;
            // 
            // ucEvaluateCell
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Controls.Add(this.lblRate);
            this.Controls.Add(this.pnlBase);
            this.Name = "ucEvaluateCell";
            this.Size = new System.Drawing.Size(580, 105);
            this.pnlBase.ResumeLayout(false);
            this.pnlBase.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBase;
        private System.Windows.Forms.Label txtContent;
        private Partial.StarList starList;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblRate;
    }
}
