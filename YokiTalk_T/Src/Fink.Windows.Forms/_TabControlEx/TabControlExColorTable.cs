using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Fink.Core;

namespace Fink.Windows.Forms
{
    public abstract class TabControlExColorTable : ColorTable
    {
        private Color _baseColor;

        public Color BaseColor
        {
            get { return _baseColor; }
            set { _baseColor = value; }
        }

        private Color _borderColor;

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }
        private Color _arrowColor;

        public Color ArrowColor
        {
            get { return _arrowColor; }
            set { _arrowColor = value; }
        }
    }
}
