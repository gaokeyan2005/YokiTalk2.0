using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yoki.View.Dialog
{
    public partial class frmSessionCalleeAckNotify : Fink.Windows.Forms.DialogFormEx
    {
        public frmSessionCalleeAckNotify(Form parent)
            : base(parent)
        {
            InitializeComponent();

            this.BtnOKText = "ACCEPT";
            this.BtnCancelText = "REFUSE";
            this.BtnCancelWidth = 118;
        }

        private RefuseType refuseType = Dialog.RefuseType.Manual;
        public RefuseType RefuseType
        {
            get
            {
                return this.refuseType;
            }
            set
            {
                this.refuseType = value;
            }
        }


        private Yoki.Business.StudentClassInfo stuClassInfo = null;
        public Yoki.Business.StudentClassInfo StuClassInfo
        {
            get
            {
                return this.stuClassInfo;
            }
            set
            {
                if (this.stuClassInfo != value)
                {
                    this.stuClassInfo = value;
                }
            }
        }

        public Image HeaderImage
        {
            get
            {
                return this.StuClassInfo == null ? null : this.StuClassInfo.HeaderImage;
            }
            set
            {
                if (this.StuClassInfo != null)
                {
                    this.StuClassInfo.HeaderImage = value;
                    this.Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintTitle(e.Graphics);
            PaintAvatar(e.Graphics);
            PaintName(e.Graphics);
            PaintGenderAndAge(e.Graphics);
            //PaintGrade(e.Graphics);
        }

        private void PaintTitle(Graphics g)
        {
            Size titleSize = ResourceHelper.NewOrder.Size;
            Rectangle titleRect = Locations[0];

            using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
            {
                int left = titleRect.Left + (titleRect.Width - titleSize.Width) / 2;
                g.DrawImage(ResourceHelper.NewOrder,
                    new Rectangle(
                    new Point(left, titleRect.Top),
                    titleSize
                    )
                    );
            };

        }
        
        private void PaintAvatar(Graphics g)
        {
            int imageWidth = Math.Min(Locations[1].Width, Locations[1].Height);
            Size avatarSize = new Size(imageWidth, imageWidth);
            Rectangle avatarRect = new Rectangle(
                new Point(
                    Locations[1].Left + (Locations[1].Width - imageWidth) / 2,
                    Locations[1].Top + (Locations[1].Height - imageWidth) / 2
                    ),
                avatarSize
                );
            
            
            Rectangle borderRect = avatarRect;
            borderRect.Inflate(5, 5);

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
                    path.AddEllipse(avatarRect);
                    if (this.StuClassInfo.HeaderImage != null)
                    {
                        using (Fink.Drawing.HAFGraphics haf = new Fink.Drawing.HAFGraphics(g))
                        {
                            GraphicsState gs = g.Save();
                            using (Bitmap bitmap = Fink.Drawing.Image.KiResizeImage(this.StuClassInfo.HeaderImage, avatarRect.Width, avatarRect.Height, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic))
                            {
                                using (Region r = new Region(path))
                                {
                                    g.Clip = r;
                                    g.DrawImage(bitmap, avatarRect);
                                }
                            }
                            g.Restore(gs);
                        }
                    }
                }

                using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.All))
                {
                    g.DrawEllipse(new Pen(brush, 1), avatarRect);
                }
            }
        }


        private void PaintName(Graphics g)
        {

            Rectangle nameRect = Locations[2];
            Font nameFont = FontUtil.TitleFont;
            Size nameSize = TextRenderer.MeasureText(g, this.StuClassInfo.UserName, nameFont, nameRect.Size, TextFormatFlags.NoPadding);

            using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
            {
                int left = nameRect.Left + (nameRect.Width - nameSize.Width) / 2;
                g.DrawString(this.StuClassInfo.UserName, nameFont, new SolidBrush(Color.FromArgb(255, 255, 255, 255)), new Point(left, nameRect.Top));
            };
        }

        private void PaintGenderAndAge(Graphics g)
        {
            int ageRight = 0;
            if (this.stuClassInfo.Age >= 0)
            {
                string ageStr = this.StuClassInfo.Age + " Years Old";
                Rectangle ageRect = Locations[3];
                Font ageFont = FontUtil.DefaultBoldFont;
                Size ageSize = TextRenderer.MeasureText(g, ageStr, ageFont, ageRect.Size, TextFormatFlags.NoPadding);

                using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
                {
                    int left = ageRect.Left + (ageRect.Width - ageSize.Width) / 2;
                    ageRight = left + ageSize.Width;
                    g.DrawString(ageStr, ageFont, new SolidBrush(Color.FromArgb(255, 240, 240, 240)), new Point(left, ageRect.Top));
                };
            }

            Rectangle genderRect = Locations[4];
            int genderLeft = ageRight + 20;
            switch (this.StuClassInfo.Gender)
            {
                case Yoki.Core.Gender.Male:
                    {
                        using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
                        {
                            g.DrawImage(ResourceHelper.GenderBoy, new Rectangle(new Point(genderLeft, genderRect.Top), genderRect.Size));
                        };
                    }
                    break;
                case Yoki.Core.Gender.Female:
                    {
                        using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
                        {
                            g.DrawImage(ResourceHelper.GenderGirl, new Rectangle(new Point(genderLeft, genderRect.Top), genderRect.Size));
                        };
                    }
                    break;
                default:
                    {
                        using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
                        {
                            g.DrawImage(ResourceHelper.GenderUnknown, new Rectangle(new Point(genderLeft, genderRect.Top), genderRect.Size));
                        };
                    }
                    break;
            }
        }


        private void PaintGrade(Graphics g)
        {
            if (this.StuClassInfo != null && !string.IsNullOrEmpty(this.StuClassInfo.Grade))
            {
                Rectangle gradeRect = Locations[5];
                Font gradeFont = FontUtil.DefaultBoldFont;
                Size gradeSize = TextRenderer.MeasureText(g, this.StuClassInfo.Grade, gradeFont, gradeRect.Size, TextFormatFlags.NoPadding);

                using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
                {
                    int left = gradeRect.Left + (gradeRect.Width - gradeSize.Width) / 2;
                    g.DrawString(this.StuClassInfo.Grade, gradeFont, new SolidBrush(Color.FromArgb(255, 240, 240, 240)), new Point(left, gradeRect.Top));
                };
            }
        }

        private Rectangle clientRectMemory = Rectangle.Empty;
        private Rectangle[] locations = null;
        private Rectangle[] Locations
        {
            get
            {
                if (this.locations == null || this.ClientRectangle != clientRectMemory)
                {
                    Rectangle[] rects = new Rectangle[5];

                    int width = this.Width - 5 * 2;

                    //title
                    rects[0] = new Rectangle(5, 16, width, 64);

                    //avatar
                    rects[1] = new Rectangle(5, 80, width, 96);

                    //Name
                    rects[2] = new Rectangle(5, 200, width, 22);

                    //Age
                    rects[3] = new Rectangle(5 - 10, 225, width, 22);

                    //Gender
                    rects[4] = new Rectangle(5, 225 - 2, 16, 16);

                    //Grade
                    //rects[5] = new Rectangle(5, 250, width, 16);

                    this.clientRectMemory = this.ClientRectangle;
                    this.locations = rects;
                }

                return this.locations;
            }
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            if (t!=null)
            {
                t.Dispose();
            }

            base.OnClosing(e);
        }

        System.Windows.Forms.Timer t = null;
        int counter = 30;
        public DialogResult ShowDialogWithCounter(IWin32Window owner, int seconds)
        {
            counter = seconds;
            this.BtnCancelText = "REFUSE (" + counter + ")";


            if (t != null)
            {
                t.Dispose();
                t = null;
            }
            t = new Timer();
            t.Tick += (o, e) =>
            {
                this.BeginInvoke((MethodInvoker)delegate {
                    this.BtnCancelText = "REFUSE (" + counter + ")";
                });
                counter--;
                if (counter <= 0)
                {
                    t.Dispose();
                    this.RefuseType = RefuseType.TickTock;
                    this.BeginInvoke((MethodInvoker)delegate {
                        this.DialogResult = DialogResult.No;                        
                        this.Close();
                        
                    });
                }
            };
            t.Interval = 1000;
            t.Start();
                        
            return this.ShowDialog(owner);
        }
        
    }


    public enum RefuseType
    {
        Manual = 1,
        TickTock = 2
    }
}
