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
    public partial class ucReload : System.Windows.Forms.UserControl
    {
        //定义委托
        public delegate void ucReloadClick(object sender, EventArgs e);
        //定义事件
        public event ucReloadClick ImageClicked;
        private void picBox_Click(object sender, EventArgs e)
        {
            if (ImageClicked != null)
                ImageClicked(sender, new EventArgs());//把按钮自身作为参数传递
        }
        public ucReload()
        {
            InitializeComponent();
        }

        private void ucReload_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;
            this.picBox.BackColor = Color.Transparent;
            this.picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.picBox.Image = global::Yoki.View.Properties.Resources.refresh_up;
            this.picBox.Location = new Point(0, 0);
            this.picBox.Width = this.Width;
            this.picBox.Height = this.Height;
        }

        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            this.picBox.Image = global::Yoki.View.Properties.Resources.refresh_down;
        }

        private void picBox_MouseUp(object sender, MouseEventArgs e)
        {
            this.picBox.Image = global::Yoki.View.Properties.Resources.refresh_down;
        }

        private void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            this.picBox.Image = global::Yoki.View.Properties.Resources.refresh_up;
        }

        private void picBox_MouseLeave(object sender, EventArgs e)
        {
            this.picBox.Image = global::Yoki.View.Properties.Resources.refresh_up;
        }
    }
}
