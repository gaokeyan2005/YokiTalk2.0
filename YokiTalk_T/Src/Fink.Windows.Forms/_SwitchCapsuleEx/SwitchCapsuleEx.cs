using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using Fink.Drawing;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace Fink.Windows.Forms
{
    internal class SwitchCapsuleExDesigner : ControlDesignerEx
    {
        public SwitchCapsuleExDesigner()
            : base()
        {
            this.AutoResizeHandles = true;

            DesignerVerb verb1 = new DesignerVerb("Edit Items", new
            EventHandler(OnEdit));
            VerbCollection.AddRange(new DesignerVerb[] { verb1 });
        }

        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            var control = this.Control as SwitchCapsuleEx;
        }

        void OnEdit(Object sender, EventArgs e)
        {
            SwitchCapsuleEx ParentControl = (SwitchCapsuleEx)Control;

        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (VerbCollection.Count == 2)
                {
                    SwitchCapsuleEx control = (SwitchCapsuleEx)Control;
                    if (control.Items == null || control.Items.Count == 0)
                    {
                        VerbCollection[0].Enabled = true;
                        VerbCollection[1].Enabled = false;
                    }
                    else
                    {
                        VerbCollection[0].Enabled = true;
                        VerbCollection[1].Enabled = true;
                    }
                }
                return VerbCollection;
            }
        }
    }

    public class SwitchCapsuleExItem
    {
        public SwitchCapsuleExItem(SwitchCapsuleEx parent)
        {
            this.Parent = parent;
        }

        public SwitchCapsuleEx Parent
        {
            get;
            set;
        }

        public UInt16 SelectedImageIndex
        {
            get;
            set;
        }

        public UInt16 UnselectedImageIndex
        {
            get;
            set;
        }
        public string KeyName
        {
            get;
            set;
        }

        private bool isChecked = false;
        [DefaultValue(false)]
        public bool IsChecked
        {
            get
            {
                return this.isChecked;
            }
            set
            {
                if (this.isChecked != value)
                {
                    this.isChecked = value;
                    this.Parent.Invalidate();
                }
            }
        }

        public Rectangle ClientRect
        {
            get;
            set;
        }
    }

    public class SwitchCapsuleCheckedArgs: EventArgs
    {
        public int SourceIndex
        {
            get;
            internal set;
        }

        public SwitchCapsuleExItem SourceItem
        {
            get;
            internal set;
        }

        public bool IsChecked
        {
            get;
            internal set;
        }
    }

    public delegate void SwitchCapsuleCheckedhandle(object sender, SwitchCapsuleCheckedArgs args);

    [Designer(typeof(SwitchCapsuleExDesigner))]
    public class SwitchCapsuleEx: System.Windows.Forms.Control
    {
        public event SwitchCapsuleCheckedhandle SwitchCapsuleCheckedChanged;

        private SwitchCapsuleExColorTable _colorTable;
        public Size _borderMarginSize = new Size(5, 3);
        private int _radius = 7;

        public SwitchCapsuleEx()
            : base()
        {
            this.ColorTable = new PureSwitchCapsuleExColorTable();
            this.MinimumSize = new System.Drawing.Size(30 + 2 * this.BorderMarginSize.Width, 30 + 2 * this.BorderMarginSize.Height);

            AddItems();

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
        }

        private void AddItems()
        {
            SwitchCapsuleExItem item = new SwitchCapsuleExItem(this);
            item.UnselectedImageIndex = 0;
            item.SelectedImageIndex = 1;
            item.KeyName = "SwitchCapsuleExItem1";
            this.Items.Add(item);

            item = new SwitchCapsuleExItem(this);
            item.UnselectedImageIndex = 2;
            item.SelectedImageIndex = 3;
            item.KeyName = "SwitchCapsuleExItem2";
            this.Items.Add(item);

            item = new SwitchCapsuleExItem(this);
            item.UnselectedImageIndex = 4;
            item.SelectedImageIndex = 5;
            item.KeyName = "SwitchCapsuleExItem3";
            this.Items.Add(item);
        }

        private const bool _AutoSize = true;
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool AutoSize
        {
            get { return _AutoSize; }
        }

        public SwitchCapsuleExColorTable ColorTable
        {
            get { return _colorTable; }
            protected set { _colorTable = value; }
        }
        [DefaultValue(typeof(Size), "5, 3")]
        public Size BorderMarginSize
        {
            get { return _borderMarginSize; }
            set
            {
                if (_borderMarginSize != value)
                {
                    _borderMarginSize = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(7)]
        public int Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    base.Invalidate();
                }
            }
        }

        private ImageList _imageList;
        public ImageList ImageList
        {
            get { return this._imageList; }
            set
            {
                this._imageList = value;
                this.Invalidate();
            }
        }

        private System.Collections.Generic.IList<SwitchCapsuleExItem> _items;
        public System.Collections.Generic.IList<SwitchCapsuleExItem> Items
        {
            get{
                
                if (this._items == null)
	            {
		             this._items = new List<SwitchCapsuleExItem>();
	            }
                return this._items;
            }
        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            Rectangle btnBaseRect = GetButtonBaseRect();
            lock (this.Items)
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    SwitchCapsuleExItem item = this.Items[i];
                    Rectangle itemRect = GetItemRect(btnBaseRect, this.Items.Count, i);
                    if (itemRect.Contains(e.Location))
                    {
                        this.Items[i].IsChecked = !this.Items[i].IsChecked;
                        if (this.SwitchCapsuleCheckedChanged != null)
                        {
                            this.SwitchCapsuleCheckedChanged(this, new SwitchCapsuleCheckedArgs() { SourceIndex = i, SourceItem = Items[i], IsChecked = this.Items[i].IsChecked });
                        }
                        break;
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            base.OnPaintBackground(e);

            if ((this.Items == null || this.Items.Count <= 0) && !this.DesignMode)
            {
                return;
            }

            using (Fink.Drawing.HAFGraphics graphic = new HAFGraphics(e.Graphics, HAFGraphicMode.AandH))
            {
                this.Size = GetSize();

                DrawBackground(e.Graphics, this.ClientRectangle);

            }
            Rectangle btnBaseRect = GetButtonBaseRect();
                lock (this.Items)
                {
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        SwitchCapsuleExItem item = this.Items[i];
                        Rectangle itemRect = GetItemRect(btnBaseRect, this.Items.Count, i);

                        using (Fink.Drawing.HAFGraphics graphic = new HAFGraphics(e.Graphics, HAFGraphicMode.All))
                        {
                            DrawButtonitem(e.Graphics, itemRect, item, i);
                        }
                    }
                    DrawSpliter(e.Graphics, btnBaseRect, this.Items.Count);
                }

        }

        private Size GetSize()
        {
            int width = (this.MinimumSize.Width - this.BorderMarginSize.Width * 2) * this.Items.Count + this.BorderMarginSize.Width * 2;
            return new Size(Math.Max(this.MinimumSize.Width, width), this.MinimumSize.Height);
        }

        private Rectangle GetItemRect(Rectangle rect, int count, int index)
        {
            int itemWidth = rect.Width / count;
            Rectangle r = new Rectangle(rect.Left + itemWidth * index, rect.Top, itemWidth, rect.Height);

            return r;
        }

        private Rectangle GetButtonBaseRect()
        {
            Rectangle rect = this.ClientRectangle;
            rect.Inflate(-1, -1);

            rect.Inflate(-this.BorderMarginSize.Width, -this.BorderMarginSize.Height);
            rect.Width--;
            rect.Height--;
            return rect;
        }

        private void DrawButtonitem(Graphics g, Rectangle rect, SwitchCapsuleExItem item, int index)
        {
            if (this.ImageList == null || this.ImageList.Images.Count <= 0)
            {
                return;
            }
            Size margin = new Size((rect.Width - this.ImageList.ImageSize.Width) / 2 + 1,
                (rect.Height - this.ImageList.ImageSize.Height) / 2 + 1);
            g.DrawImage(this.ImageList.Images[item.IsChecked ? item.SelectedImageIndex : item.UnselectedImageIndex], new Point(rect.Left + margin.Width, rect.Top + margin.Height));

        }

        private void DrawSpliter(Graphics g, Rectangle rect, int itemCount)
        {
            int itemWidth = rect.Width / itemCount;
            for (int i = 0; i < itemCount - 1; i++)
            {
                g.DrawLine(new Pen(Color.FromArgb(208, 207, 207)),
                    new Point(rect.Left + itemWidth * (i + 1), rect.Top + 1),
                    new Point(rect.Left + itemWidth * (i + 1), rect.Bottom - 1));
            }
        }

        private void DrawBackground(Graphics g, Rectangle rect)
        {
            Rectangle mainRect = rect;
            mainRect.Inflate(-1, -1);

            using (GraphicsPath path = RectangleEx.CreatePath(mainRect, this.Radius, Drawing.RoundStyle.All))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(mainRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[] { Color.FromArgb(64, 0, 0, 0),
                                                    Color.FromArgb(25, 0, 0, 0),
                                                    Color.FromArgb(5, 0, 0, 0) };
                    blend.Positions = new float[] { 0f, .15f, 1f };
                    brush.InterpolationColors = blend;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }

                using (LinearGradientBrush brush = new LinearGradientBrush(
                 rect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[] { Color.FromArgb(0, 255, 255, 255), Color.FromArgb(160, 255, 255, 255) };
                    blend.Positions = new float[] { 0f, 1f };
                    brush.InterpolationColors = blend;
                    g.DrawPath(new Pen(brush), path);
                }

            }

            //Draw group base shadow.
            Rectangle groupShadowRect = mainRect;
            groupShadowRect.Inflate(-this.BorderMarginSize.Width, -this.BorderMarginSize.Height);
            groupShadowRect.Y += 2;
            using (GraphicsPath path = RectangleEx.CreatePath(groupShadowRect, this.Radius - Math.Min(this.BorderMarginSize.Width, this.BorderMarginSize.Height), Drawing.RoundStyle.All))
            {
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = Color.FromArgb(255, 0, 0, 0);
                    brush.SurroundColors = new Color[] { Color.FromArgb(0, 0, 0, 0) };
                    brush.FocusScales = new PointF(.3f, 0.3f);

                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }
            }

            //Draw group base.
            Rectangle groupRect = GetButtonBaseRect();
            using (GraphicsPath path = RectangleEx.CreatePath(groupRect, this.Radius - Math.Min(this.BorderMarginSize.Width, this.BorderMarginSize.Height), Drawing.RoundStyle.All))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(groupRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[] { Color.FromArgb(255, 250, 250, 250),
                                                    Color.FromArgb(255, 234, 234, 234)};
                    blend.Positions = new float[] { 0f, 1f };
                    brush.InterpolationColors = blend;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.FillPath(brush, path);
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }
                Rectangle groupBorderBrushRect = groupRect;
                groupBorderBrushRect.Inflate(1, 1);
                using (LinearGradientBrush brush = new LinearGradientBrush(groupBorderBrushRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[] { Color.FromArgb(255, 197, 197, 197),
                                                    Color.FromArgb(255, 197, 197, 197),
                                                    Color.FromArgb(255, 153, 153, 153) };
                    blend.Positions = new float[] { 0f, .8f, 1f };
                    brush.InterpolationColors = blend;
                    g.DrawPath(new Pen(brush), path);
                }
            }

            Rectangle groupinnerHightLightRect = groupRect;
            groupinnerHightLightRect.Inflate(-1, -1);

            int radius = this.Radius - Math.Min(this.BorderMarginSize.Width, this.BorderMarginSize.Height) - 1;
            using (GraphicsPath path = RectangleEx.CreatePath(groupinnerHightLightRect, radius, Drawing.RoundStyle.All))
            {
                Rectangle groupinnerHightLightBrushRect = groupinnerHightLightRect;
                groupinnerHightLightBrushRect.Inflate(1, 1);
                using (LinearGradientBrush brush = new LinearGradientBrush(groupinnerHightLightBrushRect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Colors = new Color[] { Color.FromArgb(225, 255, 255, 255),
                                                    Color.FromArgb(80, 255, 255, 255),
                                                    Color.FromArgb(60, 255, 255, 255) };
                    blend.Positions = new float[] { 0f, (float)radius / groupinnerHightLightRect.Height, 1f };
                    brush.InterpolationColors = blend;
                    g.DrawPath(new Pen(brush), path);
                }
            }


        }



    }
}
