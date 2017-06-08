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
    internal class SquaresButtonGroupExColorTableDesigner : ControlDesignerEx
    {
        public SquaresButtonGroupExColorTableDesigner()
            : base()
        {
            this.AutoResizeHandles = true;

            DesignerVerb verb1 = new DesignerVerb("Edit Ttems", new
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

    public class SquaresButtonGroupExItem
    {
        public int Index
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public object Tag
        {
            get;
            set;
        }

        public Rectangle ClientRect
        {
            get;
            set;
        }

        public static Size DesignSize = new Size(28, 23);
    }


    public class SquaresButtonGroupExSelectChangedEventArgs : EventArgs
    {
        public int SelectedIndex
        {
            get;
            set;
        }
        public object SelectedItem
        {
            get;
            set;
        }
    }

    public delegate void SquaresButtonGroupExSelectChangedHandle(object sender, SquaresButtonGroupExSelectChangedEventArgs e);
    [Designer(typeof(SquaresButtonGroupExColorTableDesigner))]
    public class SquaresButtonGroupEx: System.Windows.Forms.Control
    {
        public event SquaresButtonGroupExSelectChangedHandle SquaresButtonGroupExSelectChanged;

        private SwitchCapsuleExColorTable _colorTable;
        public Size _borderMarginSize = new Size(5, 4);
        private int _radius = 7;

        private Fink.Core.ISelectableList dataList;
        public Fink.Core.ISelectableList DataList
        {
            get
            {
                return this.dataList;
            }
            set
            {
                if (this.dataList != null)
                {
                    //unregist event
                    this.dataList.ItemChanged -= new Fink.Core.SelectableListItemChangedHandle(dataList_ItemChanged);
                    this.dataList.SelectChanged -= new Fink.Core.SelectableListSelectChangedHandle(dataList_SelectChanged);
                }
                this.dataList = value;
                if (this.dataList != null)
                {
                    //regist event 
                    this.dataList.ItemChanged += new Fink.Core.SelectableListItemChangedHandle(dataList_ItemChanged);
                    this.dataList.SelectChanged += new Fink.Core.SelectableListSelectChangedHandle(dataList_SelectChanged);
                }
            }

        }

        void dataList_ItemChanged(Fink.Core.ItemChangedEventArgs e)
        {
            RefurshItems(e.Item);
        }


        void dataList_SelectChanged(Fink.Core.SelectChangedEventArgs e)
        {
            this.selectedIndex = e.SelectedIndex;
            this.Invalidate();
        }

        public void SetSelectByItemIndex(int index)
        {
            foreach (SquaresButtonGroupExItem item in this.Items)
            {
                if (item.Index == index)
                {
                    this.SelectedIndex = this.Items.IndexOf(item);
                }
            }
        }

        private void RefurshItems(object o)
        {
            SquaresButtonGroupExItem existsItem = null;
            foreach (SquaresButtonGroupExItem item in this.Items)
            {
                if (item.Tag == o)
                {
                    existsItem = item;
                    break;
                }
            }
            if (existsItem != null)
            {
                this.Items.Remove(existsItem);
            }
            else
            {
                this.AddItem(o);
            }
        }

        public SquaresButtonGroupEx()
            : base()
        {
            this.ColorTable = new PureSquaresButtonGroupExColorTable();
            this.MinimumSize = new System.Drawing.Size(SquaresButtonGroupExItem.DesignSize.Width + 2 * this.BorderMarginSize.Width, SquaresButtonGroupExItem.DesignSize.Height + 2 * this.BorderMarginSize.Height);


            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
        }



        private void AddItem(object tag)
        {
            SquaresButtonGroupExItem item = new SquaresButtonGroupExItem();
            item.Tag = tag;
            item.Index = GetIndex();
            this.Items.Add(item);
            this.SelectedIndex = this.Items.IndexOf(item);
            this.Invalidate();
        }

        private int GetIndex()
        {
            int i = 0;
            List<int> indexes = new List<int>();
            foreach (SquaresButtonGroupExItem item in this.Items)
            {
                indexes.Add(item.Index);
            }

            while (indexes.Contains(i))
            {
                i++;
            }

            return i;
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

        private System.Collections.Generic.IList<SquaresButtonGroupExItem> _items;
        public System.Collections.Generic.IList<SquaresButtonGroupExItem> Items
        {
            get{
                
                if (this._items == null)
	            {
		             this._items = new List<SquaresButtonGroupExItem>();
	            }
                return this._items;
            }
        }

        public SquaresButtonGroupExItem SelectedItem
        {
            get
            {
                return this.Items!= null && this.Items.Count > this.SelectedIndex ? this.Items[this.SelectedIndex] : null;
            }
        }

        private int selectedIndex = -1;
        public int SelectedIndex
        {
            set
            {
                bool isNeedUpdate = false;
                if (this.selectedIndex != value)
                {
                    this.selectedIndex = value;
                    isNeedUpdate = true;
                }

                if (isNeedUpdate && value < this.Items.Count && value >= 0)
                {
                    if (this.dataList !=null)
                    {
                        this.DataList.CurrectIndex = this.SelectedIndex;
                    }
                    if (this.SquaresButtonGroupExSelectChanged != null)
                    {
                        this.SquaresButtonGroupExSelectChanged(this, new SquaresButtonGroupExSelectChangedEventArgs() { SelectedIndex = this.SelectedIndex, SelectedItem = this.SelectedItem });
                    }
                }
            }
            get
            {
                return this.selectedIndex;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            foreach (SquaresButtonGroupExItem item in this.Items)
            {
                if (item.ClientRect.Contains(e.Location))
                {
                    this.SelectedIndex = this.Items.IndexOf(item);
                    break;
                }
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            base.OnPaintBackground(e);

            this.SelectedIndex = this.SelectedIndex;

            if ((this.Items == null || this.Items.Count <= 0) && !this.DesignMode)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            this.Size = GetSize();

            Rectangle btnBaseRect = GetButtonBaseRect();


            lock (this.Items)
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    SquaresButtonGroupExItem item = this.Items[i];
                    item.ClientRect = GetItemRect(btnBaseRect, this.Items.Count, i);
                }
            }

            DrawBackground(e.Graphics, this.ClientRectangle);

            lock (this.Items)
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    SquaresButtonGroupExItem item = this.Items[i];
                    DrawButtonItem(e.Graphics, item, i);
                }
                DrawSpliter(e.Graphics, btnBaseRect, this.Items.Count);
            }
        }

        private Size GetSize()
        {
            int width = SquaresButtonGroupExItem.DesignSize.Width * this.Items.Count + this.BorderMarginSize.Width * 2;
            return new Size(width, SquaresButtonGroupExItem.DesignSize.Height + this.BorderMarginSize.Height * 2);
        }

        private Rectangle GetItemRect(Rectangle rect, int count, int index)
        {
            Rectangle r = new Rectangle(new Point(rect.Left + SquaresButtonGroupExItem.DesignSize.Width * index + (index == 0 ? 0 : 1), rect.Top), SquaresButtonGroupExItem.DesignSize);
            return r;
        }

        private Rectangle GetButtonBaseRect()
        {
            Rectangle rect = this.ClientRectangle;

            rect.Inflate(-this.BorderMarginSize.Width, -this.BorderMarginSize.Height);
            return rect;
        }

        private void DrawButtonItem(Graphics g, SquaresButtonGroupExItem item, int index)
        {
            Rectangle rect = item.ClientRect;
            if (this.Items.IndexOf(item) == this.SelectedIndex)
            {
                Font selectedFont = new Font(this.Font.FontFamily, this.Font.Size + 1, FontStyle.Bold | FontStyle.Underline);
                SizeF fontSize = g.MeasureString(item.Index.ToString(), selectedFont);

                Size margin = new Size(Convert.ToInt32((rect.Width - fontSize.Width) / 2 + 1),
                    Convert.ToInt32((rect.Height - fontSize.Height) / 2 + 1));

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(180, 255, 255, 255)))
                {
                    g.DrawString(item.Index.ToString(), selectedFont, brush, new Point(rect.Left + margin.Width, rect.Top + margin.Height + 1));
                }

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 32, 140, 48)))
                {
                    g.DrawString(item.Index.ToString(), selectedFont, brush, new Point(rect.Left + margin.Width, rect.Top + margin.Height));
                }
            }
            else
            {
                SizeF fontSize = g.MeasureString(item.Index.ToString(), this.Font);

                Size margin = new Size(Convert.ToInt32((rect.Width - fontSize.Width) / 2 + 1),
                    Convert.ToInt32((rect.Height - fontSize.Height) / 2 + 2));
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 96, 96, 96)))
                {
                    g.DrawString(item.Index.ToString(), this.Font, brush, new Point(rect.Left + margin.Width, rect.Top + margin.Height));
                }
            }

        }

        private void DrawSpliter(Graphics g, Rectangle rect, int itemCount)
        {
            for (int i = 0; i < itemCount - 1; i++)
            {
                g.DrawLine(new Pen(Color.FromArgb(208, 207, 207)),
                    new Point(rect.Left + SquaresButtonGroupExItem.DesignSize.Width * (i + 1), rect.Top),
                    new Point(rect.Left + SquaresButtonGroupExItem.DesignSize.Width * (i + 1), rect.Bottom));
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
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
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
            //groupShadowRect.Y += 2;
            using (GraphicsPath path = RectangleEx.CreatePath(groupShadowRect, this.Radius - Math.Min(this.BorderMarginSize.Width, this.BorderMarginSize.Height) / 2, Drawing.RoundStyle.All))
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
            //Draw group base.

            using (GraphicsPath path = RectangleEx.CreatePath(groupRect, this.Radius - Math.Min(this.BorderMarginSize.Width, this.BorderMarginSize.Height) / 2, Drawing.RoundStyle.All))
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

            int radius = this.Radius - Math.Min(this.BorderMarginSize.Width, this.BorderMarginSize.Height) / 2 - 1;
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

            //Draw selected base.
            if (this.SelectedItem != null)
            {
                using (GraphicsPath path = RectangleEx.CreatePath(groupRect, this.Radius - Math.Min(this.BorderMarginSize.Width, this.BorderMarginSize.Height) / 2, Drawing.RoundStyle.All))
                {
                    using (System.Drawing.Region r = new System.Drawing.Region(path))
                    {
                        Rectangle rectSelect = this.SelectedItem.ClientRect;
                        rectSelect.Inflate(-1, -2);
                        r.Intersect(RectangleEx.CreatePath(rectSelect, 0, RoundStyle.None));
                        using (LinearGradientBrush brush = new LinearGradientBrush(rectSelect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical))
                        {
                            ColorBlend blend = new ColorBlend();
                            blend.Colors = new Color[] { Color.FromArgb(255, 225, 225, 225),
                                                        Color.FromArgb(255, 250, 250, 250)};
                            blend.Positions = new float[] { 0f, 1f };
                            brush.InterpolationColors = blend;
                            g.PixelOffsetMode = PixelOffsetMode.Half;
                            g.FillRegion(brush, r);
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
                }
            }

        }



    }
}
