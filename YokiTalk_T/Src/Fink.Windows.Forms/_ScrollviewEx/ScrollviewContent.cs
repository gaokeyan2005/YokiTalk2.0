using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    internal class ScrollviewContent: System.Windows.Forms.ScrollableControl
    {

        private System.Windows.Forms.Panel _container;
        public ScrollviewContent() : base()
        {
            base.SetStyle(
                   ControlStyles.UserPaint |
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.ResizeRedraw |
                   ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
            this.DoubleBuffered = true;

            _container = new Panel();
            this.Controls.Add(this._container);

        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            // 0x115 and 0x20a both tell the control to scroll. If either one comes 
            // through, you can handle the scrolling before any repaints take place
            if (m.Msg == 0x115 || m.Msg == 0x20a)
            {
                //Do you scroll processing
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (this._child != null)
            {
                this._container.Controls.Remove(this.Child);
                this.Child.Dispose();
            }
            this._container.Dispose();
            base.Dispose(disposing);   
        }

        private Control _child;
        public Control Child
        {
            get { return _child; }
            set 
            {
                OnChildChanged(this._child, value);
                _child = value;
            }
        }

        private void OnChildChanged(Control oldCtrl, Control newCtrl)
        {
            if (oldCtrl != null)
            {
                oldCtrl.SizeChanged -= ChildCtrl_SizeChanged;
                this._container.Controls.Remove(newCtrl);
                oldCtrl.Dispose();
            }

            if (newCtrl != null)
            {
                this._container.Controls.Add(newCtrl);
                this._container.Size = newCtrl.Size;
                newCtrl.SizeChanged += ChildCtrl_SizeChanged;
            }
        }

        public void ScrollTo(int y)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (this._container != null)
                {
                    this._container.Location = new Point(0, 0 - y);
                }
            });
        }

        void ChildCtrl_SizeChanged(object sender, EventArgs e)
        {
            this._container.Size = this.Child.Size;
        }

        

        protected override void OnScroll(System.Windows.Forms.ScrollEventArgs se)
        {
            //base.OnScroll(se);

        }
    }


}
