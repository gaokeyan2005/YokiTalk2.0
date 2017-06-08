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
    public partial class ucEvaluateCell : System.Windows.Forms.UserControl
    {
        public EventHandler OnResized;
        public event EventHandler<SelectedEventArgs> OnSelectedChanged;
        private string[] strs = new string[Partial.StarList.DefaultCount];


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] Strs
        {
            get
            {
                return this.strs;
            }
            set
            {
                this.strs = value;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Key
        {
            get;
            set;
        }

        public const string emptyStr = "Please select a score for this category.";
        public ucEvaluateCell()
        {
            InitializeComponent();

            this.starList.OnPreviewChanged += (o, e) =>
            {
                this.Content = this.starList.PreviewIndex < 0 ? string.Empty : strs[Math.Min(Math.Max(this.starList.PreviewIndex, 0), Partial.StarList.DefaultCount - 1)];
            };
            this.starList.OnSelectedChanged += (o, e) =>
            {
                this.Content = this.starList.SelectedIndex < 0 ? emptyStr : strs[Math.Min(Math.Max(this.starList.SelectedIndex, 0), Partial.StarList.DefaultCount - 1)];
                this.lblRate.Text = this.starList.SelectedIndex < 0 ? " " : this.starList.SelectedIndex  + 1 + "/" + Partial.StarList.DefaultCount;
if (this.OnSelectedChanged != null)
                {
                    
                    this.OnSelectedChanged(this, new SelectedEventArgs() { Key = this.Key, SelectedIndex = this.starList.SelectedIndex });
                }
            };

            this.txtContent.AutoSize = false;
            this.Content = emptyStr;
            ReSizeAll();
        }
        


        private bool isFirstRender = true;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (isFirstRender)
            {
                ReSizeAll();
                isFirstRender = false;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Title
        {
            get
            {
                return this.lblCategory.Text;
            }
            set
            {
                this.lblCategory.Text = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Content
        {
            get
            {
                return this.txtContent.Text;
            }
            private set
            {
                this.txtContent.Text = value;
                ReSizeAll();
            }
        }


        private bool isFirst = false;
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(bool), "false")]
        public bool IsFrist
        {
            get{
                return this.isFirst;
            }
            set
            {
                this.isFirst = value;
                ReSizeAll();
            }
        }

        private const int margin = 5;
        private const int left = 32;
        public void ReSizeAll()
        {
            if (this.ClientRectangle.Width <= 0 )
            {
                return;
            }

            this.pnlBase.Width = this.ClientRectangle.Width - margin * 2;

            this.txtContent.MinimumSize = new Size(this.pnlBase.Width - left, 24);
            this.txtContent.MaximumSize = new Size(this.pnlBase.Width - left, int.MaxValue);

            this.txtContent.Location = new Point(left, this.txtContent.Location.Y);

            this.txtContent.Height = 48;

            this.pnlBase.Height = this.txtContent.Top + this.txtContent.Height + margin * 2;

            this.pnlBase.Location = new Point(margin, IsFrist ? margin : 0);

            this.Height = this.pnlBase.Height + margin * (IsFrist ? 2 : 1);

            if (this.OnResized != null)
            {
                this.OnResized(this, new EventArgs());
            }
        }
    }


    public class SelectedEventArgs : EventArgs
    {
        public int Key;
        public int SelectedIndex;
    }

}
