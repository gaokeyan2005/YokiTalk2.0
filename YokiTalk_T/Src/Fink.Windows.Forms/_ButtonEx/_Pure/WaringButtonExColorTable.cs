using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Fink.Windows.Forms
{
    public class WaringButtonExColorTable: ButtonExColorTable
    {
        public WaringButtonExColorTable()
        {
            this.Background.Colors = new Color[] {
                Color.FromArgb(255, 239, 95, 86),
                Color.FromArgb(255, 255, 49, 38)
            };
            this.Background.Positions = new float[] { 0f, 1f };


            this.ActiveBackground.Colors = new Color[] {
                Color.FromArgb(255, 239, 95, 86),
                Color.FromArgb(255, 255, 49, 38)
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

            this.HighLight = Color.FromArgb(244, 140, 134);
            this.ActiveHighLight = Color.FromArgb(244, 140, 134);
            this.DisableHighLight = Color.FromArgb(217, 217, 217);


            this.Shadow = Color.FromArgb(255, 255, 255);
            this.ActiveShadow = Color.FromArgb(255, 255, 255);
            this.DisableShadow = Color.FromArgb(0, 0, 0);
            //{
            //this.Background.Colors = new Color[] {
            //    Color.FromArgb(255, 239, 95, 86),
            //    Color.FromArgb(255, 255, 49, 38)
            //};

            
            //this.Background.Positions = new float[] {0f, 1f};

            //this.Border = Color.FromArgb(255, 255, 255);
            //this.Foreground = Color.FromArgb(255, 255, 255);

            //this.InnerBorder = Color.Transparent;

            //this.HighLight = Color.FromArgb(244, 140, 134);
            //this.Shadow = Color.FromArgb(255, 255, 255);
        }
    }
}
