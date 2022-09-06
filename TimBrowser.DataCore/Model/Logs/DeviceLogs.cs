using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Model.Logs
{
    public class DeviceLogs
    {
        public DeviceLogs(List<LogEventRecordItem> eventLog, List<LogCmdRecordItem> commandLog,
            List<LogParamRecordItem> changeParameterLog, List<LogEventAndCmdRecordItem> eventAndCmdLog)
        {
            _eventLog = eventLog;
            _commandLog = commandLog;
            _changeParameterLog = changeParameterLog;
            _eventAndCmdLog = eventAndCmdLog;
        }

        public DeviceLogs(List<LogEventRecordItem> eventLog, List<LogCmdRecordItem> commandLog,
            List<LogParamRecordItem> changeParameterLog, List<LogEventAndCmdRecordItem> eventAndCmdLog, List<LogSimRecordItem> logSimRecord)
        {
            _eventLog = eventLog;
            _commandLog = commandLog;
            _changeParameterLog = changeParameterLog;
            _simLog = logSimRecord;
            _eventAndCmdLog = eventAndCmdLog;
        }

        private List<LogEventRecordItem> _eventLog;
        private List<LogCmdRecordItem> _commandLog;
        private List<LogParamRecordItem> _changeParameterLog;
        private List<LogSimRecordItem> _simLog;
        private List<LogEventAndCmdRecordItem> _eventAndCmdLog;

        public List<LogEventRecordItem> EventLog
        {
            get { return _eventLog; }
        }

        public List<LogEventAndCmdRecordItem> EventAndCmdLog
        {
            get { return _eventAndCmdLog; }
        }

        public List<LogCmdRecordItem> CommandLog
        {
            get { return _commandLog; }
        }

        public List<LogParamRecordItem> ChangeParameterLog
        {
            get { return _changeParameterLog; }
        }

        public List<LogSimRecordItem> SimLog
        {
            get { return _simLog; }
        }
    }
}
