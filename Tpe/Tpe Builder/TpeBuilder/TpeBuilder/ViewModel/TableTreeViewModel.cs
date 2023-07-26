using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Model;
using System.Windows;
using TpeBuilder.Model;
using System.Collections.ObjectModel;
using System.Reflection;
using TpeBuilder.Events;
using TpeBuilder.Helpers;

namespace TpeBuilder.ViewModel
{
    public class TableTreeViewModel : ViewModelBase
    {
        public TableTreeViewModel()
        {
            Storage.Data.Instance.OnDataChanged += DataChangedEventHandler;

            CurrentTpeTable = Storage.Data.Instance.CurrentTpeTable;
        }

        private void DataChangedEventHandler(object sender, EventArgs e)
        {
            CurrentTpeTable = Storage.Data.Instance.CurrentTpeTable;
        }

        public void TreeViewItemSelected(object item)
        {
            /*
            TpeModelTypes tpeModelType = TpeModelTypes.None;

            if (t.FullName.Contains(TpeModelTypes.TpeTable.ToString()))
            {
                tpeModelType = TpeModelTypes.TpeTable;
            }
            else if (t.FullName.Contains(TpeModelTypes.TpeGroup.ToString()))
            {
                tpeModelType = TpeModelTypes.TpeGroup;
            }
            else if (t.FullName.Contains(TpeModelTypes.TpeParameter.ToString()))
            {
                tpeModelType = TpeModelTypes.TpeParameter;
            }
            */

            Storage.Data.Instance.RaiseOnTreeViewItemSelected(this, new TreeViewItemSelectedEventArgs(item));

        }





        private ObservableCollection<TpeTable> _currentTpeTable;
        public ObservableCollection<TpeTable> CurrentTpeTable
        {
            get { return _currentTpeTable; }
            set
            {
                _currentTpeTable = value;
                NotifyPropertyChanged("CurrentTpeTable");
            }
        }
    }



}
