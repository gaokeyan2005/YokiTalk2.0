using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    public class DataGrid: System.Windows.Forms.Panel
    {
        public event MouseDoubleClickOnDataGridExHandle OnMouseDoubleClickOnDataGridEx;

        public DataGrid()
            : base()
        {
            base.SetStyle(
                   ControlStyles.UserPaint |
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.ResizeRedraw |
                   ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();

            this.BackColor = Color.FromArgb(255, 250, 250, 250);
        }

        public string[] ShowFormats
        {
            get;
            set;
        }

        private float[] _widthRate;
        public float[] widthRate
        {
            get { return this._widthRate; }
            set
            {
                this._widthRate = value;
                this.Invalidate();
            }
        }

        private List<DataGridExRow> _rows;
        public List<DataGridExRow> Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                this.Size = new Size(this.ClientSize.Width, 34 * this._rows.Count >0? 34 * this._rows.Count: 1);
                this.Invalidate();
            }
        }

        private int selectedIndex = 0;
        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }
            set
            {
                if (this.Rows != null && value <= this.Rows.Count - 1 && value >= 0 && this.selectedIndex != value)
                {
                    this.selectedIndex = value;
                    this.Invalidate();
                }
            }
        }


        public object SelectedItem
        {
            get
            {
                object r = null;
                if (this.Rows.Count > this.SelectedIndex)
                {
                    r = this.Rows[this.SelectedIndex].Data;
                }
                return r;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (this.Rows == null)
            {
                return;
            }
            foreach (DataGridExRow row in this.Rows)
            {
                if (row.ClientRect.Contains(e.Location))
                {
                    this.SelectedIndex = this.Rows.IndexOf(row);
                    this.Invalidate();
                    break;
                }
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (this.Rows == null)
            {
                return;
            }
            foreach (DataGridExRow row in this.Rows)
            {
                if (row.ClientRect.Contains(e.Location))
                {
                    this.SelectedIndex = this.Rows.IndexOf(row);
                    this.Invalidate();
                    break;
                }
            }
            if (this.OnMouseDoubleClickOnDataGridEx != null)
            {
                this.OnMouseDoubleClickOnDataGridEx(this, e);
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            this.Invalidate();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            if (this.Rows == null || this.Rows.Count == 0)
            {
                return;
            }

            for (int i = 0; i < this.Rows.Count; i++)
            {
                g.CompositingQuality = CompositingQuality.HighSpeed;
                Rectangle[] rects = GetItemsRect(i, Rows[i].ViewData.Length);
                DataGridExRow row = Rows[i];
                row.ClientRect = new Rectangle(0, rects[0].Top, this.ClientRectangle.Width, rects[0].Height);
                object[] rowViewData = row.ViewData;
                for (int j = 0; j < rects.Length; j++)
                {
                    Rectangle rect = rects[j];

                    string showformat = this.ShowFormats == null || this.ShowFormats.Length - 1 < j ? "{0}" : this.ShowFormats[j];
                    string content = rowViewData[j] == null ? "NULL" : string.Format(showformat, rowViewData[j]);

                    this.Font = new System.Drawing.Font(this.Font, FontStyle.Regular);
                    SizeF fontSize = g.MeasureString(content, this.Font);
                    PointF fontLocation = new PointF(rect.Left + (rect.Width - fontSize.Width) / 2, rect.Top + (rect.Height - fontSize.Height) / 2);


                    if (i == this.SelectedIndex)
                    {

                        using (Brush b = new SolidBrush(Color.FromArgb(255, 255, 255, 255)))
                        {
                            g.FillRectangle(b, new Rectangle(rect.Left, rect.Top, rect.Width + 1, rect.Height));
                        }

                        using (Brush b = new SolidBrush(Color.FromArgb(255, 56, 180, 75)))
                        {
                            g.FillRectangle(b, new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height));
                        }

                        g.CompositingQuality = CompositingQuality.HighQuality;
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, Color.White)))
                        {
                            g.DrawString(content, this.Font, brush, new PointF(fontLocation.X, fontLocation.Y));
                        }
                    }
                    else
                    {

                        using (Brush b = new SolidBrush(Color.FromArgb(255, 192, 194, 204)))
                        {
                            g.FillRectangle(b, new Rectangle(rect.Left, rect.Top, rect.Width + 1, i == this.SelectedIndex - 1 ? rect.Height - 1: rect.Height));
                        }

                        using (Brush b = new SolidBrush(Color.FromArgb(255, 255, 255, 255)))
                        {
                            g.FillRectangle(b, new Rectangle(rect.Left, rect.Top + 0, rect.Width, rect.Height - 1));
                        }

                        g.CompositingQuality = CompositingQuality.HighQuality;
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(225, Color.Black)))
                        {
                            g.DrawString(content, this.Font, brush, new PointF(fontLocation.X, fontLocation.Y));
                        }
                    }
                }
            }
        }

        public Rectangle GetItemRectByIndex(int rowIndex)
        {
            int dataRowHieght = 34;
            int width = this.ClientSize.Width;

            Rectangle rect = new Rectangle(0, 0 + rowIndex * dataRowHieght, width, dataRowHieght);

            return rect;
        }

        private Rectangle[] GetItemsRect(int rowIndex, int columnCount)
        {
            Queue<Rectangle> rects = new Queue<Rectangle>();

            int dataRowHieght = 34;
            int startMemory = 0;
            for (int i = 0; i < columnCount; i++)
            {
                int start = startMemory + (i == 0 ? 0 : 1);
                int avgWidth = Convert.ToInt32(Math.Floor((float)this.ClientSize.Width / columnCount));
                int width = this.widthRate.Length > i ? Convert.ToInt32(this.ClientSize.Width * this.widthRate[i]) : avgWidth;


                if (i == columnCount - 1)
                {
                    width = this.ClientSize.Width - startMemory;
                }
                startMemory += (i == 0 ? 0 : 1) + width;
                rects.Enqueue(new Rectangle(start, 0 + rowIndex * dataRowHieght, width, dataRowHieght));
            }

            return rects.ToArray();
        }
    }

    public delegate void MouseDoubleClickOnDataGridExHandle(object sender, MouseEventArgs e);


}
