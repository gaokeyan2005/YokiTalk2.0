using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fink.Drawing
{
    public enum RoundStyle
    {
        None = 0,
        LeftTop = 1,
        RightTop = 2,
        LeftBottom = 4,
        RightBottom = 8,
        
        All = LeftTop | RightTop | LeftBottom | RightBottom

    }
}
