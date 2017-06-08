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
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    class PureButtonExRenderer
    {
        private ColorTable colorTable;

        public PureButtonExRenderer(ColorTable colortable) : base()
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
