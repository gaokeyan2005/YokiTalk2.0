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
    public partial class frmEvaluateNotify : Fink.Windows.Forms.DialogFormEx
    {
#pragma warning disable CS0649 // Field 'frmEvaluateNotify.t' is never assigned to, and will always have its default value null
        System.Threading.Thread t;
#pragma warning restore CS0649 // Field 'frmEvaluateNotify.t' is never assigned to, and will always have its default value null
        public frmEvaluateNotify(Form parent, Yoki.Business.StudentClassInfo stuClassInfo)
            : base(parent)
        {
            InitializeComponent();

            this.StuClassInfo = stuClassInfo;

            this.BtnOKText = "SUBMIT";
            
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


        public Dictionary<int, int> Scores
        {
            get;
            private set;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Scores = this.ucEvaluate1.Scores;
            var  zeroItems  = this.Scores == null ? null: from p in this.Scores.Values where p <=0 select p;
            if (this.Scores == null || zeroItems.Count() > 0)
            {
                if (t != null)
                {
                    t.Abort();
                }
                e.Cancel = true;
            }
            this.DialogResult = DialogResult.OK;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintAvatar(e.Graphics);
            PaintName(e.Graphics);
            PaintGenderAndAge(e.Graphics);
            //PaintGrade(e.Graphics);
            PaintClassInfo(e.Graphics);
        }

        //private void PaintAvatar(Graphics g)
        //{
        //    if (this.StuClassInfo != null && this.StuClassInfo.HeaderImage != null)
        //    {
        //        int imageWidth = Math.Min(Locations[0].Width, Locations[0].Height);
        //        Size avatarSize = new Size(imageWidth, imageWidth);
        //        Rectangle avatarRect = new Rectangle(
        //            new Point(
        //                Locations[0].Left + (Locations[0].Width - imageWidth) / 2,
        //                Locations[0].Top + (Locations[0].Height - imageWidth) / 2
        //                ),
        //            avatarSize
        //            );
        //        FormUtil.DrawUserHeader(g, this.StuClassInfo.HeaderImage, avatarRect, 2);
        //    }
        //}


        private void PaintAvatar(Graphics g)
        {
            int imageWidth = Math.Min(Locations[0].Width, Locations[0].Height);
            Size avatarSize = new Size(imageWidth, imageWidth);
            Rectangle avatarRect = new Rectangle(
                new Point(
                    Locations[0].Left + (Locations[0].Width - imageWidth) / 2,
                    Locations[0].Top + (Locations[0].Height - imageWidth) / 2
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

            Rectangle nameRect = Locations[1];
            Font nameFont = FontUtil.TitleFont;
            Size nameSize = TextRenderer.MeasureText(g, this.StuClassInfo.UserName, nameFont, nameRect.Size, TextFormatFlags.NoPadding);

            using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
            {
                int left = nameRect.Left; // + (nameRect.Width - nameSize.Width) / 2;
                g.DrawString(this.StuClassInfo.UserName, nameFont, new SolidBrush(Color.FromArgb(255, 255, 255, 255)), new Point(left, nameRect.Top));
            };
        }

        private void PaintGenderAndAge(Graphics g)
        {
            int ageRight = 0;
            if (this.stuClassInfo.Age >= 0)
            {
                string ageStr = this.StuClassInfo.Age + "Years Old";
                Rectangle ageRect = Locations[2];
                Font ageFont = FontUtil.DefaultBoldFont;
                Size ageSize = TextRenderer.MeasureText(g, ageStr, ageFont, ageRect.Size, TextFormatFlags.NoPadding);

                using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
                {
                    int left = ageRect.Left; // + (ageRect.Width - ageSize.Width) / 2;
                    ageRight = left + ageSize.Width;
                    g.DrawString(ageStr, ageFont, new SolidBrush(Color.FromArgb(255, 240, 240, 240)), new Point(left, ageRect.Top));
                };
            }

            Rectangle genderRect = Locations[3];
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
                Rectangle gradeRect = Locations[4];
                Font gradeFont = FontUtil.DefaultBoldFont;
                Size gradeSize = TextRenderer.MeasureText(g, this.StuClassInfo.Grade, gradeFont, gradeRect.Size, TextFormatFlags.NoPadding);

                using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
                {
                    int left = gradeRect.Left; //+ (gradeRect.Width - gradeSize.Width) / 2;
                    g.DrawString(this.StuClassInfo.Grade, gradeFont, new SolidBrush(Color.FromArgb(255, 240, 240, 240)), new Point(left, gradeRect.Top));
                };
            }
        }

        private void PaintClassInfo(Graphics g)
        {
            if (this.StuClassInfo != null && !string.IsNullOrEmpty(this.StuClassInfo.ClassTitle))
            {
                Rectangle classRect = Locations[4];
                Font classFont = FontUtil.DefaultBoldFont;
                Size classSize = TextRenderer.MeasureText(g, this.StuClassInfo.ClassTitle, classFont, classRect.Size, TextFormatFlags.NoPadding);

                using (Fink.Drawing.HAFGraphics hag = new Fink.Drawing.HAFGraphics(g, Fink.Drawing.HAFGraphicMode.AandH))
                {
                    int left = classRect.Left; //+ (gradeRect.Width - gradeSize.Width) / 2;
                    g.DrawString("Class: " + this.StuClassInfo.ClassTitle, classFont, new SolidBrush(Color.FromArgb(255, 240, 240, 240)), new Point(left, classRect.Top));
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
                    int width = (this.Width - 96 * 2);

                    Rectangle[] rects = new Rectangle[5];
                    //title
                    rects[0] = new Rectangle(20, 0, 80, 120);

                    //name
                    rects[1] = new Rectangle(128, 25, width, 96);

                    //age
                    rects[2] = new Rectangle(128, 60, width, 22);

                    //Gender
                    rects[3] = new Rectangle(128 - 10, 60 - 2, 16, 16);

                    //Grade
                    //rects[4] = new Rectangle(128, 60, width, 22);

                    //Class
                    rects[4] = new Rectangle(128, 80, width, 22);

                    this.clientRectMemory = this.ClientRectangle;
                    this.locations = rects;
                }

                return this.locations;
            }
        }



    }
}
