namespace Yoki.View.UserControl
{
    partial class ucLeftMenu
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.picBoxMenu3 = new System.Windows.Forms.PictureBox();
            this.picBoxMenu2 = new System.Windows.Forms.PictureBox();
            this.picBoxMenu1 = new System.Windows.Forms.PictureBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.picBoxSetting = new System.Windows.Forms.PictureBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMenu3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMenu2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl1);
            this.panel1.Controls.Add(this.picBoxMenu3);
            this.panel1.Controls.Add(this.picBoxMenu2);
            this.panel1.Controls.Add(this.picBoxMenu1);
            this.panel1.Controls.Add(this.lbl2);
            this.panel1.Controls.Add(this.picBoxSetting);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 463);
            this.panel1.TabIndex = 0;
            // 
            // picBoxMenu3
            // 
            this.picBoxMenu3.Location = new System.Drawing.Point(0, 204);
            this.picBoxMenu3.Name = "picBoxMenu3";
            this.picBoxMenu3.Size = new System.Drawing.Size(128, 98);
            this.picBoxMenu3.TabIndex = 2;
            this.picBoxMenu3.TabStop = false;
            // 
            // picBoxMenu2
            // 
            this.picBoxMenu2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picBoxMenu2.Location = new System.Drawing.Point(0, 106);
            this.picBoxMenu2.Name = "picBoxMenu2";
            this.picBoxMenu2.Size = new System.Drawing.Size(128, 98);
            this.picBoxMenu2.TabIndex = 1;
            this.picBoxMenu2.TabStop = false;
            this.picBoxMenu2.Click += new System.EventHandler(this.picBoxMenu2_Click);
            this.picBoxMenu2.MouseEnter += new System.EventHandler(this.picBoxMenu2_MouseEnter);
            this.picBoxMenu2.MouseLeave += new System.EventHandler(this.picBoxMenu2_MouseLeave);
            // 
            // picBoxMenu1
            // 
            this.picBoxMenu1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picBoxMenu1.Location = new System.Drawing.Point(0, 8);
            this.picBoxMenu1.Name = "picBoxMenu1";
            this.picBoxMenu1.Size = new System.Drawing.Size(128, 98);
            this.picBoxMenu1.TabIndex = 0;
            this.picBoxMenu1.TabStop = false;
            this.picBoxMenu1.Click += new System.EventHandler(this.picBoxMenu1_Click);
            this.picBoxMenu1.MouseEnter += new System.EventHandler(this.picBoxMenu1_MouseEnter);
            this.picBoxMenu1.MouseLeave += new System.EventHandler(this.picBoxMenu1_MouseLeave);
            // 
            // lbl2
            // 
            this.lbl2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl2.Location = new System.Drawing.Point(0, 361);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(128, 8);
            this.lbl2.TabIndex = 8;
            // 
            // picBoxSetting
            // 
            this.picBoxSetting.Location = new System.Drawing.Point(0, 370);
            this.picBoxSetting.Name = "picBoxSetting";
            this.picBoxSetting.Size = new System.Drawing.Size(128, 92);
            this.picBoxSetting.TabIndex = 7;
            this.picBoxSetting.TabStop = false;
            // 
            // lbl1
            // 
            this.lbl1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl1.Location = new System.Drawing.Point(0, 0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(128, 8);
            this.lbl1.TabIndex = 9;
            // 
            // ucLeftMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucLeftMenu";
            this.Size = new System.Drawing.Size(128, 463);
            this.SizeChanged += new System.EventHandler(this.ucLeftMenu_SizeChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMenu3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMenu2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSetting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picBoxMenu3;
        private System.Windows.Forms.PictureBox picBoxMenu2;
        private System.Windows.Forms.PictureBox picBoxMenu1;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.PictureBox picBoxSetting;
    }
}
