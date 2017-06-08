using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Security.Permissions;

namespace Fink.Windows.Forms
{
    public abstract class FormExRenderer
    {
        #region Fields

        private EventHandlerList _events;

        private static readonly object EventRenderFormExCaption = new object();
        private static readonly object EventRenderFormExBorder = new object();
        private static readonly object EventRenderFormExBackground = new object();
        private static readonly object EventRenderFormExControlBox = new object();

        #endregion

        #region Constructors

        protected FormExRenderer()
        {
        }

        #endregion

        #region Properties

        protected EventHandlerList Events
        {
            get
            {
                if (_events == null)
                {
                    _events = new EventHandlerList();
                }
                return _events;
            }
        }

        #endregion

        #region Events

        public event FormExCaptionRenderEventHandler RenderFormExCaption
        {
            add { AddHandler(EventRenderFormExCaption, value); }
            remove { RemoveHandler(EventRenderFormExCaption, value); }
        }

        public event FormExBorderRenderEventHandler RenderFormExBorder
        {
            add { AddHandler(EventRenderFormExBorder, value); }
            remove { RemoveHandler(EventRenderFormExBorder, value); }
        }

        public event FormExBackgroundRenderEventHandler RenderFormExBackground
        {
            add { AddHandler(EventRenderFormExBackground, value); }
            remove { RemoveHandler(EventRenderFormExBackground, value); }
        }

        public event FormExControlBoxRenderEventHandler RenderFormExControlBox
        {
            add { AddHandler(EventRenderFormExControlBox, value); }
            remove { RemoveHandler(EventRenderFormExControlBox, value); }
        }

        #endregion

        #region Public Methods

        public abstract Region CreateRegion(FormEx form);

        public abstract void InitFormEx(FormEx  form);

        public void DrawFormExCaption(
            FormExCaptionRenderEventArgs e)
        {
            OnRenderFormExCaption(e);
            FormExCaptionRenderEventHandler handle =
                Events[EventRenderFormExCaption]
                as FormExCaptionRenderEventHandler;
            if (handle != null)
            {
                handle(this, e);
            }
        }

        public void DrawFormExBorder(
            FormExBorderRenderEventArgs e)
        {
            OnRenderFormExBorder(e);
            FormExBorderRenderEventHandler handle =
                Events[EventRenderFormExBorder]
                as FormExBorderRenderEventHandler;
            if (handle != null)
            {
                handle(this, e);
            }
        }

        public void DrawFormExInnerBorder(
            FormExBorderRenderEventArgs e)
        {
            OnRenderFormExInnerBorder(e);
            FormExBorderRenderEventHandler handle =
                Events[EventRenderFormExBorder]
                as FormExBorderRenderEventHandler;
            if (handle != null)
            {
                handle(this, e);
            }
        }

        public void DrawFormExBackground(
            FormExBackgroundRenderEventArgs e)
        {
            OnRenderFormExBackground(e);
            FormExBackgroundRenderEventHandler handle =
                Events[EventRenderFormExBackground]
                as FormExBackgroundRenderEventHandler;
            if (handle != null)
            {
                handle(this, e);
            }
        }

        public void DrawFormExBackgroundSub(
            FormExBackgroundRenderEventArgs e)
        {
            OnRenderFormExBackgroundSub(e);
            FormExBackgroundRenderEventHandler handle =
                Events[EventRenderFormExBackground]
                as FormExBackgroundRenderEventHandler;
            if (handle != null)
            {
                handle(this, e);
            }
        }

        public void DrawFormExControlBox(
            FormExControlBoxRenderEventArgs e)
        {
            OnRenderFormExControlBox(e);
            FormExControlBoxRenderEventHandler handle =
                Events[EventRenderFormExControlBox]
                as FormExControlBoxRenderEventHandler;
            if (handle != null)
            {
                handle(this, e);
            }
        }

        #endregion

        #region Protected Render Methods

        protected abstract void OnRenderFormExCaption(
            FormExCaptionRenderEventArgs e);

        protected abstract void OnRenderFormExBorder(
            FormExBorderRenderEventArgs e);

        protected abstract void OnRenderFormExInnerBorder(
            FormExBorderRenderEventArgs e);

        protected abstract void OnRenderFormExBackground(
            FormExBackgroundRenderEventArgs e);

        protected abstract void OnRenderFormExBackgroundSub(
            FormExBackgroundRenderEventArgs e);

        protected abstract void OnRenderFormExControlBox(
            FormExControlBoxRenderEventArgs e);

        #endregion

        #region Protected Methods

        [UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
        protected void AddHandler(object key, Delegate value)
        {
            Events.AddHandler(key, value);
        }

        [UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
        protected void RemoveHandler(object key, Delegate value)
        {
            Events.RemoveHandler(key, value);
        }

        #endregion
    }
}
