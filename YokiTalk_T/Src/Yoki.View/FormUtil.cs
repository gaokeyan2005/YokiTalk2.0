using NIM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Yoki.View
{
    public enum UIStatus
    {
        Waiting,
        InClass
    }

    public enum UIWaitingStatus
    {
        Waiting,
        NotWaiting,
        None
    }

    public class FormUtil
    {
        private static string[] serverPaths = null;
        public static string Serverpath
        {
            get
            {
                if (serverPaths == null || string.IsNullOrEmpty(serverPaths[0]))
                {
                    serverPaths = GetServerPath();
                }
                return serverPaths[0];
            }
        }
        
        public static string Browerpath
        {
            get
            {
                if (serverPaths == null || string.IsNullOrEmpty(serverPaths[1]))
                {
                    serverPaths = GetServerPath();
                }
                return serverPaths[1];
            }
        }

        private static string[] GetServerPath()
        {
            string configPath = System.Windows.Forms.Application.StartupPath + @"\Config\serverUri.txt";
            if (!System.IO.File.Exists(configPath))
            {
                throw new Exception("Server uri config file not exists.");
            }
            string[] paths = new string[2];
            using (System.IO.FileStream fs = new System.IO.FileStream(configPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(fs))
                {
                    paths[0] = sr.ReadLine().Trim() + "/";
                    paths[1] = sr.ReadLine().Trim() + "/";
                }
            }
            return paths;
        }

        public static void DrawUserHeader(Graphics g, Image content, Rectangle rect, int borderThinkness = 3)
        {
            Rectangle contentRect = rect;

            contentRect.Inflate(borderThinkness * 3, borderThinkness * 3);

            using (GraphicsPath path = Fink.Drawing.RectangleEx.CreatePath(contentRect, Math.Max(contentRect.Width, contentRect.Height) / 2, Fink.Drawing.RoundStyle.All))
            {
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = Color.FromArgb(255, 0, 0, 0);
                    brush.SurroundColors = new Color[] { Color.FromArgb(0, 0, 0, 0) };
                    brush.FocusScales = new PointF(.85f, .85f);

                    //g.TranslateTransform(0f, BoderPading);
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                    //g.TranslateTransform(0f, -BoderPading);
                }
            };


            using (Fink.Drawing.HAFGraphics haf = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.All))
            {
                contentRect.Inflate(0 - borderThinkness, 0 - borderThinkness);

                using (LinearGradientBrush brush = new LinearGradientBrush(
                   contentRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[] { Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 246, 248, 250) };
                    blend.Positions = new float[] { 0f, 1f };
                    brush.InterpolationColors = blend;
                    g.FillEllipse(brush, contentRect);
                }

                contentRect.Inflate(0 - borderThinkness, 0 - borderThinkness);


                using (LinearGradientBrush brush = new LinearGradientBrush(
                   contentRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[] { Color.FromArgb(255, 224, 232, 242), Color.FromArgb(255, 255, 255, 255) };
                    blend.Positions = new float[] { 0f, 1f };
                    brush.InterpolationColors = blend;
                    g.FillEllipse(brush, contentRect);
                }

                contentRect.Inflate(0 - borderThinkness, 0 - borderThinkness);

            }

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(contentRect);


                if (content != null)
                {
                    using (Fink.Drawing.HAFGraphics haf = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
                    {
                        GraphicsState gs = g.Save();
                        using (Bitmap bitmap = Fink.Drawing.Image.KiResizeImage(content, contentRect.Width, contentRect.Height, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic))
                        {
                            using (Region r = new Region(path))
                            {
                                g.Clip = r;
                                g.DrawImage(bitmap, contentRect);
                            }
                        }
                        g.Restore(gs);

                        g.DrawEllipse(new Pen(new SolidBrush(Color.FromArgb(255, 247, 250, 253)), 1), contentRect);
                    }
                }
            }
        }

        private static Fink.Windows.Forms.AnimationTooltipFormEx aniTooltip = null;
        public static void ShowAnimationTooltip(System.Windows.Forms.Form parent, string text)
        {
            CloseAnimationTooltip(parent);

            aniTooltip = new Fink.Windows.Forms.AnimationTooltipFormEx(parent);
            
            aniTooltip.Size = new Size(168, 168);
            aniTooltip.Font = FontUtil.DefaultBoldFont;
            Bitmap background = (Bitmap)ResourceHelper.LoadingAnimationBackground;

            Queue<Bitmap> bitmapQueue = new Queue<Bitmap>();
            for (int i = 0; i < ResourceHelper.LoadingAnimationFrames.Length; i++)
            {
                Bitmap baseBitmap = new Bitmap(aniTooltip.Size.Width, aniTooltip.Size.Height);
                string tooltipText = text;
                for (int j = 0; j < (i / 7) % 4; j++)
                {
                    tooltipText += ".";
                }
                Graphics g = Graphics.FromImage(baseBitmap);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                SizeF fontSize = g.MeasureString(tooltipText, aniTooltip.Font);
               
                using (Bitmap content = ResourceHelper.LoadingAnimationFrames[i])
                {
                    g.DrawImage(background, new Rectangle(0, 0, background.Width, background.Height), new Rectangle(0, 0, background.Width, background.Height), GraphicsUnit.Pixel);
                    Rectangle contentRect = new Rectangle((background.Width - content.Width) / 2, (background.Height - content.Height) / 3, content.Width, content.Height);


                    contentRect.Inflate(12, 12);

                    using (GraphicsPath path = Fink.Drawing.RectangleEx.CreatePath(contentRect, Math.Max(contentRect.Width, contentRect.Height)/2, Fink.Drawing.RoundStyle.All))
                    {
                        using (PathGradientBrush brush = new PathGradientBrush(path))
                        {
                            brush.CenterColor = Color.FromArgb(255, 0, 0, 0);
                            brush.SurroundColors = new Color[] { Color.FromArgb(0, 0, 0, 0) };
                            brush.FocusScales = new PointF(.5f, .5f);

                            //g.TranslateTransform(0f, BoderPading);
                            g.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillPath(brush, path);
                            g.PixelOffsetMode = PixelOffsetMode.Default;
                            //g.TranslateTransform(0f, -BoderPading);
                        }
                    };


                    contentRect.Inflate(-4, -4);
                    g.FillEllipse(new SolidBrush(Color.FromArgb(255, 250, 250, 250)), contentRect);
                    contentRect.Inflate(-8, -8);
                    g.FillEllipse(Brushes.White, contentRect);
                    g.DrawImage(content, contentRect, new Rectangle(0, 0, content.Width, content.Height), GraphicsUnit.Pixel);

                    g.DrawString(tooltipText, aniTooltip.Font, new SolidBrush(Color.FromArgb(255, 48, 48, 48)), new PointF((background.Width - fontSize.Width) / 2, background.Height - fontSize.Height - 32));
                }

                bitmapQueue.Enqueue(baseBitmap);
            }
            aniTooltip.Bitmaps = bitmapQueue.ToArray();

            aniTooltip.ShowTooltip();

        }

        public static void CloseAnimationTooltip(System.Windows.Forms.Form parent)
        {
            if (aniTooltip != null)
            {
                parent.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    aniTooltip.Close();
                });
            }
        }

        private static Fink.Windows.Forms.TooltipFromEx tooltip = null;

        public static void ShowTooltip(System.Windows.Forms.Form parent, string text, Image icon = null)
        {
            CloseTooltip(parent);

            Size textSize = Size.Empty;
            System.Threading.AutoResetEvent are = new System.Threading.AutoResetEvent(false);
            parent.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                tooltip = new Fink.Windows.Forms.TooltipFromEx(parent);
                tooltip.Size = Size.Empty;
                tooltip.Font = FontUtil.DefaultBoldFont;


                {
                    Graphics g = tooltip.CreateGraphics();
                    textSize = System.Windows.Forms.TextRenderer.MeasureText(g, text, tooltip.Font, System.Drawing.Size.Empty, System.Windows.Forms.TextFormatFlags.NoPadding);
                }

                if (textSize.Width <= 0 || textSize.Height <= 0)
                {
                    return;
                }

                tooltip.Size = new Size(textSize.Width + 50 * 2, textSize.Height + 15 * 2);
                are.Set();
            });

            are.WaitOne(1000);

            Bitmap bitmap = new Bitmap(tooltip.Width, tooltip.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);


            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (Fink.Drawing.HAFGraphics haf = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
                {
                    using (GraphicsPath path = Fink.Drawing.RectangleEx.CreatePath(tooltip.ClientRectangle, 5, Fink.Drawing.RoundStyle.All))
                    {
                        using (PathGradientBrush brush = new PathGradientBrush(path))
                        {
                            brush.CenterColor = Color.FromArgb(32, 0, 0, 0);
                            brush.SurroundColors = new Color[] { Color.FromArgb(0, 0, 0, 0) };
                            brush.FocusScales = new PointF(.75f, .55f);

                            //e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillPath(brush, path);
                            //e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
                        }
                    }

                    Rectangle baseRect = tooltip.ClientRectangle;
                    baseRect.Inflate(-8, -8);
                    using (GraphicsPath path = Fink.Drawing.RectangleEx.CreatePath(baseRect, 5, Fink.Drawing.RoundStyle.All))
                    {
                        g.FillPath(new SolidBrush(Color.FromArgb(148, 0, 0, 0)), path);
                    }

                    g.DrawString(text, tooltip.Font, Brushes.White, new Point((tooltip.Width - textSize.Width) / 2, (tooltip.Height - textSize.Height) / 2));
                }
            }
            tooltip.Bitmap = bitmap;

            tooltip.VerticalOffset = parent.Height / 2 - parent.Height / 5;

            parent.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                tooltip.ShowTooltip();
            });
        }
        public static void CloseTooltip(System.Windows.Forms.Form parent)
        {
            if (tooltip != null)
            {
                parent.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    try
                    {
                        tooltip.Close();
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }
    }
}
