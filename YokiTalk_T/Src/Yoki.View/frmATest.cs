using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yoki.View
{
    public partial class frmATest : Fink.Windows.Forms.FormEx
    {
        public frmATest(Fink.Windows.Forms.FromStyle fs = Fink.Windows.Forms.FromStyle.Metro)
            : base(fs)
        {
            InitializeComponent();
        }

        private void frmATest_Load(object sender, EventArgs e)
        {
            //this.classRoom1.LabPage.Text = "2/8";
            string[] Links = { "http://admin.chaojiwaijiao.com/upload/special_topics/2016/12/19/161219144648P276645V62.jpg", "http://admin.chaojiwaijiao.com/upload/special_topics/2016/12/19/161219144648P556303V58.jpg", "http://admin.chaojiwaijiao.com/upload/special_topics/2016/12/19/161219144648P529473V11.jpg" };
            //this.classRoom1.ImageViewer.Links = Links;
            //classRoom1.ImageList = this.imageListVideoControl;
            //Fink.Windows.Forms.SwitchCapsuleCheckedArgs aa =new Fink.Windows.Forms.SwitchCapsuleCheckedArgs();
            //this.ucReload1.ImageClicked += UcReload1_ImageClicked;
            //this.classRoom1.ImageViewer.SizeMode = PictureBoxSizeMode.StretchImage;
            //pictureBox1.ImageLocation = "http://admin.chaojiwaijiao.com/upload/special_topics/2016/12/19/161219144648P276645V62.jpg";

            ucVideoControl1.PicboxCameraClicked += UcVideoControl1_PicboxCameraClicked;
            ucVideoControl1.BoardColor = Color.Blue;
            ucVideoControl1.ValueCamera = false;
            ucVideoControl1.ValueMic = true;
            ucVideoControl1.ValueSpeaker = false;

            ucVideoControl2.ValueMic = false;
            ucVideoControl2.PicboxCameraClicked += UcVideoControl2_PicboxCameraClicked;
        }

        private void UcVideoControl2_PicboxCameraClicked(object sender, EventArgs e)
        {
            MessageBox.Show(ucVideoControl2.ValueCamera.ToString());
        }

        private void UcVideoControl1_PicboxCameraClicked(object sender, EventArgs e)
        {
            MessageBox.Show(ucVideoControl1.ValueCamera.ToString());
            //ucVideoControl1.ValueCamera = true;
        }

        private void UcReload1_ImageClicked(object sender, EventArgs e)
        {
            MessageBox.Show ("1");
        }

        private void TabControl_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //classRoom1.ImageList = this.imageListVideoControl;
        }

        private void videoControlPanel1_Click(object sender, EventArgs e)
        {
            //classRoom1.ImageList = this.imageList1;
        }

        private void ucLeftMenu1_LeftMenu1Clicked(object sender, EventArgs e)
        {

        }

        private void ucLeftMenu1_LeftMenu2Clicked(object sender, EventArgs e)
        {

        }

        private void ucChangeName1_OkClicked(object sender, EventArgs e)
        {
            MessageBox.Show(ucChangeName1.NewText);
        }
    }
}
