using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Fink.Windows.Forms
{
    internal class PureToggleButtonExColorTable: ToggleButtonExColorTable
    {
        public PureToggleButtonExColorTable()
        {
            this.Background.Colors = new Color[] {
                //Color.FromArgb(255, 100, 197, 200),
                //Color.FromArgb(255, 83, 180, 184)
                Color.FromArgb(255, 114, 198, 63),
                Color.FromArgb(255, 56, 180, 72)
            };
            this.Background.Positions = new float[] { 0f, 1f };
        }
    }
}
