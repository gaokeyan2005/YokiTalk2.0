namespace IM.View.UserControl
{
    partial class ucChat
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
            this.hostCourseware = new System.Windows.Forms.Integration.ElementHost();
            this.bookCourseware = new IM.View.libBook();
            this.ucVideoChat = new IM.View.UserControl.ucVideoChat();
            this.SuspendLayout();
            // 
            // hostCourseware
            // 
            this.hostCourseware.Location = new System.Drawing.Point(0, 0);
            this.hostCourseware.Name = "hostCourseware";
            this.hostCourseware.Size = new System.Drawing.Size(500, 600);
            this.hostCourseware.TabIndex = 1;
            this.hostCourseware.Text = "elementHost1";
            this.hostCourseware.Child = this.bookCourseware;
            // 
            // ucVideoChat
            // 
            this.ucVideoChat.BackColor = System.Drawing.Color.White;
            this.ucVideoChat.Location = new System.Drawing.Point(500, 0);
            this.ucVideoChat.Name = "ucVideoChat";
            this.ucVideoChat.Size = new System.Drawing.Size(300, 600);
            this.ucVideoChat.TabIndex = 2;
            // 
            // ucChat
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Controls.Add(this.ucVideoChat);
            this.Controls.Add(this.hostCourseware);
            this.Name = "ucChat";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost hostCourseware;
        private libBook bookCourseware;
        private ucVideoChat ucVideoChat;
    }
}
