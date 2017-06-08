using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Fink.Drawing
{
    public class RectangleEx
    {
        /// <summary>
        /// 建立带有圆角样式的路径。
        /// </summary>
        /// <param name="rect">用来建立路径的矩形。</param>
        /// <param name="_radius">圆角的大小。</param>
        /// <param name="style">圆角的样式。</param>
        /// <param name="correction">是否把矩形长宽减 1,以便画出边框。</param>
        /// <returns>建立的路径。</returns>
        public static GraphicsPath CreatePath(
            Rectangle rect, int radius, RoundStyle style)
        {
            radius = radius <= Math.Min(rect.Width, rect.Height) / 2? radius: Math.Min(rect.Width, rect.Height) / 2;
            

            GraphicsPath path = new GraphicsPath();
            CreateRadiusRect(rect, path, radius, style);

            return path;
        }

        public static GraphicsPath CreatePath(
            Rectangle rect, float radius, RoundStyle style)
        {
            radius = radius <= Math.Min(rect.Width, rect.Height) / 2 ? radius : Math.Min(rect.Width, rect.Height) / 2;

            GraphicsPath path = new GraphicsPath();
            CreateRadiusRect(rect, path, radius, style);

            return path;
        }


        public static GraphicsPath CreatePath(
            Rectangle rect, int radius, RoundStyle style, PointF offsetPoint)
        {
            GraphicsPath path = CreatePath(rect, radius, style);
            Matrix m = new Matrix();
            m.Translate(offsetPoint.X, offsetPoint.Y);
            path.Transform(m);
            return path;
        }

        public static GraphicsPath CreatePath(
            Rectangle rect, int radius, RoundStyle style, SizeF offsetSize)
        {
            RectangleF rectF = new RectangleF(rect.X, rect.Y, rect.Width + offsetSize.Width, rect.Height + offsetSize.Height);
            
            GraphicsPath path = CreatePath(rectF, radius, style);
            return path;
        }


        public static GraphicsPath CreatePath(
            RectangleF rect, int radius, RoundStyle style)
        {
            radius = radius <= Math.Min(rect.Width, rect.Height) / 2 ? radius : (int)Math.Min(rect.Width, rect.Height) / 2;


            GraphicsPath path = new GraphicsPath();
            CreateRadiusRect(rect, path, radius, style);

            return path;
        }
        private static void CreateRadiusRect(RectangleF rect, GraphicsPath path, float radius, RoundStyle style)
        {
            PointF startPoint = Point.Empty, lastPoint = PointF.Empty;

            if (style == RoundStyle.None || radius <= 0)
            {
                path.AddRectangle(rect);
                goto PATHCLOSE;
            }

            if ((style & RoundStyle.LeftTop) == RoundStyle.LeftTop)
            {
                path.AddArc(rect.Left, rect.Top, radius * 2, radius * 2, 180, 90);
                lastPoint.X = rect.Left + radius * 2;
                lastPoint.Y = rect.Top;

                startPoint.X = rect.Left;
                startPoint.Y = rect.Top + radius;
            }
            else
            {
                path.AddLine(rect.Left, rect.Top, rect.Left, rect.Top);
                startPoint.X = rect.Left;
                startPoint.Y = rect.Top;
            }

            if ((style & RoundStyle.RightTop) == RoundStyle.RightTop)
            {
                path.AddArc(
                    rect.Right - radius * 2,
                    rect.Y,
                    radius * 2,
                    radius * 2,
                    270,
                    90);
                lastPoint.X = rect.Right;
                lastPoint.Y = rect.Top + radius * 2;
            }
            else
            {
                path.AddLine(lastPoint.X, lastPoint.Y, rect.Right, rect.Y);
                lastPoint.X = rect.Right;
                lastPoint.Y = rect.Top;

            }



            if ((style & RoundStyle.RightBottom) == RoundStyle.RightBottom)
            {
                path.AddArc(
                    rect.Right - radius * 2,
                    rect.Bottom - radius * 2,
                    radius * 2,
                    radius * 2, 0, 90);
                lastPoint.X = rect.Right - radius * 2;
                lastPoint.Y = rect.Bottom;
            }
            else
            {
                path.AddLine(lastPoint.X, lastPoint.Y, rect.Right, rect.Bottom);
                lastPoint.X = rect.Right;
                lastPoint.Y = rect.Bottom;
            }


            if ((style & RoundStyle.LeftBottom) == RoundStyle.LeftBottom)
            {
                path.AddArc(
                    rect.X,
                    rect.Bottom - radius * 2,
                    radius * 2,
                    radius * 2,
                    90,
                    90);
                lastPoint.X = rect.Left;
                lastPoint.Y = rect.Bottom - radius * 2;
            }
            else
            {
                path.AddLine(lastPoint.X, lastPoint.Y, rect.X, rect.Bottom);
                lastPoint.X = rect.Left;
                lastPoint.Y = rect.Bottom;


                path.AddLine(lastPoint.X, lastPoint.Y, startPoint.X, startPoint.Y);
            }


        PATHCLOSE:
            path.CloseFigure();
        }

        public static Rectangle Clone(Rectangle rect)
        {
            Rectangle r = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            return r;
        }

        public static RectangleF Clone(RectangleF rect)
        {
            RectangleF r = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
            return r;
        }

        public static Rectangle Inflate(Rectangle rect,Size size)
        {
            Rectangle r = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            r.Inflate(size);
            return r;
        }
    }
}
