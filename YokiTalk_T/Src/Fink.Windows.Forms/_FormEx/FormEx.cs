using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Fink.Drawing;
using System.Drawing.Drawing2D;

namespace Fink.Windows.Forms
{
    public enum FromStyle
    {
        Mac,
        Metro
    }

    public enum ThemeType
    {
        Light,
        Dark
    }

    public class FormEx : Form
    {
        #region Fields

        private FormExRenderer _renderer;
        private RoundStyle _roundStyle = RoundStyle.None;
        private int _radius = 3;
        private int _captionHeight = 44;
        private Font _captionFont = SystemFonts.CaptionFont;
        private int _borderWidth = 1;
        private Size _minimizeBoxSize = new Size(40, 24);
        private Size _maximizeBoxSize = new Size(40, 24);
        private Size _closeBoxSize = new Size(56, 24);
        private Point _controlBoxOffset = new Point(6, 1);
        private int _controlBoxSpace = -1;
        private bool _active;
        private ControlBoxManager _controlBoxManager;
        private Padding _padding;
        private bool _canResize = true;
        private bool _inPosChanged;
        private ToolTip _toolTip;
        private bool _isDialog;
        private bool _closeBox = true;


        private static readonly object EventRendererChanged = new object();

        #endregion

        #region Constructors

        private FormEx()
            : base()
        {
            
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            }

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;


            _minimizeBoxSize = new Size(46, 31);
            _maximizeBoxSize = new Size(46, 31);
            _closeBoxSize = new Size(46, 31);
            _controlBoxOffset = new Point(6, 1);
            _controlBoxSpace = -1;
            this.Renderer = new MetroFormExRenderer(new MetroFormExColorTable());
            this.ControlBoxManager = new MetroControlBoxManager(this);
            
            SetStyles();

            Init();

            this.Load += (o, e) =>
            {
                this._storedWidth = this.Width;
                this._storedHeight = this.Height;
            };
        }

