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
    public partial class frmWebbrowerNotify : Fink.Windows.Forms.DialogFormEx
    {
        public frmWebbrowerNotify(Form parent, string uId)
            : base(parent)
        {
            InitializeComponent();
            this.Load += (o, e) =>
            {
                string uri = string.Format(ApplicationHelper.Browerpath + @"index.php?app=classroom&mod=Teacher&act=getEva&id={0}&page=1", uId);
                this.webBrowser1.Url = new Uri(uri);
            };
            
        }
    }
}
