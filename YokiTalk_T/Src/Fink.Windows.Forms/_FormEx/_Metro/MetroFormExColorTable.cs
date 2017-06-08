using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Fink.Windows.Forms
{
    class MetroFormExColorTable: FormExColorTable
    {
        public MetroFormExColorTable()
        {
            this.CaptionActive = Color.FromArgb(0, 235, 236, 239); ;
            this.CaptionDeactive = Color.FromArgb(0, 235, 236, 239);
            this.CaptionForeground = Color.FromArgb(60, 60, 60);
            this.Border = Color.FromArgb(0, 74, 128);
            this.InnerBorder = Color.FromArgb(255, 254, 254, 255);
            this.BackColor = Color.FromArgb(255, 255, 255);
            this.DarkThemeBackColor = Color.FromArgb(36, 47, 62);
            this.ControlBoxActive = Color.Empty;
            this.ControlBoxDeactive = Color.Empty;
            this.ControlBoxHover = Color.FromArgb(230, 230, 230);
            this.ControlBoxPressed = Color.FromArgb(204, 204, 204);

            this.ControlBoxIconActive = Color.FromArgb(0, 0, 0);
            this.ControlBoxIconDeactive = Color.FromArgb(0, 0, 0);
            this.ControlBoxIconHover = Color.FromArgb(0, 0, 0);
            this.ControlBoxIconPressed = Color.FromArgb(0, 0, 0);

            this.ControlCloseBoxDeactive = Color.Empty;
            this.ControlCloseBoxHover = Color.FromArgb(232, 17, 35);
            this.ControlCloseBoxPressed = Color.FromArgb(241, 112, 122);

            this.ControlCloseBoxIconActive = Color.FromArgb(0, 0, 0);
            this.ControlCloseBoxIconDeactive = Color.FromArgb(0, 0, 0);
            this.ControlCloseBoxIconHover = Color.FromArgb(250, 250, 250);
            this.ControlCloseBoxIconPressed = Color.FromArgb(250, 250, 250);

            this.ControlBoxInnerBorder = Color.FromArgb(128, 250, 250, 250);

            this.HighLight = Color.FromArgb(64, 255, 255, 255);
            this.Shadow = Color.FromArgb(64, 0, 0, 0);
        }
    }
}
