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
    public partial class ucLeftMenu : System.Windows.Forms.UserControl
    {
        #region 控件缩放
        double formWidth;//窗体原始宽度
        double formHeight;//窗体原始高度
        double scaleX;//水平缩放比例
        double scaleY;//垂直缩放比例
        Dictionary<string, string> ControlsInfo = new Dictionary<string, string>();//控件中心Left,Top,控件Width,控件Height,控件字体Size

        #endregion
        #region 自定义控件属性
        private bool _blMenu1 = false;
        private bool _blMenu2 = false;
        private bool _blMenu3 = false;
        /// <summary>
        /// 菜单按钮1是否选中的状态
        /// </summary>
        [Description("菜单按钮1选中状态设置")]
        public bool BlMenu1
        {
            get
            {
                return _blMenu1;
            }

            set
            {
                _blMenu1 = value;
                if (_blMenu1 == true)
                {
                    this.picBoxMenu1.BackgroundImage = Yoki.Controls.ResourceHelper.ClassRoomSelected;
                }
                else
                {
                    this.picBoxMenu1.BackgroundImage = Yoki.Controls.ResourceHelper.ClassRoomNo;
                }
            }
        }
        /// <summary>
        /// 菜单按钮2是否选中的状态
        /// </summary>
        [Description("菜单按钮2选中状态设置")]
        public bool BlMenu2
        {
            get
            {
                return _blMenu2;
            }

            set
            {
                _blMenu2 = value;
                if (_blMenu2 == true)
                {
                    this.picBoxMenu2.BackgroundImage = Yoki.Controls.ResourceHelper.ClassRoomSelected;
                }
                else
                {
                    this.picBoxMenu2.BackgroundImage = Yoki.Controls.ResourceHelper.ClassRoomNo;
                }
            }
        }
        /// <summary>
        /// 菜单按钮3是否选中的状态
        /// </summary>
        [Description("菜单按钮3选中状态设置")]
        public bool BlMenu3
        {
            get
            {
                return _blMenu3;
            }

            set
            {
                _blMenu3 = value;
            }
        }
        #endregion 自定义控件属性
        #region 自定义控件事件
        //定义委托
        public delegate void ucLeftMenu1Click(object sender, EventArgs e);
        public delegate void ucLeftMenu2Click(object sender, EventArgs e);
        //定义事件
        [Description("点击菜单按钮1发生的事件")]
        public event ucLeftMenu1Click LeftMenu1Clicked;
        [Description("点击菜单按钮2发生的事件")]
        public event ucLeftMenu2Click LeftMenu2Clicked;
        #endregion 自定义控件事件
        public ucLeftMenu()
        {
            InitializeComponent();
            this.Size = new Size(128, 463);
            //this.MaximumSize=new Size(128, 463);
            this.BackColor = Color.FromArgb(255, 240, 240, 240);
            this.lbl1.Location = new Point(0, 0);
            this.lbl1.BackColor = Color.FromArgb(255, 217, 217, 217);

            this.panel1.Location = new Point(0, 0);
            this.panel1.BackColor = Color.FromArgb(255, 240, 240, 240);

            this.picBoxMenu1.Location = new Point(0, 8);
            this.picBoxMenu1.Size = new Size(128, 98);
            this.picBoxMenu1.BackgroundImage = Yoki.Controls.ResourceHelper.ClassRoomNo;
            this.picBoxMenu1.BackgroundImageLayout = ImageLayout.Stretch;
            this.picBoxMenu2.Location = new Point(0, 106);
            this.picBoxMenu2.Size = new Size(128, 98);
            this.picBoxMenu2.BackgroundImage = Yoki.Controls.ResourceHelper.ClassRoomNo;
            this.picBoxMenu2.BackgroundImageLayout = ImageLayout.Stretch;
            this.picBoxMenu3.Location = new Point(0, 204);
            this.picBoxMenu3.Size = new Size(128, 98);
            this.picBoxMenu3.BackgroundImage = null;
            this.picBoxMenu3.BackgroundImageLayout = ImageLayout.Stretch;
            this.lbl2.Location = new Point(0, 361);
            this.lbl2.BackColor = Color.FromArgb(255, 217, 217, 217);
            this.picBoxSetting.Location = new Point(0, 369);
            this.picBoxSetting.BackgroundImage = Yoki.Controls.ResourceHelper.MenuSettings;
            this.picBoxSetting.BackgroundImageLayout = ImageLayout.Stretch;
            #region 窗体缩放
            GetAllInitInfo(this.Controls[0]);
            #endregion
        }
        
        private void picBoxMenu1_MouseEnter(object sender, EventArgs e)
        {
            this.picBoxMenu1.BackgroundImage = Yoki.Controls.ResourceHelper.ClassRoomSelected;
        }

        private void picBoxMenu1_MouseLeave(object sender, EventArgs e)
        {
            if (BlMenu1 == true)
            {
                this.picBoxMenu1.BackgroundImage = Yoki.Controls.ResourceHelper.ClassRoomSelected;
            }
            else
            {
                this.picBoxMenu1.BackgroundImage = Yoki.Controls.ResourceHelper.ClassRoomNo;
            }
        }

        private void picBoxMenu2_MouseEnter(object sender, EventArgs e)
        {
            this.picBoxMenu2.BackgroundImage = Yoki.Controls.ResourceHelper.ClassRoomSelected;
        }

        private void picBoxMenu2_MouseLeave(object sender, EventArgs e)
        {
            if (BlMenu2 == true)
            {
                this.picBoxMenu2.BackgroundImage = Yoki.Controls.ResourceHelper.ClassRoomSelected;
            }
            else
            {
                this.picBoxMenu2.BackgroundImage = Yoki.Controls.ResourceHelper.ClassRoomNo;
            }
        }

        private void picBoxMenu1_Click(object sender, EventArgs e)
        {
            if (LeftMenu1Clicked != null)
            {
                LeftMenu1Clicked(sender, new EventArgs());//把按钮自身作为参数传递
                BlMenu1 = true;
                BlMenu2 = false;
                BlMenu3 = false;
            }
        }

        private void picBoxMenu2_Click(object sender, EventArgs e)
        {
            if (LeftMenu2Clicked != null)
            {
                LeftMenu2Clicked(sender, new EventArgs());//把按钮自身作为参数传递
                BlMenu1 = false;
                BlMenu2 = true;
                BlMenu3 = false;
            }
        }
        /// <summary>
        /// 获取控件初始信息
        /// </summary>
        /// <param name="ctrlContainer"></param>
        protected void GetAllInitInfo(Control ctrlContainer)
        {
            if (ctrlContainer.Parent == this)//获取窗体的高度和宽度
            {
                formWidth = Convert.ToDouble(ctrlContainer.Width);
                formHeight = Convert.ToDouble(ctrlContainer.Height);
            }
            foreach (Control item in ctrlContainer.Controls)
            {
                if (item.Name.Trim() != "")
                {
                    //添加信息：键值：控件名，内容：据左边距离，距顶部距离，控件宽度，控件高度，控件字体。
                    ControlsInfo.Add(item.Name, (item.Left + item.Width / 2) + "," + (item.Top + item.Height / 2) + "," + item.Width + "," + item.Height + "," + item.Font.Size);
                }
                if ((item as System.Windows.Forms.UserControl) == null && item.Controls.Count > 0)
                {
                    GetAllInitInfo(item);
                }
            }

        }
        /// <summary>
        /// 获取窗体缩放比例
        /// </summary>
        /// <param name="ctrlContainer"></param>
        private void ControlsChangeInit(Control ctrlContainer)
        {
            scaleX = (Convert.ToDouble(ctrlContainer.Width) / formWidth);
            scaleY = (Convert.ToDouble(ctrlContainer.Height) / formHeight);
        }
        /// <summary>
        /// 改变控件大小
        /// </summary>
        /// <param name="ctrlContainer"></param>
        private void ControlsChange(Control ctrlContainer)
        {
            double[] pos = new double[5];//pos数组保存当前控件中心Left,Top,控件Width,控件Height,控件字体Size
            foreach (Control item in ctrlContainer.Controls)//遍历控件
            {
                if (item.Name.Trim() != "")//如果控件名不是空，则执行
                {
                    if ((item as System.Windows.Forms.UserControl) == null && item.Controls.Count > 0)//如果不是自定义控件
                    {
                        ControlsChange(item);//循环执行
                    }
                    string[] strs = ControlsInfo[item.Name].Split(',');//从字典中查出的数据，以‘，’分割成字符串组

                    for (int i = 0; i < 5; i++)
                    {
                        pos[i] = Convert.ToDouble(strs[i]);//添加到临时数组
                    }
                    double itemWidth = pos[2] * scaleX;     //计算控件宽度，double类型
                    double itemHeight = pos[3] * scaleY;    //计算控件高度
                    item.Left = Convert.ToInt32(pos[0] * scaleX - itemWidth / 2);//计算控件距离左边距离
                    item.Top = Convert.ToInt32(pos[1] * scaleY - itemHeight / 2);//计算控件距离顶部距离
                    item.Width = Convert.ToInt32(itemWidth);//控件宽度，int类型
                    item.Height = Convert.ToInt32(itemHeight);//控件高度
                    item.Font = new Font(item.Font.Name, float.Parse((pos[4] * Math.Min(scaleX, scaleY)).ToString()));//字体

                }
            }

        }

        private void ucLeftMenu_SizeChanged(object sender, EventArgs e)
        {
            if (ControlsInfo.Count > 0)//如果字典中有数据，即窗体改变
            {
                ControlsChangeInit(this.Controls[0]);//表示pannel控件
                ControlsChange(this.Controls[0]);

            }
        }
    }
}
