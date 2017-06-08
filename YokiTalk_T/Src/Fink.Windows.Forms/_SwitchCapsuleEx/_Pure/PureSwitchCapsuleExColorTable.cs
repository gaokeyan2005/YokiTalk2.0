using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Fink.Windows.Forms
{
    public class PureSwitchCapsuleExColorTable : SwitchCapsuleExColorTable
    {
        public PureSwitchCapsuleExColorTable()
        {
            this.Background.Colors = new Color[] {
                Color.FromArgb(255, 100, 197, 200),
                Color.FromArgb(255, 83, 180, 184)
            };
            this.Background.Positions = new float[] {0f, 1f};

            this.Border = Color.FromArgb(255, 255, 255);
            this.Foreground = Color.FromArgb(255, 255, 255); 

            this.InnerBorder = Color.Transparent;

            this.HighLight = Color.FromArgb(255, 255, 255);
            this.Shadow = Color.FromArgb(255, 255, 255);
        }
    }
}
