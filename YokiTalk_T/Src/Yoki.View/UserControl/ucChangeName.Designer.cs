namespace Yoki.View.UserControl
{
    partial class ucChangeName
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
            this.btnExCancel = new Fink.Windows.Forms.ButtonEx();
            this.btnExOk = new Fink.Windows.Forms.ButtonEx();
            this.txtExName = new Fink.Windows.Forms.ITextBoxEx();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnExCancel);
            this.panel1.Controls.Add(this.btnExOk);
            this.panel1.Controls.Add(this.txtExName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(362, 38);
            this.panel1.TabIndex = 16;
            // 
            // btnExCancel
            // 
            this.btnExCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnExCancel.IsWaring = false;
            this.btnExCancel.Location = new System.Drawing.Point(298, 4);
            this.btnExCancel.Name = "btnExCancel";
            this.btnExCancel.Radius = 16;
            this.btnExCancel.RoundStyle = Fink.Drawing.RoundStyle.All;
            this.btnExCancel.Size = new System.Drawing.Size(57, 30);
            this.btnExCancel.TabIndex = 18;
            this.btnExCancel.Text = "Cancel";
            this.btnExCancel.TextPading = new System.Drawing.Size(16, 7);
            this.btnExCancel.UseVisualStyleBackColor = false;
            this.btnExCancel.Click += new System.EventHandler(this.btnExCancel_Click);
            // 
            // btnExOk
            // 
            this.btnExOk.BackColor = System.Drawing.Color.Transparent;
            this.btnExOk.IsWaring = false;
            this.btnExOk.Location = new System.Drawing.Point(239, 4);
            this.btnExOk.Name = "btnExOk";
            this.btnExOk.Radius = 16;
            this.btnExOk.RoundStyle = Fink.Drawing.RoundStyle.All;
            this.btnExOk.Size = new System.Drawing.Size(57, 30);
            this.btnExOk.TabIndex = 17;
            this.btnExOk.Text = "OK";
            this.btnExOk.TextPading = new System.Drawing.Size(16, 7);
            this.btnExOk.UseVisualStyleBackColor = false;
            this.btnExOk.Click += new System.EventHandler(this.btnExOk_Click);
            // 
            // txtExName
            // 
            this.txtExName.BackColor = System.Drawing.Color.Transparent;
            this.txtExName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtExName.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtExName.Location = new System.Drawing.Point(3, 5);
            this.txtExName.Name = "txtExName";
            this.txtExName.ReadOnly = false;
            this.txtExName.Size = new System.Drawing.Size(230, 29);
            this.txtExName.TabIndex = 16;
            this.txtExName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtExName.Click += new System.EventHandler(this.txtExName_Click);
            this.txtExName.Enter += new System.EventHandler(this.txtExName_Enter);
            this.txtExName.Leave += new System.EventHandler(this.txtExName_Leave);
            // 
            // ucChangeName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Name = "ucChangeName";
            this.Size = new System.Drawing.Size(362, 38);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Fink.Windows.Forms.ButtonEx btnExCancel;
        private Fink.Windows.Forms.ButtonEx btnExOk;
        private Fink.Windows.Forms.ITextBoxEx txtExName;
    }
}
