using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    /// <summary>
    /// Wrapper over the control like TextBox.
    /// </summary>
    public interface IWrapper
    {
        Control TargetControl { get; }
        event EventHandler LostFocus;
        event KeyEventHandler KeyDown;
        event MouseEventHandler MouseDown;
    }
}
