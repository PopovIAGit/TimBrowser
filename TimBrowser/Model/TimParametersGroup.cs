using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.Model
{
    public class TimParametersGroup
    {
        public TimParametersGroup(string name, string description,
            List<TimParameterItem> parameters)
        {
            _name = name;
            _description = description;
            _parameters = parameters;
        }

        #region Fields

        private string _name;
        private string _description;
        private List<TimParameterItem> _parameters;

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public List<TimParameterItem> Parameters
        {
            get { return _parameters; }
        }

        #endregion

    }

}
