using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
     abstract class SkinRenderer
    {

        public abstract Region CreateRegion(Control control);

        public abstract void Init(Control control);

        public abstract void DrawFormExBackground();

        public abstract void DrawBorder();

        public abstract void Draw();


    }
}
