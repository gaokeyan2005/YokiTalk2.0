using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    public class DialogFormEx: Fink.Windows.Forms.FormEx
    {
        private Windows.Forms.ButtonEx btnCancel;
        private Windows.Forms.ButtonEx btnOk;

        private System.Windows.Forms.Panel panel1;

        public new System.Windows.Forms.Form ParentForm
        {
            get;
            private set;
        }

        private DialogFormEx()
            : base(FromStyle.Metro)
        {
            InitializeComponent();
            DialogFormExHelper.Instance.OpenedDialogForms.Add(this);
            //this.Renderer = new MacDialogFormExRender(new MacFormExColorTable());
            this.Renderer = new MetroFormExRenderer(new MetroFormExColorTable());

            this.KeyPreview = true;
        }
        
       [System.ComponentModel.DefaultValue("Cancel")]
        public string BtnCancelText
        {
            get
            {
                return this.btnCancel.Text;
            }
            set
            {
                this.btnCancel.Text = value;
            }
        }

        [System.ComponentModel.DefaultValue("96")]
        public int BtnCancelWidth
        {
            get
            {
                return this.btnCancel.Width;
            }
            set
            {
                this.btnCancel.Width = value;
            }
        }


        [System.ComponentModel.DefaultValue("OK")]
        public string BtnOKText
        {
            get
            {
                return this.btnOk.Text;
            }
            set
            {
                this.btnOk.Text = value;
            }
        }



        private bool isSigleButtonMode = false;
        [System.ComponentModel.DefaultValue(typeof(bool), "false")]
        public bool IsSigleButtonMode
        {
            get
            {
                return this.isSigleButtonMode;
            }
            set
            {
                this.isSigleButtonMode = value;
                this.btnCancel.Visible = !this.isSigleButtonMode;
            }
        }

        public DialogFormEx(System.Windows.Forms.Form parent)
            : this()
        {
            this.ParentForm = parent;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            DialogFormExHelper.Instance.OpenedDialogForms.Remove(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //for modifify start location
            //if (this.ParentForm != null)
            //{
            //    Size ps = this.ParentForm.Size;
            //    Size s = this.Size;

            //    if (this.ParentForm is Fink.Windows.Forms.DialogFormEx)
            //    {
            //        Point pp = this.ParentForm.PointToScreen(new Point(Convert.ToInt32(Math.Floor((ps.Width - s.Width) / 2.0)), Convert.ToInt32(Math.Floor((ps.Height - s.Height) / 2.0))));
            //        this.Location = new Point(pp.X, pp.Y);
            //    }
            //    else
            //    {
            //        Point pp = this.ParentForm.PointToScreen(new Point(Convert.ToInt32(Math.Floor((ps.Width - 282 - s.Width) / 2.0) + 282), Convert.ToInt32(Math.Floor((ps.Height - s.Height) / 2.0))));
            //        this.Location = new Point(pp.X, pp.Y);
            //    }
            //}
        }

        public virtual void OnOKClick()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        public virtual void OnCancelClick()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                this.btnOk.PerformClick();
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                this.btnCancel.PerformClick();
            }
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new Fink.Windows.Forms.ButtonEx();
            this.btnCancel.Click += (sender, e)=>
            {
                this.OnCancelClick();
            };
            this.btnOk = new Fink.Windows.Forms.ButtonEx();
            this.btnOk.Click += (sender, e) =>
            {
                this.OnOKClick();
            };
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();

            // 
            // buttonEx1
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Location = new System.Drawing.Point(31, 7);
            this.btnCancel.IsWaring = true;
            this.btnCancel.Name = "buttonEx1";
            this.btnCancel.Radius = 16;
            this.btnCancel.RoundStyle = Fink.Drawing.RoundStyle.All;
            this.btnCancel.Size = new System.Drawing.Size(96, 33);
            this.btnCancel.TabIndex = 998;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextPading = new System.Drawing.Size(31, 7);
            this.btnCancel.UseVisualStyleBackColor = false;
            //
            // buttonEx2
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))));
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.Location = new System.Drawing.Point(189, 7);
            this.btnOk.Name = "buttonEx2";
            this.btnOk.Radius = 999;
            this.btnOk.RoundStyle = Fink.Drawing.RoundStyle.All;
            this.btnOk.Size = new System.Drawing.Size(96, 33);
            this.btnOk.TabIndex = 1;
            this.btnOk.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnOk.Text = "OK";
            this.btnOk.TextPading = new System.Drawing.Size(16, 7);
            this.btnOk.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(1, 260);
            this.panel1.Name = "panel1";
            this.panel1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panel1.Size = new System.Drawing.Size(320, 48);
            this.panel1.TabIndex = 2;
            // 
            // frmCashier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.None;
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.BackColor = Color.Black;
            this.CanResize = false;
            this.ShowIcon = false;
            this.ClientSize = new System.Drawing.Size(320, 480);
            this.Controls.Add(this.panel1);
            this.IsDialog = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.panel1.TabIndex = 1;
        }


        #endregion
    }
}
