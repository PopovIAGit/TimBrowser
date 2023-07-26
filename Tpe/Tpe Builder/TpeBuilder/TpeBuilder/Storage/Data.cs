using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using TpeBuilder.Model;
using TpeBuilder.Events;

namespace TpeBuilder.Storage
{
    public class Data
    {
        static Data()
        { }

        public Data()
        { }

        private static Data instance;
        public static Data Instance
        {
            get
            {
                if (instance == null)
                    instance = new Data();
                return instance;
            }
        }

        private ObservableCollection<TpeTable> _currentTpeTable;
        public ObservableCollection<TpeTable> CurrentTpeTable
        {
            get { return _currentTpeTable; }
            set
            {
                _currentTpeTable = value;

                if (OnDataChanged != null)
                    OnDataChanged(this, new EventArgs());
            }
        }

        public EventHandler OnDataChanged;

        public EventHandler<TreeViewItemSelectedEventArgs> OnTreeViewItemSelected;

        public void RaiseOnTreeViewItemSelected(object sender, TreeViewItemSelectedEventArgs e)
        {
            if (OnTreeViewItemSelected != null)
                OnTreeViewItemSelected(sender, e);
        }
    }
}
