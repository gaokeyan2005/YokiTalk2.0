using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IM.View.UserControl
{
    public enum StatusType
    {
        WaitingOrder,
        InClass
    }

    public partial class ucStatusBar : System.Windows.Forms.UserControl
    {
        public event EventHandler OnHangupClicked;
        public event Fink.Windows.Forms.CheckedChangedEventHandler CheckedChanged;

        public event EventHandler OnLoginout;
        public event EventHandler OnChangePassword;
        public event EventHandler OnShowEvaluate;


        public System.Windows.Forms.Label txtDuration;

        public ucStatusBar()
        {
            InitializeComponent();

            this.txtDuration = this.lblDuration;

            this.btnHangup.Click += (o, e) =>
            {
                if (this.statusType == UserControl.StatusType.InClass)
                {
                    if (this.OnHangupClicked != null)
                    {
                        this.OnHangupClicked(this, new EventArgs());
                    }
                }
            };

            this.toggleStatus.CheckedChanged += (o, e) =>
            {
                IsWaitingForOrder = (o as Fink.Windows.Forms.ToggleButtonEx).Checked;
                if (this.CheckedChanged != null)
                {
                    this.CheckedChanged(o, e);
                }
            };

            this.userHeader.Click += (o, e) =>
            {
                Point p = this.PointToScreen(new Point(this.Left, this.Height));
                this.dropDownMenu1.Show(p);
            };

        }

        public UserInfo UserInfo
        {
            get
            {
                return this.userHeader.UserInfo;
            }
            set
            {
                this.userHeader.UserInfo = value;
            }
        }

        public bool IsWaitingForOrder
        {
            get
            {
                return this.toggleStatus.Checked;
            }
            set
            {
                this.toggleStatus.Checked = value;
            }
        }


        [DefaultValue(false)]
        public bool IsClassBegin
        {
            set
            {
                System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("IM.View.Properties.Resources", typeof(IM.View.Properties.Resources).Assembly);

                if (value)
                {
                    this.picClassBeginStatus.Image = (Bitmap)resourceManager.GetObject("status_on");
                }
                else
                {
                    this.picClassBeginStatus.Image = (Bitmap)resourceManager.GetObject("status_off");
                }
            }
        }

        [DefaultValue(typeof(StatusType), "WaitingOrder")]
        private StatusType statusType = StatusType.WaitingOrder;
        public StatusType StatusType
        {
            get
            {
                return this.statusType;
            }
            set
            {
                if (this.statusType != value)
                {
                    this.statusType = value;
                    switch (this.statusType)
                    {
                        case UserControl.StatusType.WaitingOrder:
                            this.btnHangup.Visible = false;
                            this.toggleStatus.Visible = true;
                            this.picClassBeginStatus.Visible = false;
                            break;
                        case UserControl.StatusType.InClass:
                            this.btnHangup.Visible = true;
                            this.toggleStatus.Visible = false;
                            this.picClassBeginStatus.Visible = true;
                            break;
                        default:
                            this.btnHangup.Visible = false;
                            this.toggleStatus.Visible = true;
                            break;
                    }
                }
            }
        }

        private void logoutToolStripButton_Click(object sender, EventArgs e)
        {
            if (this.OnLoginout!= null)
            {
                this.OnLoginout(this, e);
            }
        }

        private void changePasswordToolStripButton_Click(object sender, EventArgs e)
        {

            if (this.OnChangePassword != null)
            {
                this.OnChangePassword(this, e);
            }
        }

        private void EvaluatetoolStripButton_Click(object sender, EventArgs e)
        {
            if (this.OnShowEvaluate != null)
            {
                this.OnShowEvaluate(this, e);
            }

        }
    }
}
