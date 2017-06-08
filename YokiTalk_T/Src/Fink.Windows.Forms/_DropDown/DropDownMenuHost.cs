using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace Fink.Windows.Forms
{
    [ProvideProperty("DropDownMenu", typeof(Control))]
    public class DropDownMenuHost : ToolStripControlHost
    {
        public DropDownMenuHost(Control target): base(target)
        {
            Overflow = ToolStripItemOverflow.Always;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }


    }
}
