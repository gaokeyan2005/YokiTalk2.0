using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fink.Windows.Forms
{
    public class CheckedEventArgs : EventArgs
    {
        public bool Checked
        {
            get;
            set;
        }
    }
}
