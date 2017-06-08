using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fink.Windows.Forms
{
    internal enum ControlState
    {
        /// <summary>
        ///  正常。
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 鼠标进入。
        /// </summary>
        Hover = 2,
        /// <summary>
        /// 鼠标按下。
        /// </summary>
        Pressed = 4,
        /// <summary>
        /// 获得焦点。
        /// </summary>
        Focused = 8,

        Selected = 16
    }
}
