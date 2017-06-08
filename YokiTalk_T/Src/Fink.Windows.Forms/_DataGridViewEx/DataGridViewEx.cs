using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Fink.Windows.Forms
{
    public class DataGridViewEx: System.Windows.Forms.Control
    {
        private DataGrid _data; 
        private ScrollviewEx _scroll;
        private GridViewHeader _header;

        public event MouseDoubleClickOnDataGridExHandle OnMouseDoubleClickOnDataGridEx;
        public DataGridViewEx()
            : base()
        {
            Init();
        }

        private void Init()
        {
            this.SuspendLayout();
            _header = new GridViewHeader();
            _header.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            _header.Location = new System.Drawing.Point(0, 0);
            _data = new DataGrid();
            _data.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            _data.OnMouseDoubleClickOnDataGridEx+=new MouseDoubleClickOnDataGridExHandle((sender, e)=>{
                if (this.OnMouseDoubleClickOnDataGridEx != null)
                {
                    this.OnMouseDoubleClickOnDataGridEx(sender, e);
                }
            });
            _scroll = new ScrollviewEx();
            _scroll.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            _scroll.OnScrollContentSizeChanged += new EventHandler(_scroll_OnScrollContentSizeChanged);
            _scroll.Child = _data;
            
            _scroll.Location = new System.Drawing.Point(0, this._header.DesignHeight);
            this.Controls.Add(_scroll);
            this.Controls.Add(_header);
            this.ResumeLayout();
        }

        void _scroll_OnScrollContentSizeChanged(object sender, EventArgs e)
        {
            if (this._scroll.Height <= 0)
            {
                return;
            }
            int subDistance = _data.Size.Height - _scroll.Size.Height;
            if (subDistance > 0)
            {
                this._scroll.Value = subDistance;
            }
        }

        public int ScrollValue
        {
            get
            {
                return this._scroll.Value;
            }
            set
            {
                this._scroll.Value = value;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (_header != null)
            {
                _header.Size = new System.Drawing.Size(this.ClientRectangle.Width, _header.DesignHeight);
            }
            if (_data != null)
            {
                _data.Size = new System.Drawing.Size(this.ClientRectangle.Width, _data.ClientSize.Height);
            }
            if (_scroll != null)
            {
                _scroll.Size = new System.Drawing.Size(this.ClientRectangle.Width, this.ClientRectangle.Height - _header.DesignHeight);
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this._data.SelectedIndex;
            }
            set
            {
                if (this._data.Rows != null && this._data.Rows.Count > value)
                {
                    this._data.SelectedIndex = value;
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        FiteScrollPositionBySelectedIndex(this._data.SelectedIndex);
                    });
                }
            }
        }


        public object SelectedItem
        {
            get
            {
                return this._data.SelectedItem;
            }
        }


        private void FiteScrollPositionBySelectedIndex(int index)
        {
            Rectangle rect = this._data.GetItemRectByIndex(index);
            int scrollTop = this._scroll.Value;
            int scrollViewport = this._scroll.Height;

            if (rect.Top < scrollTop)
            {
                this._scroll.Value = rect.Top;
            }
            else if (rect.Bottom > scrollTop + scrollViewport)
            {
                this._scroll.Value = rect.Bottom - scrollViewport;
            }
            //this._scroll.Value;
        }


        public override void Refresh()
        {
            base.Refresh();
            this.DataSource = this.DataSource;
            this._data.Refresh();
            this._scroll.Refresh();
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                this._data.Font = base.Font;
                this._data.Invalidate();
                this._header.Font = base.Font;
                this._header.Invalidate();
            }
        }

        public string[] Titles
        {
            get { return this._header.Titles; }
            set
            {
                this._header.Titles = value;
                this._header.Invalidate();
            }
        }

        private string[] _showColumns;
        public string[] ShowColumns
        {
            get { return this._showColumns; }
            set
            {
                this._showColumns = value;
            }
        }

        private string[] _showFormats;
        public string[] ShowFormats
        {
            get { return this._showFormats; }
            set
            {
                this._showFormats = value;
                this._data.ShowFormats = this._showFormats;
                this._data.Invalidate();
            }
        }

        private float[] _widthRate;
        public float[] WidthRate
        {
            get { return this._widthRate; }
            set
            {
                this._widthRate = value;
                this._header.WidthRate = this.WidthRate;
                this._data.widthRate = this.WidthRate;
            }
        }

        private object _dataSource;

        public object DataSource
        {
            get { return _dataSource; }
            set
            {
                if (value != null)
                {
                    _dataSource = value;
                    DataGridViewDataSouce ds = DataSourceHelper.Instance.GetDataSoucre(this._dataSource, this.ShowColumns);

                    this.Invoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        if (this.Titles == null || this.Titles.Length == 0)
                        {
                            this._header.Titles = ds.titles;
                        }
                        this._data.Rows = ds.Data;
                        if (ds.Data.Count <= 0)
                        {
                            this._data.SelectedIndex = 0;
                        }

                        if (this._data.SelectedIndex >= ds.Data.Count)
                        {
                            this._data.SelectedIndex = ds.Data.Count - 1;
                        }

                        this._scroll.Refresh();
                    });

                }
            }
        }

        public List<DataGridExRow> DataRows
        {
            get
            {
                return this._data.Rows;
            }
        }

    }

    public class DataGridExRow
    {
        public object Data
        {
            get;
            set;
        }

        public object[] ViewData
        {
            get;
            set;
        }

        public Rectangle ClientRect
        {
            get;
            set;
        }

        public CompareResult Compare(string fragmentText, List<string> searhOnProperty = null)
        {
            Type t = this.Data.GetType();
            System.Reflection.PropertyInfo[] ps = t.GetProperties();

            foreach (System.Reflection.PropertyInfo p in ps)
            {
                if (searhOnProperty != null && searhOnProperty.IndexOf(p.Name) < 0)
                {
                    continue;
                }
                string text = p.GetValue(this.Data, null) !=null ? p.GetValue(this.Data, null).ToString(): string.Empty;

                if (text.ToLower().StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase))
                {
                    return CompareResult.VisibleAndSelected;
                }
                else if (text.ToLower().Contains(fragmentText.ToLower()))
                {
                    return CompareResult.Visible;
                }
            }
            return CompareResult.Hidden;
        }
    }

    public class DataGridViewDataSouce
    {
        public string[] titles
        {
            get;
            set;
        }
        public List<DataGridExRow> Data
        {
            get;
            set;
        }
    }

    public class DataSourceHelper
    {
        private DataSourceHelper()
        {

        }

        private static DataSourceHelper _Instance;

        public static DataSourceHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DataSourceHelper();
                }
                return _Instance;
            }
        }

        public DataGridViewDataSouce GetDataSoucre(object os, string[] showColumns = null)
        {
            if (os is System.Data.DataTable)
            {
                return GetDataSoucreFromDataTable(os);
            } 
            else
            {
                return GetDataSoucreFromEnumerable(os, showColumns);
            }
        }

        private DataGridViewDataSouce GetDataSoucreFromEnumerable(object os, string[] showColumns = null)
        {
            DataGridViewDataSouce src = new DataGridViewDataSouce();

            System.Collections.IEnumerable ose = os as System.Collections.IEnumerable;

            Type t;
            System.Reflection.PropertyInfo[] ps = null;
            foreach (var o in ose)
            {
                t = o.GetType();
                ps = t.GetProperties();
                System.Collections.Generic.Queue<string> nameQ = new Queue<string>();
                foreach (System.Reflection.PropertyInfo p in ps)
                {
                    nameQ.Enqueue(p.Name);
                }
                src.titles = nameQ.ToArray();
                break;
            }

            src.Data = new List<DataGridExRow>();
            foreach (var o in ose)
            {
                DataGridExRow dataRow = new DataGridExRow();
                Queue<object> dgrViewData = new Queue<object>();
                if (showColumns != null)
                {

                    string pattern = @"^[\s\S]+?[\s\S]+:[\s\S]+$";

                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);

                    foreach (string n in showColumns)
                    {
                        string name = n;
                        if (regex.IsMatch(name))
                        {
                            string[] ss = name.Split(new char[] { '?', ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (ss.Length == 3)
                            {
                                bool b = false;
                                foreach (System.Reflection.PropertyInfo p in ps)
                                {
                                    if (p.Name == ss[0].Trim())
                                    {
                                        b = Convert.ToBoolean(p.GetValue(o, null));
                                        break;
                                    }
                                }
                                name = (b ? ss[1].Trim() : ss[2]);
                            }
                        }
                        System.Reflection.PropertyInfo pHandle = null;
                        foreach (System.Reflection.PropertyInfo p in ps)
                        {
                            if (p.Name == name)
                            {
                                pHandle = p;
                                break;
                            }
                        }
                        if (pHandle != null)
                        {
                            dgrViewData.Enqueue(pHandle.GetValue(o, null));
                        }
                    }
                }

                dataRow.Data = o;
                dataRow.ViewData = dgrViewData.ToArray();
                
                src.Data.Add(dataRow); 
            }

             return src;
        }

        private DataGridViewDataSouce GetDataSoucreFromDataTable(object os)
        {
            DataGridViewDataSouce src = new DataGridViewDataSouce();

            System.Data.DataTable dt = os as System.Data.DataTable;


            System.Collections.Generic.Queue<string> nameQ = new Queue<string>();
            foreach (System.Data.DataColumn c in dt.Columns)
            {
                nameQ.Enqueue(c.Caption);
            }
            src.titles = nameQ.ToArray();

            src.Data = new List<DataGridExRow>();
            foreach (System.Data.DataRow r in dt.Rows)
            {

                DataGridExRow dataRow = new DataGridExRow();
                Queue<object> dgrViewData = new Queue<object>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dgrViewData.Enqueue(r[i]);
                }
                dataRow.Data = r;
                dataRow.ViewData = dgrViewData.ToArray();
                src.Data.Add(dataRow);
            }

            return src;
        }

    }

}
