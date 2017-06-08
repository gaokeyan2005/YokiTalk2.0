namespace IM.View.Dialog
{
    partial class frmPasswordNotify
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
            this.label2 = new System.Windows.Forms.Label();
            this.lblOld = new System.Windows.Forms.Label();
            this.txtNew = new Fink.Windows.Forms.ITextBoxEx();
            this.txtOld = new Fink.Windows.Forms.ITextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNew2 = new Fink.Windows.Forms.ITextBoxEx();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "New Password:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOld
            // 
            this.lblOld.BackColor = System.Drawing.Color.White;
            this.lblOld.Location = new System.Drawing.Point(14, 60);
            this.lblOld.Name = "lblOld";
            this.lblOld.Size = new System.Drawing.Size(124, 15);
            this.lblOld.TabIndex = 7;
            this.lblOld.Text = "Old Password:";
            this.lblOld.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNew
            // 
            this.txtNew.BackColor = System.Drawing.Color.White;
            this.txtNew.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtNew.Location = new System.Drawing.Point(145, 99);
            this.txtNew.Name = "txtNew";
            this.txtNew.ReadOnly = false;
            this.txtNew.Size = new System.Drawing.Size(162, 29);
            this.txtNew.TabIndex = 6;
            this.txtNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtOld
            // 
            this.txtOld.BackColor = System.Drawing.Color.White;
            this.txtOld.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtOld.Location = new System.Drawing.Point(145, 54);
            this.txtOld.Name = "txtOld";
            this.txtOld.ReadOnly = false;
            this.txtOld.Size = new System.Drawing.Size(162, 29);
            this.txtOld.TabIndex = 5;
            this.txtOld.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Confirm Password:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNew2
            // 
            this.txtNew2.BackColor = System.Drawing.Color.White;
            this.txtNew2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtNew2.Location = new System.Drawing.Point(145, 144);
            this.txtNew2.Name = "txtNew2";
            this.txtNew2.ReadOnly = false;
            this.txtNew2.Size = new System.Drawing.Size(162, 29);
            this.txtNew2.TabIndex = 9;
            this.txtNew2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmPasswordNotify
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.BtnCancelText = "CANCEL";
            this.BtnOKText = "SUBMIT";
            this.CaptionFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ClientSize = new System.Drawing.Size(331, 245);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNew2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblOld);
            this.Controls.Add(this.txtNew);
            this.Controls.Add(this.txtOld);
            this.Name = "frmPasswordNotify";
            this.Text = "Change Password";
            this.Controls.SetChildIndex(this.txtOld, 0);
            this.Controls.SetChildIndex(this.txtNew, 0);
            this.Controls.SetChildIndex(this.lblOld, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtNew2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOld;
        private Fink.Windows.Forms.ITextBoxEx txtNew;
        private Fink.Windows.Forms.ITextBoxEx txtOld;
        private System.Windows.Forms.Label label3;
        private Fink.Windows.Forms.ITextBoxEx txtNew2;
    }
}