using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace TimBrowser.Model
{
    public class TimChosenParameterItem
    {
        public TimChosenParameterItem(string parameterName, List<TimParameterItem> chosenParameter)
        {
            _parameterName = parameterName;
            _chosenParameters = chosenParameter;
        }

        private string _parameterName;
        private List<TimParameterItem> _chosenParameters;

        

        public string ParameterName
        {
            get { return _parameterName; }
        }

        public List<TimParameterItem> ChosenParameters
        {
            get { return _chosenParameters; }
        }
    }
}
