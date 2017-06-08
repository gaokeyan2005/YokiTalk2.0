using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using Fink.Core;

namespace Fink.Windows.Forms
{
    public abstract class GroupPanelExColorTable: ColorTable
    {
        public GroupPanelExColorTable()
            : base()
        {

        }

        public Color HeaderBackColor { get; set; }

        public Color HeaderBorder { get; set; }

        public Color HeaderForeground { get; set; }

        public Color HeaderHighLight { get; set; }

        public Color HeaderShadow { get; set; }



        private Color _backNormal;
        private Color _backHover;

        public virtual Color BackNormal
        {
            get { return _backNormal; }
            set { this._backNormal = value; }
        }


        public Color BackHover
        {
            get
            {
                return _backHover;
            }
            set { this._backHover = value; }
        }

    }
}
