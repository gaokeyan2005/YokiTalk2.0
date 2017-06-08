using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoki.Controls
{
    public class UserHeader: System.Windows.Forms.PictureBox
    {
        internal static Size HeaderSize = new Size(128, 96);

        ToolTip m_ToolTip = new ToolTip();

        Fink.Windows.Forms.DropDownMenu dropdown = null;
        public UserHeader()
        {
            base.SetStyle(
                   ControlStyles.UserPaint |
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.ResizeRedraw |
                   ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
            InitDropdown();

            
            //m_ToolTip.ToolTipTitle = "123";
            m_ToolTip.SetToolTip(this, this.ToolTipText);
            //m_ToolTip.Active = true;

            this.HasNotify = true;
        }

        private void InitDropdown ()
        {
            if (this.dropdown == null)
            {
                this.dropdown = new Fink.Windows.Forms.DropDownMenu();

                this.dropdown.Closed += (o, e) =>
                 {
                     this.IsHover = false;
                 };

                this.dropdown.BackColor = System.Drawing.Color.White;
                this.dropdown.Font = this.Font;
                this.dropdown.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
                this.dropdown.Name = "dropDownMenu1";
                this.dropdown.Padding = new System.Windows.Forms.Padding(5);
                this.dropdown.SearchPattern = null;
                this.dropdown.Size = new System.Drawing.Size(138, 103);

                Queue<System.Windows.Forms.ToolStripItem> items = new Queue<ToolStripItem>();
                items.Enqueue(new System.Windows.Forms.ToolStripButton("Change Icon") { Width = 100, Font = this.Font, Padding = new Padding(15, 5, 15, 5) });
                items.Enqueue(new System.Windows.Forms.ToolStripButton("Modify Profile") { Width = 100, Font = this.Font, Padding = new Padding(15, 5, 15, 5) });

                this.dropdown.Items.AddRange(items.ToArray());

            }
        }

        private Image headerImage = null;
        public Image HeaderImage
        {
            get
            {
                return this.headerImage;
            }
            set
            {
                this.headerImage = value;
            }
        }
        

        private bool isHover = false;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsHover
        {
            get
            {
                return this.isHover;
            }
            set
            {
                if (this.isHover != value)
                {
                    this.isHover = value;
                    this.Invalidate();
                }

            }

        }


        private Color notifyColor = Color.FromArgb(255, 56, 180, 75);
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color NotifyColor
        {
            get
            {
                return this.notifyColor;
            }
            set
            {
                if (this.notifyColor != value)
                {
                    this.notifyColor = value;
                    this.Invalidate();
                }

            }
        }

        private string toolTipText = string.Empty;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ToolTipText
        {
            get
            {
                return this.toolTipText;
            }
            set
            {
                if (this.toolTipText != value)
                {
                    this.toolTipText = value;
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        m_ToolTip.SetToolTip(this, this.ToolTipText);
                    });
                }

            }
        }


        [DefaultValue(typeof(bool), "false")]
        public bool HasNotify
        {
            get;
            set;
        }

        [DefaultValue("UserName")]
        public string HeaderTitle
        {
            get;
            set;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintBackground(e.Graphics);
            PaintHeander(e.Graphics);
            PaintNotify(e.Graphics);
            PaintDropdownIcon(e.Graphics);
            PaintUserName(e.Graphics);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Locations == null || locations.Any(p=> p == null) )
            {
                return;
            }

            if (Locations[0].Contains(e.Location) || Locations[2].Contains(e.Location))
            {
                this.IsHover = true;
            }
            else
            {
                this.IsHover = false;
            }

            if (Locations[1].Contains(e.Location))
            {
                m_ToolTip.Active = true;
            }
            else
            {
                m_ToolTip.Active = false;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (Locations == null || Locations.Any(p => p == null))
            {
                return;
            }

            if (Locations[0].Contains(e.Location) || Locations[2].Contains(e.Location))
            {
                Point p = this.PointToScreen(new Point(Locations[2].Left, Locations[2].Bottom + 5));
                this.dropdown.Show(p);
            }
        }

        private void PaintBackground(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 36, 47, 62)), this.ClientRectangle);
        }

        private void PaintHeander(Graphics g)
        {
            Rectangle headerRect = Locations[0];


            Rectangle borderRect = Locations[0];
            borderRect.Inflate(4, 4);

            using (LinearGradientBrush brush = new LinearGradientBrush(
                     borderRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
            {
                ColorBlend blend = new ColorBlend();
                blend.Colors = new Color[] { Color.FromArgb(250, 30, 39, 51), Color.FromArgb(255, 47, 62, 82) };
                blend.Positions = new float[] { 0f, 1f };
                brush.InterpolationColors = blend;

                using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.All))
                {
                    g.FillEllipse(brush, borderRect);
                }

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(headerRect);
                    if (this.HeaderImage != null)
                    {
                        using (Fink.Drawing.HAFGraphics haf = new Fink.Drawing.HAFGraphics(g))
                        {
                            GraphicsState gs = g.Save();
                            using (Bitmap bitmap = Fink.Drawing.Image.KiResizeImage(this.HeaderImage, headerRect.Width, headerRect.Height, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic))
                            {
                                using (Region r = new Region(path))
                                {
                                    g.Clip = r;
                                    g.DrawImage(bitmap, headerRect);
                                }
                            }
                            g.Restore(gs);
                        }
                    }
                }

                using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.All))
                {
                    g.DrawEllipse(new Pen(brush, 1), headerRect);
                }
            }
        }

        private void  PaintNotify(Graphics g)
        {
            if (!this.HasNotify)
            {
                return;
            }

            Rectangle notifyRect = Locations[1];
            
            Rectangle borderRect = Locations[1];
            borderRect.Inflate(2, 2);

            using (LinearGradientBrush brush = new LinearGradientBrush(
                     borderRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
            {
                ColorBlend blend = new ColorBlend();
                blend.Colors = new Color[] { Color.FromArgb(250, 30, 39, 51), Color.FromArgb(255, 47, 62, 82) };
                blend.Positions = new float[] { 0f, 1f };
                brush.InterpolationColors = blend;

                using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.All))
                {
                    g.FillEllipse(brush, borderRect);
                }

                using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g))
                {
                    g.FillEllipse(new SolidBrush(this.NotifyColor), notifyRect);
                }

                using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.All))
                {
                    g.DrawEllipse(new Pen(brush, 1), notifyRect);
                }
            }

        }

        private void PaintDropdownIcon(Graphics g)
        {
            Rectangle dropdownRect = Locations[2];

            using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g))
            {
                GraphicsState gs = g.Save();
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddLine(new Point(0, dropdownRect.Height / 2), new Point(dropdownRect.Width, dropdownRect.Height / 2));
                    path.AddLine(new Point(dropdownRect.Width, dropdownRect.Height / 2), new Point(dropdownRect.Width / 2, dropdownRect.Height));
                    path.CloseFigure();
                    using (Matrix matrix = new Matrix())
                    {
                        matrix.Translate(dropdownRect.Left, dropdownRect.Top);
                        path.Transform(matrix);
                    }

                    g.FillPath(new SolidBrush(this.IsHover ? Color.FromArgb(255, 255, 255, 255) : Color.FromArgb(255, 204, 204, 204)), path);
                }
                g.Restore(gs);
            }
        }

        private void PaintUserName(Graphics g)
        {
            Rectangle nameRect = Locations[3];
            Size nameSize = TextRenderer.MeasureText(g, HeaderTitle, this.Font, nameRect.Size, TextFormatFlags.NoPadding);

            using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
            {
                int left = nameRect.Left + (nameRect.Width - nameSize.Width) / 2;
                g.DrawString(this.HeaderTitle, this.Font, new SolidBrush(Color.FromArgb(255, 255, 255, 255)), new Point(left, nameRect.Top));
            };
        }

        private Rectangle clientRectMemory = Rectangle.Empty;
        private Rectangle[] locations = null;
        private Rectangle[] Locations
        {
            get
            {
                if (this.locations == null || this.ClientRectangle != clientRectMemory)
                {
                    Rectangle[] rects = new Rectangle[4];
                    
                    //userHeader
                    rects[0] = new Rectangle(38, 12, 48, 48);

                    //notify
                    rects[1] = new Rectangle(88, 10, 9, 9);

                    //dropdown icon
                    rects[2] = new Rectangle(97, 32, 10, 10);

                    //UserName max region 
                    rects[3] = new Rectangle(10, 70, 104, 16);

                    this.clientRectMemory = this.ClientRectangle;
                    this.locations = rects;
                }

                return this.locations;
            }
        }


    }
}
