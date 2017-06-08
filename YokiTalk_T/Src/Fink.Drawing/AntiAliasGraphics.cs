using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Fink.Drawing
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    public class AntiAliasGraphics : IDisposable
    {
        private SmoothingMode _oldMode;
        private Graphics _graphics;

        public AntiAliasGraphics(Graphics graphics)
        {
            _graphics = graphics;
            _oldMode = graphics.SmoothingMode;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //g.CompositingQuality = CompositingQuality.HighQuality;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _graphics.SmoothingMode = _oldMode;
        }

        #endregion
    }


    public enum HAFGraphicMode
    {
        None = 0,
        AntiAlias = 1,
        HighQuality = 2,
        HalfOffset =4,
        AandH = 3,
        All = 7, 
    }

    public class HAFGraphics : IDisposable
    {
        private SmoothingMode _oldMode;
        private CompositingQuality _oldQuality;
        private PixelOffsetMode _oldOffset;
        private Graphics _graphics;

        public HAFGraphics(Graphics graphics, HAFGraphicMode mode = HAFGraphicMode.All)
        {
            _graphics = graphics;
            _oldMode = graphics.SmoothingMode;
            _oldQuality = graphics.CompositingQuality;
            _oldOffset = graphics.PixelOffsetMode;
            graphics.SmoothingMode = (HAFGraphicMode.AntiAlias & mode) == HAFGraphicMode.AntiAlias ? SmoothingMode.AntiAlias : _oldMode;
            graphics.CompositingQuality = (HAFGraphicMode.HighQuality & mode) == HAFGraphicMode.HighQuality ? CompositingQuality.HighQuality: _oldQuality;
            graphics.PixelOffsetMode = (HAFGraphicMode.HalfOffset & mode) == HAFGraphicMode.HalfOffset ? PixelOffsetMode.Half : _oldOffset;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _graphics.SmoothingMode = _oldMode;
            _graphics.CompositingQuality = _oldQuality;
            _graphics.PixelOffsetMode = _oldOffset;
        }

        #endregion
    }
}
