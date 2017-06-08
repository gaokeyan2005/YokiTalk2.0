using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Fink.Core;

namespace Fink.Windows.Forms
{
    public class FormExColorTable : ColorTable
    {
        public Color DarkThemeBackColor
        {
            get;
            protected set;
        }

        public Color CaptionActive
        {
            get;
            protected set;
        }

        public Color CaptionDeactive
        {
            get;
            protected set;
        }

        public Color CaptionForeground
        {
            get;
            protected set;
        }

        public Color ControlBoxActive
        {
            get;
            protected set;
        }

        public Color ControlBoxDeactive
        {
            get;
            protected set;
        }

        public Color ControlBoxIconActive
        {
            get;
            protected set;
        }

        public Color ControlBoxIconHover
        {
            get;
            protected set;
        }

        public Color ControlBoxIconPressed
        {
            get;
            protected set;
        }

        public Color ControlBoxIconDeactive
        {
            get;
            protected set;
        }

        public Color ControlBoxHover
        {
            get;
            protected set;
        }

        public Color ControlBoxPressed
        {
            get;
            protected set;
        }

        public Color ControlCloseBoxActive
        {
            get;
            protected set;
        }

        public Color ControlCloseBoxDeactive
        {
            get;
            protected set;
        }

        public Color ControlCloseBoxHover
        {
            get;
            protected set;
        }

        public virtual Color ControlCloseBoxPressed
        {
            get;
            protected set;
        }

        public Color ControlCloseBoxIconActive
        {
            get;
            protected set;
        }

        public Color ControlCloseBoxIconHover
        {
            get;
            protected set;
        }

        public Color ControlCloseBoxIconPressed
        {
            get;
            protected set;
        }

        public Color ControlCloseBoxIconDeactive
        {
            get;
            protected set;
        }

        public virtual Color ControlBoxInnerBorder
        {
            get;
            protected set;
        }
    }
}
