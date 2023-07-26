using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Model;
using TimBrowser.DataCore.Helpers;

namespace TimBrowser.DataCore.Model.Logs
{
    public class LogSimRecordItem
    {
        public LogSimRecordItem(DateTime dateAndTime, string dataSIM)// List<ParameterItem> dataSIM)
        {
            _dateAndTime = dateAndTime;
            _dataSimValue = dataSIM;
        }

        #region Fields

        private DateTime _dateAndTime;
        //private List<ParameterItem> _dataSimValue;
        private string _dataSimValue;

        #endregion

        #region Properties

        public DateTime DateAndTime
        {
            get { return _dateAndTime; }
        }

        //public List<ParameterItem> DataSimValue
        public string DataSimValue
        {
            get { return _dataSimValue; }
        }

        #endregion
    }
}
