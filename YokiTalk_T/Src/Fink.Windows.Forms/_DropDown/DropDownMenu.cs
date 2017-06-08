using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Fink.Windows.Forms
{
    public class DropDownMenu : ToolStripDropDown
    {
        private Control Target { get; set; }
        private DropDownMenuHost Host { get; set; }
        
        /// <summary>
        /// Regex pattern for serach fragment around caret
        /// </summary>
        [Description("Regex pattern for serach fragment around caret")]
        [DefaultValue(@"[\w\.]")]
        public string SearchPattern { get; set; }
        
        public DropDownMenu()
        {
            //AutoClose = true;
            //AutoSize = false;
            //Margin = Padding.Empty;
            //Padding = Padding.Empty;
            //DropShadowEnabled = true;
        }


        public DropDownMenu(Control target)
            : this()
        {
            this.Target = target;
            this.Target.Location = new Point(1, 0);

            System.Windows.Forms.Panel p = new Panel();
            p.BackColor = this.BackColor; //System.Drawing.Color.FromArgb(255, 192, 194, 204);
            p.Size = new System.Drawing.Size(this.Target.Width + 2, this.Target.Height + 2);
            p.Controls.Add(this.Target);

            this.Host = new DropDownMenuHost(p);
            this.Items.Add(this.Host);
            this.Size = p.Size;
        }

        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.ClipRectangle);
            base.OnPaint(e);
        }
        
        private void ShowMenu()
        {
            if (!Host.Visible)
            {
                var args = new CancelEventArgs();
                OnOpening(args);
                if (!args.Cancel)
                {
                    //calc screen point for popup menu
                    //point.Offset(2, TargetControlWrapper.TargetControl.Height + 2);
                    //point = TargetControlWrapper.GetPositionFromCharIndex(Fragment.Start);
                    //point.Offset(2, TargetControlWrapper.TargetControl.Font.Height + 2);
                    //
                    //Point point = TargetControlWrapper.TargetControl.Location;
                    //Host.Show(TargetControlWrapper.TargetControl, new Point(Math.Min(targetControlWrapper.TargetControl.Width - Host.Width, 0), TargetControlWrapper.TargetControl.Height));
                    //if (CaptureFocus)
                    //{
                    //    //(Host.ListView  as Control).Focus();
                    //    //ProcessKey((char) Keys.Down, Keys.None);
                    //}
                }
            }
            else
            {
            }
                //(Host.ListView as Control).Invalidate();
        }

    }
}
