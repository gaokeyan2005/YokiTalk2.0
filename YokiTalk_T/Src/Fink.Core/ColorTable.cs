using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Fink.Core
{
    public abstract class ColorTable
    {
        public Color BackColor
        {
            get;
            protected set;
        }

        private ColorBlend _background;
        public ColorBlend Background
        {
            get {
                if (this._background == null)
                {
                    this._background = new ColorBlend();
                }
                return this._background; 
            }
            protected set { this._background = value; }
        }


        public Color Foreground
        {
            get;
            protected set;
        }


        public Color Border
        {
            get;
            protected set;
        }

        public Color InnerBorder
        {
            get;
            protected set;
        }


        public Color HighLight
        {
            get;
            protected set;
        }

        public Color Shadow
        {
            get;
            protected set;
        }
    }
}
