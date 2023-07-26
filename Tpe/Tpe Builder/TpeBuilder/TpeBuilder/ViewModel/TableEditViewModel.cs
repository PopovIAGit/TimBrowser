using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeBuilder.Model;

namespace TpeBuilder.ViewModel
{
    public class TableEditViewModel : ViewModelBase
    {
        public TableEditViewModel()
        {
        }



        private TpeTable _currentTpeTable;

        public TpeTable CurrentTpeTable
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
