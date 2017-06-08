using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    public class TextBoxEx : System.Windows.Forms.TextBox
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowDC(int hwnd);


        [DllImport("user32.dll")]
        private static extern int ReleaseDC(int hwnd, int hdc);


        /// <summary>
        /// 是否启用焦点,默认不启用
        /// </summary>
        private bool _IsFocusHotTrack = true;
        /// <summary>
        /// 获取焦点时背景色
        /// </summary>
        private Color _FocusBackColor = Color.White;
        /// <summary>
        /// 获取焦点时边框色
        /// </summary>
        private Color _FocusBorderColor = Color.FromArgb(255, 56, 180, 75);
        /// <summary>
        /// 失去焦点背景色
        /// </summary>
        private Color _LostBackColor;
        /// <summary>
        /// 失去焦点边框色
        /// </summary>
        private Color _LostBorderColor = Color.Transparent;
        /// <summary>
        /// 边框线宽度
        /// </summary>
        private int _BorderWidth = 1;
        /// <summary>
        /// 组件的父容器
        /// </summary>
        private Panel _ParentContainer = null;

        /// <summary>
        /// 是否启用热点
        /// </summary>
        [Category("行为"),
        Description("获得或设置一个值，指示当鼠标经过控件时控件边框是否发生变化。只在控件的BorderStyle为FixedSingle时有效"),
        DefaultValue(true)]
        public bool FocusHotTrack
        {
            get { return _IsFocusHotTrack; }
            set
            {
                this._IsFocusHotTrack = value;
                //在该值发生变化时重绘控件，下同 
                //在设计模式下，更改该属性时，如果不调用该语句， 
                //则不能立即看到设计试图中该控件相应的变化 
                this.Invalidate();
            }

        }
        /// <summary>
        /// 获取焦点时背景色
        /// </summary>
        public Color HotBackColor
        {
            get { return this._FocusBackColor; }
            set
            {
                this._FocusBackColor = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 获取焦点时边框色
        /// </summary>
        public Color HotBorderColor
        {
            get { return this._FocusBorderColor; }
            set
            {
                this._FocusBorderColor = value;
                this.Invalidate();
            }

        }
        /// <summary>
        /// 失去焦点时背景色
        /// </summary>
        public Color LostBackColor
        {
            get { return this._LostBackColor; }
            set
            {
                this._LostBackColor = value;
                this.Invalidate();
            }

        }
        /// <summary>
        /// 失去焦点时背景色
        /// </summary>
        public Color LostBorderColor
        {
            get { return this._LostBorderColor; }
            set
            {
                this._LostBorderColor = value;
                this.Invalidate();
            }

        }
        /// <summary>
        /// 边框线宽度
        /// </summary>

        [DefaultValue(1)]
        //[EditorBrowsable(EditorBrowsableState.Never),Browsable(false)]
        public int BorderWidth
        {
            get { return this._BorderWidth; }
            set
            {
                this._BorderWidth = value;
                this.Invalidate();
            }

        }
        public TextBoxEx()
            : base()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._LostBackColor = this.BackColor;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetParentContainer();
        }
        /// <summary>
        /// 设置TextBox添加父容器
        /// </summary>
        private void SetParentContainer()
        {
            this._ParentContainer = new Panel();
            this._ParentContainer.Controls.Add(this);
        }
        /// <summary>
        /// Windows消息处理函数
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            //0xf:WM_PRINT 0x133:WM_CTLCOLOREDIT
            if (m.Msg == 0xf || m.Msg == 0x133)
            {
                //ResetBorderColor(m.HWnd);
            }
        }
        /// <summary>
        /// 重绘边框
        /// </summary>
        /// <param name="hWnd"></param>
        internal void ResetBorderColor(IntPtr hWnd)
        {
            //根据颜色和边框像素取得一条线
            System.Drawing.Pen pen = new Pen(this._LostBackColor, this._BorderWidth);
            //得到当前的句柄
            IntPtr hdc = new IntPtr(GetWindowDC(hWnd.ToInt32()));
            if (hdc.ToInt32() == 0)
            {
                return;
            }
            if (pen != null)
            {
                if (_IsFocusHotTrack)
                {
                    if (this.Focused)
                    {
                        pen.Color = this._FocusBorderColor;
                        this.BackColor = this._FocusBackColor;
                    }
                    else
                    {
                        pen.Color = this._LostBorderColor;
                        this.BackColor = this._LostBackColor;
                    }
                    //绘制边框
                    System.Drawing.Graphics g = this.Parent.CreateGraphics();
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.DrawRectangle(pen, this.Location.X - 1, this.Location.Y - 1, this.Size.Width + 1, this.Size.Height + 1);
                    this.CreateGraphics().DrawRectangle(new Pen(pen.Color, 1), 0, 0, this.Size.Width - 1, this.Size.Height - 1);
                    pen.Dispose();
                }

            }
            //释放 
            ReleaseDC(hWnd.ToInt32(), hdc.ToInt32());
        }
        /// <summary>
        /// 获取焦点
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            if (this._IsFocusHotTrack)
            {
                this.Parent.Invalidate();
            }
            base.OnGotFocus(e);
        }
        /// <summary>
        /// 失去焦点
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            if (this._IsFocusHotTrack)
            {
                this.Parent.Invalidate();
            }
            base.OnLostFocus(e);
        }
        /// <summary>
        /// 鼠标进入控件编辑区
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            if (this.Focused)
            {
                this.Parent.Invalidate();
                this.Invalidate();
            }
            base.OnMouseEnter(e);
        }
        /// <summary>
        /// 鼠标离开编辑区
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (this.Focused)
            {
                this.Parent.Invalidate();
            }
            base.OnMouseLeave(e);
        }
    }

}



