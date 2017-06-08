using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using Fink.Core;

namespace Fink.Windows.Forms
{
    public class ToolStripExColorTable: ColorTable
    {
        public ToolStripExColorTable(): base()
        {
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.Border = Color.FromArgb(160, 161, 163);
            this.BackNormal = Color.FromArgb(250, 250, 250);

            this.BackHover.Colors = new Color[] {
                Color.FromArgb(255, 83, 180, 184),
                Color.FromArgb(255, 100, 197, 200)
            };
            this.BackHover.Positions = new float[] { 0f, 1f };

            this.BackPressed = Color.FromArgb(226, 176, 0);
            this.Foreground = Color.FromArgb(82, 82, 82);
            this.DropDownImageBack = Color.FromArgb(233, 238, 238);
            this.DropDownImageSeparator = Color.FromArgb(197, 197, 197);

            this.HighLight = Color.White;
        }

        private Color _backNormal;
        private ColorBlend _backHover;
        private Color _backPressed;
        private Color _dropDownImageBack;
        private Color _dropDownImageSeparator;

        public virtual Color BackNormal
        {
            get { return _backNormal; }
            set { this._backNormal = value; }
        }


        public ColorBlend BackHover
        {
            get
            {
                if (this._backHover == null)
                {
                    this._backHover = new ColorBlend();
                }
                return _backHover;
            }
            private set { this._backHover = value; }
        }

        public virtual Color BackPressed
        {
            get { return _backPressed; }
            set { this._backPressed = value; }
        }

        public virtual Color DropDownImageBack
        {
            get { return _dropDownImageBack; }
            set { this._dropDownImageBack = value; }
        }

        public virtual Color DropDownImageSeparator
        {
            get { return _dropDownImageSeparator; }
            set { this._dropDownImageSeparator = value; }
        }
    }
}
