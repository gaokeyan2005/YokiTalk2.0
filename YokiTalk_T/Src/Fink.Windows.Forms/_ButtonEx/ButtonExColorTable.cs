using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fink.Core;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Fink.Windows.Forms
{
    public abstract class ButtonExColorTable : ColorTable
    {
        private ColorBlend _activeBackground;
        public ColorBlend ActiveBackground
        {
            get
            {
                if (this._activeBackground == null)
                {
                    this._activeBackground = new ColorBlend();
                }
                return this._activeBackground;
            }
            protected set { this._activeBackground = value; }
        }

        private ColorBlend _disableBackground;
        public ColorBlend DisableBackground
        {
            get
            {
                if (this._disableBackground == null)
                {
                    this._disableBackground = new ColorBlend();
                }
                return this._disableBackground;
            }
            protected set { this._disableBackground = value; }
        }


        public Color ActiveForeground
        {
            get;
            protected set;
        }



        public Color DisableForeground
        {
            get;
            protected set;
        }

        public Color ActiveHighLight
        {
            get;
            protected set;
        }

        public Color DisableHighLight
        {
            get;
            protected set;
        }



        public Color ActiveShadow
        {
            get;
            protected set;
        }

        public Color DisableShadow
        {
            get;
            protected set;
        }
    }
}
