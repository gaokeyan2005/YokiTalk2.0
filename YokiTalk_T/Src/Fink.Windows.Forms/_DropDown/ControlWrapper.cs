using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    class ControlWrapper: IWrapper
    {
        private Control target;
        private ControlWrapper(Control target)
        {
            this.target = target;
        }

        public static ControlWrapper Create(Control targetControl)
        {
            var result = new ControlWrapper(targetControl);
            return result;
        }

        public System.Windows.Forms.Control TargetControl
        {
            get { throw new NotImplementedException(); }
        }

#pragma warning disable CS0067 // The event 'ControlWrapper.LostFocus' is never used
        public event EventHandler LostFocus;
#pragma warning restore CS0067 // The event 'ControlWrapper.LostFocus' is never used

#pragma warning disable CS0067 // The event 'ControlWrapper.KeyDown' is never used
        public event System.Windows.Forms.KeyEventHandler KeyDown;
#pragma warning restore CS0067 // The event 'ControlWrapper.KeyDown' is never used

#pragma warning disable CS0067 // The event 'ControlWrapper.MouseDown' is never used
        public event System.Windows.Forms.MouseEventHandler MouseDown;
#pragma warning restore CS0067 // The event 'ControlWrapper.MouseDown' is never used


    }
}
