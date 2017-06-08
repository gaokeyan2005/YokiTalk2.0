using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    public class WrapperEventArgs : EventArgs
    {
        public Control TargetControl { get; private set; }
        public IWrapper Wrapper { get; set; }

        public WrapperEventArgs(Control targetControl)
        {
            this.TargetControl = targetControl;
        }
    }
}
