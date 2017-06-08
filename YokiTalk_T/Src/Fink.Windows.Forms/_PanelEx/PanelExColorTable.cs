using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using Fink.Core;

namespace Fink.Windows.Forms
{
    public class PanelExColorTable : ColorTable
    {
        public PanelExColorTable()
            : base()
        {
            this.BackColor = Color.FromArgb(235, 236, 239);
            this.Border = Color.FromArgb(250, 250, 250);

            this.Foreground = Color.FromArgb(250, 250, 250);
            this.HighLight = Color.FromArgb(255, 255, 255);
            this.Shadow = Color.FromArgb(0, 0, 0);
        }

        

    }
}