        public FormEx(FromStyle formStyle = FromStyle.Mac)
            : this()
        {
            switch (formStyle)
            {
                case FromStyle.Mac:
                    _minimizeBoxSize = new Size(16, 16);
                    _maximizeBoxSize = new Size(16, 16);
                    _closeBoxSize = new Size(16, 16);
                    _controlBoxOffset = new Point(13, 9);
                    _controlBoxSpace = 4;
                    this.ShowIcon = false;
                    this.Renderer = new MacFormExRenderer(new MacFormExColorTable());
                    this.ControlBoxManager = new MacControlBoxManager(this);
                    break;
                case FromStyle.Metro:
                    _minimizeBoxSize = new Size(46, 31);
                    _maximizeBoxSize = new Size(46, 31);
                    _closeBoxSize = new Size(46, 31);
                    _controlBoxOffset = new Point(6, 1);
                    _controlBoxSpace = -1;
                    this.Renderer = new MetroFormExRenderer(new MetroFormExColorTable());
                    this.ControlBoxManager = new MetroControlBoxManager(this);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Events

        public event EventHandler RendererChangled
        {
            add { base.Events.AddHandler(EventRendererChanged, value); }
            remove { base.Events.RemoveHandler(EventRendererChanged, value); }
        }

        #endregion

        #region Properties

        [DefaultValue(typeof(bool), "false")]
        public bool IsDialog
        {
            get { return _isDialog; }
            set { _isDialog = value; }
        }


        private ThemeType _themeType = ThemeType.Light;
        [DefaultValue(typeof(ThemeType), "Light")]
        public ThemeType ThemeType
        {
            get { return _themeType; }
            set
            {
                if (_themeType != value)
                {
                    _themeType = value;
                    SetReion();
                    base.Invalidate();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FormExRenderer Renderer
        {
            get
            {
                return _renderer;
            }
            set
            {
                _renderer = value;
                OnRendererChanged(EventArgs.Empty);
            }
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                base.Invalidate(new Rectangle(
                    0,
                    0,
                    Width,
                    CaptionHeight + 1));
            }
        }

        [DefaultValue(typeof(int), "64")]
        public int TitleOffset
        {
            get;
            set;
        }

        [DefaultValue(typeof(RoundStyle), "1")]
        public RoundStyle RoundStyle
        {
            get { return _roundStyle; }
            set
            {
                if (_roundStyle != value)
                {
                    _roundStyle = value;
                    SetReion();
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(8)]
        public int Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    _radius = value < 4 ? 4 : value;
                    SetReion();
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(31)]
        public int CaptionHeight
        {
            get { return _captionHeight; }
            set
            {
                if (_captionHeight != value)
                {
                    _captionHeight = value < _borderWidth ?
                                    _borderWidth : value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(1)]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                if (_borderWidth != value)
                {
                    _borderWidth = value < 1 ? 1 : value;
                }
            }
        }

        [DefaultValue(typeof(Font), "CaptionFont")]
        public Font CaptionFont
        {
            get { return _captionFont; }
            set
            {
                if (value == null)
                {
                    _captionFont = SystemFonts.CaptionFont;
                }
                else
                {
                    _captionFont = value;
                }
                base.Invalidate(CaptionRect);
            }
        }


        [DefaultValue(typeof(bool), "true")]
        public bool CloseBox
        {
            get { return _closeBox; }
            set
            {
                if (_closeBox != value)
                {
                    _closeBox = value;
                    base.Invalidate();
                }
            }
        }



        [DefaultValue(typeof(Size), "46, 31")]
        public Size MinimizeBoxSize
        {
            get { return _minimizeBoxSize; }
            set
            {
                if (_minimizeBoxSize != value)
                {
                    _minimizeBoxSize = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Size), "46, 31")]
        public Size MaximizeBoxSize
        {
            get { return _maximizeBoxSize; }
            set
            {
                if (_maximizeBoxSize != value)
                {
                    _maximizeBoxSize = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Size), "46, 31")]
        public Size CloseBoxSize
        {
            get { return _closeBoxSize; }
            set
            {
                if (_closeBoxSize != value)
                {
                    _closeBoxSize = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Point), "6, 0")]
        public Point ControlBoxOffset
        {
            get { return _controlBoxOffset; }
            set
            {
                if (_controlBoxOffset != value)
                {
                    _controlBoxOffset = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(-1)]
        public int ControlBoxSpace
        {
            get { return _controlBoxSpace; }
            set
            {
                if (_controlBoxSpace != value)
                {
                    _controlBoxSpace = value < 0 ? 0 : value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(true)]
        public bool CanResize
        {
            get { return _canResize; }
            set { _canResize = value; }
        }

        [DefaultValue(typeof(Padding), "0")]
        public new Padding Padding
        {
            get { return _padding; }
            set
            {
                _padding = value;
                base.Padding = new Padding(
                    BorderWidth + _padding.Left,
                    CaptionHeight + _padding.Top,
                    BorderWidth + _padding.Right,
                    BorderWidth + _padding.Bottom);
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = FormBorderStyle.None; }
        }

        protected override Padding DefaultPadding
        {
            get
            {
                return new Padding(
                    BorderWidth,
                    CaptionHeight,
                    BorderWidth,
                    BorderWidth);
            }
        }


        internal Rectangle CaptionRect
        {
            get { return new Rectangle(0, 0, Width, CaptionHeight); }
        }

        internal ControlBoxManager ControlBoxManager
        {
            get
            {
                return _controlBoxManager;
            }
            set
            {
                this._controlBoxManager = value;
                OnRendererChanged(EventArgs.Empty);
            }
        }

        internal Rectangle IconRect
        {
            get
            {
                if (base.ShowIcon && base.Icon != null)
                {
                    int width = SystemInformation.SmallIconSize.Width;
                    int height = SystemInformation.SmallIconSize.Height;
                    if (CaptionHeight - BorderWidth - 4 < width)
                    {
                        width = CaptionHeight - BorderWidth - 4;
                    }
                    return new Rectangle(
                        BorderWidth + 5,
                        BorderWidth + (CaptionHeight - BorderWidth - height) / 2,
                        width,
                        width);
                }
                return Rectangle.Empty;
            }
        }

        internal ToolTip ToolTip
        {
            get { return _toolTip; }
        }

        #endregion

        #region Override Methods

        protected virtual void OnRendererChanged(EventArgs e)
        {
            Renderer.InitFormEx(this);
            EventHandler handler =
                base.Events[EventRendererChanged] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
            base.Invalidate();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetReion();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            SetReion();
            base.OnSizeChanged(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            ControlBoxManager.ProcessMouseOperate(
                e.Location, MouseOperate.Move);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            ControlBoxManager.ProcessMouseOperate(
                e.Location, MouseOperate.Down);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            ControlBoxManager.ProcessMouseOperate(
                e.Location, MouseOperate.Up);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ControlBoxManager.ProcessMouseOperate(
                Point.Empty, MouseOperate.Leave);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            ControlBoxManager.ProcessMouseOperate(
                PointToClient(MousePosition), MouseOperate.Hover);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Rectangle rect = ClientRectangle;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            FormExRenderer renderer = Renderer;

            renderer.DrawFormExBackground(
                new FormExBackgroundRenderEventArgs(
                this, g, rect));

            //renderer.DrawFormExInnerBorder(
            //    new FormExBorderRenderEventArgs(
            //    this, g, rect, _active));

            renderer.DrawFormExBackgroundSub(
                new FormExBackgroundRenderEventArgs(
                this, g, rect));

            renderer.DrawFormExBorder(
                new FormExBorderRenderEventArgs(
                this, g, rect, _active));

            renderer.DrawFormExCaption(
                new FormExCaptionRenderEventArgs(
                this, g, CaptionRect, _active));

            if (ControlBoxManager.CloseBoxVisibale)
            {
                renderer.DrawFormExControlBox(
                    new FormExControlBoxRenderEventArgs(
                    this,
                    g,
                    ControlBoxManager.CloseBoxRect,
                    _active,
                    ControlBoxStyle.Close,
                    ControlBoxManager.CloseBoxState));
            }

            if (ControlBoxManager.MaximizeBoxVisibale)
            {
                renderer.DrawFormExControlBox(
                    new FormExControlBoxRenderEventArgs(
                    this,
                    g,
                    ControlBoxManager.MaximizeBoxRect,
                    _active,
                    ControlBoxStyle.Maximize,
                    ControlBoxManager.MaximizeBoxState));
            }

            if (ControlBoxManager.MinimizeBoxVisibale)
            {
                renderer.DrawFormExControlBox(
                    new FormExControlBoxRenderEventArgs(
                    this,
                    g,
                    ControlBoxManager.MinimizeBoxRect,
                    _active,
                    ControlBoxStyle.Minimize,
                    ControlBoxManager.MinimizeBoxState));
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                if (!DesignMode)
                {
                    cp.Style |= (int)NativeMethods.WindowStyle.WS_THICKFRAME;

                    if (ControlBox)
                    {
                        cp.Style |= (int)NativeMethods.WindowStyle.WS_SYSMENU;
                    }

                    if (MinimizeBox)
                    {
                        cp.Style |= (int)NativeMethods.WindowStyle.WS_MINIMIZEBOX;
                    }

                    if (!MaximizeBox)
                    {
                        cp.Style &= ~(int)NativeMethods.WindowStyle.WS_MAXIMIZEBOX;
                    }

                    if (_inPosChanged)
                    {
                        cp.Style &= ~((int)NativeMethods.WindowStyle.WS_THICKFRAME |
                            (int)NativeMethods.WindowStyle.WS_SYSMENU);
                        cp.ExStyle &= ~((int)NativeMethods.WindowStyleEx.WS_EX_DLGMODALFRAME |
                            (int)NativeMethods.WindowStyleEx.WS_EX_WINDOWEDGE);
                    }
                }
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)NativeMethods.WindowMessages.WM_NCHITTEST:
                    WmNcHitTest(ref m);
                    break;
                case (int)NativeMethods.WindowMessages.WM_NCPAINT:
                    break;
                case (int)NativeMethods.WindowMessages.WM_NCCALCSIZE:
                    WndProcNonClientCalcSize(ref m);
                    break;
                case (int)NativeMethods.WindowMessages.WM_SYSCOMMAND:
                    WndProcSysCommand(ref m);
                    break;
                case (int)NativeMethods.WindowMessages.WM_WINDOWPOSCHANGED:
                    _inPosChanged = true;
                    base.WndProc(ref m);
                    _inPosChanged = false;
                    break;
                case (int)NativeMethods.WindowMessages.WM_GETMINMAXINFO:

                    WmGetMinMaxInfo(ref m);
                    //for reshown after size changed 's bug
                    this._storedHeight = this.Height;
                    this._storedWidth = this.Width;
                    break;
                case (int)NativeMethods.WindowMessages.WM_NCACTIVATE:
                    WmNcActive(ref m);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        private void WndProcNonClientCalcSize(ref Message m)
        {
            if (m.WParam == NativeMethods.FALSE)
            {
                NativeMethods.RECT ncRect = (NativeMethods.RECT)m.GetLParam(typeof(NativeMethods.RECT));
                Rectangle rect = new Rectangle(ncRect.Rect.Location, new Size(ncRect.Rect.Width - 16, ncRect.Rect.Height - 39));
                Marshal.StructureToPtr(rect, m.LParam, true);

                m.Result = NativeMethods.TRUE;
            }
            else if (m.WParam == NativeMethods.TRUE)
            {
                NativeMethods.NCCALCSIZE_PARAMS ncParams = (NativeMethods.NCCALCSIZE_PARAMS)m.GetLParam(typeof(NativeMethods.NCCALCSIZE_PARAMS));
                Rectangle proposed = ncParams.rectProposed.Rect;
                ncParams.rectProposed = NativeMethods.RECT.FromRectangle(proposed);
                ncParams.rectBeforeMove = NativeMethods.RECT.FromRectangle(ncParams.rectProposed.Rect);
                
                Marshal.StructureToPtr(ncParams, m.LParam, false);

                m.Result = NativeMethods.TRUE;
            }
        }

        private int _storedWidth = 0;
        private int _storedHeight = 0;
        private const int SC_RESTORE = 0xF120;
        private void WndProcSysCommand(ref Message m)
        {
            UInt32 param;
            if (IntPtr.Size == 4)
                param = (UInt32)(m.WParam.ToInt32());
            else
                param = (UInt32)(m.WParam.ToInt64());

            if ((param & 0xFFF0) == (int)SC_RESTORE)
            {
                this.Height = this._storedHeight;
                this.Width = this._storedWidth;
            }
            else //if (this.WindowState == FormWindowState.Normal)
            {
                this._storedHeight = this.Height;
                this._storedWidth = this.Width;
            }
            base.WndProc(ref m);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (_controlBoxManager != null)
                {
                    _controlBoxManager.Dispose();
                    _controlBoxManager = null;
                }

                _renderer = null;
                _toolTip.Dispose();
            }
        }

        #endregion

        #region WinAPI override
        private const int ALT = 0xA4;
        private const int EXTENDEDKEY = 0x1;
        private const int KEYUP = 0x2;
        private const uint Restore = 9;

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();


        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        };

        [DllImport("DwmApi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(
            IntPtr hwnd,
            ref MARGINS pMarInset);

        #endregion

        public static void ActivateWindow(IntPtr mainWindowHandle)
        {
            //check if already has focus
            if (mainWindowHandle == GetForegroundWindow()) return;

            //check if window is minimized
            if (IsIconic(mainWindowHandle))
            {
                ShowWindow(mainWindowHandle, Restore);
            }

            // Simulate a key press
            keybd_event((byte)ALT, 0x45, EXTENDEDKEY | 0, 0);

            //SetForegroundWindow(mainWindowHandle);

            // Simulate a key release
            keybd_event((byte)ALT, 0x45, EXTENDEDKEY | KEYUP, 0);

            SetForegroundWindow(mainWindowHandle);
        }

        #region Message Methods

        private void WmNcHitTest(ref Message m)
        {
            int wparam = m.LParam.ToInt32();
            Point point = new Point(
                NativeMethods.LOWORD(wparam),
                NativeMethods.HIWORD(wparam));
            point = PointToClient(point);

            if (IconRect.Contains(point))
            {
                m.Result = new IntPtr(
                    (int)NativeMethods.NCHITTEST.HTSYSMENU);
                return;
            }

            int handleWidth = 10;

            if (_canResize)
            {
                if (point.X < handleWidth && point.Y < handleWidth)
                {
                    m.Result = new IntPtr(
                        (int)NativeMethods.NCHITTEST.HTTOPLEFT);
                    return;
                }

                if (point.X > Width - handleWidth && point.Y < handleWidth)
                {
                    m.Result = new IntPtr(
                        (int)NativeMethods.NCHITTEST.HTTOPRIGHT);
                    return;
                }

                if (point.X < handleWidth && point.Y > Height - handleWidth)
                {
                    m.Result = new IntPtr(
                        (int)NativeMethods.NCHITTEST.HTBOTTOMLEFT);
                    return;
                }

                if (point.X > Width - handleWidth && point.Y > Height - handleWidth)
                {
                    m.Result = new IntPtr(
                        (int)NativeMethods.NCHITTEST.HTBOTTOMRIGHT);
                    return;
                }

                if (point.Y < handleWidth)
                {
                    m.Result = new IntPtr(
                        (int)NativeMethods.NCHITTEST.HTTOP);
                    return;
                }

                if (point.Y > Height - handleWidth)
                {
                    m.Result = new IntPtr(
                        (int)NativeMethods.NCHITTEST.HTBOTTOM);
                    return;
                }

                if (point.X < handleWidth)
                {
                    m.Result = new IntPtr(
                       (int)NativeMethods.NCHITTEST.HTLEFT);
                    return;
                }

                if (point.X > Width - handleWidth)
                {
                    m.Result = new IntPtr(
                       (int)NativeMethods.NCHITTEST.HTRIGHT);
                    return;
                }
            }

            if (point.Y < CaptionHeight)
            {
                if (!ControlBoxManager.CloseBoxRect.Contains(point) &&
                    !ControlBoxManager.MaximizeBoxRect.Contains(point) &&
                    !ControlBoxManager.MinimizeBoxRect.Contains(point))
                {
                    m.Result = new IntPtr(
                      (int)NativeMethods.NCHITTEST.HTCAPTION);
                    return;
                }
            }
            m.Result = new IntPtr(
                     (int)NativeMethods.NCHITTEST.HTCLIENT);
        }

        private void WmGetMinMaxInfo(ref Message m)
        {
            NativeMethods.MINMAXINFO minmax =
                (NativeMethods.MINMAXINFO)Marshal.PtrToStructure(
                m.LParam, typeof(NativeMethods.MINMAXINFO));

            if (MaximumSize != Size.Empty)
            {
                minmax.maxTrackSize = MaximumSize;
            }
            else
            {
                Rectangle rect = Screen.GetWorkingArea(this);

                minmax.maxPosition = new Point(
                    rect.X - BorderWidth,
                    rect.Y);
                minmax.maxTrackSize = new Size(
                    rect.Width + BorderWidth * 2,
                    rect.Height + BorderWidth);
            }

            if (MinimumSize != Size.Empty)
            {
                minmax.minTrackSize = MinimumSize;
            }
            else
            {
                minmax.minTrackSize = new Size(
                    CloseBoxSize.Width + MinimizeBoxSize.Width +
                    MaximizeBoxSize.Width + ControlBoxOffset.X +
                    ControlBoxSpace * 2 + SystemInformation.SmallIconSize.Width +
                    BorderWidth * 2 + 3,
                    CaptionHeight);
            }

            Marshal.StructureToPtr(minmax, m.LParam, false);
        }

        private void WmNcActive(ref Message m)
        {
            if (m.WParam.ToInt32() == 1 || m.WParam.ToInt32() == 2097153)
            {
                _active = true;
            }
            else
            {
                _active = false;
            }
            m.Result = NativeMethods.TRUE;
            base.Invalidate();
        }

        #endregion

        #region Private Methods

        private void SetStyles()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);
            UpdateStyles();
        }

        private void SetReion()
        {
            if (base.Region != null)
            {
                base.Region.Dispose();
            }
            base.Region = Renderer.CreateRegion(this);
        }

        private void Init()
        {
            //MARGINS margins = new MARGINS();
            //margins.cxLeftWidth = -1;
            //margins.cxRightWidth = -1;
            //margins.cyTopHeight = -1;
            //margins.cyBottomHeight = -1;

            //int hr = DwmExtendFrameIntoClientArea(this.Handle, ref margins);

            _toolTip = new ToolTip();
            Renderer.InitFormEx(this);
            base.Padding = DefaultPadding;
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormEx
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "FormEx";
            this.ShowIcon = false;
            this.ResumeLayout(false);

        }

    }
}