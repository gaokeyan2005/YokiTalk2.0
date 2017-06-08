using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Design;
using System.ComponentModel.Design;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace Fink.Windows.Forms
{
    internal class TabControlExDesigner : ParentControlDesignerEx
    {
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            var ctl = this.Control as TabControl;
            EnableDesignMode(ctl, ctl.Name);
            foreach (TabPage page in ctl.TabPages)
            {
                MessageBox.Show(ctl.TabPages.Count.ToString());
                EnableDesignMode(page, page.Name);
            }
            
        }

        public TabControlExDesigner()
            : base()
        {
            DesignerVerb verb1 = new DesignerVerb("Add Tab", new
            EventHandler(OnAddPage));
            DesignerVerb verb2 = new DesignerVerb("Remove Tab", new
            EventHandler(OnRemovePage));
            VerbCollection.AddRange(new DesignerVerb[] { verb1, verb2 });
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (VerbCollection.Count == 2)
                {
                    TabControlEx MyControl = (TabControlEx)Control;
                    if (MyControl.TabCount == 0)
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

        void OnAddPage(Object sender, EventArgs e)
        {
            TabControlEx ParentControl = (TabControlEx)Control;
            System.Windows.Forms.Control.ControlCollection oldTabs = ParentControl.Controls;

            RaiseComponentChanging(TypeDescriptor.GetProperties(ParentControl)["TabPages"]);

            TabPageEx page = (TabPageEx)(DesignerHost.CreateComponent(typeof(TabPageEx)));
            page.Text = page.Name;
            ParentControl.TabPages.Add(page);

            EnableDesignMode(page, page.Name);
            RaiseComponentChanged(TypeDescriptor.GetProperties(ParentControl)["TabPages"],
            oldTabs, ParentControl.TabPages);
            //ParentControl.SelectedTab = P;

            SetVerbs();
        }
        void OnRemovePage(Object sender, EventArgs e)
        {
            TabControlEx ParentControl = (TabControlEx)Control;
            System.Windows.Forms.Control.ControlCollection oldTabs =
            ParentControl.Controls;

            RaiseComponentChanging(TypeDescriptor.GetProperties(ParentControl)["TabPages"]);

            TabPage page = ParentControl.SelectedTab;
            ParentControl.TabPages.Remove(page);

            RaiseComponentChanged(TypeDescriptor.GetProperties(ParentControl)["TabPages"], oldTabs, ParentControl.TabPages);
            //ParentControl.SelectedTab = P;

            SetVerbs();
        }

        private void SetVerbs()
        {
            TabControlEx ParentControl = (TabControlEx)Control;

            switch (ParentControl.TabPages.Count)
            {
                case 0:
                    VerbCollection[0].Enabled = true;
                    VerbCollection[1].Enabled = false;
                    break;
                default:
                    VerbCollection[0].Enabled = true;
                    VerbCollection[1].Enabled = true;
                    break;
            }
        }
    }
    internal class TabControlExToolboxItem : ToolboxItem
    {
        public TabControlExToolboxItem(Type toolType)
            : base(toolType)
        {
        }
        TabControlExToolboxItem(SerializationInfo info, StreamingContext context)
        {
            Deserialize(info, context);
        }
        protected override IComponent[] CreateComponentsCore(
        IDesignerHost host,
        IDictionary defaultValues)
        {
            IComponent[] comps = base.CreateComponentsCore(host, defaultValues);
            return comps;
        }
    }
    
    [Designer(typeof(TabControlExDesigner))]
    [ToolboxItem(typeof(TabControlExToolboxItem))]
    public class TabControlEx : TabControl
    {
        #region Fields

        private UpDownButtonNativeWindow _upDownButtonNativeWindow;

        private TabControlExColorTable _colorTable;

        public TabControlExColorTable ColorTable
        {
            get {
                if (this._colorTable == null)
                {
                    this._colorTable = new PureTabControlExColorTable();
                }
                return _colorTable; }
        }

        private int _radius = 3;
        private int _unselectMargin = 3;
        private bool _hasCloseBtn = true;

        private Point _closeBtnMargin = new Point(3, 5);
        private Size _closeBtnSize = new Size(8, 8);

        private int _headerHeight = 36;

        private const string UpDownButtonClassName = "msctls_updown32";
        private static readonly object EventPaintUpDownButton = new object();


        #endregion

        #region Constructors

        public TabControlEx()
            : base()
        {
            this.Multiline = false;
            Init();
            SetStyles();
        }

        private void Init()
        {
            this.BackColor = this.ColorTable.BackColor;
            this.ItemSize = new System.Drawing.Size(0, HeaderHeight);
        }

        #endregion


        #region Events

        public event UpDownButtonPaintEventHandler PaintUpDownButton
        {
            add { base.Events.AddHandler(EventPaintUpDownButton, value); }
            remove { base.Events.RemoveHandler(EventPaintUpDownButton, value); }
        }

        #endregion

        #region Properties

        [DefaultValue(typeof(Point), "3, 5")]
        public Point CloseBtnMargin
        {
            get { return _closeBtnMargin; }
            set { _closeBtnMargin = value; }
        }

        [DefaultValue(typeof(Size), "8, 8")]
        public Size CloseBtnSize
        {
            get { return _closeBtnSize; }
            set { _closeBtnSize = value; }
        }

        [DefaultValue(3)]
        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        [DefaultValue(3)]
        public int UnselectMargin
        {
            get { return _unselectMargin; }
            set { _unselectMargin = value; }
        }

        [DefaultValue(36)]
        public int HeaderHeight
        {
            get { return this._headerHeight; }
            set { this._headerHeight = value; }
        }

        internal IntPtr UpDownButtonHandle
        {
            get { return FindUpDownButton(); }
        }

        #endregion


        #region Protected Methods

        protected virtual void OnPaintUpDownButton(
            UpDownButtonPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;

            Color upButtonBaseColor = this.ColorTable.BaseColor;
            Color upButtonBorderColor = this.ColorTable.BorderColor;
            Color upButtonArrowColor = this.ColorTable.ArrowColor;

            Color downButtonBaseColor = this.ColorTable.BaseColor;
            Color downButtonBorderColor = this.ColorTable.BorderColor;
            Color downButtonArrowColor = this.ColorTable.ArrowColor;

            Rectangle upButtonRect = rect;
            upButtonRect.X += 4;
            upButtonRect.Y += 4;
            upButtonRect.Width = rect.Width / 2 - 8;
            upButtonRect.Height -= 8;

            Rectangle downButtonRect = rect;
            downButtonRect.X = upButtonRect.Right + 2;
            downButtonRect.Y += 4;
            downButtonRect.Width = rect.Width / 2 - 8;
            downButtonRect.Height -= 8;

            if (Enabled)
            {
                if (e.MouseOver)
                {
                    if (e.MousePress)
                    {
                        if (e.MouseInUpButton)
                        {
                            upButtonBaseColor = GetColor(this.ColorTable.BaseColor, 0, -35, -24, -9);
                        }
                        else
                        {
                            downButtonBaseColor = GetColor(this.ColorTable.BaseColor, 0, -35, -24, -9);
                        }
                    }
                    else
                    {
                        if (e.MouseInUpButton)
                        {
                            upButtonBaseColor = GetColor(this.ColorTable.BaseColor, 0, 35, 24, 9);
                        }
                        else
                        {
                            downButtonBaseColor = GetColor(this.ColorTable.BaseColor, 0, 35, 24, 9);
                        }
                    }
                }
            }
            else
            {
                upButtonBaseColor = SystemColors.Control;
                upButtonBorderColor = SystemColors.ControlDark;
                upButtonArrowColor = SystemColors.ControlDark;

                downButtonBaseColor = SystemColors.Control;
                downButtonBorderColor = SystemColors.ControlDark;
                downButtonArrowColor = SystemColors.ControlDark;
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color backColor = Enabled ? this.ColorTable.BackColor : SystemColors.Control;

            using (SolidBrush brush = new SolidBrush(this.ColorTable.BackColor))
            {
                rect.Inflate(1, 1);
                g.FillRectangle(brush, rect);
            }

            RenderButton(
                g,
                upButtonRect,
                upButtonBaseColor,
                upButtonBorderColor,
                upButtonArrowColor,
                ArrowDirection.Left);
            RenderButton(
                g,
                downButtonRect,
                downButtonBaseColor,
                downButtonBorderColor,
                downButtonArrowColor,
                ArrowDirection.Right);

            UpDownButtonPaintEventHandler handler =
                base.Events[EventPaintUpDownButton] as UpDownButtonPaintEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Rectangle rect = GetTabRect(this.SelectedIndex);
            if (rect.Contains(e.Location))
            {
                this.Invalidate(rect);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawTabContrl(e.Graphics);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            if (_upDownButtonNativeWindow != null)
            {
                _upDownButtonNativeWindow.Dispose();
                _upDownButtonNativeWindow = null;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        #endregion

        #region Help Methods

        private IntPtr FindUpDownButton()
        {
            return NativeMethods.FindWindowEx(
                base.Handle,
                IntPtr.Zero,
                UpDownButtonClassName,
                null);
        }

        private void SetStyles()
        {
            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
        }

        private void DrawTabContrl(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            DrawDrawBackgroundAndHeader(g);

            DrawTabPages(g);
            DrawBorder(g);
        }

        private void DrawDrawBackgroundAndHeader(Graphics g)
        {
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;

            switch (Alignment)
            {
                case TabAlignment.Top:
                    x = 0;
                    y = 0;
                    width = ClientRectangle.Width;
                    height = ClientRectangle.Height - DisplayRectangle.Height;
                    break;
                case TabAlignment.Bottom:
                    x = 0;
                    y = DisplayRectangle.Height;
                    width = ClientRectangle.Width;
                    height = ClientRectangle.Height - DisplayRectangle.Height;
                    break;
                case TabAlignment.Left:
                    x = 0;
                    y = 0;
                    width = ClientRectangle.Width - DisplayRectangle.Width;
                    height = ClientRectangle.Height;
                    break;
                case TabAlignment.Right:
                    x = DisplayRectangle.Width;
                    y = 0;
                    width = ClientRectangle.Width - DisplayRectangle.Width;
                    height = ClientRectangle.Height;
                    break;
            }
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.FillRectangle(new SolidBrush(this.ColorTable.BackColor), ClientRectangle);
            g.PixelOffsetMode = PixelOffsetMode.Default;

            Rectangle headerRect = new Rectangle(x, y, width - 200, height);
            Color backColor = Enabled ? this.ColorTable.BackColor : SystemColors.Control;
            using (SolidBrush brush = new SolidBrush(this.ColorTable.BaseColor))
            {
                Rectangle rect = Fink.Drawing.RectangleEx.Clone(ClientRectangle);
                rect.Y += this.HeaderHeight + 2;
                rect.Height -= this.HeaderHeight + 2;

                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.FillRectangle(brush, rect);
                g.PixelOffsetMode = PixelOffsetMode.Default;
            }
        }

        private void DrawTabPages(Graphics g)
        {
            Rectangle tabRect;
            Point cusorPoint = PointToClient(MousePosition);
            bool hasSetClip = false;
            bool alignHorizontal =
                (Alignment == TabAlignment.Top ||
                Alignment == TabAlignment.Bottom);
            LinearGradientMode mode = alignHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            if (alignHorizontal)
            {
                IntPtr upDownButtonHandle = UpDownButtonHandle;
                bool hasUpDown = upDownButtonHandle != IntPtr.Zero;
                if (hasUpDown)
                {
                    if (NativeMethods.IsWindowVisible(upDownButtonHandle))
                    {
                        NativeMethods.RECT upDownButtonRect = new NativeMethods.RECT();
                        NativeMethods.GetWindowRect(
                            upDownButtonHandle, ref upDownButtonRect);
                        Rectangle upDownRect = Rectangle.FromLTRB(
                            upDownButtonRect.Left,
                            upDownButtonRect.Top,
                            upDownButtonRect.Right,
                            upDownButtonRect.Bottom);
                        upDownRect = RectangleToClient(upDownRect);

                        switch (Alignment)
                        {
                            case TabAlignment.Top:
                                upDownRect.Y = 0;
                                break;
                            case TabAlignment.Bottom:
                                upDownRect.Y =
                                    ClientRectangle.Height - DisplayRectangle.Height;
                                break;
                        }
                        upDownRect.Height = ClientRectangle.Height;
                        g.SetClip(upDownRect, CombineMode.Exclude);
                        hasSetClip = true;
                    }
                }
            }
            for (int index = 0; index < base.TabCount; index++)
            {
                TabPage page = TabPages[index];

                ControlState state = ControlState.Normal;

                tabRect = GetTabRect(index);
                FixHeaderBound(ref tabRect);

                page.SetBounds(tabRect.Left, tabRect.Top, tabRect.Width, tabRect.Height);

                state = SelectedIndex == index ? state | ControlState.Selected : state;
                if (state != ControlState.Selected)
                {
                    tabRect.Y += this.UnselectMargin;
                    tabRect.Height -= this.UnselectMargin;
                }
                state = tabRect.Contains(cusorPoint) ? state | ControlState.Hover : state;

                Color baseColor = this.ColorTable.BaseColor;
                Color borderColor = this.ColorTable.BorderColor;

                RenderTabBackgroundInternal(
                    g,
                    tabRect,
                    baseColor,
                    borderColor,
                    .45F,
                    mode,
                    state);

                bool hasImage = DrawTabImage(g, page, tabRect);

                DrawtabText(g, page, tabRect, hasImage, state);

                if (page is TabPageEx && !(page as TabPageEx).CanClose)
                {
                    //
                }
                else if (page is TabPageEx && (page as TabPageEx).CanClose)
                {
                    DrawCloseBtn(g, tabRect, state, cusorPoint);
                }
                else
                {
                    //
                }

            }
            if (hasSetClip)
            {
                g.ResetClip();
            }
        }

        private void DrawCloseBtn(Graphics g, Rectangle tabRect, ControlState state, Point cusorPoint)
        {

            #region DrawCloseBtn
            if (this._hasCloseBtn)
            {
                Rectangle cBtnRect = new Rectangle(tabRect.Right - this.CloseBtnMargin.X - this.CloseBtnSize.Width, tabRect.Top + this.CloseBtnMargin.Y, this.CloseBtnSize.Width, this.CloseBtnSize.Height);

                bool closeBtnHover = cBtnRect.Contains(cusorPoint);

                using (GraphicsPath path = CreateCloseFlagPath(cBtnRect))
                {
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    if (closeBtnHover)
                    {
                        g.FillPath(new SolidBrush(this.ColorTable.ArrowColor), path);
                    }
                    else if ((state & ControlState.Selected) == ControlState.Selected)
                    {
                        g.FillPath(new SolidBrush(this.ColorTable.ArrowColor), path);
                    }
                    else if ((state & ControlState.Selected) != ControlState.Selected && closeBtnHover)
                    {
                        Fink.Drawing.HsbColor color = Fink.Drawing.ColorEx.RgbToHsb(this.ColorTable.ArrowColor);
                        color.B = color.B <= 0 ? .4f : Math.Min(color.B * 1.5, .96f);
                        color.S = color.S <= 0 ? .2f : Math.Max(color.B / 5, .1f);
                        Color closeBtnColor = Fink.Drawing.ColorEx.HsbToRgb(color);
                        g.FillPath(new SolidBrush(closeBtnColor), path);
                    }
                    else
                    {

                    }
                    g.PixelOffsetMode = PixelOffsetMode.Default;
                }
            }
            #endregion
        }

        private void FixHeaderBound(ref Rectangle rect)
        {
            rect.X -= 2;
            rect.Width -= 3;
        }


        private GraphicsPath CreateCloseFlagPath(Rectangle rect)
        {
            PointF centerPoint = new PointF(
                rect.X + rect.Width / 2,
                rect.Y + rect.Height / 2);

            RectangleF rectArea = new RectangleF((int)(centerPoint.X) - 3f, (int)(centerPoint.Y) - 3f, 7, 7);

            GraphicsPath path = new GraphicsPath();
            path.FillMode = FillMode.Winding;

            path.AddLine(new PointF(rectArea.Left + 1, rectArea.Top), rectArea.Location);

            path.AddLine(rectArea.Location, new PointF(rectArea.Left, rectArea.Top + 1));

            path.AddLine(new PointF(rectArea.Left, rectArea.Top + 1), new PointF(rectArea.Left + (rectArea.Width / 2) - 1, rectArea.Top + (rectArea.Height / 2)));

            path.AddLine(new PointF(rectArea.Left + (rectArea.Width / 2) - 1, rectArea.Top + (rectArea.Height / 2)), new PointF(rectArea.Left, rectArea.Bottom - 1));

            path.AddLine(new PointF(rectArea.Left, rectArea.Bottom - 1), new PointF(rectArea.Left, rectArea.Bottom));

            path.AddLine(new PointF(rectArea.Left, rectArea.Bottom), new PointF(rectArea.Left + 1, rectArea.Bottom));

            path.AddLine(new PointF(rectArea.Left + 1, rectArea.Bottom), new PointF(rectArea.Left + (rectArea.Width / 2), rectArea.Bottom - (rectArea.Height / 2 - 1)));

            path.AddLine(new PointF(rectArea.Left + (rectArea.Width / 2), rectArea.Bottom - (rectArea.Height / 2 - 1)), new PointF(rectArea.Right - 1, rectArea.Bottom));

            path.AddLine(new PointF(rectArea.Right - 1, rectArea.Bottom), new PointF(rectArea.Right, rectArea.Bottom));

            path.AddLine(new PointF(rectArea.Right, rectArea.Bottom), new PointF(rectArea.Right, rectArea.Bottom - 1));

            path.AddLine(new PointF(rectArea.Right, rectArea.Bottom - 1), new PointF(rectArea.Right - (rectArea.Width / 2 - 1), rectArea.Top + (rectArea.Height / 2)));

            path.AddLine(new PointF(rectArea.Right - (rectArea.Width / 2 - 1), rectArea.Top + (rectArea.Height / 2)), new PointF(rectArea.Right, rectArea.Top + 1));

            path.AddLine(new PointF(rectArea.Right, rectArea.Top + 1), new PointF(rectArea.Right, rectArea.Top));

            path.AddLine(new PointF(rectArea.Right, rectArea.Top), new PointF(rectArea.Right - 1, rectArea.Top));

            path.AddLine(new PointF(rectArea.Right - 1, rectArea.Top), new PointF(rectArea.Left + (rectArea.Width / 2), rectArea.Top + (rectArea.Height / 2 - 1)));

            path.CloseFigure();
            return path;
        }

        private void DrawtabText(
            Graphics g, TabPage page, Rectangle tabRect, bool hasImage, ControlState state)
        {
            Rectangle textRect = tabRect;
            RectangleF newTextRect;
            StringFormat sf;
            Size fontSize = TextRenderer.MeasureText(g, page.Text, page.Font, Size.Empty, TextFormatFlags.NoPadding);
            int shadowMargin = 1;

            switch (Alignment)
            {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    if (hasImage)
                    {
                        textRect.X = tabRect.X + Radius / 2 + tabRect.Height - 2;
                        textRect.Width = tabRect.Width - Radius - tabRect.Height;
                    }
                    textRect.Height = fontSize.Height;
                    textRect.Y = tabRect.Bottom - textRect.Height - 5 + ((state & ControlState.Selected) == ControlState.Selected ? 0 : this.UnselectMargin);

                    Color foreColor = Color.Transparent;
                    Color shadowColor = Color.Transparent;

                    if ((state & ControlState.Selected) == ControlState.Selected)
                    {
                        foreColor = page.ForeColor;
                        shadowColor = Color.FromArgb(48, 255, 255, 255);
                        shadowMargin = 1;
                    }
                    else if ((state & ControlState.Selected) != ControlState.Selected
                        && (state & ControlState.Hover) == ControlState.Hover)
                    {
                        Fink.Drawing.HsbColor color = Fink.Drawing.ColorEx.RgbToHsb(page.ForeColor);
                        color.B = color.B <= 0 ? .4f : Math.Min(color.B * 2, .96f);
                        foreColor = Fink.Drawing.ColorEx.HsbToRgb(color);

                        shadowColor = Color.FromArgb(48, 255, 255, 255);
                        shadowMargin = 1;
                    }
                    else if ((state & ControlState.Selected) != ControlState.Selected)
	                {
		                Fink.Drawing.HsbColor color = Fink.Drawing.ColorEx.RgbToHsb(page.ForeColor);
                        color.B = color.B <= 0? .6f: Math.Min(color.B * 4, .72f);
                        foreColor = Fink.Drawing.ColorEx.HsbToRgb(color);

                        shadowColor = Color.FromArgb(48, 255, 255, 255);
                        shadowMargin = 1;
	                }

                    TextRenderer.DrawText(
                        g,
                        page.Text,
                        page.Font,
                        new Rectangle(textRect.Left, textRect.Top + shadowMargin, textRect.Width, textRect.Height),
                        shadowColor);

                    TextRenderer.DrawText(
                        g,
                        page.Text,
                        page.Font,
                        textRect,
                        foreColor);
                    break;
                case TabAlignment.Left:
                    if (hasImage)
                    {
                        textRect.Height = tabRect.Height - tabRect.Width + 2;
                    }
                    g.TranslateTransform(textRect.X, textRect.Bottom);
                    g.RotateTransform(270F);
                    sf = new StringFormat(StringFormatFlags.NoWrap);
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.Character;
                    newTextRect = textRect;
                    newTextRect.X = 0;
                    newTextRect.Y = 0;
                    newTextRect.Width = textRect.Height;
                    newTextRect.Height = textRect.Width;
                    using (Brush brush = new SolidBrush(page.ForeColor))
                    {
                        g.DrawString(
                            page.Text,
                            page.Font,
                            brush,
                            newTextRect,
                            sf);
                    }
                    g.ResetTransform();
                    break;
                case TabAlignment.Right:
                    if (hasImage)
                    {
                        textRect.Y = tabRect.Y + Radius / 2 + tabRect.Width - 2;
                        textRect.Height = tabRect.Height - Radius - tabRect.Width;
                    }
                    g.TranslateTransform(textRect.Right, textRect.Y);
                    g.RotateTransform(90F);
                    sf = new StringFormat(StringFormatFlags.NoWrap);
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.Character;
                    newTextRect = textRect;
                    newTextRect.X = 0;
                    newTextRect.Y = 0;
                    newTextRect.Width = textRect.Height;
                    newTextRect.Height = textRect.Width;
                    using (Brush brush = new SolidBrush(page.ForeColor))
                    {
                        g.DrawString(
                            page.Text,
                            page.Font,
                            brush,
                            newTextRect,
                            sf);
                    }
                    g.ResetTransform();
                    break;
            }
        }

        private void DrawBorder(Graphics g)
        {
            if (SelectedIndex != -1)
            {
                Rectangle tabRect = GetTabRect(SelectedIndex);
                FixHeaderBound(ref tabRect);

                Rectangle clipRect = ClientRectangle;
                Point[] points = new Point[6];


                IntPtr upDownButtonHandle = UpDownButtonHandle;
                bool hasUpDown = upDownButtonHandle != IntPtr.Zero;
                if (hasUpDown)
                {
                    if (NativeMethods.IsWindowVisible(upDownButtonHandle))
                    {
                        NativeMethods.RECT upDownButtonRect = new NativeMethods.RECT();
                        NativeMethods.GetWindowRect(
                            upDownButtonHandle,
                            ref upDownButtonRect);
                        Rectangle upDownRect = Rectangle.FromLTRB(
                            upDownButtonRect.Left,
                            upDownButtonRect.Top,
                            upDownButtonRect.Right,
                            upDownButtonRect.Bottom);
                        upDownRect = RectangleToClient(upDownRect);

                        tabRect.X = tabRect.X > upDownRect.X ?
                            upDownRect.X : tabRect.X;
                        tabRect.Width = tabRect.Right > upDownRect.X ?
                            upDownRect.X - tabRect.X : tabRect.Width;
                    }
                }

                switch (Alignment)
                {
                    case TabAlignment.Top:
                        points[0] = new Point(
                            tabRect.X,
                            tabRect.Bottom);
                        points[1] = new Point(
                            clipRect.X,
                            tabRect.Bottom);
                        points[2] = new Point(
                            clipRect.X,
                            clipRect.Bottom - 1);
                        points[3] = new Point(
                            clipRect.Right - 1,
                            clipRect.Bottom - 1);
                        points[4] = new Point(
                            clipRect.Right - 1,
                            tabRect.Bottom);
                        points[5] = new Point(
                            tabRect.Right,
                            tabRect.Bottom);
                        break;
                    case TabAlignment.Bottom:
                        points[0] = new Point(
                            tabRect.X,
                            tabRect.Y);
                        points[1] = new Point(
                            clipRect.X,
                            tabRect.Y);
                        points[2] = new Point(
                            clipRect.X,
                            clipRect.Y);
                        points[3] = new Point(
                            clipRect.Right - 1,
                            clipRect.Y);
                        points[4] = new Point(
                            clipRect.Right - 1,
                            tabRect.Y);
                        points[5] = new Point(
                            tabRect.Right,
                            tabRect.Y);
                        break;
                    case TabAlignment.Left:
                        points[0] = new Point(
                            tabRect.Right,
                            tabRect.Y);
                        points[1] = new Point(
                            tabRect.Right,
                            clipRect.Y);
                        points[2] = new Point(
                            clipRect.Right - 1,
                            clipRect.Y);
                        points[3] = new Point(
                            clipRect.Right - 1,
                            clipRect.Bottom - 1);
                        points[4] = new Point(
                            tabRect.Right,
                            clipRect.Bottom - 1);
                        points[5] = new Point(
                            tabRect.Right,
                            tabRect.Bottom);
                        break;
                    case TabAlignment.Right:
                        points[0] = new Point(
                            tabRect.X,
                            tabRect.Y);
                        points[1] = new Point(
                            tabRect.X,
                            clipRect.Y);
                        points[2] = new Point(
                            clipRect.X,
                            clipRect.Y);
                        points[3] = new Point(
                            clipRect.X,
                            clipRect.Bottom - 1);
                        points[4] = new Point(
                            tabRect.X,
                            clipRect.Bottom - 1);
                        points[5] = new Point(
                            tabRect.X,
                            tabRect.Bottom);
                        break;
                }
                using (Pen pen = new Pen(this.ColorTable.BorderColor))
                {
                    g.DrawLines(pen, points);
                }
            }
        }

        internal void RenderArrowInternal(
             Graphics g,
             Rectangle dropDownRect,
             ArrowDirection direction,
             Brush brush)
        {
            Point point = new Point(
                dropDownRect.Left + (dropDownRect.Width / 2),
                dropDownRect.Top + (dropDownRect.Height / 2));
            Point[] points = null;
            switch (direction)
            {
                case ArrowDirection.Left:
                    points = new Point[] { 
                        new Point(point.X + 1, point.Y - 4), 
                        new Point(point.X + 1, point.Y + 4), 
                        new Point(point.X - 2, point.Y) };
                    break;

                case ArrowDirection.Up:
                    points = new Point[] { 
                        new Point(point.X - 3, point.Y + 1), 
                        new Point(point.X + 3, point.Y + 1), 
                        new Point(point.X, point.Y - 1) };
                    break;

                case ArrowDirection.Right:
                    points = new Point[] {
                        new Point(point.X - 1, point.Y - 4), 
                        new Point(point.X - 1, point.Y + 4), 
                        new Point(point.X + 2, point.Y) };
                    break;

                default:
                    points = new Point[] {
                        new Point(point.X - 3, point.Y - 1), 
                        new Point(point.X + 3, point.Y - 1), 
                        new Point(point.X, point.Y + 1) };
                    break;
            }
            g.FillPolygon(brush, points);
        }

        internal void RenderButton(
            Graphics g,
            Rectangle rect,
            Color baseColor,
            Color borderColor,
            Color arrowColor,
            ArrowDirection direction)
        {
            RenderBackgroundInternal(
                g,
                rect,
                baseColor,
                borderColor,
                0.45f,
                true,
                LinearGradientMode.Vertical);
            using (SolidBrush brush = new SolidBrush(arrowColor))
            {
                RenderArrowInternal(
                    g,
                    rect,
                    direction,
                    brush);
            }
        }

        internal void RenderBackgroundInternal(
          Graphics g,
          Rectangle rect,
          Color baseColor,
          Color borderColor,
          float basePosition,
          bool drawBorder,
          LinearGradientMode mode)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
               rect, Color.Transparent, Color.Transparent, mode))
            {
                Color[] colors = new Color[4];
                colors[0] = GetColor(baseColor, 0, 35, 24, 9);
                colors[1] = GetColor(baseColor, 0, 13, 8, 3);
                colors[2] = baseColor;
                colors[3] = GetColor(baseColor, 0, 68, 69, 54);

                ColorBlend blend = new ColorBlend();
                blend.Positions =
                    new float[] { 0.0f, basePosition, basePosition + 0.05f, 1.0f };
                blend.Colors = colors;
                brush.InterpolationColors = blend;
                g.FillRectangle(brush, rect);
            }
            if (baseColor.A > 80)
            {
                Rectangle rectTop = rect;
                if (mode == LinearGradientMode.Vertical)
                {
                    rectTop.Height = (int)(rectTop.Height * basePosition);
                }
                else
                {
                    rectTop.Width = (int)(rect.Width * basePosition);
                }
                using (SolidBrush brushAlpha =
                    new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                {
                    g.FillRectangle(brushAlpha, rectTop);
                }
            }

            if (drawBorder)
            {
                using (Pen pen = new Pen(borderColor))
                {
                    g.DrawRectangle(pen, rect);
                }
            }
        }

        internal void RenderTabBackgroundInternal(
          Graphics g,
          Rectangle rect,
          Color baseColor,
          Color borderColor,
          float basePosition,
          LinearGradientMode mode,
          ControlState state)
        {
            Rectangle outerRect = Fink.Drawing.RectangleEx.Clone(rect);
            outerRect.Y++;
            outerRect.Height -= 1;

            using (GraphicsPath path = Fink.Drawing.RectangleEx.CreatePath(outerRect, this.Radius, Drawing.RoundStyle.LeftTop | Drawing.RoundStyle.RightTop))
            {
                using (SolidBrush brush = new SolidBrush((state & ControlState.Selected) == ControlState.Selected ? Color.FromArgb(255, 255, 255) : Color.FromArgb(245, 245, 245)))
                {
                    g.FillPath(brush,path);
                    if ((state & ControlState.Selected) != ControlState.Selected)
                    {
                        goto DRAWBORDER;
                    }
                }

                RectangleF innerRect = Fink.Drawing.RectangleEx.Clone(outerRect);
                innerRect.Inflate(-1, -1);
                innerRect.Y += 2;
                using (GraphicsPath path1 = Fink.Drawing.RectangleEx.CreatePath(innerRect, this.Radius - 1, Drawing.RoundStyle.LeftTop | Drawing.RoundStyle.RightTop))
                {
                  

                    using (LinearGradientBrush brush = new LinearGradientBrush(
               innerRect, Color.Transparent, Color.Transparent, mode))
                    {
                        Color[] colors = new Color[2];
                        colors[0] = Color.FromArgb(255, 245, 245, 245);
                        colors[1] = this.ColorTable.BaseColor;

                        ColorBlend blend = new ColorBlend();
                        blend.Positions =
                            new float[] { 0.0f, 1.0f };
                        blend.Colors = colors;
                        brush.InterpolationColors = blend;
                        g.PixelOffsetMode = PixelOffsetMode.Half;
                        g.FillPath(brush, path1);
                        g.PixelOffsetMode = PixelOffsetMode.Default;
                    }
                }

            DRAWBORDER:
                using (Pen pen = new Pen(borderColor))
                {
                    if (Multiline)
                    {
                        g.DrawPath(pen, path);
                    }
                    {
                        g.DrawLines(pen, path.PathPoints);
                    }
                    g.DrawLine(new Pen(this.ColorTable.BaseColor), outerRect.Left, outerRect.Bottom, outerRect.Right, outerRect.Bottom);
                }
            }
        }

        private bool DrawTabImage(Graphics g, TabPage page, Rectangle rect)
        {
            bool hasImage = false;
            if (ImageList != null)
            {
                Image image = null;
                if (page.ImageIndex != -1)
                {
                    image = ImageList.Images[page.ImageIndex];
                }
                else if (page.ImageKey != null)
                {
                    image = ImageList.Images[page.ImageKey];
                }

                if (image != null)
                {
                    hasImage = true;
                    Rectangle destRect = Rectangle.Empty;
                    Rectangle srcRect = new Rectangle(Point.Empty, image.Size);
                    switch (Alignment)
                    {
                        case TabAlignment.Top:
                        case TabAlignment.Bottom:
                            destRect = new Rectangle(
                                 rect.X + Radius / 2 + 2,
                                 rect.Y + 2,
                                 rect.Height - 4,
                                 rect.Height - 4);
                            break;
                        case TabAlignment.Left:
                            destRect = new Rectangle(
                                rect.X + 2,
                                rect.Bottom - (rect.Width - 4) - Radius / 2 - 2,
                                rect.Width - 4,
                                rect.Width - 4);
                            break;
                        case TabAlignment.Right:
                            destRect = new Rectangle(
                                rect.X + 2,
                                rect.Y + Radius / 2 + 2,
                                rect.Width - 4,
                                rect.Width - 4);
                            break;
                    }

                    g.DrawImage(
                        image,
                        destRect,
                        srcRect,
                        GraphicsUnit.Pixel);
                }
            }
            return hasImage;
        }

        private GraphicsPath CreateTabPath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            switch (Alignment)
            {
                case TabAlignment.Top:
                    rect.X++;
                    rect.Width -= 2;
                    path.AddLine(
                        rect.X,
                        rect.Bottom,
                        rect.X,
                        rect.Y + Radius / 2);
                    path.AddArc(
                        rect.X,
                        rect.Y,
                        Radius,
                        Radius,
                        180F,
                        90F);
                    path.AddArc(
                        rect.Right - Radius,
                        rect.Y,
                        Radius,
                        Radius,
                        270F,
                        90F);
                    path.AddLine(
                        rect.Right,
                        rect.Y + Radius / 2,
                        rect.Right,
                        rect.Bottom);
                    break;
                case TabAlignment.Bottom:
                    rect.X++;
                    rect.Width -= 2;
                    path.AddLine(
                        rect.X,
                        rect.Y,
                        rect.X,
                        rect.Bottom - Radius / 2);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - Radius,
                        Radius,
                        Radius,
                        180,
                        -90);
                    path.AddArc(
                        rect.Right - Radius,
                        rect.Bottom - Radius,
                        Radius,
                        Radius,
                        90,
                        -90);
                    path.AddLine(
                        rect.Right,
                        rect.Bottom - Radius / 2,
                        rect.Right,
                        rect.Y);

                    break;
                case TabAlignment.Left:
                    rect.Y++;
                    rect.Height -= 2;
                    path.AddLine(
                        rect.Right,
                        rect.Y,
                        rect.X + Radius / 2,
                        rect.Y);
                    path.AddArc(
                        rect.X,
                        rect.Y,
                        Radius,
                        Radius,
                        270F,
                        -90F);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - Radius,
                        Radius,
                        Radius,
                        180F,
                        -90F);
                    path.AddLine(
                        rect.X + Radius / 2,
                        rect.Bottom,
                        rect.Right,
                        rect.Bottom);
                    break;
                case TabAlignment.Right:
                    rect.Y++;
                    rect.Height -= 2;
                    path.AddLine(
                        rect.X,
                        rect.Y,
                        rect.Right - Radius / 2,
                        rect.Y);
                    path.AddArc(
                        rect.Right - Radius,
                        rect.Y,
                        Radius,
                        Radius,
                        270F,
                        90F);
                    path.AddArc(
                        rect.Right - Radius,
                        rect.Bottom - Radius,
                        Radius,
                        Radius,
                        0F,
                        90F);
                    path.AddLine(
                        rect.Right - Radius / 2,
                        rect.Bottom,
                        rect.X,
                        rect.Bottom);
                    break;
            }
            path.CloseFigure();
            return path;
        }

        private Color GetColor(Color colorBase, int a, int r, int g, int b)
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;

            if (a + a0 > 255) { a = 255; } else { a = Math.Max(a + a0, 0); }
            if (r + r0 > 255) { r = 255; } else { r = Math.Max(r + r0, 0); }
            if (g + g0 > 255) { g = 255; } else { g = Math.Max(g + g0, 0); }
            if (b + b0 > 255) { b = 255; } else { b = Math.Max(b + b0, 0); }

            return Color.FromArgb(a, r, g, b);
        }

        #endregion

        #region UpDownButtonNativeWindow

        private class UpDownButtonNativeWindow : NativeWindow, IDisposable
        {
            private TabControlEx _owner;
            private bool _bPainting;

            public UpDownButtonNativeWindow(TabControlEx owner)
                : base()
            {
                _owner = owner;
                base.AssignHandle(owner.UpDownButtonHandle);
            }

            private bool LeftKeyPressed()
            {
                if (SystemInformation.MouseButtonsSwapped)
                {
                    return (NativeMethods.GetKeyState(NativeMethods.VK_RBUTTON) < 0);
                }
                else
                {
                    return (NativeMethods.GetKeyState(NativeMethods.VK_LBUTTON) < 0);
                }
            }

            private void DrawUpDownButton()
            {
                bool mouseOver = false;
                bool mousePress = LeftKeyPressed();
                bool mouseInUpButton = false;

                NativeMethods.RECT rect = new NativeMethods.RECT();

                NativeMethods.GetClientRect(base.Handle, ref rect);

                Rectangle clipRect = Rectangle.FromLTRB(
                    rect.Top, rect.Left, rect.Right, rect.Bottom);

                Point cursorPoint = new Point();
                NativeMethods.GetCursorPos(ref cursorPoint);
                NativeMethods.GetWindowRect(base.Handle, ref rect);

                mouseOver = NativeMethods.PtInRect(ref rect, cursorPoint);

                cursorPoint.X -= rect.Left;
                cursorPoint.Y -= rect.Top;

                mouseInUpButton = cursorPoint.X < clipRect.Width / 2;

                using (Graphics g = Graphics.FromHwnd(base.Handle))
                {
                    UpDownButtonPaintEventArgs e =
                        new UpDownButtonPaintEventArgs(
                        g,
                        clipRect,
                        mouseOver,
                        mousePress,
                        mouseInUpButton);
                    _owner.OnPaintUpDownButton(e);
                }
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case NativeMethods.WM_PAINT:
                        if (!_bPainting)
                        {
                            NativeMethods.PAINTSTRUCT ps =
                                new NativeMethods.PAINTSTRUCT();
                            _bPainting = true;
                            NativeMethods.BeginPaint(m.HWnd, ref ps);
                            DrawUpDownButton();
                            NativeMethods.EndPaint(m.HWnd, ref ps);
                            _bPainting = false;
                            m.Result = NativeMethods.TRUE;
                        }
                        else
                        {
                            base.WndProc(ref m);
                        }
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }

            #region IDisposable 成员

            public void Dispose()
            {
                _owner = null;
                base.ReleaseHandle();
            }

            #endregion
        }

        #endregion
 
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.TCM_ADJUSTRECT)
            {
                NativeMethods.RECT rect = (NativeMethods.RECT)(m.GetLParam(typeof(NativeMethods.RECT)));
                //Adjust these values to suit, dependant upon Appearance
                rect.Left += 1;
                rect.Right -= 1;
                rect.Top += this.HeaderHeight + 3;
                rect.Bottom -= 1;
                Marshal.StructureToPtr(rect, m.LParam, true);
            }
            else
            {
                base.WndProc(ref m);
            } 
        }
    }
}
