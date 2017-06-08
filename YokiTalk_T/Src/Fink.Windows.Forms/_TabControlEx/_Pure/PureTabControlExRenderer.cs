using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Fink.Drawing;

namespace Fink.Windows.Forms
{
    class PureTabControlExRenderer
    {
        private TabControlExColorTable colorTable;

        public PureTabControlExRenderer(TabControlExColorTable colortable)
            : base()
        {
            this.colorTable = colortable;
        }

        public TabControlExColorTable ColorTable
        {
            get
            {
                return colorTable;
            }
        }


    }
}
