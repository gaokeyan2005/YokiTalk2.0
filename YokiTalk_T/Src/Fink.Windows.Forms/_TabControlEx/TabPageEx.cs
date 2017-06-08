using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace Fink.Windows.Forms
{

    internal class TabPageExDesigner : ParentControlDesignerEx
    {
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            var page = this.Control as TabPageEx;
            EnableDesignMode(page, page.Name);
        }
    }
    [Designer(typeof(TabPageExDesigner))]
    public class TabPageEx : TabPage
    {
        public TabPageEx(): base()
        {
            this.BackColor = Color.Transparent;

            SetStyle(ControlStyles.DoubleBuffer
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }

        private bool _canClose = false;
        [DefaultValue(false)]
        public bool CanClose
        {
            get
            {
                return this._canClose;
            }
            set
            {
                this._canClose = value;
            }
        }


    }
}
