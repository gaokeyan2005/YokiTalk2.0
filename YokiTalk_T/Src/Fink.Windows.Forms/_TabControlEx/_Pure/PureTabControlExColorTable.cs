using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Fink.Windows.Forms
{
    public class PureTabControlExColorTable : TabControlExColorTable
    {
        public PureTabControlExColorTable()
        {
            this.BaseColor = Color.FromArgb(255, 255, 255);
            this.BackColor = Color.FromArgb(255, 255, 255);
            this.BorderColor = Color.FromArgb(192, 194, 204);
            this.ArrowColor = Color.FromArgb(128, 131, 143);
        }
    }
}
