using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeBuilder.Helpers;

namespace TpeBuilder.Events
{
    public class TreeViewItemSelectedEventArgs : EventArgs
    {
        public TreeViewItemSelectedEventArgs(object selectedItem)
        {
            _selectedItem = selectedItem;
        }

        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
        }

    }
}
