using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fink.Core
{
    public class ItemChangedEventArgs : EventArgs
    {
        public object Item
        {
            get;
            set;
        }
    }

    public class SelectChangedEventArgs : EventArgs
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


    public delegate void SelectableListSelectChangedHandle(SelectChangedEventArgs e);
    public delegate void SelectableListItemChangedHandle(ItemChangedEventArgs e);
    public interface ISelectableList : System.Collections.IList
    {
        event SelectableListItemChangedHandle ItemChanged;
        event SelectableListSelectChangedHandle SelectChanged;
        int CurrectIndex
        {
            get;
            set;
        }
    }

    public abstract class SelectableList<T> : System.Collections.Generic.List<T>, ISelectableList
    {
        public event SelectableListItemChangedHandle ItemChanged;
        public event SelectableListSelectChangedHandle SelectChanged;
        public SelectableList():base()
        {

        }



        public int currectIndex = 0;
        public int CurrectIndex
        {
            get
            {
                return this.currectIndex;
            }
            set
            {
                bool isRise = this.currectIndex != value;
                this.currectIndex = value;
                if (isRise && this.SelectChanged != null)
                {
                    this.SelectChanged(new SelectChangedEventArgs() { SelectedIndex = this.CurrectIndex, SelectedItem = CurrectItem });
                }

            }
        }
        public void SendItemChangedMsg(T t)
        {
            if (this.ItemChanged != null)
            {
                this.ItemChanged(new ItemChangedEventArgs() { Item = t });
            }
        }

        public void SendSelectChangedMsg(SelectChangedEventArgs e)
        {
            if (this.SelectChanged != null)
            {
                this.SelectChanged(e);
            }
        }

        public T CurrectItem
        {
            get {
                if (this.CurrectIndex >= this.Count)
                {
                    this.CurrectIndex = this.Count - 1;
                }
                else if (this.CurrectIndex < 0)
                {
                    this.CurrectIndex = 0;
                }
                return this[this.CurrectIndex];
            }
        }

    }
}
