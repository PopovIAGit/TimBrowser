using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Model;
using TimBrowser.DataCore.Helpers;

namespace TimBrowser.DataCore.Model.Logs
{
    public class LogParamRecordItem
    {
        public LogParamRecordItem(DateTime dateAndTime, ParameterItem paramWithNewValue)
        {
            _dateAndTime = dateAndTime;
            _paramWithNewValue = paramWithNewValue;
        }

        #region Fields

        private DateTime _dateAndTime;
        private ParameterItem _paramWithNewValue;

        #endregion

        #region Properties

        public DateTime DateAndTime
        {
            get { return _dateAndTime; }
        }

        public ParameterItem ParamWithNewValue
        {
            get { return _paramWithNewValue; }
        }

        #endregion
    }
}
