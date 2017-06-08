using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Fink.Drawing;
using Fink.Core;

namespace Fink.Windows.Forms
{
    class WaringButtonExRenderer
    {
        private ColorTable colorTable;

        public WaringButtonExRenderer(ColorTable colortable)
            : base()
        {
            this.colorTable = colortable;
        }

        public ColorTable ColorTable
        {
            get
            {
                return colorTable;
            }
        }

    }
}
