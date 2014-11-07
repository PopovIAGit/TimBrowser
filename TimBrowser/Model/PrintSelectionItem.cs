using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.Model
{
    public class PrintSelectionItem : PropertyChangedBase
    {
        public PrintSelectionItem(PrintSelectionIds id, string name, bool isSet)
        {
            _id = id;
            _name = name;
            _isSet = isSet;
        }

        private PrintSelectionIds _id;
        private string _name;
        private bool _isSet;

        public PrintSelectionIds Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public bool IsSet
        {
            get { return _isSet; }
            set
            {
                _isSet = value;
                NotifyOfPropertyChange("IsSet");
            }
        }

    }
}
