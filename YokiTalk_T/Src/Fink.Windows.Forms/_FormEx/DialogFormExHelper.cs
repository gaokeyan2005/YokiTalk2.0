using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fink.Windows.Forms
{
    public class DialogFormExHelper
    {
        private DialogFormExHelper()
        {

        }

        private static DialogFormExHelper _instance;
        public static DialogFormExHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DialogFormExHelper();
                }
                return _instance;
            }
        }

        public bool HasDialog(System.Windows.Forms.Form form)
        {
            foreach (DialogFormEx f in DialogFormExHelper.Instance.OpenedDialogForms)
            {
                if (f.ParentForm == form)
                {
                    return true;
                }
            }
            return false;
        }


        public DialogFormEx GetDialog(System.Windows.Forms.Form form)
        {
            foreach (DialogFormEx f in DialogFormExHelper.Instance.OpenedDialogForms)
            {
                if (f.ParentForm == form)
                {
                    return f;
                }
            }
            return null;
        }


        private List<DialogFormEx> openedDialogForms;
        public List<DialogFormEx> OpenedDialogForms
        {
            get
            {
                if (this.openedDialogForms == null)
                {
                    this.openedDialogForms = new List<DialogFormEx>();
                }
                return this.openedDialogForms;
            }
        }
    }
}
