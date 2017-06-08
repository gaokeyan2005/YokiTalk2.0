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
    public partial class ucVideoChat : System.Windows.Forms.UserControl
    {
        public IM.View.PictureBox SurfaceRemote;
        public IM.View.PictureBox SurfaceLocal;

        public event EventHandler<HardwareSwitchEventArgs> OnCameraSwitched;
        public event EventHandler<HardwareSwitchEventArgs> OnMicSwitched;
        public event EventHandler<HardwareSwitchEventArgs> OnSpeakerSwitched;

        public ucVideoChat()
        {
            InitializeComponent();
            this.SurfaceRemote = this.pbRemote;
            this.SurfaceLocal = this.pbLocal;

            this.SizeChanged += (o, e) =>
            {
                ReArray();
            };

            this.Load += (o, e) =>
            {
                ReArray();
            };

            this.switchControls.SwitchCapsuleCheckedChanged += (o, e) =>
            {
                switch (e.SourceIndex)
                {
                    case 0:
                        if (this.OnCameraSwitched != null)
                        {
                            this.OnCameraSwitched(this, new HardwareSwitchEventArgs() { IsOpen = !e.IsChecked });
                        }
                        break;
                    case 1:
                        if (this.OnMicSwitched != null)
                        {
                            this.OnMicSwitched(this, new HardwareSwitchEventArgs() { IsOpen = !e.IsChecked });
                        }
                        break;
                    case 2:
                        if (this.OnSpeakerSwitched != null)
                        {
                            this.OnSpeakerSwitched(this, new HardwareSwitchEventArgs() { IsOpen = !e.IsChecked });
                        }
                        break;
                    default:
                        break;
                }
            };
        }


        public StuInfoViewModel InfoModel
        {
            get
            {
                return this.ucStuInfo.InfoModel;
            }
            set
            {
                this.ucStuInfo.InfoModel = value;
            }
        }

        int margin = 5;
        private void ReArray()
        {
            Size rectSize = this.ClientSize;

            int videoLen = rectSize.Width - margin * 2;

            this.pnlVideoBase.Width = videoLen;
            this.pnlVideoBase.Height = videoLen;
            this.pnlVideoBase.Location = new Point(margin, 0);


            this.pbRemote.Width = videoLen;
            this.pbRemote.Height = videoLen;
            this.pbRemote.Location = new Point(0, 0);
            this.pbRemote.Invalidate();


            this.pbLocal.Location = new Point(this.pbRemote.Location.X + this.pbRemote.Width - this.pbLocal.Width
                , this.pbRemote.Location.Y + this.pbRemote.Height - this.pbLocal.Height);

            this.switchControls.Top = videoLen + margin * 2;
            this.switchControls.Size = new Size(100, 36);

            int hostLen = Math.Min(rectSize.Height - this.switchControls.Top - this.switchControls.Size.Height - 28 - margin * 2
                ,rectSize.Width - margin * 2);

            this.hostStuInfo.Size = new Size(hostLen, hostLen);

            this.hostStuInfo.Location = new Point((rectSize.Width - hostLen) / 2
               , rectSize.Height - this.switchControls.Top + this.switchControls.Size.Height + margin * 2);

        }
    }
    public class HardwareSwitchEventArgs: EventArgs{
        public bool IsOpen{
            get;
            internal set;
        }
    }
}
