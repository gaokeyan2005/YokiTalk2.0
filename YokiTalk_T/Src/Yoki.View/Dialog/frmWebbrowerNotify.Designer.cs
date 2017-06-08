namespace IM.View.Dialog
{
    partial class frmWebbrowerNotify
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(1, 44);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowserMyEvaluation";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.ScriptErrorsSuppressed = true; //禁用错误脚本提示   
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false; //禁用右键菜单   
            this.webBrowser1.WebBrowserShortcutsEnabled = false; //禁用快捷键
            this.webBrowser1.AllowWebBrowserDrop = false;//禁止拖拽
            this.webBrowser1.Size = new System.Drawing.Size(598, 507);
            this.webBrowser1.TabIndex = 2;
            // 
            // frmWebbrowerNotify
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.CaptionFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.Controls.Add(this.webBrowser1);
            this.IsSigleButtonMode = true;
            this.Name = "frmWebbrowerNotify";
            this.Text = "My Evaluation";
            this.ThemeType = Fink.Windows.Forms.ThemeType.Dark;
            this.Controls.SetChildIndex(this.webBrowser1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}