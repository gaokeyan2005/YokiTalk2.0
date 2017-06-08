using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Fink.Windows.Forms
{
    public class PureButtonExColorTable : ButtonExColorTable
    {
        public PureButtonExColorTable()
        {
            this.Background.Colors = new Color[] {
                Color.FromArgb(255, 92, 198, 64),
                Color.FromArgb(255, 56, 180, 72)
            };
            this.Background.Positions = new float[] { 0f, 1f };


            this.ActiveBackground.Colors = new Color[] {
                Color.FromArgb(255, 82, 193, 54),
                Color.FromArgb(255, 46, 175, 62)
            };
            this.ActiveBackground.Positions = new float[] { 0f, 1f };


            this.DisableBackground.Colors = new Color[] {
                Color.FromArgb(255, 199, 199, 199),
                Color.FromArgb(255, 181, 181, 181)
            };
            this.DisableBackground.Positions = new float[] { 0f, 1f };


            this.Border = Color.FromArgb(255, 255, 255);


            this.Foreground = Color.FromArgb(255, 255, 255);
            this.ActiveForeground = Color.FromArgb(240, 240, 240);
            this.DisableForeground = Color.FromArgb(80, 80, 80);

            this.InnerBorder = Color.Transparent;

            this.HighLight = Color.FromArgb(114, 216, 125);
            this.ActiveHighLight = Color.FromArgb(114, 216, 125);
            this.DisableHighLight = Color.FromArgb(217, 217, 217);


            this.Shadow = Color.FromArgb(6, 79, 9);
            this.ActiveShadow = Color.FromArgb(6, 79, 9);
            this.DisableShadow = Color.FromArgb(0, 0, 0);
        }
    }
}
