using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Model.Logs;
using TimBrowser.DataCore.Model.Device;
using TpeParameters.Model;

namespace TimBrowser.DataCore.Model
{
    public class InformationModuleData
    {
        public InformationModuleData(DeviceInfo deviceInfo, List<DeviceLogInfo> deviceLogsInfo,
            DeviceLogs deviceLogs, TableItem currentParameters)
        {
            _deviceInfo = deviceInfo;
            _deviceLogInfo = deviceLogsInfo;
            _deviceLogs = deviceLogs;
            _currentParameters = currentParameters;
        }

        #region Fields

        private DeviceInfo _deviceInfo;
        private List<DeviceLogInfo> _deviceLogInfo;
        private DeviceLogs _deviceLogs;
        private TableItem _currentParameters;

        #endregion

        #region Properties

        public DeviceInfo DeviceInfo
        {
            get { return _deviceInfo; }
        }

        public List<DeviceLogInfo> DeviceLogInfo
        {
            get { return _deviceLogInfo; }
        }
        
        public DeviceLogs DeviceLogs
        {
            get { return _deviceLogs; }
        }

        public TableItem CurrentParameters
        {
            get { return _currentParameters; }
        }

        #endregion

    }
}
