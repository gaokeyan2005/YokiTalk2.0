using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{

    internal class ScrollviewExDesigner : System.Windows.Forms.Design.ControlDesigner
    {
#pragma warning disable CS0414 // The field 'ScrollviewExDesigner.innerControl' is assigned but its value is never used
        private Control innerControl = null;   
#pragma warning restore CS0414 // The field 'ScrollviewExDesigner.innerControl' is assigned but its value is never used
        public ScrollviewExDesigner()
            : base()
        {
            this.AutoResizeHandles = true;


            //DesignerVerb verb1 = new DesignerVerb("Edit Ttems", new
            //EventHandler(OnEdit));
            //VerbCollection.AddRange(new DesignerVerb[] { verb1 });
        }

        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);

        }





    }

    [Designer(typeof(ScrollviewExDesigner))]
    public class ScrollviewEx: System.Windows.Forms.Control
    {
        public event EventHandler OnScrollContentSizeChanged;

        private ScrollBarEx _bar;
        private ScrollviewContent _view;

        internal ScrollviewContent View
        {
            get
            {
                return this._view;
            }
        }

        public ScrollviewEx()
            : base()
        {
            base.SetStyle(
                   ControlStyles.UserPaint |
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.ResizeRedraw |
                   ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
            this.BackColor = Color.FromArgb(255, 192, 194, 204);

            Init();
            this.SizeChanged += ScrollviewEx_SizeChanged;
        }

        void ScrollviewEx_SizeChanged(object sender, EventArgs e)
        {
            this._bar.Size = new System.Drawing.Size(22, this.ClientRectangle.Height);
            this._bar.Location = new System.Drawing.Point(this.ClientRectangle.Width - 22, 0);

            this._view.Size = new System.Drawing.Size(this.ClientRectangle.Width - 23, this.ClientRectangle.Height);
            this._view.Location = new System.Drawing.Point(0, 0);

            OnSizeChanged();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            this.Value -= e.Delta / 5;
        }

        public Control Child
        {
            get { return this._view.Child; }
            set
            {
                this._view.Child = value;
                this._view.Child.SizeChanged += (o, e) =>
                {
                    OnSizeChanged();
                };
                OnSizeChanged();
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Value
        {
            get
            {
                return this._bar.Value;
            }
            set
            {
                this._bar.Value = value;
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            OnSizeChanged();
        }

        void Child_SizeChanged(object sender, EventArgs e)
        {
            OnSizeChanged();
        }


        private void Init()
        {
            this._bar = new ScrollBarEx();
            this._bar.ScrollValueChanged += _bar_ScrollValueChanged;
            this.Controls.Add(this._bar);
            this._view = new ScrollviewContent();
            this._view.BackColor = this.BackColor;
            this._view.AutoScroll = false;
            this._view.VerticalScroll.Visible = false;
            this._view.AutoScrollPosition = new Point(0, 0);
            this.Controls.Add(this._view);
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }

            set
            {
                base.BackColor = value;
                if (this._view != null)
                {
                    this._view.BackColor = base.BackColor;
                }
            }
        }

        void _bar_ScrollValueChanged(object sender, ScrollEventArgs e)
        {
            this._view.ScrollTo(e.Value);
        }
        
        private void OnSizeChanged()
        {
            if (this._view.Child!=null)
            {
                this._view.Child.Width = this._view.Width;
                int maxY = this._view.Child.ClientRectangle.Top + this._view.Child.ClientRectangle.Height;

                this._view.VerticalScroll.Maximum = maxY;
                this._bar.MaxValue = maxY;
                this._bar.ViewportValue = this.ClientRectangle.Height;
                if (this._view.Handle != null && this._bar.ViewportValue > this._view.Child.Height)
                {
                    this._bar.Value = 0;
                }
                if (this.OnScrollContentSizeChanged != null)
                {
                    this.OnScrollContentSizeChanged(this, new EventArgs());
                }
            }
        }

    }
}
