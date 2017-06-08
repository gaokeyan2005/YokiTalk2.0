using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Fink.Windows.Forms
{
    /// <summary>
    /// Control for displaying menu items, hosted in AutocompleteMenu.
    /// </summary>
    public interface IAutocompleteListView
    {
        /// <summary>
        /// Image list
        /// </summary>
        ImageList ImageList { get; set; }

        /// <summary>
        /// Index of current selected item
        /// </summary>
        int SelectedIndex { get; set; }
        /// <summary>
        /// Index of current selected item
        /// </summary>
        object SelectedItem { get;}

        /// <summary>
        /// List of visible elements
        /// </summary>
        object DataSource { get; set; }


        string[] ShowColumns { get; set; }

        /// <summary>
        /// Duration (ms) of tooltip showing
        /// </summary>
        int ToolTipDuration { get; set; }

        /// <summary>
        /// Occurs when user selected item for inserting into text
        /// </summary>
        event EventHandler ItemSelected;

        /// <summary>
        /// Occurs when current hovered item is changing
        /// </summary>
        event EventHandler<HoveredEventArgs> ItemHovered;

        /// <summary>
        /// Shows tooltip
        /// </summary>
        /// <param name="autocompleteItem"></param>
        /// <param name="control"></param>
        void ShowToolTip(AutocompleteItem autocompleteItem, Control control = null);

        /// <summary>
        /// Returns rectangle of item
        /// </summary>
        Rectangle GetItemRectangle(int itemIndex);
    }
}
