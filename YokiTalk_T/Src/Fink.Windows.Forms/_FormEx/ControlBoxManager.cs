﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    internal abstract class ControlBoxManager : IDisposable
    {
        private FormEx _owner;

        public FormEx Owner
        {
            get { return _owner; }
        }
        private bool _mouseDown;
        private ControlBoxState _closBoxState;
        private ControlBoxState _minimizeBoxState;
        private ControlBoxState _maximizeBoxState;

        public ControlBoxManager(FormEx owner)
        {
            _owner = owner;
        }

        public bool CloseBoxVisibale
        {
            get { return _owner.ControlBox; }
        }

        public bool MaximizeBoxVisibale
        {
            get { return _owner.ControlBox && _owner.MaximizeBox; }
        }

        public bool MinimizeBoxVisibale
        {
            get { return _owner.ControlBox && _owner.MinimizeBox; }
        }

        public abstract Rectangle CloseBoxRect
        {
            get;
        }

        public abstract Rectangle MaximizeBoxRect
        {
            get;
        }

        public abstract Rectangle MinimizeBoxRect
        {
            get;
        }

        public ControlBoxState CloseBoxState
        {
            get { return _closBoxState; }
            protected set
            {
                if (_closBoxState != value)
                {
                    _closBoxState = value;
                    if (_owner != null)
                    {
                        Invalidate(CloseBoxRect);
                        Invalidate(MinimizeBoxRect);
                        Invalidate(MaximizeBoxRect);
                    }
                }
            }
        }

        public ControlBoxState MinimizeBoxState
        {
            get { return _minimizeBoxState; }
            protected set
            {
                if (_minimizeBoxState != value)
                {
                    _minimizeBoxState = value;
                    if (_owner != null)
                    {
                        Invalidate(MinimizeBoxRect);
                        Invalidate(CloseBoxRect);
                        Invalidate(MaximizeBoxRect);
                    }
                }
            }
        }

        public ControlBoxState MaximizeBoxState
        {
            get { return _maximizeBoxState; }
            protected set
            {
                if (_maximizeBoxState != value)
                {
                    _maximizeBoxState = value;
                    if (_owner != null)
                    {
                        Invalidate(MaximizeBoxRect);
                        Invalidate(CloseBoxRect);
                        Invalidate(MinimizeBoxRect);
                    }
                }
            }
        }

        internal Point ControlBoxOffset
        {
            get { return _owner.ControlBoxOffset; }
        }

        internal int ControlBoxSpace
        {
            get { return _owner.ControlBoxSpace; }
        }

        public void ProcessMouseOperate(
            Point mousePoint, MouseOperate operate)
        {
            if (!_owner.ControlBox)
            {
                return;
            }

            Rectangle closeBoxRect = CloseBoxRect;
            Rectangle minimizeBoxRect = MinimizeBoxRect;
            Rectangle maximizeBoxRect = MaximizeBoxRect;

            bool closeBoxVisibale = CloseBoxVisibale;
            bool minimizeBoxVisibale = MinimizeBoxVisibale;
            bool maximizeBoxVisibale = MaximizeBoxVisibale;

            switch (operate)
            {
                case MouseOperate.Move:
                    ProcessMouseMove(
                        mousePoint,
                        closeBoxRect,
                        minimizeBoxRect,
                        maximizeBoxRect,
                        closeBoxVisibale,
                        minimizeBoxVisibale,
                        maximizeBoxVisibale);
                    break;
                case MouseOperate.Down:
                    ProcessMouseDown(
                        mousePoint,
                        closeBoxRect,
                        minimizeBoxRect,
                        maximizeBoxRect,
                        closeBoxVisibale,
                        minimizeBoxVisibale,
                        maximizeBoxVisibale);
                    break;
                case MouseOperate.Up:
                    ProcessMouseUP(
                        mousePoint,
                        closeBoxRect,
                        minimizeBoxRect,
                        maximizeBoxRect,
                        closeBoxVisibale,
                        minimizeBoxVisibale,
                        maximizeBoxVisibale);
                    break;
                case MouseOperate.Leave:
                    ProcessMouseLeave(
                        closeBoxVisibale,
                        minimizeBoxVisibale,
                        maximizeBoxVisibale);
                    break;
                case MouseOperate.Hover:
                    break;
            }
        }

        private void ProcessMouseMove(
            Point mousePoint,
            Rectangle closeBoxRect,
            Rectangle minimizeBoxRect,
            Rectangle maximizeBoxRect,
            bool closeBoxVisibale,
            bool minimizeBoxVisibale,
            bool maximizeBoxVisibale)
        {
            string toolTip = string.Empty;
            bool hide = true;
            if (closeBoxVisibale)
            {
                if (closeBoxRect.Contains(mousePoint))
                {
                    hide = false;
                    if (!_mouseDown)
                    {
                        if (CloseBoxState != ControlBoxState.Hover)
                        {
                            toolTip = "Close";
                        }
                        CloseBoxState = ControlBoxState.Hover;
                    }
                    else
                    {
                        if (CloseBoxState == ControlBoxState.PressedLeave)
                        {
                            CloseBoxState = ControlBoxState.Pressed;
                        }
                    }
                }
                else
                {
                    if (!_mouseDown)
                    {
                        CloseBoxState = ControlBoxState.Normal;
                    }
                    else
                    {
                        if (CloseBoxState == ControlBoxState.Pressed)
                        {
                            CloseBoxState = ControlBoxState.PressedLeave;
                        }
                    }
                }
            }

            if (minimizeBoxVisibale)
            {
                if (minimizeBoxRect.Contains(mousePoint))
                {
                    hide = false;
                    if (!_mouseDown)
                    {
                        if (MinimizeBoxState != ControlBoxState.Hover)
                        {
                            toolTip = "Minimize";
                        }
                        MinimizeBoxState = ControlBoxState.Hover;
                    }
                    else
                    {
                        if (MinimizeBoxState == ControlBoxState.PressedLeave)
                        {
                            MinimizeBoxState = ControlBoxState.Pressed;
                        }
                    }
                }
                else
                {
                    if (!_mouseDown)
                    {
                        MinimizeBoxState = ControlBoxState.Normal;
                    }
                    else
                    {
                        if (MinimizeBoxState == ControlBoxState.Pressed)
                        {
                            MinimizeBoxState = ControlBoxState.PressedLeave;
                        }
                    }
                }
            }

            if (maximizeBoxVisibale)
            {
                if (maximizeBoxRect.Contains(mousePoint))
                {
                    hide = false;
                    if (!_mouseDown)
                    {
                        if (MaximizeBoxState != ControlBoxState.Hover)
                        {
                            bool maximize = 
                                _owner.WindowState == FormWindowState.Maximized;
                            toolTip = maximize ? "Restore Down" : "Maximize";
                        }
                        MaximizeBoxState = ControlBoxState.Hover;
                    }
                    else
                    {
                        if (MaximizeBoxState == ControlBoxState.PressedLeave)
                        {
                            MaximizeBoxState = ControlBoxState.Pressed;
                        }
                    }
                }
                else
                {
                    if (!_mouseDown)
                    {
                        MaximizeBoxState = ControlBoxState.Normal;
                    }
                    else
                    {
                        if (MaximizeBoxState == ControlBoxState.Pressed)
                        {
                            MaximizeBoxState = ControlBoxState.PressedLeave;
                        }
                    }
                }
            }

            if (toolTip != string.Empty)
            {
                HideToolTip();
                ShowTooTip(toolTip);
            }

            if (hide)
            {
                HideToolTip();
            }
        }

        private void ProcessMouseDown(
            Point mousePoint,
            Rectangle closeBoxRect,
            Rectangle minimizeBoxRect,
            Rectangle maximizeBoxRect,
            bool closeBoxVisibale,
            bool minimizeBoxVisibale,
            bool maximizeBoxVisibale)
        {
            _mouseDown = true;

            if (closeBoxVisibale)
            {
                if (closeBoxRect.Contains(mousePoint))
                {
                    CloseBoxState = ControlBoxState.Pressed;
                    return;
                }
            }

            if (minimizeBoxVisibale)
            {
                if (minimizeBoxRect.Contains(mousePoint))
                {
                    MinimizeBoxState = ControlBoxState.Pressed;
                    return;
                }
            }

            if (maximizeBoxVisibale)
            {
                if (maximizeBoxRect.Contains(mousePoint))
                {
                    MaximizeBoxState = ControlBoxState.Pressed;
                    return;
                }
            }
        }

        private void ProcessMouseUP(
            Point mousePoint, 
            Rectangle closeBoxRect,
            Rectangle minimizeBoxRect,
            Rectangle maximizeBoxRect, 
            bool closeBoxVisibale, 
            bool minimizeBoxVisibale, 
            bool maximizeBoxVisibale)
        {
            _mouseDown = false;

            if (closeBoxVisibale)
            {
                if (closeBoxRect.Contains(mousePoint))
                {
                    if (CloseBoxState == ControlBoxState.Pressed)
                    {
                        _owner.Close();
                        CloseBoxState = ControlBoxState.Normal;
                        return;
                    }
                }
                CloseBoxState = ControlBoxState.Normal;
            }

            if (minimizeBoxVisibale)
            {
                if (minimizeBoxRect.Contains(mousePoint))
                {
                    if (MinimizeBoxState == ControlBoxState.Pressed)
                    {
                        _owner.WindowState = FormWindowState.Minimized;
                        MinimizeBoxState = ControlBoxState.Normal;
                        return;
                    }
                }
                MinimizeBoxState = ControlBoxState.Normal;
            }

            if (maximizeBoxVisibale)
            {
                if (maximizeBoxRect.Contains(mousePoint))
                {
                    if (MaximizeBoxState == ControlBoxState.Pressed)
                    {
                        bool maximize =
                            _owner.WindowState == FormWindowState.Maximized;
                        if (maximize)
                        {
                            _owner.WindowState = FormWindowState.Normal;
                        }
                        else
                        {
                            _owner.WindowState = FormWindowState.Maximized;
                        }

                        MaximizeBoxState = ControlBoxState.Normal;
                        return;
                    }
                }
                MaximizeBoxState = ControlBoxState.Normal;
            }
        }

        private void ProcessMouseLeave(
            bool closeBoxVisibale,
            bool minimizeBoxVisibale,
            bool maximizeBoxVisibale)
        {
            if (closeBoxVisibale)
            {
                if (CloseBoxState == ControlBoxState.Pressed)
                {
                    CloseBoxState = ControlBoxState.PressedLeave;
                }
                else
                {
                    CloseBoxState = ControlBoxState.Normal;
                }
            }

            if (minimizeBoxVisibale)
            {
                if (MinimizeBoxState == ControlBoxState.Pressed)
                {
                    MinimizeBoxState = ControlBoxState.PressedLeave;
                }
                else
                {
                    MinimizeBoxState = ControlBoxState.Normal;
                }
            }

            if (maximizeBoxVisibale)
            {
                if (MaximizeBoxState == ControlBoxState.Pressed)
                {
                    MaximizeBoxState = ControlBoxState.PressedLeave;
                }
                else
                {
                    MaximizeBoxState = ControlBoxState.Normal;
                }
            }

            HideToolTip();
        }

        internal void Invalidate(Rectangle rect)
        {
            _owner.Invalidate(rect);
        }

        private void ShowTooTip(string toolTipText)
        {
            if (_owner != null)
            {
                _owner.ToolTip.Active = true;
                _owner.ToolTip.SetToolTip(_owner, toolTipText);
            }
        }

        private void HideToolTip()
        {
            if (_owner != null)
            {
                _owner.ToolTip.Active = false;
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _owner = null;
        }

        #endregion
    }
}