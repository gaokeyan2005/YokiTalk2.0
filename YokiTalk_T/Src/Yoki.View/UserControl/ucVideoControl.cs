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
    public partial class ucVideoControl : System.Windows.Forms.UserControl
    {
        #region 自定义控件属性值
        private  bool blPicboxCamera = true;//图像控制状态
        private  bool blPicboxMic = true;//话筒控制状态
        private  bool blPicboxSpeaker = true;//声音控制状态 
        private Color _BoardColor = Color.Transparent;

        [Description("控件边框的颜色")]//引用 System.ComponentModel;
        //[DefaultValue(typeof(Color), "Red")]//指定默认颜色值
        //[Browsable(false)]//不显示在属性窗口的时候
        [Category("外观")]//指定属性分组
        public Color BoardColor
        {
            get { return _BoardColor; }
            set
            {
                if (value == _BoardColor) return;
                _BoardColor = value;
                this.Invalidate();
            }
        }

        [Description("图像值设置")]
        public bool ValueCamera
        {
            get
            {
                return blPicboxCamera;
            }

            set
            {
                blPicboxCamera = value;
                if (blPicboxCamera == true)
                {
                    blPicboxCamera = true;
                    this.picBoxCamera.BackgroundImage = Yoki.View.Properties.Resources.control_camera;
                }
                else
                {
                    blPicboxCamera = false;
                    this.picBoxCamera.BackgroundImage = Yoki.View.Properties.Resources.control_disable_camera;
                }
            }
        }

        [Description("话筒值设置")]
        public bool ValueMic
        {
            get
            {
                return blPicboxMic;
            }

            set
            {
                blPicboxMic = value;
                if (blPicboxMic == true)
                {
                    blPicboxMic = true;
                    this.picBoxMic.BackgroundImage = Yoki.View.Properties.Resources.control_mic;
                }
                else
                {
                    blPicboxMic = false;
                    this.picBoxMic.BackgroundImage = Yoki.View.Properties.Resources.control_disable_mic;
                }
            }
        }

        [Description("声音值设置")]
        public bool ValueSpeaker
        {
            get
            {
                return blPicboxSpeaker;
            }

            set
            {
                blPicboxSpeaker = value;
                if (blPicboxSpeaker == true)
                {
                    blPicboxSpeaker = true;
                    this.picBoxSpeaker.BackgroundImage = Yoki.View.Properties.Resources.control_speaker;
                }
                else
                {
                    blPicboxSpeaker = false;
                    this.picBoxSpeaker.BackgroundImage = Yoki.View.Properties.Resources.control_disable_speaker;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Pen p = new Pen(this._BoardColor))
            {
                g.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
            }
            base.OnPaint(e);
        }
        #endregion 自定义控件属性值

        #region 自定义控件事件
        //定义委托
        public delegate void ucVideoControlPicboxCameraClick(object sender, EventArgs e);
        public delegate void ucVideoControlPicboxMicClick(object sender, EventArgs e);
        public delegate void ucVideoControlPicboxSpeakerClick(object sender, EventArgs e);
        //定义事件
        [Description("点击图像发生的事件")]
        public event ucVideoControlPicboxCameraClick PicboxCameraClicked;
        [Description("点击话筒发生的事件")]
        public event ucVideoControlPicboxCameraClick PicboxMicClicked;
        [Description("点击声音发生的事件")]
        public event ucVideoControlPicboxCameraClick PicboxSpeakerClicked;

        private void picBoxCamera_Click(object sender, EventArgs e)
        {
            if (PicboxCameraClicked != null)
            {
                PicboxCameraClicked(sender, new EventArgs());//把按钮自身作为参数传递
                if (blPicboxCamera == true)
                {
                    blPicboxCamera = false;
                    this.picBoxCamera.BackgroundImage = Yoki.View.Properties.Resources.control_disable_camera;
                }
                else
                {
                    blPicboxCamera = true;
                    this.picBoxCamera.BackgroundImage = Yoki.View.Properties.Resources.control_camera;
                }
            }
        }

        private void picBoxMic_Click(object sender, EventArgs e)
        {
            if (PicboxMicClicked != null)
            {
                PicboxMicClicked(sender, new EventArgs());//把按钮自身作为参数传递
                if (blPicboxMic == true)
                {
                    blPicboxMic = false;
                    this.picBoxMic.BackgroundImage = Yoki.View.Properties.Resources.control_disable_mic;
                }
                else
                {
                    blPicboxMic = true;
                    this.picBoxMic.BackgroundImage = Yoki.View.Properties.Resources.control_mic;
                }
            }
        }

        private void picBoxSpeaker_Click(object sender, EventArgs e)
        {
            if (PicboxSpeakerClicked != null)
            {
                PicboxSpeakerClicked(sender, new EventArgs());//把按钮自身作为参数传递
                if (blPicboxSpeaker == true)
                {
                    blPicboxSpeaker = false;
                    this.picBoxSpeaker.BackgroundImage = Yoki.View.Properties.Resources.control_disable_speaker;
                }
                else
                {
                    blPicboxSpeaker = true;
                    this.picBoxSpeaker.BackgroundImage = Yoki.View.Properties.Resources.control_speaker;
                }
            }
        }
        #endregion 自定义控件事件
        public ucVideoControl()
        {
            InitializeComponent();
            //初始化
            this.MinimumSize =new Size(102, 39);
            this.MaximumSize = new Size(102, 39);
            this.BackColor = Color.Transparent;
            this.picBoxCamera.BackgroundImage = Yoki.View.Properties.Resources.control_camera;
            this.picBoxCamera.BackgroundImageLayout = ImageLayout.Center;
            this.picBoxCamera.BackColor = Color.Transparent;
            this.picBoxMic.BackgroundImage = Yoki.View.Properties.Resources.control_mic;
            this.picBoxMic.BackgroundImageLayout = ImageLayout.Center;
            this.picBoxMic.BackColor = Color.Transparent;
            this.picBoxSpeaker.BackgroundImage = Yoki.View.Properties.Resources.control_speaker;
            this.picBoxSpeaker.BackgroundImageLayout = ImageLayout.Center;
            this.picBoxSpeaker.BackColor = Color.Transparent;
            blPicboxCamera = true;
            blPicboxMic = true;
            blPicboxSpeaker = true;
        }
        
    }
}
