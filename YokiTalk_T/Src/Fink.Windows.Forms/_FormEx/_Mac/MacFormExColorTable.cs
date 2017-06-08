using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Fink.Windows.Forms
{
    class MacFormExColorTable: FormExColorTable
    {
        public MacFormExColorTable()
        {
            this.CaptionActive = Color.FromArgb(0, 235, 236, 239); ;
            this.CaptionDeactive = Color.FromArgb(0, 235, 236, 239);
            this.CaptionForeground = Color.FromArgb(255,255,255);
            this.Border = Color.FromArgb(7, 28, 40);
            this.InnerBorder = Color.FromArgb(255, 50, 50, 58);
            this.BackColor = Color.FromArgb(7, 28, 40);
            this.ControlBoxActive = Color.Empty;
            this.ControlBoxDeactive = Color.Empty;
            this.ControlBoxHover = Color.FromArgb(37, 114, 151);
            this.ControlBoxPressed = Color.FromArgb(27, 84, 111);

            this.ControlBoxIconActive = Color.FromArgb(80, 80, 80);
            this.ControlBoxIconDeactive = Color.FromArgb(88, 172, 218);
            this.ControlBoxIconHover = Color.FromArgb(250, 250, 250);
            this.ControlBoxIconPressed = Color.FromArgb(250, 250, 250);

            this.ControlCloseBoxDeactive = Color.Empty;
            this.ControlCloseBoxHover = Color.FromArgb(179, 39, 15);
            this.ControlCloseBoxPressed = Color.FromArgb(153, 33, 12);

            this.ControlCloseBoxIconActive = Color.FromArgb(80, 80, 80);
            this.ControlCloseBoxIconDeactive = Color.FromArgb(88, 172, 218);
            this.ControlCloseBoxIconHover = Color.FromArgb(250, 250, 250);
            this.ControlCloseBoxIconPressed = Color.FromArgb(250, 250, 250);

            this.ControlBoxInnerBorder = Color.FromArgb(128, 250, 250, 250);

            this.HighLight = Color.FromArgb(64, 255, 255, 255);
            this.Shadow = Color.FromArgb(64, 0, 0, 0);
        }


        public MacFormExColorTable(Color backColor):base()
        {
            this.CaptionActive = Color.FromArgb(0, 235, 236, 239); ;
            this.CaptionDeactive = Color.FromArgb(0, 235, 236, 239);
            this.CaptionForeground = Color.FromArgb(255, 255, 255);
            this.Border = Color.FromArgb(7, 28, 40);
            this.InnerBorder = Color.FromArgb(255, 50, 50, 58);
            this.BackColor = backColor;
            this.ControlBoxActive = Color.Empty;
            this.ControlBoxDeactive = Color.Empty;
            this.ControlBoxHover = Color.FromArgb(37, 114, 151);
            this.ControlBoxPressed = Color.FromArgb(27, 84, 111);

            this.ControlBoxIconActive = Color.FromArgb(80, 80, 80);
            this.ControlBoxIconDeactive = Color.FromArgb(88, 172, 218);
            this.ControlBoxIconHover = Color.FromArgb(250, 250, 250);
            this.ControlBoxIconPressed = Color.FromArgb(250, 250, 250);

            this.ControlCloseBoxDeactive = Color.Empty;
            this.ControlCloseBoxHover = Color.FromArgb(179, 39, 15);
            this.ControlCloseBoxPressed = Color.FromArgb(153, 33, 12);

            this.ControlCloseBoxIconActive = Color.FromArgb(80, 80, 80);
            this.ControlCloseBoxIconDeactive = Color.FromArgb(88, 172, 218);
            this.ControlCloseBoxIconHover = Color.FromArgb(250, 250, 250);
            this.ControlCloseBoxIconPressed = Color.FromArgb(250, 250, 250);

            this.ControlBoxInnerBorder = Color.FromArgb(128, 250, 250, 250);

            this.HighLight = Color.FromArgb(64, 255, 255, 255);
            this.Shadow = Color.FromArgb(64, 0, 0, 0);
        }
    }
}
