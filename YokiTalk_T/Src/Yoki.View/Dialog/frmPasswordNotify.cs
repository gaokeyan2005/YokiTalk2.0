using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IM.View.Dialog
{
    public partial class frmPasswordNotify : Fink.Windows.Forms.DialogFormEx
    {
        string uid = string.Empty;
        public frmPasswordNotify(Form parent, string uid)
            : base(parent)
        {
            InitializeComponent();
            this.txtOld.TextBox.PasswordChar = '*';
            this.txtNew.TextBox.PasswordChar = '*';
            this.txtNew2.TextBox.PasswordChar = '*';

            this.uid = uid;

            this.Load += (o, e) =>
            {

            };

            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
           if(this.DialogResult == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(this.lblOld.Text) || string.IsNullOrEmpty(this.txtNew.Text) || string.IsNullOrEmpty(this.txtNew2.Text))
                {
                    MessageBox.Show("Password can't be null.");
                    e.Cancel = true;
                    return;
                }

                if (this.txtNew.Text != this.txtNew2.Text)
                {
                    MessageBox.Show("Password confirm not same as password.");
                    e.Cancel = true;
                    return;
                }

                IM.Comm.Object.JsonObject ro = IM.Comm.CommUtility.ChangePassword(ApplicationHelper.Serverpath, this.uid, this.txtOld.Text, this.txtNew.Text);
                if (ro == null || ro.Code != 0)
                {
                    MessageBox.Show(ro == null  || string.IsNullOrEmpty(ro.Msg) ?  "No response." : ro.Msg);
                    e.Cancel = true;
                    return;
                }

                MessageBox.Show("Password modifily successful.");
            }
            base.OnClosing(e);
        }


    }
}
