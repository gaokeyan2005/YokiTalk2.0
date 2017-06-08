using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    public class AutocompleteListView : DataGridViewEx, IAutocompleteListView
    {
        private readonly ToolTip toolTip = new ToolTip();
#pragma warning disable CS0414 // The field 'AutocompleteListView.hoveredItemIndex' is assigned but its value is never used
        private int hoveredItemIndex = -1;
#pragma warning restore CS0414 // The field 'AutocompleteListView.hoveredItemIndex' is assigned but its value is never used
#pragma warning disable CS0169 // The field 'AutocompleteListView.oldItemCount' is never used
        private int oldItemCount;
#pragma warning restore CS0169 // The field 'AutocompleteListView.oldItemCount' is never used
#pragma warning disable CS0414 // The field 'AutocompleteListView.selectedItemIndex' is assigned but its value is never used
        private int selectedItemIndex = -1;
#pragma warning restore CS0414 // The field 'AutocompleteListView.selectedItemIndex' is assigned but its value is never used
#pragma warning disable CS0169 // The field 'AutocompleteListView.visibleItems' is never used
        private IList<AutocompleteItem> visibleItems;
#pragma warning restore CS0169 // The field 'AutocompleteListView.visibleItems' is never used

        /// <summary>
        /// Duration (ms) of tooltip showing
        /// </summary>
        public int ToolTipDuration { get; set; }

        /// <summary>
        /// Occurs when user selected item for inserting into text
        /// </summary>
        public event EventHandler ItemSelected;


        /// <summary>
        /// Occurs when current hovered item is changing
        /// </summary>
        public event EventHandler<HoveredEventArgs> ItemHovered;

        internal AutocompleteListView()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            base.Font = new Font(FontFamily.GenericSansSerif, 9);
            //VerticalScroll.SmallChange = ItemHeight;
            BackColor = Color.White;
            LeftPadding = 18;
            ToolTipDuration = 3000;

            Titles = new string[] { "Name", "Spec.", "Price" };
            ShowColumns = new string[] { "DisplayName", "DisplaySpec", "Price" }; 
           ShowFormats = new string[] { "{0}", "{0}", "$ {0:N2}" };
           WidthRate = new float[] { .6f, .2f, .2f };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                toolTip.Dispose();
            }
            base.Dispose(disposing);
        }


#pragma warning disable CS0169 // The field 'AutocompleteListView.itemHeight' is never used
        private int itemHeight;
