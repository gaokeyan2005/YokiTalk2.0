using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Yoki.Controls
{
    public class WaitingRoom: Fink.Windows.Forms.PanelEx
    {
        public Yoki.Core.DataEventHandle<bool> OnSwitchChanged;

        private WaitOrderAnimatinPanel waitPanel = null;
        private Fink.Windows.Forms.ToggleButtonEx switchStatus = null;

        public bool IsWaiting
        {
            get
            {
                return this.waitPanel.IsWaiting;
            }
            set
            {
                if (this.waitPanel != null)
                {
                    this.waitPanel.IsWaiting = value;
                }
            }
        }

        public void ChangeStatus(bool isChecked)
        {

            this.switchStatus.Checked = isChecked;
        }

        public WaitingRoom() : base()
        {
            InitializeComponent();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Rearray();
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {
                base.Font = value;
                this.switchStatus.Font = base.Font;
            }
        }

        private void InitializeComponent()
        {
            this.switchStatus = new Fink.Windows.Forms.ToggleButtonEx();
            this.switchStatus.Font = this.Font;
            this.waitPanel = new Yoki.Controls.WaitOrderAnimatinPanel();

            this.SuspendLayout();
            // 
            // switchStatus
            // 
            this.switchStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.switchStatus.BackColor = System.Drawing.Color.White;
            this.switchStatus.Location = new System.Drawing.Point(0, 0);
            this.switchStatus.Name = "switchStatus";
            this.switchStatus.Checked = true;
            this.switchStatus.Size = new System.Drawing.Size(60, 30);
            this.switchStatus.CheckedChanged += (o, e) =>
             {
                 this.IsWaiting = !this.IsWaiting;
                 if (this.OnSwitchChanged != null)
                 {
                     this.OnSwitchChanged(this.IsWaiting);
                 }
             };

            this.waitPanel.BackColor = Color.FromArgb(255, 245, 245, 245);
            this.waitPanel.Size = new Size(0, 0);
            this.waitPanel.Location = new Point(0, 39);
            this.waitPanel.IsWaiting = true;

            this.Controls.Add(this.switchStatus);
            this.Controls.Add(this.waitPanel);


            this.ResumeLayout();
        }


        private static int SwitchButtonAreaHeight = 38;
        private void Rearray()
        {
            if (this.Width <= 0 || this.Height <= 0)
            {
                return;
            }
            this.SuspendLayout();


            this.waitPanel.Size = new Size(this.Width, this.Height - SwitchButtonAreaHeight);
            this.waitPanel.Location = new Point(0, SwitchButtonAreaHeight);

            this.switchStatus.Location = new Point(this.Width - this.switchStatus.Width, (SwitchButtonAreaHeight - this.switchStatus.Height) / 2);

            this.ResumeLayout();
        }
    }
}
