using NIM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yoki.Comm;

namespace Yoki.View
{
    public partial class frmLogin : Fink.Windows.Forms.FormEx
    {
        public bool IsLoginSuccessed
        {
            get;
            set;
        }

        public frmLogin(Fink.Windows.Forms.FromStyle fs = Fink.Windows.Forms.FromStyle.Metro)
            : base(fs)
        {
            InitializeComponent();
            
            this.Text += Business.AccountController.GetEnvironment();
            this.txtPwd.TextBox.PasswordChar = '*';

            this.txtUName.Text = "testtest1";
            this.txtPwd.Text = "123456";

            this.lblUName.Font = FontUtil.DefaultBoldFont;
            this.lblPwd.Font = FontUtil.DefaultBoldFont;
            this.txtUName.Font = FontUtil.DefaultBoldFont;
            this.txtPwd.Font = FontUtil.DefaultBoldFont;
            this.btnLogin.Font = FontUtil.DefaultBoldFont;
            this.linkCantLogin.Font = FontUtil.DefaultBoldFont;

            this.KeyPreview = true;

            this.btnLogin.Click += (o, e) =>
            {
                //登录相应事件
                DoLogin();
            };
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter)
            {
                DoLogin();
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
            else
            {
                base.OnKeyPress(e);
            }
        }
        private void DoLogin()
        {
            Task tLogin = new Task(() =>
            {
                bool isSuccess = false;
                this.BeginInvoke((MethodInvoker)delegate
                {
                    this.btnLogin.Enabled = false;
                    FormUtil.ShowAnimationTooltip(this, "Yoki Talk login");
                });
                try
                {
                    //clear data
                    Business.AccountController.Instance.ClearData();
                    // login Yoki
                    KeyValuePair<int, string> result = Yoki.View.Business.AccountController.Instance.Login(this.txtUName.Text, this.txtPwd.Text);
                    if (result.Key != 0)
                    {
                        throw new Exception(result.Value);
                    }
                    else
                    {
                        CommUserInfo.UserId = Business.AccountController.Instance.Data.UserID.ToString();
                        CommUserInfo.Account = this.txtUName.Text.ToString();
                        CommUserInfo.UserNickName = Business.AccountController.Instance.Data.NickName;
                        //判断用户是否存在
                        if (Business.AccountController.Instance.Data.UserID > 0)
                        {
                            string userID = Business.AccountController.Instance.Data.UserID.ToString();
                            //存在用户信息时，开始登录网易云信用户帐号信息
                            isSuccess = Yoki.IM.Manager.Instance.IMLogin(userID, userID);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0}, {1}", ex.GetType().Name, ex.Message));
                }
                finally
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        FormUtil.CloseAnimationTooltip(this);
                        this.btnLogin.Enabled = true;


                        if (isSuccess)
                        {
                            this.IsLoginSuccessed = true;
                            this.Close();
                        }
                    });
                }


            });
            tLogin.Start();
        }
    }
}