#pragma warning restore CS0169 // The field 'AutocompleteListView.itemHeight' is never used

        public int ItemHeight
        {
            get { return 34; }
        }

        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
            }
        }

        public int LeftPadding { get; set; }

        public ImageList ImageList { get; set; }



        private void OnItemHovered(HoveredEventArgs e)
        {
            if (ItemHovered != null)
                ItemHovered(this, e);
        }



        private void ScrollToSelected()
        {
            //int y = SelectedItemIndex*ItemHeight - VerticalScroll.Value;
            //if (y < 0)
            //    VerticalScroll.Value = SelectedItemIndex*ItemHeight;
            //if (y > ClientSize.Height - ItemHeight)
            //    VerticalScroll.Value = Math.Min(VerticalScroll.Maximum,
            //                                    SelectedItemIndex*ItemHeight - ClientSize.Height + ItemHeight);
            ////some magic for update scrolls
            //AutoScrollMinSize -= new Size(1, 0);
            //AutoScrollMinSize += new Size(1, 0);
        }

        public Rectangle GetItemRectangle(int itemIndex)
        {
            var y = 32; //itemIndex * ItemHeight - VerticalScroll.Value;
            return new Rectangle(0, y, ClientSize.Width - 1, ItemHeight - 1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    bool rtl = RightToLeft == RightToLeft.Yes;
        //    AdjustScroll();
        //    int startI = VerticalScroll.Value/ItemHeight - 1;
        //    int finishI = (VerticalScroll.Value + ClientSize.Height)/ItemHeight + 1;
        //    startI = Math.Max(startI, 0);
        //    finishI = Math.Min(finishI, VisibleItems.Count);
        //    int y = 0;

        //    for (int i = startI; i < finishI; i++)
        //    {
        //        y = i*ItemHeight - VerticalScroll.Value;

        //        if (ImageList != null && VisibleItems[i].ImageIndex >= 0)
        //            if (rtl)
        //                e.Graphics.DrawImage(ImageList.Images[VisibleItems[i].ImageIndex], Width - 1 - LeftPadding, y);
        //            else
        //                e.Graphics.DrawImage(ImageList.Images[VisibleItems[i].ImageIndex], 1, y);

        //        var textRect = new Rectangle(LeftPadding, y, ClientSize.Width - 1 - LeftPadding, ItemHeight - 1);
        //        if (rtl)
        //            textRect = new Rectangle(1, y, ClientSize.Width - 1 - LeftPadding, ItemHeight - 1);

        //        if (i == SelectedItemIndex)
        //        {
        //            Brush selectedBrush = new LinearGradientBrush(new Point(0, y - 3), new Point(0, y + ItemHeight),
        //                                                          Color.White, Color.Orange);
        //            e.Graphics.FillRectangle(selectedBrush, textRect);
        //            e.Graphics.DrawRectangle(Pens.Orange, textRect);
        //        }
        //        if (i == hoveredItemIndex)
        //            e.Graphics.DrawRectangle(Pens.Red, textRect);

        //        var sf = new StringFormat();
        //        if (rtl)
        //            sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;

        //        var args = new PaintItemEventArgs(e.Graphics, e.ClipRectangle)
        //                       {
        //                           Font = Font,
        //                           TextRect = new RectangleF(textRect.Location, textRect.Size),
        //                           StringFormat = sf,
        //                           IsSelected = i == SelectedItemIndex,
        //                           IsHovered = i == hoveredItemIndex
        //                       };
        //        //call drawing
        //        VisibleItems[i].OnPaint(args);
        //    }
        //}

        //protected override void OnScroll(System.Windows.Forms.ScrollEventArgs se)
        //{
        //    base.OnScroll(se);
        //    Invalidate(true);
        //}

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (e.Button == MouseButtons.Left)
            {
                SelectedIndex = PointToItemIndex(e.Location);
                ScrollToSelected();
                Invalidate();
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            SelectedIndex = PointToItemIndex(e.Location);
            Invalidate();
            OnItemSelected();
        }

        private void OnItemSelected()
        {
            if (ItemSelected != null)
                ItemSelected(this, EventArgs.Empty);
        }


        private int PointToItemIndex(Point p)
        {
            return 20; // (p.Y + VerticalScroll.Value) / ItemHeight;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var host = Parent as AutocompleteMenuHost;
            if (host != null)
                if (host.Menu.ProcessKey((char) keyData, Keys.None))
                    return true;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void SelectItem(int itemIndex)
        {
            SelectedIndex = itemIndex;
            ScrollToSelected();
            Invalidate();
        }

        public void ShowToolTip(AutocompleteItem autocompleteItem, Control control = null)
        {
            string title = autocompleteItem.ToolTipTitle;
            string text = autocompleteItem.ToolTipText;
            if (control == null)
                control = this;

            if (string.IsNullOrEmpty(title))
            {
                toolTip.ToolTipTitle = null;
                toolTip.SetToolTip(control, null);
                return;
            }

            if (string.IsNullOrEmpty(text))
            {
                toolTip.ToolTipTitle = null;
                toolTip.Show(title, control, Width + 3, 0, ToolTipDuration);
            }
            else
            {
                toolTip.ToolTipTitle = title;
                toolTip.Show(text, control, Width + 3, 0, ToolTipDuration);
            }
        }

    }
}