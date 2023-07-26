using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TpeParameters.Model
{
    public class TableItem
    {
        public TableItem(int id, int deviceId, string deviceName, double firmwareVersion,
            List<GroupItem> groups, List<ParameterItem> rootParameters)
        {
            _id = id;
            _deviceId = deviceId;
            _deviceName = deviceName;
            _firmwareVersion = firmwareVersion;
            _groups = groups;
            _rootParameters = rootParameters;
        }

        private int _id;
        private int _deviceId;
        private string _deviceName;
        private double _firmwareVersion;

        private List<GroupItem> _groups;
        private List<ParameterItem> _rootParameters;

        public int Id
        {
            get { return _id; }
        }

        public int DeviceId
        {
            get { return _deviceId; }
        }

        public string DeviceName
        {
            get { return _deviceName; }
        }

        public double FirmwareVersion
        {
            get { return _firmwareVersion; }
        }

        /// <summary>
        /// Список групп
        /// </summary>    
        public List<GroupItem> Groups
        {
            get { return _groups; }
        }

        /// <summary>
        /// Корневые параметры (без группы)
        /// </summary>
        public List<ParameterItem> RootParameters
        {
            get { return _rootParameters; }
        }
    }
}
