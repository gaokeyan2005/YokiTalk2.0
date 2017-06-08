using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace Fink.Windows.Forms
{
    public class ParentControlDesignerEx : System.Windows.Forms.Design.ParentControlDesigner
    {
        public ParentControlDesignerEx()
            : base()
        {
        }

        private DesignerVerbCollection m_verbs = new DesignerVerbCollection();

        public DesignerVerbCollection VerbCollection
        {
            get { return m_verbs; }
            set { m_verbs = value; }
        }
        private IDesignerHost m_DesignerHost;
        private ISelectionService m_SelectionService;

        public IDesignerHost DesignerHost
        {
            get
            {
                if (m_DesignerHost == null)
                    m_DesignerHost =
                    (IDesignerHost)(GetService(typeof(IDesignerHost)));

                return m_DesignerHost;
            }
        }

        public ISelectionService SelectionService
        {
            get
            {
                if (m_SelectionService == null)
                    m_SelectionService = (ISelectionService)(this.GetService(typeof(ISelectionService)));
                return m_SelectionService;
            }
        }

        private const int WM_NCHITTEST = 0x84;

        private const int HTTRANSPARENT = -1;
        private const int HTCLIENT = 1;
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
            {
                //select tabcontrol when Tabcontrol clicked outside of
                if (m.Result.ToInt32() == HTTRANSPARENT)
                    m.Result = (IntPtr)HTCLIENT;
            }
        }

        private enum TabControlHitTest
        {
            TCHT_NOWHERE = 1,
            TCHT_ONITEMICON = 2,
            TCHT_ONITEMLABEL = 4,
            TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
        }
        private const int TCM_HITTEST = 0x130D;

        private struct TCHITTESTINFO
        {
            public System.Drawing.Point pt;
            public TabControlHitTest flags;
        }

        protected override bool GetHitTest(System.Drawing.Point point)
        {
            if (this.SelectionService.PrimarySelection == this.Control)
            {
                TCHITTESTINFO hti = new TCHITTESTINFO();

                hti.pt = this.Control.PointToClient(point);
                hti.flags = 0;

                System.Windows.Forms.Message m = new System.Windows.Forms.Message();
                m.HWnd = this.Control.Handle;
                m.Msg = TCM_HITTEST;

                IntPtr lparam =
                System.Runtime.InteropServices.Marshal.AllocHGlobal(System.Runtime.InteropServices.Marshal.SizeOf(hti));
                System.Runtime.InteropServices.Marshal.StructureToPtr(hti, lparam, false);
                m.LParam = lparam;

                base.WndProc(ref m);
                System.Runtime.InteropServices.Marshal.FreeHGlobal(lparam);

                if (m.Result.ToInt32() != -1)
                    return hti.flags != TabControlHitTest.TCHT_NOWHERE;
            }
            return false;
        }

        //protected override void
        //OnPaintAdornments(System.Windows.Forms.PaintEventArgs pe)
        //{
        //    //Don't want DrawGrid dots.
        //}

        ////Fix the AllSizable selectionrule on DockStyle.Fill
        //public override System.Windows.Forms.Design.SelectionRules
        //SelectionRules
        //{
        //    get
        //    {
        //        if (Control.Dock == System.Windows.Forms.DockStyle.Fill)
        //            return
        //            System.Windows.Forms.Design.SelectionRules.Visible;
        //        return base.SelectionRules;
        //    }
        //}
    }


    public class ControlDesignerEx : System.Windows.Forms.Design.ControlDesigner
    {
        public ControlDesignerEx()
            : base()
        {
        }

        private IDesignerHost m_DesignerHost;
        public IDesignerHost DesignerHost
        {
            get
            {
                if (m_DesignerHost == null)
                    m_DesignerHost =
                    (IDesignerHost)(GetService(typeof(IDesignerHost)));

                return m_DesignerHost;
            }
        }

        private DesignerVerbCollection m_verbs = new DesignerVerbCollection();

        public DesignerVerbCollection VerbCollection
        {
            get { return m_verbs; }
            set { m_verbs = value; }
        }

    }
}
