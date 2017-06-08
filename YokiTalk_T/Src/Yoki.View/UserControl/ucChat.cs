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
    public partial class ucChat : System.Windows.Forms.UserControl
    {
        public System.Windows.Forms.Integration.ElementHost SurfaceCourseware;
        public IM.View.PictureBox SurfaceRemote;
        public IM.View.PictureBox SurfaceLocal;

        public libBook LibBook;



        public event EventHandler<HardwareSwitchEventArgs> OnCameraSwitched;
        public event EventHandler<HardwareSwitchEventArgs> OnMicSwitched;
        public event EventHandler<HardwareSwitchEventArgs> OnSpeakerSwitched;

        public ucChat()
        {
            InitializeComponent();
            this.SurfaceCourseware = this.hostCourseware;
            this.LibBook = this.bookCourseware;
            this.SurfaceRemote = this.ucVideoChat.SurfaceRemote;
            this.SurfaceLocal = this.ucVideoChat.SurfaceLocal;

            this.SizeChanged += UCChat_SizeChanged;
            this.Load += (o, e) =>
            {
                ReArray();
            };


            this.ucVideoChat.OnCameraSwitched += (o, e) =>
            {
                if (this.OnCameraSwitched != null)
                {
                    this.OnCameraSwitched(o, e);
                }
            };
            this.ucVideoChat.OnMicSwitched += (o, e) =>
            {
                if (this.OnMicSwitched != null)
                {
                    this.OnMicSwitched(o, e);
                }
            };
            this.ucVideoChat.OnSpeakerSwitched += (o, e) =>
            {
                if (this.OnSpeakerSwitched != null)
                {
                    this.OnSpeakerSwitched(o, e);
                }
            };

        }


        public StuInfoViewModel InfoModel
        {
            get
            {
                return this.ucVideoChat.InfoModel;
            }
            set
            {
                this.ucVideoChat.InfoModel = value;
            }
        }

        void UCChat_SizeChanged(object sender, EventArgs e)
        {
            ReArray();
        }

        //private const int btnAreaHeight = 80;
        private void ReArray()
        {
            Size rectSize = this.ClientSize;

            int videoLen = rectSize.Height / 2;

            this.ucVideoChat.Width = videoLen;
            this.ucVideoChat.Height = this.ClientSize.Height;
            this.ucVideoChat.Location = new Point(this.ClientSize.Width - this.ucVideoChat.Width, 0);


            this.hostCourseware.Width = this.ClientSize.Width - videoLen;
            this.hostCourseware.Height = this.ClientSize.Height;

        }
        
    }
}
