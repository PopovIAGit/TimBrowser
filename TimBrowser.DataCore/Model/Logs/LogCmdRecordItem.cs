using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Model;
using TimBrowser.DataCore.Helpers;

namespace TimBrowser.DataCore.Model.Logs
{
    public class LogCmdRecordItem
    {
        public LogCmdRecordItem(DateTime dateAndTime, ParameterItem command, ParameterItem status,
            CommandSource commandSource)
        {
            _dateAndTime = dateAndTime;
            _command = command;
            _status = status;
            _commandSource = commandSource;
        }

        #region Fields

        private DateTime _dateAndTime;
        private ParameterItem _command;
        private ParameterItem _status;
        private CommandSource _commandSource;

        #endregion

        #region Properties

        public DateTime DateAndTime
        {
            get { return _dateAndTime; }
        }

        public ParameterItem Command
        {
            get { return _command; }
        }

        public ParameterItem Status
        {
            get { return _status; }
        }

        public CommandSource commandSource
        {
            get { return _commandSource; }
        }

        #endregion
    }
}
