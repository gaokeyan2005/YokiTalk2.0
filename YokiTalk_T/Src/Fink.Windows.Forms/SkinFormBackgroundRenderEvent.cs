using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Fink.Windows.Forms
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    public delegate void FormExBackgroundRenderEventHandler(
        object sender,
        FormExBackgroundRenderEventArgs e);

    public class FormExBackgroundRenderEventArgs : PaintEventArgs
    {
        private FormEx _formEx;

        public FormExBackgroundRenderEventArgs(
            FormEx formEx,
            Graphics g,
            Rectangle clipRect)
            : base(g, clipRect)
        {
            _formEx = formEx;
        }

        public FormEx FormEx
        {
            get { return _formEx; }
        }
    }
}
