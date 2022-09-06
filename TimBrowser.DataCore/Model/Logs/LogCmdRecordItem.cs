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
            _statusDig = null;
            _commandSource = commandSource;
        }

        public LogCmdRecordItem(DateTime dateAndTime, ParameterItem command, ParameterItem status, ParameterItem statusDig,
            CommandSource commandSource)
        {
            _dateAndTime = dateAndTime;
            _command = command;
            _status = status;
            _statusDig = statusDig;
            _commandSource = commandSource;
        }

        public LogCmdRecordItem(DateTime dateAndTime, ParameterItem command, ParameterItem status, ParameterItem statusDig,
            CommandSource commandSource, string position)
        {
            _dateAndTime = dateAndTime;
            _position = position;
            _command = command;
            _status = status;
            _statusDig = statusDig;
            _commandSource = commandSource;
        }

        #region Fields

        private DateTime _dateAndTime;
        private ParameterItem _command;
        private ParameterItem _status;
        private ParameterItem _statusDig;
        private CommandSource _commandSource;
        private string _position;

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

        public ParameterItem StatusDig
        {
            get { return _statusDig; }
        }

        public CommandSource commandSource
        {
            get { return _commandSource; }
        }

        public string position
        {
            get { return _position; }
        }

        #endregion
    }
}
