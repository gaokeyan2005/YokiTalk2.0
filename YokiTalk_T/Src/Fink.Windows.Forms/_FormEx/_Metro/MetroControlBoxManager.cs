using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    internal class MetroControlBoxManager : ControlBoxManager
    {

        public MetroControlBoxManager(FormEx owner):base(owner)
        {

        }

        public override Rectangle CloseBoxRect
        {
            get
            {
                if (CloseBoxVisibale)
                {
                    Point offset = ControlBoxOffset;
                    Size size = Owner.CloseBoxSize;
                    return new Rectangle(
                        Owner.Width - offset.X - size.Width,
                        offset.Y,
                        size.Width,
                        size.Height);
                }
                return Rectangle.Empty;
            }
        }

        public override Rectangle MaximizeBoxRect
        {
            get
            {
                if (MaximizeBoxVisibale)
                {
                    Point offset = ControlBoxOffset;
                    Size size = Owner.MaximizeBoxSize;
                    return new Rectangle(
                        CloseBoxRect.X - ControlBoxSpace - size.Width,
                        offset.Y,
                        size.Width,
                        size.Height);
                }
                return Rectangle.Empty;
            }
        }

        public override Rectangle MinimizeBoxRect
        {
            get
            {
                if (MinimizeBoxVisibale)
                {
                    Point offset = ControlBoxOffset;
                    Size size = Owner.MinimizeBoxSize;
                    int x = MaximizeBoxVisibale ?
                        MaximizeBoxRect.X - ControlBoxSpace -  size.Width:
                        CloseBoxRect.X - ControlBoxSpace - size.Width;
                    return new Rectangle(
                        x,
                        offset.Y,
                        size.Width,
                        size.Height);
                }
                return Rectangle.Empty;
            }
        }

    }
}
