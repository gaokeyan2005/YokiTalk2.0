using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yoki.View.UserControl
{
    public partial class ucChangeName : System.Windows.Forms.UserControl
    {
        private string strPrompt = "Please enter a name in English.";//默认提示文字
        #region 自定义控件属性
        private string _newText;
        /// <summary>
        /// 文本框中的值
        /// </summary>
        [Description("文本框中的值")]
        public string NewText
        {
            get
            {
                if (txtExName.Text == strPrompt)
                {
                    _newText = "";
                }
                else
                {
                    _newText = txtExName.Text;
                }                    
                return _newText;
            }

            set
            {
                _newText = value;
                txtExName.Text = _newText;
            }
        }
        #endregion 自定义控件属性
        #region 自定义控件事件
        //定义委托
        public delegate void ucChangeNameOkClick(object sender, EventArgs e);
        //定义事件
        [Description("点击OK按钮发生的点击事件")]
        public event ucChangeNameOkClick OkClicked;
        #endregion 自定义控件事件
        public ucChangeName()
        {
            InitializeComponent();
            this.Size = new Size(362, 38);
            this.MaximumSize = new Size(362, 38);
            txtExName.Size = new Size(230, 30);
            txtExName.Text = strPrompt;
            txtExName.TextForeColor = Color.Gainsboro;
        }

        private void txtExName_Click(object sender, EventArgs e)
        {
            if (txtExName.Text == strPrompt)
            {
                txtExName.Text = "";
                txtExName.TextForeColor = Color.Black;
            }

        }

        private void txtExName_Leave(object sender, EventArgs e)
        {
            if (txtExName.Text == string.Empty || txtExName.Text == strPrompt)
            {
                txtExName.Text = strPrompt;
                txtExName.TextForeColor = Color.Gainsboro;
            }
        }

        private void txtExName_Enter(object sender, EventArgs e)
        {
            if (txtExName.Text == strPrompt)
            {
                txtExName.Text = "";
                txtExName.TextForeColor = Color.Black;
            }
        }

        private void btnExCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void btnExOk_Click(object sender, EventArgs e)
        {
            if (OkClicked != null)
            {
                OkClicked(sender, new EventArgs());//把按钮自身作为参数传递
            }
        }
    }
}
