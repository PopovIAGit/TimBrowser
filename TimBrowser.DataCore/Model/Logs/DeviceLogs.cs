using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Model.Logs
{
    public class DeviceLogs
    {
        public DeviceLogs(List<LogEventRecordItem> eventLog, List<LogCmdRecordItem> commandLog,
            List<LogParamRecordItem> changeParameterLog)
        {
            _eventLog = eventLog;
            _commandLog = commandLog;
            _changeParameterLog = changeParameterLog;
        }

        private List<LogEventRecordItem> _eventLog;
        private List<LogCmdRecordItem> _commandLog;
        private List<LogParamRecordItem> _changeParameterLog;

        public List<LogEventRecordItem> EventLog
        {
            get { return _eventLog; }
        }

        public List<LogCmdRecordItem> CommandLog
        {
            get { return _commandLog; }
        }

        public List<LogParamRecordItem> ChangeParameterLog
        {
            get { return _changeParameterLog; }
        }
    }
}
