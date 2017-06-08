using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Fink.Windows.Forms
{
    public class PureCyanGroupPanelExColorTable : GroupPanelExColorTable
    {
        public PureCyanGroupPanelExColorTable()
            : base()
        {
            this.HeaderBackColor = Color.FromArgb(51, 184, 184);
            this.HeaderBorder = Color.FromArgb(54, 152, 152);

            this.HeaderForeground = Color.FromArgb(250, 250, 250);
            this.HeaderHighLight = Color.FromArgb(65, 204, 204);
            this.HeaderShadow = Color.FromArgb(0, 0, 0);

            this.BackColor = Color.FromArgb(245, 245, 245);
            this.Border = Color.FromArgb(250, 250, 250);
            this.BackNormal = Color.FromArgb(250, 250, 250);
            this.BackHover = Color.FromArgb(255, 83, 180, 184);

            this.Foreground = Color.FromArgb(250, 250, 250);
            this.HighLight = Color.FromArgb(255, 255, 255);
            this.Shadow = Color.FromArgb(0, 0, 0);
        }
    }
}
