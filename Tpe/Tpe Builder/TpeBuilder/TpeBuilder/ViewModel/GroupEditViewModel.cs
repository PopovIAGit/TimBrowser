using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeBuilder.Model;

namespace TpeBuilder.ViewModel
{
    public class GroupEditViewModel : ViewModelBase
    {        
        private TpeGroup _currentTpeGroup;

        public TpeGroup CurrentTpeGroup
        {
            get { return _currentTpeGroup; }
            set
            {
                _currentTpeGroup = value;
                NotifyPropertyChanged("CurrentTpeGroup");
            }
        }
    }
}
